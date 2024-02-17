import {
    Flex,
    Center,
    Text,
    Heading,
    FormControl,
    FormLabel,
    Stack,
    Box,
    Textarea,
    useToast,
    FormErrorMessage,
    Input,
    TableContainer,
    Table,
    Thead,
    Tr,
    Td,
    Tooltip,
    IconButton,
    Tbody,
    Th,
} from "@chakra-ui/react";
import { useState, useEffect } from "react";
import { getAllCourseSettings } from "../services/course";
import { AllCourseSettings } from "../models/course";
import { showToast } from "./../utils/toastUtils";
import Button from "../components/Button";
import { useNavigate, useParams } from "react-router-dom";
import { BaseRoutes } from "../constants";
import {
    CourseGroupingDTO,
    CourseGroupingModificationRequestDTO,
    CourseGroupingRequestDTO,
    MultiCourseGroupingDTO,
} from "../models/courseGrouping";
import { GetCourseGroupingByName, InitiateCourseGroupingDeletion } from "../services/courseGrouping";
import { InfoIcon } from "@chakra-ui/icons";

export default function DeleteCourse() {
    const { dossierId } = useParams();
    const navigate = useNavigate();
    const toast = useToast();

    const [isLoading, toggleLoading] = useState(false);
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [courseGroupingError, setCourseGroupingError] = useState(true);
    const [rationaleError, setRationaleError] = useState(true);
    const [resourceImplicationError, setResourceImplicationError] = useState(true);

    const [allCourseSettings, setAllCourseSettings] = useState<AllCourseSettings>(null);
    const [courseGroupings, setCourseGroupings] = useState<CourseGroupingDTO[]>([]);
    const [selectedCourseGrouping, setSelectedCourseGrouping] = useState<CourseGroupingDTO>(null);

    const [searchInput, setSearchInput] = useState<string>("");
    const [rationale, setRationale] = useState("");
    const [resourceImplication, setResourceImplication] = useState("");
    const [comment, setComment] = useState("");

    const handleChangeRationale = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        if (e.currentTarget.value.length === 0) setRationaleError(true);
        else setRationaleError(false);
        setRationale(e.currentTarget.value);
    };
    const handleChangeResourceImplication = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        if (e.currentTarget.value.length === 0) setResourceImplicationError(true);
        else setResourceImplicationError(false);
        setResourceImplication(e.currentTarget.value);
    };
    const handleChangeComment = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setComment(e.currentTarget.value);
    };
    const handleSubmitRequest = () => {
        setFormSubmitted(true);
        if (courseGroupingError || rationaleError || resourceImplicationError) return;
        else {
            const subGroupingReferences = selectedCourseGrouping?.subGroupingReferences?.map(
                // eslint-disable-next-line @typescript-eslint/no-unused-vars
                ({ id, parentGroupId, ...rest }) => rest
            );
            const courseGroupingRequestDTO: CourseGroupingRequestDTO = {
                name: selectedCourseGrouping.name,
                requiredCredits: selectedCourseGrouping.requiredCredits,
                isTopLevel: selectedCourseGrouping.isTopLevel,
                school: selectedCourseGrouping.school,
                description: selectedCourseGrouping.description,
                notes: selectedCourseGrouping.notes,
                subGroupingReferences: subGroupingReferences ? subGroupingReferences : [],
                courseIdentifiers: selectedCourseGrouping.courseIdentifiers,
                commonIdentifier: selectedCourseGrouping.commonIdentifier,
            };

            const courseGroupingDeletionRequest: CourseGroupingModificationRequestDTO = {
                dossierId: dossierId,
                rationale: rationale,
                resourceImplication: resourceImplication,
                comment: comment,
                courseGrouping: courseGroupingRequestDTO,
            };
            InitiateCourseGroupingDeletion(dossierId, courseGroupingDeletionRequest)
                .then(() => {
                    showToast(toast, "Success!", "Course grouping deletion request successfully created.", "success");
                    toggleLoading(false);
                    navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId));
                })
                .catch((err) => {
                    showToast(
                        toast,
                        "Error!",
                        err.response ? err.response.data : "One or more validation errors occurred",
                        "error"
                    );
                    toggleLoading(false);
                });
        }
    };

    const searchByName = (name: string) => {
        GetCourseGroupingByName(name)
            .then((res: MultiCourseGroupingDTO) => {
                setCourseGroupings(res.data);
            })
            .catch((err) => {
                if (err.response.status == 400) {
                    showToast(
                        toast,
                        "Error!",
                        err.response ? "You cannot search an empty string." : "One or more validation errors occurred",
                        "error"
                    );
                }
            });
    };

    const handleRowClick = (courseGrouping: CourseGroupingDTO) => {
        setSelectedCourseGrouping((prevSelectedCourseGrouping: { id: string }) => {
            if (prevSelectedCourseGrouping && prevSelectedCourseGrouping.id === courseGrouping.id) {
                setCourseGroupingError(true);
                return null;
            } else {
                setCourseGroupingError(false);
                return courseGrouping;
            }
        });
    };

    useEffect(() => {
        getAllCourseSettings()
            .then((res) => {
                setAllCourseSettings(res.data);
            })
            .catch((err) => {
                showToast(toast, "Error!", err.message, "error");
            });
    }, []);

    return (
        <>
            {allCourseSettings && (
                <Box>
                    <form>
                        <Flex>
                            <Stack w="60%" p={8} m="auto">
                                <Button
                                    style="primary"
                                    variant="outline"
                                    width="100px"
                                    height="40px"
                                    onClick={() => navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId))}
                                >
                                    Back
                                </Button>
                                <Stack>
                                    <Center>
                                        <Heading as="h1" size="2xl" color="brandRed">
                                            Delete Course Grouping
                                        </Heading>
                                    </Center>
                                </Stack>
                                <Stack>
                                    <FormControl isInvalid={courseGroupingError && formSubmitted}>
                                        <FormLabel htmlFor="search-text">Search Course Grouping By Title:</FormLabel>
                                        <Input
                                            id="searcher"
                                            type="text"
                                            value={searchInput}
                                            onChange={(e) => setSearchInput(e.target.value)}
                                        />
                                        <FormErrorMessage>You must select a course grouping.</FormErrorMessage>
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
                                </Stack>
                                <Stack mb={10}>
                                    <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                                        <Table
                                            variant="simple"
                                            style={{ backgroundColor: "white", tableLayout: "auto" }}
                                        >
                                            <Thead backgroundColor={"#e2e8f0"}>
                                                <Tr display={"flex"}>
                                                    <Th width={"65%"}>Name</Th>
                                                    <Th width={"15%"}>Credits Required</Th>
                                                    <Th width={"15%"}>State</Th>
                                                    <Th width={"15%"}>See Details</Th>
                                                </Tr>
                                            </Thead>
                                            <Tbody>
                                                {courseGroupings.slice(0, courseGroupings.length).map((grouping) => (
                                                    <Tr
                                                        key={grouping.id}
                                                        display={"flex"}
                                                        onClick={() => handleRowClick(grouping)}
                                                        cursor="pointer"
                                                        background={
                                                            selectedCourseGrouping &&
                                                            selectedCourseGrouping.id === grouping.id
                                                                ? "brandBlue600"
                                                                : "null"
                                                        }
                                                    >
                                                        <Td width={"65%"} style={{ whiteSpace: "normal" }}>
                                                            {grouping.name}
                                                        </Td>
                                                        <Td width={"15%"}>
                                                            <Text
                                                                overflow="hidden"
                                                                textOverflow="ellipsis"
                                                                maxW={"500px"}
                                                            >
                                                                {grouping.requiredCredits}
                                                            </Text>
                                                        </Td>
                                                        <Td width={"15%"}>{grouping.school}</Td>

                                                        <Td width={"15%"}>
                                                            <Tooltip label="Curriculum Details">
                                                                <IconButton
                                                                    ml={2}
                                                                    aria-label="Details"
                                                                    icon={<InfoIcon />}
                                                                    onClick={() => {
                                                                        // handleNavigateToCurriculumDetails(grouping.id);
                                                                    }}
                                                                />
                                                            </Tooltip>
                                                        </Td>
                                                    </Tr>
                                                ))}
                                            </Tbody>
                                        </Table>
                                    </TableContainer>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            <Text align="center">Rationale</Text>
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <FormControl isInvalid={rationaleError && formSubmitted}>
                                            <Textarea
                                                value={rationale}
                                                onChange={handleChangeRationale}
                                                placeholder="Explain reasoning for this course grouping deletion."
                                                minH={"200px"}
                                            ></Textarea>
                                            <FormErrorMessage>Rationale is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            <Text align="center">Resource Implication</Text>
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <FormControl isInvalid={resourceImplicationError && formSubmitted}>
                                            <Textarea
                                                value={resourceImplication}
                                                onChange={handleChangeResourceImplication}
                                                placeholder="Explain any resource implications for the course grouping deletion."
                                                minH={"200px"}
                                            ></Textarea>
                                            <FormErrorMessage>Resource Implication is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            <Text align="center">Comment</Text>
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <FormControl>
                                            <Textarea
                                                value={comment}
                                                onChange={handleChangeComment}
                                                placeholder="Add any additional comments."
                                                minH={"200px"}
                                            ></Textarea>
                                        </FormControl>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Button
                                        style="primary"
                                        width="auto"
                                        height="50px"
                                        variant="solid"
                                        marginTop="16px"
                                        onClick={() => handleSubmitRequest()}
                                        isLoading={isLoading}
                                    >
                                        Submit
                                    </Button>
                                </Stack>
                            </Stack>
                        </Flex>
                    </form>
                </Box>
            )}
        </>
    );
}
