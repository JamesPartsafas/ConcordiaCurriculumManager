import React, { useEffect, useState } from "react";
import Select from "../components/Select";
import { Box, Button, Container, FormLabel, Heading, Stack } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import { Input } from "@chakra-ui/react";
import { AllCourseSettings } from "../models/course";
import { getAllCourseSettings } from "../services/course";
interface SubjectItem {
    value: string;
}

export default function CourseBrowser() {
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
    const [catalog, setCatalog] = useState<string>("");

    // Function to handle subject selection
    const handleSubjectChange = (selectedOption: SubjectItem | null) => {
        setSelectedSubject(selectedOption);
    };

    // Function to handle catalog selection
    return (
        <>
            {courseSettings && (
                <form>
                    <Container maxW="lg" py={{ base: "12", md: "24" }} px={{ base: "0", sm: "8" }}>
                        <Stack spacing="8">
                            <Stack spacing="6">
                                <Heading textAlign="center" size="lg">
                                    Course Browser
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

                                    <FormLabel htmlFor="catalog">Catalog N°</FormLabel>

                                    <Input
                                        onChange={(e) => setCatalog(e.target.value)}
                                        htmlSize={4}
                                        width="auto"
                                        value={catalog}
                                    />
                                </Stack>
                                <div>
                                    <Box h={6} />
                                </div>

                                <Stack spacing="6">
                                    <Button
                                        isDisabled={selectedSubject ? false : true}
                                        onClick={() =>
                                            navigate(`/CourseDetails?subject=${selectedSubject}&catalog=${catalog}`)
                                        }
                                        backgroundColor="#932439"
                                        color="white"
                                        _hover={{ bg: "#7A1D2E" }}
                                        type="submit"
                                    >
                                        Enter
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
