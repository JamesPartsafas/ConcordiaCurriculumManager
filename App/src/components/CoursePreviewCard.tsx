import { Box, Card, CardBody, CardHeader, Center, Heading, Stack, Text } from "@chakra-ui/react";

const CoursePreview = ({
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
}) => {
    return (
        <Stack>
            <Center>
                <Heading as="h2" size="xl" color="brandRed">
                    Version Preview
                </Heading>
            </Center>
            <Stack>
                <Card align="center">
                    <CardHeader>
                        <Heading size="md">
                            {" "}
                            {courseSubject} {courseCatalog} {courseTitle} {"("}
                            {courseCreditValue} {parseInt(courseCreditValue) === 1 ? "credit)" : "credits)"}
                        </Heading>
                    </CardHeader>
                    <CardBody>
                        <Box p={2}>
                            <Heading size="xl">
                                {courseCreditValue === "" ? null : <Text display={"inline"}></Text>}
                            </Heading>
                            <Text>
                                <b>Course Career:</b> {courseCareer ? courseCareer : "Not specified"}
                            </Text>
                            <Text>
                                <b>Description:</b>{" "}
                                {courseDescription ? courseDescription : "No description for this class"}
                            </Text>
                            <Text>
                                <b>Prerequisites and Corerequisites:</b> {coursePreReqs ? coursePreReqs : "None"}
                            </Text>
                            <Text>
                                <b>Component(s):</b>{" "}
                                {courseComponents.length === 0
                                    ? "None"
                                    : courseComponents.map(
                                          (component) =>
                                              component.componentName + " " + component.hours + " hour(s) per week. "
                                      )}
                            </Text>
                            <Text>
                                <b>Equivalent Courses: </b> {courseEquivalentCourses ? courseEquivalentCourses : "None"}
                            </Text>
                            <Text>
                                <b>Course Notes: </b> {courseNotes ? courseNotes : "None"}
                            </Text>
                        </Box>
                    </CardBody>
                </Card>
            </Stack>
        </Stack>
    );
};

export default CoursePreview;
