import json
import uuid
from bs4 import BeautifulSoup


def load_json_data(filename):
    with open(filename, 'r', encoding='utf-8') as loaded_file:
        return json.load(loaded_file)


courses_data = load_json_data('Courses.json')['Data']
course_identifiers_data = load_json_data('CourseIdentifiers.json')['Data']


def get_course_identifier_id(concordia_course_id):
    for item in course_identifiers_data["Data"]:
        if item["ConcordiaCourseId"] == concordia_course_id:
            return item["Id"]
    return None


with open('Section 71.30.1 Course Requirements (BEng in Electrical Engineering) - Concordia University.html', 'r',
          encoding='utf-8') as file:
    soup = BeautifulSoup(file.read(), 'html.parser')


def generate_course_grouping_reference_id():
    return str(uuid.uuid4())


def find_or_create_course_grouping(groupings, name, required_credits, is_top_level, description, notes):
    for grouping in groupings:
        if grouping['Name'] == name:
            return grouping['Id'], False

    new_id = str(uuid.uuid4())
    common_identifier = str(uuid.uuid4())
    groupings.append({
        "Id": new_id,
        "CommonIdentifier": common_identifier,
        "Name": name,
        "RequiredCredits": required_credits,
        "IsTopLevel": is_top_level,
        "School": 0,
        "Description": description,
        "Notes": notes,
        "State": 0,
        "Version": 1,
        "Published": True,
        "CourseGroupingReferences": [],
        "CourseGroupingCourseIdentifier": []
    })
    return new_id, True


course_groupings = {
    "Data": []
}

for program in soup.find_all('div', class_='program-node'):
    program_title = program.find('div', class_='title program-title').text.strip()

    required_credits = "0.00"
    description = "Program description here."
    notes = "Program notes here."

    program_id, is_new = find_or_create_course_grouping(course_groupings['Data'], program_title, required_credits, True,
                                                        description, notes)

    if not is_new:
        continue

    sub_groups = program.find_next_sibling('div', class_='program-node-children')
    for group in sub_groups.find_all('div', class_='defined-group'):
        group_title = group.find('div', class_='title').text.strip()
        sub_group_id, _ = find_or_create_course_grouping(course_groupings['Data'], group_title, "0.00", False,
                                                         "Sub-group description", "Sub-group notes")

        course_groupings['Data'][-1]['CourseGroupingReferences'].append({
            "Id": generate_course_grouping_reference_id(),
            "ChildGroupCommonIdentifier": course_groupings['Data'][-1]['CommonIdentifier'],

            "GroupingType": 0
        })

with open('UpdatedCourseGroupings.json', 'w', encoding='utf-8') as f:
    json.dump(course_groupings, f, ensure_ascii=False, indent=4)
