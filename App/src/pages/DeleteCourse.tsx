import Header from "../shared/Header";
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
import { AllCourseSettings, CourseCareer } from "../models/course";
import AutocompleteInput from "../components/Select";
import { showToast } from "./../utils/toastUtils";
import Button from "../components/Button";

export default function DeleteCourse() {
    const toast = useToast();

    const [isLoading, toggleLoading] = useState(false);
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [courseSubjectError, setCourseSubjectError] = useState(true);
    const [courseCatalogError, setCourseCatalogError] = useState(true);
    const [courseCareersError, setCourseCareersError] = useState(true);
    const [rationalError, setRationalError] = useState(true);

    const [allCourseSettings, setAllCourseSettings] = useState<AllCourseSettings>(null);
    const [subject, setSubject] = useState("");
    const [catalog, setCatalog] = useState("");
    const [rationale, setRationale] = useState("");
    const [courseCareer, setCouresCareer] = useState<CourseCareer>(null);
    const [courseCareers, setCourseCareers] = useState<string[]>([]);

    const handleChangeCourseCareer = (value: string) => {
        if (value.length === 0) setCourseCareersError(true);
        else setCourseCareersError(false);
        const career = allCourseSettings?.courseCareers.find((career) => career.careerName === value);
        setCouresCareer(career);
    };
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
    const handleRationale = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        if (e.currentTarget.value.length === 0) setRationalError(true);
        else setRationalError(false);
        setRationale(e.currentTarget.value);
    };
    const handleSubmitRequest = () => {
        setFormSubmitted(true);
        if (courseCatalogError || courseSubjectError || rationalError || courseCareersError) return;
        else {
            toggleLoading(true);
            const courseDeletionRequest = {
                dossierId: "09a073e7-3f84-4ac8-bc5d-f3b405e25987", // To be changed when dossier details page is done
                rationale: "string",
                resourceImplication: "string",
                subject: subject,
                catalog: catalog,
            };
            deleteCourse(courseDeletionRequest)
                .then(() => {
                    showToast(toast, "Success!", "Course deletion request successfully added.", "success");
                    toggleLoading(false);
                })
                .catch(() => {
                    showToast(toast, "Error!", "One or more validation errors occurred", "error");
                    toggleLoading(false);
                });
        }
    };
    useEffect(() => {
        getAllCourseSettings()
            .then((res) => {
                setAllCourseSettings(res.data);
                const courseCareersTemp = res.data.courseCareers.map((career) => career.careerName);
                setCourseCareers(courseCareersTemp);
            })
            .catch((err) => {
                showToast(toast, "Error!", err.message, "error");
            });
    }, []);

    return (
        <>
            {allCourseSettings && (
                <Box>
                    <Header></Header>
                    <form>
                        <Flex>
                            <Stack w="60%" p={8} m="auto">
                                <Stack>
                                    <Center>
                                        <Heading as="h1" size="2xl" color="brandRed">
                                            Delete Course
                                        </Heading>
                                    </Center>
                                </Stack>
                                <Stack>
                                    <Stack>
                                        <FormControl isInvalid={courseCareersError && formSubmitted}>
                                            <FormLabel m={0}>Course Career</FormLabel>
                                            <AutocompleteInput
                                                options={courseCareers}
                                                onSelect={handleChangeCourseCareer}
                                                width="100%"
                                                placeholder={courseCareer ? courseCareer.careerName : "Select Career"}
                                            />
                                            <FormErrorMessage>Course Career is required</FormErrorMessage>
                                        </FormControl>
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
                                        <FormControl isInvalid={rationalError && formSubmitted}>
                                            <Textarea
                                                value={rationale}
                                                onChange={handleRationale}
                                                placeholder="Explain reasoning for this course deletion."
                                                minH={"200px"}
                                            ></Textarea>
                                            <FormErrorMessage>Rationale is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Button
                                        style="primary"
                                        width="auto"
                                        height="50px"
                                        variant="solid"
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
