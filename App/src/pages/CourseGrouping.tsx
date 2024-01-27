import { get } from "http";
import { useEffect, useState } from "react";
import { GetCourseGrouping } from "../services/courseGrouping";
import { CourseGroupingDTO } from "../models/courseGrouping";
import { set } from "react-hook-form";
import { useParams } from "react-router-dom";
import { Box, Heading } from "@chakra-ui/react";

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
            <Box mt={10} backgroundColor={"brandRed"} height={"100px"} display={"flex"} alignItems={"center"} justifyContent={"center"}>
                <Heading textAlign={"center"} color={"white"}>Degree Requirements for {courseGrouping?.name}</Heading>
            </Box>
        </>
    );
}
