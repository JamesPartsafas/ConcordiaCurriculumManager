import React from "react";
import {
    Box,
    Button,
    Container,
    FormControl,
    FormLabel,
    Heading,
    HStack,
    Input,
    Stack,
    Select,
} from "@chakra-ui/react";
import logo from "../assets/logo.png";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import axios from "axios"; // Import Axios

export default function CreateGroup() {
    const { register, handleSubmit } = useForm();
    const navigate = useNavigate();

    const onSubmit = (data) => {
        // Make an HTTP POST request to the server to create a new group
        axios
            .post("/api/group", {  // Assuming your route is "/api/group" for creating a group
                name: data.groupName,  // Make sure this matches your GroupCreateDTO
                // Add other data here, e.g., groupType, faculty, department, selectedMembers
                // Replace "data.groupName" with the actual form field names
            })
            .then((response) => {
                // Handle a successful response from the server
                console.log("Group created:", response.data);
                navigate("/manageablegroup");
            })
            .catch((error) => {
                // Handle errors if the request fails
                console.error("Error creating group:", error);
            });
    };

    return (
        <>
            <form onSubmit={handleSubmit(onSubmit)}>
                <Container maxW="lg" py={{ base: "12", md: "24" }} px={{ base: "0", sm: "8" }}>
                    <Stack spacing="8">
                        <Stack spacing="6">
                            <img
                                src={logo}
                                alt="Logo"
                                width="50px"
                                height="50px"
                                style={{ margin: "auto" }}
                            />
                            <Heading textAlign="center" size="lg">
                                Create New Group
                            </Heading>
                        </Stack>
                        <Box
                            py={{ base: "0", sm: "8" }}
                            px={{ base: "4", sm: "10" }}
                            bg={{ base: "transparent", sm: "bg.surface" }}
                            boxShadow={{ base: "none", sm: "2xl" }}
                            borderRadius={{ base: "none", sm: "xl" }}
                        >
                            <Stack spacing="6">
                                <FormControl>
                                    <FormLabel htmlFor="groupType">Group Type</FormLabel>
                                    <Select
                                        id="groupType"
                                        {...register("groupType", {
                                            required: true,
                                        })}
                                    >
                                        <option value="option1">Group 1</option>
                                        <option value="option2">Group 2</option>
                                        <option value="option3">Group 3</option>
                                    </Select>
                                </FormControl>
                                <FormControl>
                                    <FormLabel htmlFor="groupName">Group Name</FormLabel>
                                    <Input
                                        id="groupName"
                                        type="text"
                                        {...register("groupName", {
                                            required: true,
                                        })}
                                    />
                                </FormControl>
                                <FormControl>
                                    <FormLabel htmlFor="faculty">Faculty/School</FormLabel>
                                    <Select
                                        id="faculty"
                                        {...register("faculty", {
                                            required: true,
                                        })}
                                    >
                                        <option value="faculty1">Faculty 1</option>
                                        <option value="faculty2">Faculty 2</option>
                                        <option value="faculty3">Faculty 3</option>
                                    </Select>
                                </FormControl>
                                <FormControl>
                                    <FormLabel htmlFor="department">Department</FormLabel>
                                    <Input
                                        id="department"
                                        type="text"
                                        {...register("department", {
                                            required: true,
                                        })}
                                    />
                                </FormControl>
                                <FormControl>
                                    <FormLabel htmlFor="selectedMembers">
                                        Select Members to Add
                                    </FormLabel>
                                    <Select
                                        id="selectedMembers"
                                        multiple
                                        {...register("selectedMembers", {
                                            required: true,
                                        })}
                                    >
                                        <option value="user1">User 1</option>
                                        <option value="user2">User 2</option>
                                        <option value="user3">User 3</option>
                                    </Select>
                                </FormControl>
                            </Stack>
                        </Box>
                        <HStack justify="space-between">
                            <Button
                                backgroundColor="#932439"
                                color="white"
                                _hover={{ bg: "#7A1D2E" }}
                                type="submit"
                            >
                                Create Group
                            </Button>
                        </HStack>
                    </Stack>
                </Container>
            </form>
        </>
    );
}
