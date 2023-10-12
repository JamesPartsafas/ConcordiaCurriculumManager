import React from "react";
import Select from "react-select";
import {
    Box,
    Button,
    Container,
    FormLabel,
    Heading,
    Stack,
} from "@chakra-ui/react";


const options1 = [
    { value: "COMP", label: "COMP" },
    { value: "SOEN", label: "SOEN" },
    { value: "ENGR", label: "ENGR" },
];
const options2 = [
    { value: "201", label: "201" },
    { value: "301", label: "301" },
    { value: "401", label: "401" },
];

const CourseSelector1 = () => <Select options={options1} />;
const CourseSelector2 = () => <Select options={options2} />;


function App() {
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
                    ><Stack spacing="5">
                            <FormLabel htmlFor="subject">Subject</FormLabel>

                            < CourseSelector1 />

                            <FormLabel htmlFor="catalog">Catalog N°</FormLabel>

                            < CourseSelector2 />
                        </Stack>
                        <div>
                            <Box h={6} />
                        </div>

                        <Stack spacing="6">
                            <Button
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
