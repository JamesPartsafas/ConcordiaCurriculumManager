import { Box, Checkbox, Heading, ListItem, UnorderedList, Text } from "@chakra-ui/react";
import { CourseGroupingDTO } from "../models/courseGrouping";
import { useState } from "react";

interface CourseGroupingComponentProps {
    courseGrouping: CourseGroupingDTO;
    inheritedRequiredCredits?: number;
}

export default function CourseGroupingComponent(prop: CourseGroupingComponentProps) {
    const [selectedCourses, setSelectedCourses] = useState<number[]>([]);

    // Determine the effective required credits for this grouping
    const requiredCredits =
        prop.courseGrouping?.requiredCredits === "N/A"
            ? prop.inheritedRequiredCredits
            : prop.courseGrouping?.requiredCredits;

    // Calculate total credits for selected courses
    const totalCredits =
        prop.courseGrouping?.courses
            ?.filter((course) => selectedCourses.includes(course.courseID))
            .reduce((acc, course) => acc + Number(course.creditValue), 0) || 0;

    // Handle course selection toggle
    const toggleCourseSelection = (courseID: number) => {
        setSelectedCourses((prev) =>
            prev.includes(courseID) ? prev.filter((c) => c !== courseID) : [...prev, courseID]
        );
    };

    return (
        <Box mt={5}>
            {prop.courseGrouping && (
                <Heading size="md" marginBottom={2}>
                    {prop.courseGrouping?.name}{" "}
                    {prop.courseGrouping?.isTopLevel
                        ? ""
                        : `(${prop.courseGrouping?.requiredCredits} credits required)`}
                </Heading>
            )}

            {prop.courseGrouping && (
                <Text mt={2} color={totalCredits >= Number(requiredCredits) ? "green.500" : "red.500"}>
                    Total Selected Credits: {totalCredits}{" "}
                    {totalCredits >= Number(requiredCredits)
                        ? "(Requirement Met)"
                        : "(Requirement Not Met)" + `(${requiredCredits} required)`}
                </Text>
            )}

            {prop.courseGrouping?.courses && prop.courseGrouping?.courses.length > 0 && (
                <UnorderedList ml={12} styleType="none">
                    {prop.courseGrouping?.courses.map((course, index) => (
                        <ListItem key={index}>
                            <Checkbox
                                onChange={() => toggleCourseSelection(course.courseID)}
                                isChecked={selectedCourses.includes(course.courseID)}
                            >
                                {course.subject + " " + course.catalog + " " + course.title} - {course.creditValue}{" "}
                                credits
                            </Checkbox>
                        </ListItem>
                    ))}
                </UnorderedList>
            )}

            {prop.courseGrouping?.subGroupings && prop.courseGrouping?.subGroupings.length > 0 && (
                <Box ml={8}>
                    {prop.courseGrouping?.subGroupings.map((subGroup, index) => (
                        <CourseGroupingComponent
                            key={index}
                            courseGrouping={subGroup}
                            inheritedRequiredCredits={
                                Number(requiredCredits) / prop.courseGrouping?.subGroupings.length
                            }
                        />
                    ))}
                </Box>
            )}
        </Box>
    );
}
