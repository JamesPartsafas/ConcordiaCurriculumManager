import React, { useEffect, useState } from "react";
import { Box, Card, CardBody, Center, HStack, Heading, Stack, Text } from "@chakra-ui/react";
import OldStringDiff from "./OldStringDiff"; // Assuming StringDiff is in the same directory
import NewStringDiff from "./NewStringDiff"; // Assuming StringDiff is in the same directory
import { CourseGroupingDTO, SchoolEnumArray } from "../../models/courseGrouping";
import { diffWordsWithSpace } from "diff";

// Adapting detectChanges function and other utilities for CourseGroupingDTO...

const detectChanges = (oldText, newText) => {
    if (!oldText || oldText === "") oldText = "None";
    if (!newText || newText === "") newText = "None";
    const diffResult = diffWordsWithSpace(oldText, newText);
    const oldTextArray = [];
    const newTextArray = [];

    diffResult.forEach((part) => {
        if (part.added) {
            newTextArray.push({ value: part.value, type: "added" });
        } else if (part.removed) {
            oldTextArray.push({ value: part.value, type: "removed" });
        } else {
            oldTextArray.push({ value: part.value, type: "unchanged" });
            newTextArray.push({ value: part.value, type: "unchanged" });
        }
    });

    return { oldTextArray, newTextArray };
};

const CourseGroupingDiffViewer = ({
    oldGrouping,
    newGrouping,
}: {
    oldGrouping: CourseGroupingDTO;
    newGrouping: CourseGroupingDTO;
}) => {
    const [diffResults, setDiffResults] = useState(null);

    useEffect(() => {
        if (!oldGrouping || !newGrouping) return;
        const results = {};
        results["Name"] = detectChanges(oldGrouping.name, newGrouping.name);
        results["Description"] = detectChanges(oldGrouping.description, newGrouping.description);
        results["Credits"] = detectChanges(oldGrouping.requiredCredits, newGrouping.requiredCredits);
        results["Notes"] = detectChanges(oldGrouping.notes, newGrouping.notes);
        results["School"] = detectChanges(
            SchoolEnumArray[oldGrouping.school].label,
            SchoolEnumArray[newGrouping.school].label
        );
        results["Top Level"] = detectChanges(
            oldGrouping.isTopLevel ? "Yes" : "No",
            newGrouping.isTopLevel ? "Yes" : "No"
        );
        getSubCourseGrouping(oldGrouping, newGrouping, results);
        // same thing for courses
        let oldCourseRef = "";
        let newCourseRef = "";
        oldGrouping.courseIdentifiers.filter((oldRef) => {
            const newCourse = oldGrouping.courses.find((course) => oldRef.concordiaCourseId === course.courseID);
            if (newCourse) {
                oldCourseRef = oldCourseRef + newCourse.subject + " " + newCourse.catalog.toString();
                if (oldGrouping.courseIdentifiers.indexOf(oldRef) < oldGrouping.courseIdentifiers.length - 1) {
                    oldCourseRef = oldCourseRef + ", ";
                }
            }
        });
        newGrouping.courseIdentifiers.filter((newRef) => {
            const newCourse = newGrouping.courses.find((course) => newRef.concordiaCourseId === course.courseID);
            if (newCourse) {
                newCourseRef = newCourseRef + newCourse.subject + " " + newCourse.catalog.toString();
                if (newGrouping.courseIdentifiers.indexOf(newRef) < newGrouping.courseIdentifiers.length - 1) {
                    newCourseRef = newCourseRef + ", ";
                }
            }
        });
        results["Courses"] = detectChanges(oldCourseRef, newCourseRef);
        results["Published"] = detectChanges(
            oldGrouping.published ? "Yes" : "No",
            newGrouping.published ? "Yes" : "No"
        );
        setDiffResults(results);
    }, [oldGrouping, newGrouping]);

    function getSubCourseGrouping(oldGrouping, newGrouping, results) {
        let oldSubGroupingRef = "";
        let newSubGroupingRef = "";
        oldGrouping.subGroupingReferences.filter((oldRef) => {
            const newSubGrouping = oldGrouping.subGroupings.find(
                (subGrouping) => oldRef.childGroupCommonIdentifier === subGrouping.commonIdentifier
            );
            if (newSubGrouping) {
                oldSubGroupingRef = oldSubGroupingRef + newSubGrouping.name;
                if (oldGrouping.subGroupingReferences.indexOf(oldRef) < oldGrouping.subGroupingReferences.length - 1) {
                    oldSubGroupingRef = oldSubGroupingRef + ", ";
                }
            }
        });
        newGrouping.subGroupingReferences.filter((newRef) => {
            const newSubGrouping = newGrouping.subGroupings.find(
                (subGrouping) => newRef.childGroupCommonIdentifier === subGrouping.commonIdentifier
            );
            if (newSubGrouping) {
                newSubGroupingRef = newSubGroupingRef + newSubGrouping.name;
                if (newGrouping.subGroupingReferences.indexOf(newRef) < newGrouping.subGroupingReferences.length - 1) {
                    newSubGroupingRef = newSubGroupingRef + ", ";
                }
            }
        });
        results["Sub-Groupings"] = detectChanges(oldSubGroupingRef, newSubGroupingRef);
    }
    return (
        (diffResults && (
            <HStack align="stretch" spacing={4}>
                <Stack width="50%">
                    <Center>
                        <Heading as="h2" size="xl" color="brandRed">
                            Old Version
                        </Heading>
                    </Center>
                    <Stack>
                        <Card>
                            <CardBody>
                                <Box bg={"gray.200"} p={2}>
                                    {Object.keys(diffResults).map((field, index) => (
                                        <Text key={index}>
                                            <b>{field}</b>
                                            {": "}
                                            <OldStringDiff oldTextArray={diffResults[field].oldTextArray} />
                                        </Text>
                                    ))}
                                </Box>
                            </CardBody>
                        </Card>
                    </Stack>
                </Stack>
                <Stack width="50%">
                    <Center>
                        <Heading as="h2" size="xl" color="brandRed">
                            New Version
                        </Heading>
                    </Center>
                    <Stack>
                        <Card>
                            <CardBody>
                                <Box bg={"gray.200"} p={2}>
                                    {Object.keys(diffResults).map((field, index) => (
                                        <Text key={index}>
                                            <b>{field}</b>
                                            {": "}
                                            <NewStringDiff newTextArray={diffResults[field].newTextArray} />
                                        </Text>
                                    ))}
                                </Box>
                            </CardBody>
                        </Card>
                    </Stack>
                </Stack>
            </HStack>
        )) || <></>
    );
};

export default CourseGroupingDiffViewer;
