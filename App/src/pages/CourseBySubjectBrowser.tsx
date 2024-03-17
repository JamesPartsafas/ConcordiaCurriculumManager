import { useEffect, useState } from "react";
import { getAllCourseSettings } from "../services/course";
import { AllCourseSettings } from "../models/course";
import { useNavigate } from "react-router-dom";
import { Box, Container, FormLabel, Heading, Stack } from "@chakra-ui/react";
import Button from "../components/Button";
import Select from "../components/Select";
interface SubjectItem {
    value: string;
}

export default function CourseBySubject() {
    const [courseSettings, setCourseSettings] = useState<AllCourseSettings>(null);

    useEffect(() => {
        requestCourseSettings();
    }, []);

    function requestCourseSettings() {
        getAllCourseSettings().then((res) => {
            setCourseSettings(res.data);
        });
        console.log(courseSettings);
    }

    const navigate = useNavigate();
    // State variables to store selected values
    const [selectedSubject, setSelectedSubject] = useState<SubjectItem | null>(null);

    // Function to handle subject selection
    const handleSubjectChange = (selectedOption: SubjectItem | null) => {
        setSelectedSubject(selectedOption);
    };

    return (
        <>
            {courseSettings && (
                <form>
                    <Container maxW="lg" py={{ base: "12", md: "24" }} px={{ base: "0", sm: "8" }}>
                        <Stack spacing="8">
                            <Stack spacing="6">
                                <Heading textAlign="center" size="lg">
                                    Course By Subject Browser
                                </Heading>
                            </Stack>

                            <Box
                                py={{ base: "0", sm: "8" }}
                                px={{ base: "4", sm: "10" }}
                                bg={{ base: "transparent", sm: "bg.surface" }}
                                boxShadow={{ base: "none", sm: "2xl" }}
                                borderRadius={{ base: "none", sm: "xl" }}
                            >
                                <Stack spacing="5">
                                    <FormLabel htmlFor="subject">Subject</FormLabel>

                                    <Select options={courseSettings?.courseSubjects} onSelect={handleSubjectChange} />
                                </Stack>
                                <div>
                                    <Box h={6} />
                                </div>

                                <Stack spacing="6">
                                    <Button
                                        isDisabled={selectedSubject ? false : true}
                                        onClick={() => navigate(`/CoursesFromSubject?subject=${selectedSubject}`)}
                                        backgroundColor="#932439"
                                        color="white"
                                        _hover={{ bg: "#7A1D2E" }}
                                        type="submit"
                                    >
                                        Enter
                                    </Button>
                                    <Button
                                        style="primary"
                                        variant="outline"
                                        height="40px"
                                        width="fit-content"
                                        onClick={() => navigate(-1)}
                                    >
                                        Return
                                    </Button>
                                </Stack>
                            </Box>
                        </Stack>
                    </Container>
                </form>
            )}
        </>
    );
}
