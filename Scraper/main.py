import json
import uuid
import re
from bs4 import BeautifulSoup


def load_json_data(filename):
    with open(filename, 'r', encoding='utf-8') as loaded_file:
        return json.load(loaded_file)


courses_data = load_json_data('../Seeder/src/data/Courses.json')['Data']
course_identifiers_data = load_json_data('../Seeder/src/data/CourseIdentifiers.json')['Data']


def get_course_identifier_id(concordia_course_id):
    for item in course_identifiers_data:
        if item["ConcordiaCourseId"] == concordia_course_id:
            return item["Id"]
    return None


with open('CoursePages/Section 71.50.2 Course Requirements (BEng in Civil Engineering) - Concordia University.html', 'r', encoding='utf-8') as file:
    soup = BeautifulSoup(file.read(), 'html.parser')


def generate_uuid():
    return str(uuid.uuid4())


course_groupings = {
    "Data": []
}

for program in soup.find_all('div', class_='program-node'):
    program_title_raw = program.find('div', class_='title program-title').text.strip()
    credits_search = re.search(r'\((\d+\.?\d*) credits\)', program_title_raw)
    if credits_search:
        required_credits = credits_search.group(1)
        program_title = re.sub(r' \(\d+\.?\d* credits\)', '', program_title_raw)
    else:
        required_credits = "N/A"
        program_title = program_title_raw

    program_id = generate_uuid()
    common_identifier = generate_uuid()

    new_program = {
        "Id": program_id,
        "CommonIdentifier": common_identifier,
        "Name": program_title,
        "RequiredCredits": required_credits,
        "IsTopLevel": True,
        "School": 0,
        "Description": "",
        "Notes": "",
        "State": 0,
        "Version": 1,
        "Published": True,
        "CourseGroupingReferences": [],
        "CourseGroupingCourseIdentifier": []
    }

    course_groupings["Data"].append(new_program)

    sub_groups = program.find_next_sibling('div', class_='program-node-children')
    for group in sub_groups.find_all('div', class_='defined-group'):
        group_title_raw = group.find('div', class_='title').text.strip()
        # Extract credits for the subgroup
        credits_search = re.search(r'\((\d+\.?\d*) credits\)', group_title_raw)
        if credits_search:
            subgroup_required_credits = credits_search.group(1)
            group_title = re.sub(r' \(\d+\.?\d* credits\)', '', group_title_raw)
        else:
            subgroup_required_credits = "N/A"
            group_title = group_title_raw

        sub_group_id = generate_uuid()
        sub_group_common_identifier = generate_uuid()

        new_sub_group = {
            "Id": sub_group_id,
            "CommonIdentifier": sub_group_common_identifier,
            "Name": group_title,
            "RequiredCredits": subgroup_required_credits,
            "IsTopLevel": False,
            "School": 0,
            "Description": "",
            "Notes": "",
            "State": 0,
            "Version": 1,
            "Published": True,
            "CourseGroupingReferences": [],
            "CourseGroupingCourseIdentifier": []
        }

        new_program["CourseGroupingReferences"].append({
            "Id": generate_uuid(),
            "ChildGroupCommonIdentifier": sub_group_common_identifier,
            "GroupingType": 0
        })

        course_groupings["Data"].append(new_sub_group)

        for course_html in group.find_all('div', class_='formatted-course'):
            subject, catalog = course_html.find('span', class_='course-code-number').text.split()
            course_id = next(
                (c["CourseID"] for c in courses_data if c["Subject"] == subject and c["Catalog"] == catalog), None)
            if course_id:
                course_identifier_id = get_course_identifier_id(course_id)
                if course_identifier_id:
                    new_sub_group["CourseGroupingCourseIdentifier"].append({
                        "CourseGroupingId": sub_group_id,
                        "CourseIdentifiersId": course_identifier_id
                    })

with open('GeneratedCourseGroupings.json', 'w', encoding='utf-8') as f:
    json.dump(course_groupings, f, ensure_ascii=False, indent=4)
