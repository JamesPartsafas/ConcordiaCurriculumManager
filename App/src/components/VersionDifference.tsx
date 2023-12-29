import React from "react";
import PropTypes from "prop-types";
import { Box, Text, VStack, Stack } from "@chakra-ui/react";

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
        <VStack align="stretch">
            <Box>
                <Text fontSize="lg" fontWeight="bold">
                    Old Version
                </Text>
                {differences.map(({ key, oldValue, isChanged }) => (
                    <>
                        <Stack key={key} direction="row">
                            <Text key={key} as={isChanged ? "s" : "span"} color={isChanged ? "red" : "black"}>
                                {key}: {oldValue}
                            </Text>
                        </Stack>
                    </>
                ))}
            </Box>
            <Box>
                <Text fontSize="lg" fontWeight="bold">
                    New Version
                </Text>
                {differences.map(({ key, newValue, isChanged }) => (
                    <>
                        <Stack key={key} direction="row">
                            <Text key={key} color={isChanged ? "blue" : "black"}>
                                {key}: {newValue}
                            </Text>
                        </Stack>
                    </>
                ))}
            </Box>
        </VStack>
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
