import {
    Stack,
    FormControl,
    Input,
    FormErrorMessage,
    FormLabel,
    TableContainer,
    Tbody,
    Thead,
    Table,
    Tr,
    Th,
    Td,
    useToast,
    Text,
    ModalFooter,
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalCloseButton,
    ModalBody,
} from "@chakra-ui/react";
import Button from "../Button";
import { showToast } from "../../utils/toastUtils";
import { GetCourseGroupingByName } from "../../services/courseGrouping";
import { useState } from "react";
import {
    CourseGroupingDTO,
    CourseGroupingModificationInputDTO,
    MultiCourseGroupingDTO,
} from "../../models/courseGrouping";

export default function SearchCourseGrouping(props: {
    isOpen;
    onClose: () => void;
    onSelectCourseGrouping: (courseGrouping) => void;
}) {
    const toast = useToast();

    const [courseGroupingError, setCourseGroupingError] = useState(true);
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [searchInput, setSearchInput] = useState<string>("");

    const [courseGroupings, setCourseGroupings] = useState<CourseGroupingDTO[]>([]);
    const [selectedCourseGrouping, setSelectedCourseGrouping] = useState<CourseGroupingDTO>(null);

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
    const handleSubmitRequest = () => {
        setFormSubmitted(true);

        const subGroupingReferences = selectedCourseGrouping?.subGroupingReferences?.map(
            // eslint-disable-next-line @typescript-eslint/no-unused-vars
            ({ id, parentGroupId, ...rest }) => rest
        );
        const courseGroupingRequestDTO: CourseGroupingModificationInputDTO = {
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

        console.log(courseGroupingRequestDTO);
        props.onSelectCourseGrouping(courseGroupingRequestDTO);
        handleClose();
    };
    function handleClose() {
        props.onClose();
    }

    return (
        <>
            <Modal isOpen={props.isOpen} onClose={handleClose}>
                <ModalOverlay />
                <ModalContent maxWidth="80%">
                    <ModalHeader>Seach Course Grouping By Name</ModalHeader>
                    <ModalCloseButton />

                    <ModalBody>
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
                                <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                                    <Thead backgroundColor={"#e2e8f0"}>
                                        <Tr display={"flex"}>
                                            <Th width={"65%"}>Name</Th>
                                            <Th width={"20%"}>Credits Required</Th>
                                            <Th width={"15%"}>State</Th>
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
                                                    selectedCourseGrouping && selectedCourseGrouping.id === grouping.id
                                                        ? "brandBlue600"
                                                        : "null"
                                                }
                                            >
                                                <Td width={"65%"} style={{ whiteSpace: "normal" }}>
                                                    {grouping.name}
                                                </Td>
                                                <Td width={"20%"}>
                                                    <Text overflow="hidden" textOverflow="ellipsis" maxW={"500px"}>
                                                        {grouping.requiredCredits}
                                                    </Text>
                                                </Td>
                                                <Td width={"15%"}>{grouping.school}</Td>
                                            </Tr>
                                        ))}
                                    </Tbody>
                                </Table>
                            </TableContainer>
                        </Stack>
                        <Stack>
                            {/* Add Select to chose Grouping type subgrouping or optional grouping */}
                            <Select onChange={(e) => setGroupingType(parseInt(e.target.value))}>
                                <option value="0">Sub-Grouping</option>
                                <option value="1">Optional Grouping</option>
                            </Select>
                            <Button
                                style="primary"
                                width="auto"
                                height="50px"
                                variant="solid"
                                marginTop="16px"
                                onClick={() => handleSubmitRequest()}
                            >
                                Submit
                            </Button>
                        </Stack>
                    </ModalBody>
                    <ModalFooter></ModalFooter>
                </ModalContent>
            </Modal>
        </>
    );
}
