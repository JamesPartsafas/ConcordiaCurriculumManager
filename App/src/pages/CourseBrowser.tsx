import React, { useState } from "react";
import Select from "react-select";
import { Box, Button, Container, FormLabel, Heading, Stack } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import { Input } from "@chakra-ui/react";
interface SubjectItem {
    value: string;
    label: string;
}

// when the api is implemented I will make the subjects available in the drop down
const SUBJECT_ITEMS: Array<SubjectItem> = [
    { value: "COMP", label: "COMP" },
    { value: "SOEN", label: "SOEN" },
    { value: "ENGR", label: "ENGR" },
];

// interface CatalogItem {
//     value: string;
//     label: string;
// }

const CourseSelector1: React.FC<{ onChange: (selectedOption: SubjectItem | null) => void }> = ({ onChange }) => (
    <Select options={SUBJECT_ITEMS} onChange={onChange} />
);

function App() {
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

                            <CourseSelector1 onChange={handleSubjectChange} />

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
                                    navigate(`/CourseDetails?subject=${selectedSubject?.value}&catalog=${catalog}`)
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
    );
}

export default App;
