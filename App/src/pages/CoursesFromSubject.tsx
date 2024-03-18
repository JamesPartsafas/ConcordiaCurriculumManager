import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { Course } from "../models/course";
import { getCoursesDataFromSubject } from "../services/course";
import {
    Box,
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
import Button from "../components/Button";

export default function CoursesFromSubject() {
    function useQuery() {
        return new URLSearchParams(useLocation().search);
    }
    const query = useQuery(); // Use the useQuery function
    const [courseData, setCourseData] = useState<Course[]>([]);
    const subject: string = query.get("subject");
    const navigate = useNavigate();

    useEffect(() => {
        getCoursesDataFromSubject(subject).then((res) => {
            setCourseData(res.data);
        });
    }, []);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const totalResults = courseData.length;
    const resultsPerPage = 10;
    const startIndex = courseData.length === 0 ? 0 : (currentPage - 1) * resultsPerPage + 1;
    const endIndex = Math.min(currentPage * resultsPerPage, totalResults);
    return (
        <>
            <Box maxW="8xl" m="auto">
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    Search Results for Subject {subject}
                </Text>
                <Flex flexDirection="column">
                    <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                        <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                            <Thead backgroundColor={"#e2e8f0"}>
                                <Tr display={"flex"}>
                                    <Th minW={"300px"} maxW={"300px"}>
                                        Title
                                    </Th>
                                    <Th minW={"700px"} maxW={"700px"}>
                                        Description
                                    </Th>
                                    <Th minW={"170px"} maxW={"170px"}>
                                        Credit Value
                                    </Th>
                                    <Th width={"25%"}></Th>
                                </Tr>
                            </Thead>
                            <Tbody>
                                {courseData.slice(startIndex - 1, endIndex).map((course) => (
                                    <Tr key={course.courseID} display={"flex"}>
                                        <Td minW={"300px"} maxW={"300px"} style={{ whiteSpace: "normal" }}>
                                            {course.title}
                                        </Td>
                                        <Td minW={"700px"} maxW={"700px"}>
                                            <Text
                                                overflow="hidden"
                                                textOverflow="ellipsis"
                                                maxW={"700px"}
                                                style={{ whiteSpace: "normal" }}
                                            >
                                                {course.description}
                                            </Text>
                                        </Td>
                                        <Td minW={"170px"} maxW={"170px"}>
                                            {course.creditValue}
                                        </Td>

                                        <Td width={"25%"}>
                                            <Tooltip label="Course Details">
                                                <IconButton
                                                    ml={2}
                                                    aria-label="Details"
                                                    icon={<InfoIcon />}
                                                    onClick={() => {
                                                        navigate(
                                                            `/CourseDetails?subject=${course.subject}&catalog=${course.catalog}`
                                                        );
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
                    <Button
                        style="secondary"
                        variant="solid"
                        width="240px"
                        height="40px"
                        margin="5px"
                        onClick={() => navigate(-1)}
                    >
                        Go back to browser
                    </Button>
                </Flex>
            </Box>
        </>
    );
}
