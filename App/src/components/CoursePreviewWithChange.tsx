import React from "react";
import { Box, Card, CardBody, Center, Heading, Stack, Text } from "@chakra-ui/react";

const CoursePreviewWithChange = ({
    courseCareer,
    courseDescription,
    coursePreReqs,
    courseTitle,
    courseCreditValue,
    courseEquivalentCourses,
    courseNotes,
    courseSubject,
    courseCatalog,
    courseComponents,
    changedFields,
    version, // Indicates if it's the old or new version of the course
}: {
    courseCareer: string;
    courseDescription: string;
    coursePreReqs: string;
    courseTitle: string;
    courseCreditValue: string;
    courseEquivalentCourses: string;
    courseNotes: string;
    courseSubject: string;
    courseCatalog: string;
    courseComponents: { componentName: string; hours?: number }[];
    changedFields: { [key: string]: boolean };
    version: "old" | "new";
}) => {
    // Function to apply conditional styling
    const getStyleForChange = (field: string) => {
        if (changedFields[field]) {
            return version === "old"
                ? { textDecoration: "line-through", color: "red" }
                : { fontWeight: "bold", color: "blue" };
        }
        return {};
    };

    return (
        <Stack>
            <Center>
                <Heading as="h2" size="xl" color="brandRed">
                    {version === "old" ? "Old Version" : "Current Version"}
                </Heading>
            </Center>
            <Stack>
                <Card>
                    <CardBody>
                        <Box bg={"gray.200"} p={2}>
                            <Heading size="xl">
                                <span style={getStyleForChange("subject")}>{courseSubject} </span>
                                <span style={getStyleForChange("catalog")}>{courseCatalog} </span>
                                <span style={getStyleForChange("title")}>{courseTitle} </span>
                                {courseCreditValue === "" ? null : (
                                    <Text display={"inline"} style={getStyleForChange("creditValue")}>
                                        {"("}
                                        {courseCreditValue} {parseInt(courseCreditValue) === 1 ? "credit)" : "credits)"}
                                    </Text>
                                )}
                            </Heading>
                            <Text>
                                <b>Course Career:</b>{" "}
                                <span style={getStyleForChange("career")}>
                                    {courseCareer ? courseCareer : "Not specified"}
                                </span>
                            </Text>
                            <Text>
                                <b>Description:</b>{" "}
                                <span style={getStyleForChange("description")}>
                                    {courseDescription ? courseDescription : "No description for this class"}
                                </span>
                            </Text>
                            <Text>
                                <b>Prerequisites and Corerequisites:</b>
                                <span style={getStyleForChange("preReqs")}>
                                    {coursePreReqs ? coursePreReqs : "None"}
                                </span>
                            </Text>
                            <Text>
                                <b>Component(s):</b>{" "}
                                <span style={getStyleForChange("componentCodes")}>
                                    {courseComponents.length === 0
                                        ? "Not Available"
                                        : courseComponents.map(
                                              (component) =>
                                                  component.componentName +
                                                  " " +
                                                  (component.hours || 0) +
                                                  " hour(s) per week. "
                                          )}
                                </span>
                            </Text>
                            <Text>
                                <b>Equivalent Courses: </b>{" "}
                                <span style={getStyleForChange("equivalentCourses")}>
                                    {" "}
                                    {courseEquivalentCourses ? courseEquivalentCourses : "None"}
                                </span>
                            </Text>
                            <Text>
                                <b>Course Notes: </b>{" "}
                                <span style={getStyleForChange("courseNotes")}>
                                    {courseNotes ? courseNotes : "None"}
                                </span>{" "}
                            </Text>
                        </Box>
                    </CardBody>
                </Card>
            </Stack>
        </Stack>
    );
};

export default CoursePreviewWithChange;
