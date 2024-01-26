import { Box, HStack } from "@chakra-ui/react";
import { AllCourseSettings, Course, componentMappings } from "../models/course";
import CoursePreviewWithChange from "./CoursePreviewWithChange";
const CourseDifferenceViewer = ({
    oldCourse,
    newCourse,
    allCourseSettings,
}: {
    oldCourse: Course;
    newCourse: Course;
    allCourseSettings: AllCourseSettings;
}) => {
    const compareCourses = (oldCourse, newCourse) => {
        const fieldsToCompare = [
            "catalog",
            "title",
            "career",
            "description",
            "preReqs",
            "componentCodes",
            "subject",
            "courseNotes",
            "equivalentCourses",
            "creditValue",
        ]; // Fields to compare
        return fieldsToCompare.map((key) => {
            const oldValue = oldCourse[key];
            const newValue = newCourse[key];
            const isChanged = oldValue !== newValue;
            return { key, oldValue, newValue, isChanged };
        });
    };
    const getCourseComponentsObject = (courseDetails: Course) => {
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
    };
    const differences = compareCourses(oldCourse, newCourse);
    const oldCourseCareer = allCourseSettings?.courseCareers.find((career) => career.careerCode === oldCourse.career)
        .careerName;
    const newCourseCareer = allCourseSettings?.courseCareers.find((career) => career.careerCode === newCourse.career)
        .careerName;
    const oldCourseComponents = getCourseComponentsObject(oldCourse);
    const newCourseComponents = getCourseComponentsObject(newCourse);
    const changedFields = {};
    differences.forEach(({ key, isChanged }) => {
        changedFields[key] = isChanged;
    });
    return (
        <>
            {oldCourse && newCourse && (
                <HStack align="stretch" spacing={4}>
                    <Box w="100%">
                        <CoursePreviewWithChange
                            courseCareer={oldCourseCareer} // Update these props as needed
                            courseDescription={oldCourse.description}
                            coursePreReqs={oldCourse.preReqs}
                            courseTitle={oldCourse.title}
                            courseCreditValue={oldCourse.creditValue} // Add this if available
                            courseEquivalentCourses={oldCourse.equivalentCourses} // Add this if available
                            courseNotes={oldCourse.courseNotes}
                            courseSubject={oldCourse.subject} // Add this if available
                            courseCatalog={oldCourse.catalog}
                            courseComponents={oldCourseComponents}
                            changedFields={changedFields}
                            version="old"
                        />
                    </Box>
                    <Box w="100%">
                        <CoursePreviewWithChange
                            courseCareer={newCourseCareer} // Update these props as needed
                            courseDescription={newCourse.description}
                            coursePreReqs={newCourse.preReqs}
                            courseTitle={newCourse.title}
                            courseCreditValue={newCourse.creditValue} // Add this if available
                            courseEquivalentCourses={newCourse.equivalentCourses} // Add this if available
                            courseNotes={newCourse.courseNotes}
                            courseSubject={newCourse.subject} // Add this if available
                            courseCatalog={newCourse.catalog}
                            courseComponents={newCourseComponents}
                            changedFields={changedFields}
                            version="new"
                        />
                    </Box>
                </HStack>
            )}
        </>
    );
};

export default CourseDifferenceViewer;
