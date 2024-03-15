import { Box, Checkbox, Heading, ListItem, UnorderedList, Text } from "@chakra-ui/react";
import { CourseGroupingDTO } from "../models/courseGrouping";
import { useEffect, useState } from "react";

interface CourseGroupingComponentProps {
    courseGrouping: CourseGroupingDTO;
    inheritedRequiredCredits?: number;
    onTotalCreditsChange?: (subgroupId: string | number, credits: number, isFulfilled: boolean) => void;
    selectAll?: boolean;
}

export default function CourseGroupingComponent(prop: CourseGroupingComponentProps) {
    const [selectedCourses, setSelectedCourses] = useState<number[]>([]);
    const [subgroupCredits, setSubgroupCredits] = useState<Record<string | number, number>>({});
    const [subgroupFulfillment, setSubgroupFulfillment] = useState<Record<string | number, boolean>>({});

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

    const allSubgroupsFulfilled = Object.values(subgroupFulfillment).every((isFulfilled) => isFulfilled);

    // Determine if all courses should be selected
    //if a group has courses, they should all be selected before the extra credits are spilled to the subgroups
    const shouldAllCoursesBeSelected =
        prop.courseGrouping?.courses.reduce((acc, course) => acc + Number(course.creditValue), 0) <
        Number(requiredCredits);

    const areAllCoursesSelected = prop.courseGrouping?.courses.every((course) =>
        selectedCourses.includes(course.courseID)
    );

    // Effect to report total credits including subgroups to parent
    useEffect(() => {
        const totalSubgroupCredits = Object.values(subgroupCredits).reduce((acc, credits) => acc + credits, 0);
        const allSubgroupsFulfilled = Object.values(subgroupFulfillment).every((isFulfilled) => isFulfilled);

        // Assuming the current group's requirement is also considered fulfilled
        // if its own totalCredits and all its subgroups' requirements are fulfilled.
        const isCurrentGroupFulfilled =
            (!shouldAllCoursesBeSelected || areAllCoursesSelected || !shouldAllCoursesBeSelected) &&
            totalCredits + totalSubgroupCredits >= Number(requiredCredits) &&
            allSubgroupsFulfilled;

        prop.onTotalCreditsChange?.(
            prop.courseGrouping.id,
            totalCredits + totalSubgroupCredits,
            isCurrentGroupFulfilled
        );
    }, [prop.courseGrouping?.id, totalCredits, subgroupCredits, subgroupFulfillment, requiredCredits]);

    useEffect(() => {
        if (prop.selectAll) {
            const allCourseIDs = prop.courseGrouping.courses.map((course) => course.courseID);
            setSelectedCourses(allCourseIDs);
        } else {
            setSelectedCourses([]);
        }
    }, [prop.selectAll]);

    // Adjusted to handle updates from subgroups correctly
    const handleSubgroupTotalCreditsChange = (subgroupId: string | number, credits: number, isFulfilled: boolean) => {
        setSubgroupCredits((prev) => ({ ...prev, [subgroupId]: credits }));
        setSubgroupFulfillment((prev) => ({ ...prev, [subgroupId]: isFulfilled }));
    };

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
                    {prop.courseGrouping?.isTopLevel ? "" : `(${requiredCredits} credits required)`}
                </Heading>
            )}

            {prop.courseGrouping && (
                <Text
                    mt={2}
                    color={
                        (!shouldAllCoursesBeSelected || areAllCoursesSelected || !shouldAllCoursesBeSelected) &&
                        totalCredits + Object.values(subgroupCredits).reduce((acc, value) => acc + value, 0) >=
                            Number(requiredCredits) &&
                        allSubgroupsFulfilled
                            ? "green.500"
                            : "red.500"
                    }
                >
                    Total Selected Credits:{" "}
                    {totalCredits + Object.values(subgroupCredits).reduce((acc, value) => acc + value, 0)}{" "}
                    {(!shouldAllCoursesBeSelected || areAllCoursesSelected || !shouldAllCoursesBeSelected) &&
                    totalCredits + Object.values(subgroupCredits).reduce((acc, value) => acc + value, 0) >=
                        Number(requiredCredits) &&
                    allSubgroupsFulfilled
                        ? "(Requirement Met)"
                        : "(Requirement Not Met)"}
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
                            key={subGroup.id || index}
                            courseGrouping={subGroup}
                            inheritedRequiredCredits={
                                Number(requiredCredits) / prop.courseGrouping?.subGroupings.length //divide the number of credits equally on the subgroups. This is a naive approach
                            }
                            onTotalCreditsChange={handleSubgroupTotalCreditsChange}
                            selectAll={prop.selectAll}
                        />
                    ))}
                </Box>
            )}
        </Box>
    );
}
