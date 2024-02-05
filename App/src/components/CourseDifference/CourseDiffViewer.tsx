import { useEffect, useState } from "react";
import { Box, Card, CardBody, Center, HStack, Heading, Stack, Text } from "@chakra-ui/react";
import OldStringDiff from "./OldStringDiff"; // Assuming StringDiff is in the same directory
import NewStringDiff from "./NewStringDiff"; // Assuming StringDiff is in the same directory
import { AllCourseSettings, Course } from "../../models/course";
import { diffWordsWithSpace } from "diff";
import { getCourseCareerNameByCode, getCourseComponentsObject } from "../../utils/courseGetters";

const detectChanges = (oldText, newText) => {
    if (oldText === "") oldText = "None";
    if (newText === "") newText = "None";
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

const componentCodesToString = (componentCodes) => {
    return componentCodes.map(({ componentName, hours }) => `${componentName}: ${hours || 0} hour(s)`).join(", ");
};

const CourseDiffViewer = ({
    oldCourse,
    newCourse,
    allCourseSettings,
}: {
    oldCourse: Course;
    newCourse: Course;
    allCourseSettings: AllCourseSettings;
}) => {
    const [diffResults, setDiffResults] = useState(null);
    const fieldsToCompare = ["title", "creditValue", "description", "preReqs", "courseNotes", "equivalentCourses"];
    const fieldsToTitle = {
        career: "Course Career",
        description: "Description",
        preReqs: "Pre-requisites",
        componentCodes: "Components",
        equivalentCourses: "Equivalent Courses",
        courseNotes: "Course Notes",
    };
    useEffect(() => {
        const results = {};
        fieldsToCompare.forEach((field) => {
            results[field] = detectChanges(oldCourse[field], newCourse[field]);
        });
        const oldCourseCareer = getCourseCareerNameByCode(oldCourse.career, allCourseSettings);
        const newCourseCareer = getCourseCareerNameByCode(newCourse.career, allCourseSettings);
        const oldComponentCodesString = componentCodesToString(getCourseComponentsObject(oldCourse, allCourseSettings));
        const newComponentCodesString = componentCodesToString(getCourseComponentsObject(newCourse, allCourseSettings));
        results["career"] = detectChanges(oldCourseCareer, newCourseCareer);
        results["componentCodes"] = detectChanges(oldComponentCodesString, newComponentCodesString);
        setDiffResults(results);
    }, [oldCourse, newCourse]);

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
                                    <Heading size="xl">
                                        {oldCourse.subject} {oldCourse.catalog}{" "}
                                        <OldStringDiff
                                            oldTextArray={detectChanges(oldCourse.title, newCourse.title).oldTextArray}
                                        />{" "}
                                        <Text as="span" display="inline-block">
                                            {" ("}
                                            <OldStringDiff
                                                oldTextArray={
                                                    detectChanges(oldCourse.creditValue, newCourse.creditValue)
                                                        .oldTextArray
                                                }
                                            />{" "}
                                            {parseInt(oldCourse.creditValue) === 1 ? "credit)" : "credits)"}
                                        </Text>
                                    </Heading>

                                    {Object.keys(fieldsToTitle).map((field, index) => (
                                        <Text key={index}>
                                            <b>{fieldsToTitle[field]}</b>
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
                            Current Version
                        </Heading>
                    </Center>
                    <Stack>
                        <Card>
                            <CardBody>
                                <Box bg={"gray.200"} p={2}>
                                    <Heading size="xl">
                                        {newCourse.subject} {newCourse.catalog}{" "}
                                        <NewStringDiff
                                            newTextArray={detectChanges(oldCourse.title, newCourse.title).newTextArray}
                                        />{" "}
                                        <Text as="span" display="inline-block">
                                            {" ("}
                                            <NewStringDiff
                                                newTextArray={
                                                    detectChanges(oldCourse.creditValue, newCourse.creditValue)
                                                        .newTextArray
                                                }
                                            />{" "}
                                            {parseInt(newCourse.creditValue) === 1 ? "credit)" : "credits)"}
                                        </Text>
                                    </Heading>

                                    {Object.keys(fieldsToTitle).map((field, index) => (
                                        <Text key={index}>
                                            <b>{fieldsToTitle[field]}</b>
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

export default CourseDiffViewer;
