import { useEffect, useState } from "react";
import { GetCourseGrouping } from "../services/courseGrouping";
import { CourseGroupingDTO } from "../models/courseGrouping";

import { Link, useParams } from "react-router-dom";
import { Box, Heading, ListItem, Text, UnorderedList } from "@chakra-ui/react";
import { BaseRoutes } from "../constants";

export default function CourseGrouping() {
    const [courseGrouping, setCourseGrouping] = useState<CourseGroupingDTO>();

    const { courseGroupingId } = useParams();

    useEffect(() => {
        requestCourseGrouping(courseGroupingId);
    }, [courseGroupingId]);

    function requestCourseGrouping(CourseGroupingId: string) {
        GetCourseGrouping(CourseGroupingId)
            .then(
                (response) => {
                    console.log(response.data);
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
                mb={10}
                backgroundColor={"brandRed"}
                minH={"100px"}
                display="flex"
                flexDirection={"row"}
                alignItems="center"
            >
                <Box w={"70%"} margin={"auto"} py={3}>
                    <Heading size={"3xl"} color={"white"}>
                        {courseGrouping?.isTopLevel
                            ? " Degree Requirements for " + courseGrouping?.name
                            : "Degree Requirements" }
                    </Heading>
                </Box>
            </Box>

            <Box w={"70%"} margin={"auto"}>
                {courseGrouping?.isTopLevel ? (
                    <Heading size={"2xl"} mb={3}>
                        Degree Requirements
                    </Heading>
                ): (
                    <Heading size={"2xl"} mb={3}>
                        {courseGrouping?.name}
                    </Heading>
                
                )}
                <Text>{courseGrouping?.description}</Text>

                <Heading mb={3} mt={10} color={"brandRed"}>
                    {courseGrouping?.name}{" "}
                    {parseFloat(courseGrouping?.requiredCredits)
                        ? " (" + parseFloat(courseGrouping?.requiredCredits) + " credits)"
                        : ""}
                </Heading>

                {courseGrouping?.subGroupings.map((subGrouping, index) => (
                    <Text ml={2} mb={4} key={index}>
                        {parseFloat(subGrouping.requiredCredits)
                            ? parseFloat(subGrouping.requiredCredits).toString().padEnd(5, "\u00A0") +
                              "credits from the "
                            : ""}
                        {subGrouping.name} <br />
                    </Text>
                ))}

                <UnorderedList ml={12} mt={2}>
                    {courseGrouping?.courses.map((course, index) => (
                        <ListItem key={index}>
                            <Text mb={2}>
                                {course.subject + " " + course.catalog + " " + course.title} (
                                {parseFloat(course.creditValue)})
                            </Text>
                        </ListItem>
                    ))}
                </UnorderedList>

                {courseGrouping?.subGroupings.map((subGrouping, index) => (
                    <Box ml={2} mb={5} key={index}>
                        <Link to={BaseRoutes.CourseGrouping.replace(":courseGroupingId", subGrouping.id)}>
                            <Heading color={"brandRed"} mb={3}>
                                {subGrouping.name}
                                {parseFloat(subGrouping?.requiredCredits)
                                    ? " (" + parseFloat(subGrouping?.requiredCredits) + " credits)"
                                    : ""}
                            </Heading>
                        </Link>

                        <Text mb={3}>{subGrouping?.description}</Text>

                        {subGrouping.notes && (
                            <Text mt={3} mb={3}>
                                <u>
                                    <b> Notes:</b>
                                </u>{" "}
                                {subGrouping.notes}
                            </Text>
                        )}

                        {/* level 2 */}
                        {subGrouping?.subGroupings.map((subGrouping, index) => (
                            <Box ml={5} mt={5} mb={5} key={index}>
                                <Link to={BaseRoutes.CourseGrouping.replace(":courseGroupingId", subGrouping.id)}>
                                    <Heading size={"md"} mb={3}>
                                        {subGrouping.name}
                                    </Heading>
                                </Link>

                                <Text>{subGrouping?.description}</Text>

                                {subGrouping.notes && (
                                    <Text mt={3} mb={3}>
                                        <u>
                                            <b> Notes:</b>
                                        </u>{" "}
                                        {subGrouping.notes}
                                    </Text>
                                )}

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
                {courseGrouping?.notes && (
                    <Box mt={12}>
                        <Heading mb={3}>
                            Notes
                        </Heading>
                        <Text mt={3} mb={3}>
                            {courseGrouping?.notes}
                        </Text>
                    </Box>
                )}
            </Box>
        </>
    );
}
