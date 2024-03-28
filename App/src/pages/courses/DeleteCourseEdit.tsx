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
} from "@chakra-ui/react";
import { useState, useEffect } from "react";
import { editCourseDeletionRequest } from "../../services/course";
import { CourseDeletionRequest, DeletedCourse } from "../../models/course";
import { showToast } from "../../utils/toastUtils";
import Button from "../../components/Button";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { BaseRoutes } from "../../constants";

export default function DeleteCourseEdit() {
    const { dossierId } = useParams();
    const navigate = useNavigate();
    const location = useLocation();
    const toast = useToast();

    const [isLoading, toggleLoading] = useState(false);
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [rationaleError, setRationaleError] = useState(false);
    const [resourceImplicationError, setResourceImplicationError] = useState(false);
    const [formError, setFormError] = useState(true);

    const emptyCourse: DeletedCourse = {
        subject: "",
        catalog: "",
        title: "",
        description: "",
        creditValue: "",
        preReqs: "",
        career: 0,
        equivalentCourses: "",
        componentCodes: undefined,
        dossierId: "",
        courseNotes: "",
        rationale: "",
        supportingFiles: undefined,
        resourceImplication: "",
        id: "",
        createdDate: undefined,
        modifiedDate: undefined,
        version: 0,
        published: false,
        courseState: 0,
        comment: "",
    };
    const emptyCourseDeletionRequest: CourseDeletionRequest = {
        course: emptyCourse,
        courseId: "",
        id: "",
        dossierId: "",
        rationale: "",
        resourceImplication: "",
        comment: "",
        createdDate: undefined,
        modifiedDate: undefined,
    };
    const [courseDeletionRequest, setCourseDeletionRequest] =
        useState<CourseDeletionRequest>(emptyCourseDeletionRequest);
    const [rationale, setRationale] = useState("");
    const [resourceImplication, setResourceImplication] = useState("");
    const [comment, setComment] = useState("");

    const handleChangeRationale = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        if (e.currentTarget.value.length === 0) setRationaleError(true);
        else setRationaleError(false);
        setRationale(e.currentTarget.value);
        setFormError(false);
    };
    const handleChangeResourceImplication = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        if (e.currentTarget.value.length === 0) setResourceImplicationError(true);
        else setResourceImplicationError(false);
        setResourceImplication(e.currentTarget.value);
        setFormError(false);
    };
    const handleChangeComment = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setComment(e.currentTarget.value);
        setFormError(false);
    };
    const handleSubmitRequest = () => {
        setFormSubmitted(true);
        if (formError) {
            showToast(toast, "Error!", "You cannot submit a change that performs no modifications.", "error");
            return;
        } else if (rationale.trim().length === 0) {
            setRationaleError(true);
            return;
        } else if (resourceImplication.trim().length === 0) {
            setResourceImplicationError(true);
            return;
        } else {
            toggleLoading(true);
            const courseDeletionRequestData = {
                dossierId: dossierId,
                rationale: rationale,
                resourceImplication: resourceImplication,
                comment: comment,
                id: courseDeletionRequest.id,
            };
            editCourseDeletionRequest(dossierId, courseDeletionRequestData)
                .then(() => {
                    showToast(toast, "Success!", "Course deletion request successfully changed.", "success");
                    toggleLoading(false);
                    navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId));
                })
                .catch(() => {
                    showToast(toast, "Error!", "One or more validation errors occurred", "error");
                    toggleLoading(false);
                });
        }
    };
    useEffect(() => {
        setRationale(location.state.key.rationale);
        setResourceImplication(location.state.key.resourceImplication);
        setComment(location.state.key.comment);
        setCourseDeletionRequest(location.state.key);
    }, []);

    return (
        <>
            {
                <Box>
                    <form>
                        <Flex>
                            <Stack w="60%" p={8} m="auto">
                                <Button
                                    style="primary"
                                    variant="outline"
                                    width="10%"
                                    height="40px"
                                    onClick={() => navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId))}
                                >
                                    Back
                                </Button>
                                <Stack>
                                    <Center>
                                        <Heading as="h1" size="2xl" color="brandRed">
                                            Edit Course Deletion Request
                                        </Heading>
                                    </Center>
                                </Stack>
                                <Stack>
                                    <Stack>
                                        <FormControl>
                                            <FormLabel m={0}>Subject</FormLabel>
                                            <Input
                                                value={courseDeletionRequest?.course?.subject}
                                                width="100%"
                                                readOnly
                                            />
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <FormControl>
                                            <FormLabel m={0}>Course Code</FormLabel>
                                            <Input
                                                value={courseDeletionRequest?.course?.catalog}
                                                width="100%"
                                                readOnly
                                            />
                                        </FormControl>
                                    </Stack>
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
                                                placeholder="Explain reasoning for this course deletion."
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
                                                placeholder="Explain any resource implications for the course deletion."
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
                                                //defaultValue={courseDeletionRequest?.comment}
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
            }
        </>
    );
}
