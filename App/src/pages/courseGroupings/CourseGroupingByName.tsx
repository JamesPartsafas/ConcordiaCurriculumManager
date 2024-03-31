import { useState } from "react";
import { BaseRoutes } from "../../constants";
import { CourseGroupingDTO, MultiCourseGroupingDTO } from "../../models/courseGrouping";
import { useNavigate } from "react-router-dom";
import {
    Box,
    Flex,
    FormControl,
    FormLabel,
    Input,
    Table,
    TableContainer,
    Text,
    Tr,
    Thead,
    Th,
    Tbody,
    Td,
    Tooltip,
    IconButton,
    Tfoot,
    useToast,
} from "@chakra-ui/react";
import { InfoIcon } from "@chakra-ui/icons";
import Button from "../../components/Button";
import { GetCourseGroupingByName } from "../../services/courseGrouping";
import { showToast } from "../../utils/toastUtils";

export default function CourseGroupingByName() {
    const [courseGroupings, setCourseGroupings] = useState<CourseGroupingDTO[]>([]);
    const [searchInput, setSearchInput] = useState<string>("");
    const toast = useToast(); // Use the useToast hook
    const handleChange = (event) => setSearchInput(event.target.value);
    const navigate = useNavigate();

    function handleNavigateToCurriculumDetails(input: string) {
        console.log(input);
        navigate(BaseRoutes.CourseGrouping.replace(":courseGroupingId", input));
    }

    function searchByName(name: string) {
        GetCourseGroupingByName(name)
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
                        Curriculum by Name
                    </Text>
                    <FormControl>
                        <FormLabel htmlFor="search-text">Search By Title:</FormLabel>
                        <Input id="searcher" type="text" value={searchInput} onChange={handleChange} />
                    </FormControl>
                    <Button
                        mt={4}
                        mb={2}
                        style="secondary"
                        variant="outline"
                        width="22%"
                        height="40px"
                        onClick={() => {
                            searchByName(searchInput);
                        }}
                    >
                        Search
                    </Button>
                    <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                        <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                            <Thead backgroundColor={"#e2e8f0"}>
                                <Tr display={"flex"}>
                                    <Th
                                        overflow="hidden"
                                        textOverflow="ellipsis"
                                        minW={"450px"}
                                        maxW={"450px"}
                                        style={{ whiteSpace: "normal" }}
                                    >
                                        Name
                                    </Th>
                                    <Th overflow="hidden" textOverflow="ellipsis" minW={"250px"} maxW={"250px"}>
                                        Credits Required
                                    </Th>
                                    <Th overflow="hidden" textOverflow="ellipsis" minW={"120px"} maxW={"120px"}>
                                        State
                                    </Th>
                                    <Th width={"25%"}>See Details</Th>
                                </Tr>
                            </Thead>
                            <Tbody>
                                {courseGroupings.slice(0, courseGroupings.length).map((grouping) => (
                                    <Tr key={grouping.id} display={"flex"}>
                                        <Td overflow="hidden" textOverflow="ellipsis" minW={"450px"} maxW={"450px"}>
                                            {grouping.name}
                                        </Td>
                                        <Td minW={"255px"} maxW={"255px"}>
                                            <Text overflow="hidden" textOverflow="ellipsis" maxW={"250px"}>
                                                {grouping.requiredCredits}
                                            </Text>
                                        </Td>
                                        <Td overflow="hidden" textOverflow="ellipsis" minW={"130px"} maxW={"130px"}>
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
