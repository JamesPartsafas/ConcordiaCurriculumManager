import {
    Flex,
    Center,
    Text,
    Heading,
    FormControl,
    Stack,
    Box,
    Textarea,
    useToast,
    FormErrorMessage,
} from "@chakra-ui/react";
import { useState, useEffect } from "react";
import { getAllCourseSettings } from "../../services/course";
import { AllCourseSettings } from "../../models/course";
import { showToast } from "../../utils/toastUtils";
import Button from "../../components/Button";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { BaseRoutes } from "../../constants";
import {
    CourseGroupingDTO,
    CourseGroupingModificationRequestDTO,
    CourseGroupingModificationInputDTO,
} from "../../models/courseGrouping";
import { EditCourseGroupingDeletion } from "../../services/courseGrouping";

export default function DeleteCourse() {
    const { dossierId } = useParams();
    const navigate = useNavigate();
    const toast = useToast();
    const location = useLocation();

    const [isLoading, toggleLoading] = useState(false);
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [rationaleError, setRationaleError] = useState(false);
    const [resourceImplicationError, setResourceImplicationError] = useState(false);

    const [allCourseSettings, setAllCourseSettings] = useState<AllCourseSettings>(null);
    const [selectedCourseGrouping, setSelectedCourseGrouping] = useState<CourseGroupingDTO>(null);

    const [rationale, setRationale] = useState("");
    const [resourceImplication, setResourceImplication] = useState("");
    const [comment, setComment] = useState("");
    const [requestId, setRequestId] = useState("");

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
        if (rationale.trim().length === 0) {
            setRationaleError(true);
            return;
        } else if (resourceImplication.trim().length === 0) {
            setResourceImplicationError(true);
            return;
        } else {
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

            const courseGroupingDeletionRequest: CourseGroupingModificationRequestDTO = {
                dossierId: dossierId,
                rationale: rationale,
                resourceImplication: resourceImplication,
                comment: comment,
                courseGrouping: courseGroupingRequestDTO,
            };
            EditCourseGroupingDeletion(dossierId, requestId, courseGroupingDeletionRequest)
                .then(() => {
                    showToast(toast, "Success!", "Course grouping deletion request successfully edited.", "success");
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

    useEffect(() => {
        setRationale(location.state.key.rationale);
        setResourceImplication(location.state.key.resourceImplication);
        setComment(location.state.key.comment);
        setSelectedCourseGrouping(location.state.key.courseGrouping);
        setRequestId(location.state.key.id);
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
                                <Stack mt={10} mb={10}>
                                    <Center>
                                        <Heading as="h1" size="2xl" color="brandRed">
                                            Edit Course Grouping Deletion Request
                                        </Heading>
                                    </Center>
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
