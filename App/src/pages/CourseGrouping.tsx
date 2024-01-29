import { useEffect, useState } from "react";
import { GetCourseGrouping } from "../services/courseGrouping";
import { CourseGroupingDTO } from "../models/courseGrouping";

import { useParams } from "react-router-dom";
import { Box, Heading, ListItem, Text, UnorderedList } from "@chakra-ui/react";

export default function CourseGrouping() {
    const [courseGrouping, setCourseGrouping] = useState<CourseGroupingDTO>();

    const { courseGroupingId } = useParams();

    useEffect(() => {
        requestCourseGrouping(courseGroupingId);
    }, []);

    function requestCourseGrouping(CourseGroupingId: string) {
        GetCourseGrouping(CourseGroupingId)
            .then(
                (response) => {
                    setCourseGrouping(response.data);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    return (
        <>
            <Box
                mt={10}
                backgroundColor={"brandRed"}
                minH={"100px"}
                display="flex"
                flexDirection={"row"}
                alignItems="center"
            >
                <Box w={"70%"} margin={"auto"} py={3}>
                    <Heading size={"3xl"} color={"white"}>
                        Degree Requirements for {courseGrouping?.name}
                    </Heading>
                </Box>
            </Box>

            <Box w={"70%"} margin={"auto"}>
                <Heading size={"2xl"} mb={3} mt={10}>
                    Degree Requirements
                </Heading>
                <Text>{courseGrouping?.description}</Text>

                <Heading mb={3} mt={10} color={"brandRed"}>
                    {courseGrouping?.name} ({parseFloat(courseGrouping?.requiredCredits)} credits)
                </Heading>

                {courseGrouping?.subGroupings.map((subGrouping, index) => (
                    <Text ml={2} mb={3} key={index}>
                        {parseFloat(subGrouping.requiredCredits).toString().padEnd(5, "\u00A0")} credits from the{" "}
                        {subGrouping.name} <br />
                    </Text>
                ))}

                {courseGrouping?.subGroupings.map((subGrouping, index) => (
                    <Box ml={2} mb={5} key={index}>
                        <Heading color={"brandRed"} mb={3}>
                            {subGrouping.name} ({parseFloat(subGrouping.requiredCredits)} credits)
                        </Heading>

                        <Text>{subGrouping?.description}</Text>

                        {/* level 2 */}
                        {subGrouping?.subGroupings.map((subGrouping, index) => (
                            <Box ml={5} mt={5} mb={5} key={index}>
                                <Heading size={"md"} mb={3}>
                                    {subGrouping.name}
                                </Heading>

                                <Text>{subGrouping?.description}</Text>

                                <UnorderedList ml={12} mt={2}>
                                    {subGrouping.courses.map((course, index) => (
                                        <ListItem key={index}>
                                            <Text mb={2}>
                                                {course.subject + " " + course.catalog + " " + course.title} (
                                                {parseFloat(course.creditValue)})
                                            </Text>
                                        </ListItem>
                                    ))}
                                </UnorderedList>
                            </Box>
                        ))}

                        <UnorderedList ml={12}>
                            {subGrouping.courses.map((course, index) => (
                                <ListItem key={index}>
                                    <Text mb={2}>
                                        {course.subject + " " + course.catalog + " " + course.title} (
                                        {parseFloat(course.creditValue)})
                                    </Text>
                                </ListItem>
                            ))}
                        </UnorderedList>
                    </Box>
                ))}
            </Box>
        </>
    );
}
