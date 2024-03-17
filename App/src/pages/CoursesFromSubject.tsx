import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { Course } from "../models/course";
import { BaseRoutes } from "../constants";
import { getCoursesDataFromSubject } from "../services/course";
import {
    Flex,
    IconButton,
    Spacer,
    Table,
    TableContainer,
    Tbody,
    Td,
    Text,
    Tfoot,
    Th,
    Thead,
    Tooltip,
    Tr,
} from "@chakra-ui/react";
import { InfoIcon } from "@chakra-ui/icons";
import { Button as ChakraButton } from "@chakra-ui/react";

export default function CoursesFromSubject() {
    function useQuery() {
        return new URLSearchParams(useLocation().search);
    }
    const query = useQuery(); // Use the useQuery function
    const [courseData, setCourseData] = useState<Course[]>([]);
    const subject: string = query.get("subject");
    const navigate = useNavigate();

    useEffect(() => {
        getCoursesDataFromSubject(subject)
            .then((res) => {
                setCourseData(res.data);
            })
            .catch(() => {
                navigate(BaseRoutes.NoData);
            });
    }, []);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const totalResults = courseData.length;
    const resultsPerPage = 10;
    const startIndex = courseData.length === 0 ? 0 : (currentPage - 1) * resultsPerPage + 1;
    const endIndex = Math.min(currentPage * resultsPerPage, totalResults);
    return (
        <>
            <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                    <Thead backgroundColor={"#e2e8f0"}>
                        <Tr display={"flex"}>
                            <Th minW={"200px"} maxW={"200px"}>
                                Title
                            </Th>
                            <Th minW={"450px"} maxW={"450px"}>
                                Description
                            </Th>
                            <Th minW={"120px"} maxW={"120px"}>
                                Credit Value
                            </Th>
                            <Th width={"25%"}></Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        {courseData.slice(startIndex - 1, endIndex).map((course) => (
                            <Tr key={course.courseID} display={"flex"}>
                                <Td minW={"200px"} maxW={"200px"} style={{ whiteSpace: "normal" }}>
                                    {course.title}
                                </Td>
                                <Td minW={"450px"} maxW={"450px"}>
                                    <Text
                                        overflow="hidden"
                                        textOverflow="ellipsis"
                                        maxW={"500px"}
                                        style={{ whiteSpace: "normal" }}
                                    >
                                        {course.description}
                                    </Text>
                                </Td>
                                <Td minW={"120px"} maxW={"120px"}>
                                    {course.creditValue}
                                </Td>

                                <Td width={"25%"}>
                                    <Tooltip label="Dossier Details">
                                        <IconButton
                                            ml={2}
                                            aria-label="Details"
                                            icon={<InfoIcon />}
                                            onClick={() => {
                                                `/CourseDetails?subject=${course.subject}&catalog=${course.catalog}`;
                                            }}
                                        />
                                    </Tooltip>
                                </Td>
                            </Tr>
                        ))}
                    </Tbody>
                    <Tfoot>
                        <Tr>
                            <Td height={20}>
                                <Flex>
                                    <Text alignSelf="center">
                                        Showing {startIndex} to {endIndex} of {courseData.length} results
                                    </Text>
                                    <Spacer />
                                    <ChakraButton
                                        mr={4}
                                        p={4}
                                        variant="outline"
                                        onClick={() => setCurrentPage(currentPage - 1)}
                                        isDisabled={startIndex == 0 || startIndex == 1}
                                    >
                                        Previous
                                    </ChakraButton>
                                    <ChakraButton
                                        p={4}
                                        variant="outline"
                                        onClick={() => setCurrentPage(currentPage + 1)}
                                        isDisabled={endIndex == totalResults}
                                    >
                                        Next
                                    </ChakraButton>
                                </Flex>
                            </Td>
                        </Tr>
                    </Tfoot>
                </Table>
            </TableContainer>
        </>
    );
}
