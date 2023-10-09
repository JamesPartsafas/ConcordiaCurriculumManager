import React, { useState } from "react";
import { Box, Flex, Heading, Link, Text, Divider, useToast } from "@chakra-ui/react";
import Button from "../components/Button";
import AutocompleteInput from "../components/Select";
import { showToast } from "./../utils/toastUtils"; // Import the utility function
import { useLoading } from "./../utils/loadingContext"; // Import the useLoading hook

function ComponentsList() {
    const brandColors = {
        brandRed: "#912338",
        brandRed600: "#9a4a59",
        brandRed100: "#91233847",
        brandBlue: "#0072A8",
        brandBlue600: "#1987a7",
        brandBlue100: "#e6f4f8",
        brandGray: "#353535",
        brandGray500: "#6E6E6E",
        brandGray200: "#BCBCBC",
    };
    const toast = useToast(); // Use the useToast hook

    const { isLoading, toggleLoading } = useLoading(); // Use the useLoading hook
    const handleLoadingButtonClick = () => {
        toggleLoading(); // Call the toggleLoading function to update isLoading
        setTimeout(() => {
            toggleLoading(); // Just for testing purposes, stop loading after 3 seconds
        }, 3000);
    };
    const courses = ["Math", "Science", "History", "English"];
    const [selectedCourse, setSelectedCourse] = useState("");

    const handleCourseSelect = (value) => {
        setSelectedCourse(value);
    };

    return (
        <Box p={4}>
            <Box>
                <Heading mb={4}>Brand Colors:</Heading>
                <Flex flexWrap="wrap" justifyContent="space-between" width="70%" m="auto">
                    {Object.entries(brandColors).map(([colorName, colorCode]) => (
                        <Box
                            key={colorName}
                            width="calc(33.33% - 8px)" // Set 33.33% width with margin
                            marginRight="4px"
                            marginBottom="8px"
                        >
                            <Box
                                width="100%"
                                backgroundColor={colorCode}
                                borderRadius="md"
                                boxShadow="md"
                                textAlign="center"
                                color="white"
                                padding={2}
                            >
                                <Text fontSize="md" fontWeight="bold">
                                    {colorName}
                                </Text>
                                <Text fontSize="sm">{colorCode}</Text>
                            </Box>
                        </Box>
                    ))}
                </Flex>
            </Box>
            <Divider mt={6} mb={6} />
            <Heading mb={4}>Button Showcase: {"<Button>"}</Heading>
            <Text mb="2">
                <strong>Button Props:</strong> type (primary, secondary), variant (solid, outline),
                width (string), height (string). And can pass other Chakra props as well.
            </Text>
            <Flex alignItems="center" mb={4} width="50%">
                <Button type="primary" variant="solid" width="50%" height="40px" mr="2">
                    Solid Primary Button
                </Button>
                <Button type="primary" variant="outline" width="50%" height="40px">
                    Outline Primary Button
                </Button>
            </Flex>
            <Flex alignItems="center" mb={4} width="50%">
                <Button type="secondary" variant="solid" width="50%" height="40px" mr="2">
                    Solid Secondary Button
                </Button>
                <Button type="secondary" variant="outline" width="50%" height="40px">
                    Outline Secondary Button
                </Button>
            </Flex>
            <Flex alignItems="center" mb={4} width="50%">
                <Button
                    type="primary"
                    variant="solid"
                    width="50%"
                    height="40px"
                    mr="2"
                    isLoading
                    loadingText="Submitting"
                >
                    Solid Secondary Button
                </Button>
                <Button type="secondary" variant="outline" width="50%" height="40px" isLoading>
                    Outline Secondary Button
                </Button>
            </Flex>
            <Divider mt={6} mb={6} />
            <Heading mb={4}>Inputs Showcase</Heading>
            <Text mb="2">
                <strong>For regular inputs:</strong> Use the inputs from chakra UI, they are
                customized them to fit the brand.
            </Text>
            <Link href="https://chakra-ui.com/docs/components/input">Chakra Inputs</Link>
            <br></br>
            <Link href="https://chakra-ui.com/docs/components/input" color="brandBlue">
                Chakra Number Inputs
            </Link>
            <Divider mt={6} mb={6} />
            <Heading mb={4}>Input with autocomplete: {"<AutocompleteInput>"}</Heading>
            <Text mb="2">
                <strong>Select Props:</strong> options (array), onSelect (function), width (string)
            </Text>
            <Box p={4}>
                <Text>Selected Course: {selectedCourse}</Text>
                <AutocompleteInput options={courses} onSelect={handleCourseSelect} width="50%" />
            </Box>
            <Divider mt={6} mb={6} />
            <Heading mb={4}>Toast: {"useToast()"}</Heading>
            <Text mb="2">
                <strong>Toast Props:</strong> toast (useToast), title (string), description
                (string), status = (success, error, warning, info), position = (top-right, top,
                top-left, bottom-right, bottom, bottom-left, left, right)
            </Text>
            <Flex>
                <Button
                    onClick={() =>
                        showToast(toast, "Success!", "This is a success toast message.", "success")
                    }
                    type="primary"
                    variant="solid"
                    width="50%"
                    height="40px"
                    mr="2"
                >
                    Success Toast
                </Button>
                <Button
                    onClick={() =>
                        showToast(toast, "Warning!", "This is a warning toast message.", "warning")
                    }
                    type="primary"
                    variant="solid"
                    width="50%"
                    height="40px"
                    mr="2"
                >
                    Warning Toast
                </Button>
                <Button
                    onClick={() =>
                        showToast(toast, "Error!", "This is an error toast message.", "error")
                    }
                    type="primary"
                    variant="solid"
                    width="50%"
                    height="40px"
                    mr="2"
                >
                    Error Toast
                </Button>
            </Flex>
            <Divider mt={6} mb={6} />
            <Heading mb={4}>Loading Spinner: {"useLoading()"}</Heading>
            <Text mb="2">
                <strong>Toast Props:</strong> isLoading (boolean), toggleLoading (function)
            </Text>
            <Button
                type="primary"
                variant="solid"
                width="50%"
                height="40px"
                onClick={handleLoadingButtonClick}
            >
                {isLoading ? "Stop Loading" : "Start Loading"}
            </Button>
            <Divider mt={6} mb={6} />
            <Heading mb={4}>Links: {"<Link>"}</Heading>
            <Text mb="2">
                <strong>Use Chakra Link:</strong> They are customized to fit the branding.
                <br />
                <Link href="https://chakra-ui.com/docs/components/link">
                    Chakra Link Documentation
                </Link>
            </Text>
        </Box>
    );
}

export default ComponentsList;
