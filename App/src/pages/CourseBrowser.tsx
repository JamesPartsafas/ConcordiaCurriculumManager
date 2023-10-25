import React from "react";
import Select from "react-select";
import { Box, Button, Container, FormLabel, Heading, Stack } from "@chakra-ui/react";

interface SubjectItem {
    value: string;
    label: string;
}

const SUBJECT_ITEMS: Array<SubjectItem> = [
    { value: "COMP", label: "COMP" },
    { value: "SOEN", label: "SOEN" },
    { value: "ENGR", label: "ENGR" },
];

interface CatalogItem {
    value: string;
    label: string;
}

const CATALOG_ITEMS: Array<CatalogItem> = [
    { value: "201", label: "201" },
    { value: "301", label: "301" },
    { value: "401", label: "401" },
];

function CourseBrowser() {
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

                            <Select options={SUBJECT_ITEMS} />

                            <FormLabel htmlFor="catalog">Catalog N°</FormLabel>

                            <Select options={CATALOG_ITEMS} />
                        </Stack>
                        <div>
                            <Box h={6} />
                        </div>

                        <Stack spacing="6">
                            <Button backgroundColor="#932439" color="white" hover={{ bg: "#7A1D2E" }} type="submit">
                                Enter
                            </Button>
                        </Stack>
                    </Box>
                </Stack>
            </Container>
        </form>
    );
}
export default CourseBrowser;
