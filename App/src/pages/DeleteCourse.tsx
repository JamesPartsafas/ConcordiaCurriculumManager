import {
    Flex,
    Center,
    Text,
    Heading,
    FormControl,
    FormLabel,
    Stack,
    NumberInput,
    NumberInputField,
    Box,
    Textarea,
    useToast,
    FormErrorMessage,
} from "@chakra-ui/react";
import { useState, useEffect } from "react";
import { deleteCourse, getAllCourseSettings } from "../services/course";
import { AllCourseSettings } from "../models/course";
import AutocompleteInput from "../components/Select";
import { showToast } from "./../utils/toastUtils";
import Button from "../components/Button";
import { useNavigate, useParams } from "react-router-dom";
import { BaseRoutes } from "../constants";

export default function DeleteCourse() {
    const { dossierId } = useParams();
    const navigate = useNavigate();
    const toast = useToast();

    const [isLoading, toggleLoading] = useState(false);
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [courseSubjectError, setCourseSubjectError] = useState(true);
    const [courseCatalogError, setCourseCatalogError] = useState(true);
    const [rationaleError, setRationaleError] = useState(true);
    const [resourceImplicationError, setResourceImplicationError] = useState(true);

    const [allCourseSettings, setAllCourseSettings] = useState<AllCourseSettings>(null);
    const [subject, setSubject] = useState("");
    const [catalog, setCatalog] = useState("");
    const [rationale, setRationale] = useState("");
    const [resourceImplication, setResourceImplication] = useState("");
    const [comment, setComment] = useState("");

    const handleChangeSubject = (value: string) => {
        if (value.length === 0) setCourseSubjectError(true);
        else setCourseSubjectError(false);
        setSubject(value);
    };
    const handleChangeCatalog = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.currentTarget.value.length === 0) setCourseCatalogError(true);
        else setCourseCatalogError(false);
        setCatalog(e.currentTarget.value);
    };
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
        if (courseSubjectError || courseCatalogError || rationaleError || resourceImplicationError) return;
        else {
            toggleLoading(true);
            const courseDeletionRequest = {
                dossierId: dossierId,
                rationale: rationale,
                resourceImplication: resourceImplication,
                subject: subject,
                catalog: catalog,
                comment: comment,
            };
            deleteCourse(courseDeletionRequest)
                .then(() => {
                    showToast(toast, "Success!", "Course deletion request successfully added.", "success");
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
                                            Delete Course
                                        </Heading>
                                    </Center>
                                </Stack>
                                <Stack>
                                    <Stack>
                                        <FormControl isInvalid={courseSubjectError && formSubmitted}>
                                            <FormLabel m={0}>Subject</FormLabel>
                                            <AutocompleteInput
                                                options={allCourseSettings?.courseSubjects}
                                                onSelect={handleChangeSubject}
                                                width="100%"
                                                placeholder="Subject"
                                            />
                                            <FormErrorMessage>Subject is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <FormControl isInvalid={courseCatalogError && formSubmitted}>
                                            <FormLabel m={0}>Course Code</FormLabel>
                                            <NumberInput value={catalog}>
                                                <NumberInputField
                                                    placeholder="Course Code"
                                                    pl="16px"
                                                    onChange={handleChangeCatalog}
                                                />
                                            </NumberInput>
                                            <FormErrorMessage>Course code is required</FormErrorMessage>
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
