## Overview
This script is designed to scrape engineering course information from the "Degree Requirements" HTML pages from the Undergraduate Calendar (e.g. https://www.concordia.ca/academics/undergraduate/calendar/current/section-71-gina-cody-school-of-engineering-and-computer-science/section-71-30-department-of-electrical-and-computer-engineering/section-71-30-1-course-requirements-beng-in-electrical-engineering-.html#:~:text=Degree%20Requirements,administered%20by%20either%20the%20CIADI%20() and generate a `CourseGroupings.json`, which contains detailed course groupings and course identifiers.

## Prerequisites
- Python 3.6 or newer
- BeautifulSoup4
```
pip install beautifulsoup4
```

## Usage
```
python scraper.py path_to_your_html_file.html
```

## Notes
- The script is designed to parse specific HTML structures as described above. If the HTML structure of your documents differs, you may need to modify the script accordingly.