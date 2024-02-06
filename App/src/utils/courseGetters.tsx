import { AllCourseSettings, Course, componentMappings } from "../models/course";

export function getCourseComponentsObject(courseDetails: Course, allCourseSettings: AllCourseSettings) {
    const courseComponentsTemp = allCourseSettings?.courseComponents
        .filter((component) =>
            Object.keys(courseDetails?.componentCodes || {}).includes(componentMappings[component.componentName])
        )
        .map((filteredComponent) => {
            const code = componentMappings[filteredComponent.componentName];
            const hours = courseDetails.componentCodes[code];
            return {
                ...filteredComponent,
                hours: hours,
            };
        });
    return courseComponentsTemp;
}

export function getCourseCareerNameByCode(courseCareerCode: number, allCourseSettings: AllCourseSettings) {
    const courseCareer = allCourseSettings?.courseCareers.find((career) => career.careerCode === courseCareerCode)
        .careerName;
    return courseCareer;
}
