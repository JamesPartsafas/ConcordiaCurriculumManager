import React from "react";
import PropTypes from "prop-types";
import { Box, Text, Stack, HStack, Heading } from "@chakra-ui/react";

const CourseDifferenceViewer = ({ oldCourse, newCourse }) => {
    const compareCourses = (oldCourse, newCourse) => {
        const fieldsToCompare = ["catalog", "title", "career", "description", "preReqs"]; // Fields to compare
        return fieldsToCompare.map((key) => {
            const oldValue = oldCourse[key];
            const newValue = newCourse[key];
            const isChanged = oldValue !== newValue;
            return { key, oldValue, newValue, isChanged };
        });
    };

    const differences = compareCourses(oldCourse, newCourse);

    return (
        <HStack align="stretch">
            <Box bg={"gray.200"} p={2}>
                <Heading as="h2" size="xl" color="brandRed">
                    Old Version
                </Heading>
                {differences.map(({ key, oldValue, isChanged }) => (
                    <>
                        <Stack key={key} direction="row">
                            <Text>{key}: </Text>
                            <Text key={key} as={isChanged ? "s" : "span"} color={isChanged ? "red" : "black"}>
                                {oldValue}
                            </Text>
                        </Stack>
                    </>
                ))}
            </Box>
            <Box bg={"gray.200"} p={2}>
                <Heading as="h2" size="xl" color="brandRed">
                    Current Version
                </Heading>
                {differences.map(({ key, newValue, isChanged }) => (
                    <>
                        <Stack key={key} direction="row">
                            <Text>{key}: </Text>
                            <Text key={key} color={isChanged ? "blue" : "black"}>
                                {newValue}
                            </Text>
                        </Stack>
                    </>
                ))}
            </Box>
        </HStack>
    );
};

CourseDifferenceViewer.propTypes = {
    oldCourse: PropTypes.shape({
        subject: PropTypes.string,
        catalog: PropTypes.string,
        title: PropTypes.string,
        description: PropTypes.string,
        creditValue: PropTypes.string,
        preReqs: PropTypes.string,
        career: PropTypes.number,
        equivalentCourses: PropTypes.string,
        componentCodes: PropTypes.object,
        dossierId: PropTypes.string,
        courseNotes: PropTypes.string,
        rationale: PropTypes.string,
        supportingFiles: PropTypes.object,
        resourceImplication: PropTypes.string,
        courseID: PropTypes.number,
        comment: PropTypes.string,
    }).isRequired,
    newCourse: PropTypes.shape({
        subject: PropTypes.string,
        catalog: PropTypes.string,
        title: PropTypes.string,
        description: PropTypes.string,
        creditValue: PropTypes.string,
        preReqs: PropTypes.string,
        career: PropTypes.number,
        equivalentCourses: PropTypes.string,
        componentCodes: PropTypes.object,
        dossierId: PropTypes.string,
        courseNotes: PropTypes.string,
        rationale: PropTypes.string,
        supportingFiles: PropTypes.object,
        resourceImplication: PropTypes.string,
        courseID: PropTypes.number,
        comment: PropTypes.string,
    }).isRequired,
};

export default CourseDifferenceViewer;
