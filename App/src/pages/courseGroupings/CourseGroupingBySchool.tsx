import {
    Box,
    Flex,
    Select,
    TableContainer,
    Table,
    Text,
    Thead,
    Tr,
    Th,
    Tbody,
    Td,
    Tooltip,
    IconButton,
    Tfoot,
    useToast,
} from "@chakra-ui/react";
import { useEffect, useRef, useState } from "react";
import { CourseGroupingDTO, MultiCourseGroupingDTO, SchoolEnum } from "../../models/courseGrouping";
import Button from "../../components/Button";
import { GetCourseGroupingBySchool } from "../../services/courseGrouping";
import { InfoIcon } from "@chakra-ui/icons";
import { BaseRoutes } from "../../constants";
import { useNavigate } from "react-router-dom";
import { showToast } from "../../utils/toastUtils";
export default function GroupingBySchool() {
    const selectedSchoolRef = useRef<HTMLSelectElement>(null);
    const [selectedSchool, setSelectedSchool] = useState<string>("GinaCody");
    const [changer, setChanger] = useState<SchoolEnum>(SchoolEnum.GinaCody);
    const [courseGroupings, setCourseGroupings] = useState<CourseGroupingDTO[]>([]);
    const toast = useToast(); // Use the useToast hook
    const navigate = useNavigate();

    useEffect(() => {
        if (selectedSchool == "Gina Cody School of Engineering") {
            setChanger(SchoolEnum.GinaCody);
        }
        if (selectedSchool == "Faculty of Arts And Science") {
            setChanger(SchoolEnum.ArtsAndScience);
        }
        if (selectedSchool == "Faculty of Fine Arts") {
            setChanger(SchoolEnum.FineArts);
        }
        if (selectedSchool == "John Molson School of Business") {
            setChanger(SchoolEnum.JMSB);
        }
    }, [selectedSchool]);

    function searchBySchool(input: SchoolEnum) {
        console.log(input);
        GetCourseGroupingBySchool(input)
            .then((res: MultiCourseGroupingDTO) => {
                setCourseGroupings(res.data);
                if (res.data.length == 0) {
                    showToast(toast, "Error!", "No results found.", "error");
                }
            })
            .catch((err) => {
                console.log(err);
                showToast(toast, "Error!", "An error has occured.", "error");
            });
    }

    function handleNavigateToCurriculumDetails(input: string) {
        console.log(input);
        navigate(BaseRoutes.CourseGrouping.replace(":courseGroupingId", input));
    }
    return (
        <div>
            <Box maxW="5xl" m="auto">
                <Button
                    style="primary"
                    variant="outline"
                    height="40px"
                    width="fit-content"
                    margin="2%"
                    onClick={() => navigate(-1)}
                >
                    Return
                </Button>
                <Flex flexDirection="column">
                    <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                        Curriculum by School
                    </Text>
                    <Select
                        ref={selectedSchoolRef}
                        value={selectedSchool}
                        onChange={(e) => setSelectedSchool(e.target.value)}
                    >
                        <option
                            key={0}
                            value={"Gina Cody School of Engineering"} // Store the entire component object as a string
                        >
                            Gina Cody School of Engineering
                        </option>
                        <option
                            key={1}
                            value={"Faculty of Arts And Science"} // Store the entire component object as a string
                        >
                            Faculty of Arts And Science
                        </option>
                        <option
                            key={2}
                            value={"Faculty of Fine Arts"} // Store the entire component object as a string
                        >
                            Faculty of Fine Arts
                        </option>
                        <option
                            key={3}
                            value={"John Molson School of Business"} // Store the entire component object as a string
                        >
                            John Molson School of Business
                        </option>
                    </Select>
                    <Button
                        mt={4}
                        mb={2}
                        style="secondary"
                        variant="outline"
                        width="22%"
                        height="40px"
                        onClick={() => {
                            searchBySchool(changer);
                        }}
                    >
                        Search
                    </Button>
                    <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                        <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                            <Thead backgroundColor={"#e2e8f0"}>
                                <Tr display={"flex"}>
                                    <Th minW={"250px"} maxW={"250px"}>
                                        Name
                                    </Th>
                                    <Th minW={"450px"} maxW={"450px"}>
                                        Credits Required
                                    </Th>
                                    <Th minW={"120px"} maxW={"120px"}>
                                        State
                                    </Th>
                                    <Th width={"25%"}>See Details</Th>
                                </Tr>
                            </Thead>
                            <Tbody>
                                {courseGroupings.slice(0, courseGroupings.length).map((grouping) => (
                                    <Tr key={grouping.id} display={"flex"}>
                                        <Td minW={"250px"} maxW={"250px"} style={{ whiteSpace: "normal" }}>
                                            {grouping.name}
                                        </Td>
                                        <Td minW={"455px"} maxW={"455px"}>
                                            <Text
                                                overflow="hidden"
                                                textOverflow="ellipsis"
                                                maxW={"500px"}
                                                style={{ whiteSpace: "normal" }}
                                            >
                                                {grouping.requiredCredits}
                                            </Text>
                                        </Td>
                                        <Td minW={"130px"} maxW={"130px"}>
                                            {grouping.school}
                                        </Td>

                                        <Td width={"25%"}>
                                            <Tooltip label="Curriculum Details">
                                                <IconButton
                                                    ml={2}
                                                    aria-label="Details"
                                                    icon={<InfoIcon />}
                                                    onClick={() => {
                                                        handleNavigateToCurriculumDetails(grouping.id);
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
                                            <Text alignSelf="center">Showing {courseGroupings.length} results</Text>
                                        </Flex>
                                    </Td>
                                </Tr>
                            </Tfoot>
                        </Table>
                    </TableContainer>
                </Flex>
            </Box>
        </div>
    );
}
