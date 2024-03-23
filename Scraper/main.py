import json
from bs4 import BeautifulSoup

with open('Courses.json', 'r') as f:
    courses_data = json.load(f)

with open('CourseIdentifiers.json', 'r') as f:
    course_identifiers_data = json.load(f)


def get_course_identifier_id(concordia_course_id):
    for item in course_identifiers_data["Data"]:
        if item["ConcordiaCourseId"] == concordia_course_id:
            return item["Id"]
    return None


with open('engineering_courses.html', 'r', encoding='utf-8') as file:
    soup = BeautifulSoup(file.read(), 'html.parser')

course_groupings = {
    "Data": []
}

for program in soup.find_all('div', class_='program-node'):
    program_title = program.find('div', class_='title program-title').text.strip()
    sub_groups = program.find_next_sibling('div', class_='program-node-children')

    main_group = {
        "Id": str(program['id']),
        "Name": program_title,
        "SubGroupings": []
    }

    for group in sub_groups.find_all('div', class_='defined-group'):
        group_title = group.find('div', class_='title').text.strip()
        group_id = group['title']
        courses = []

        for course in group.find_all('div', class_='formatted-course'):
            course_code = course.find('span', class_='course-code-number').a.text.strip()
            subject, catalog = course_code.split()

            for course_item in courses_data["Data"]:
                if course_item["Subject"] == subject and course_item["Catalog"] == catalog:
                    course_id = get_course_identifier_id(course_item["CourseID"])
                    if course_id:
                        courses.append({
                            "CourseIdentifiersId": course_id,
                            "CreditValue": course.find('span', class_='course-credits').text.strip()
                        })
                    break

        main_group["SubGroupings"].append({
            "Id": group_id,
            "Name": group_title,
            "Courses": courses
        })

    course_groupings["Data"].append(main_group)

with open('CourseGroupings.json', 'w', encoding='utf-8') as f:
    json.dump(course_groupings, f, ensure_ascii=False, indent=4)
