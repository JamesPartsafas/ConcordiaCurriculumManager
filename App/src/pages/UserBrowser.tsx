import { useEffect, useState } from "react";
import { UserDTO } from "../models/user";
import { useNavigate } from "react-router-dom";
import { AllUsersResponseDTO, getAllUsers, searchUsersByEmail } from "../services/user";
import {
    Box,
    Container,
    Heading,
    Stack,
    useToast,
    FormControl,
    FormLabel,
    Input,
    OrderedList,
    HStack,
    ListItem,
} from "@chakra-ui/react";
import Button from "../components/Button";
import { BaseRoutes } from "../constants";
import logo from "../assets/logo.png";

export default function UserBrowser() {
    const [users, setUsers] = useState<UserDTO[]>([]);
    const [searchInput, setSearchInput] = useState<string>();
    const navigate = useNavigate();
    const handleChange = (event) => setSearchInput(event.target.value);
    const toast = useToast(); // Initialize useToast hook

    function getUsers() {
        console.log("Grabbing User info");
        getAllUsers()
            .then((res: AllUsersResponseDTO) => {
                setUsers(res.data);
            })
            .catch((err) => {
                console.log(err);
                toast({
                    title: "Error",
                    description: "Failed to retrieve user information.",
                    status: "error",
                    duration: 5000,
                    position: "top-right",
                    isClosable: true,
                });
            });
    }

    function searchUsers(input: string) {
        console.log("Searching for " + input);
        searchUsersByEmail(input)
            .then((res: AllUsersResponseDTO) => {
                console.log(JSON.stringify(res.data));
                setUsers(res.data);
            })
            .catch((err) => {
                console.log(err);
                toast({
                    title: "Error",
                    description: "Failed to search for users.",
                    status: "error",
                    duration: 5000,
                    position: "top-right",
                    isClosable: true,
                });
            });
    }

    useEffect(() => {
        getUsers();
    }, []);

    return (
        <>
            <Button style="primary" variant="outline" height="40px" ml={"10%"} mt={5} onClick={() => navigate(-1)}>
                Back
            </Button>
            <Container maxW="lg" py={{ base: "12", md: "15" }} px={{ base: "0", sm: "8" }}>
                <Stack spacing="8">
                    <Box
                        py={{ base: "0", sm: "8" }}
                        px={{ base: "4", sm: "10" }}
                        bg={{ base: "transparent", sm: "bg.surface" }}
                        boxShadow={{ base: "none", sm: "2xl" }}
                        borderRadius={{ base: "none", sm: "xl" }}
                    >
                        <Stack>
                            <img src={logo} alt="Logo" width="50px" height="50px" style={{ margin: "auto" }} />
                            <Heading textAlign="center" size="lg" mt={3}>
                                View User Profile
                            </Heading>

                            <FormControl>
                                <FormLabel htmlFor="search-text">Search:</FormLabel>
                                <Input id="searcher" type="text" value={searchInput} onChange={handleChange} />
                                <Button
                                    mt={4}
                                    mb={2}
                                    style="secondary"
                                    variant="outline"
                                    width="22%"
                                    height="40px"
                                    onClick={() => searchUsers(searchInput)}
                                >
                                    Search
                                </Button>
                            </FormControl>

                            <div id="item-list">
                                <OrderedList>
                                    {users.map((item, index) => (
                                        <HStack justify="space-between" key={index}>
                                            <ListItem>{item.email}</ListItem>

                                            <Button
                                                m={1}
                                                style="primary"
                                                variant="outline"
                                                width="18%"
                                                height="40px"
                                                onClick={() => navigate(BaseRoutes.profile + "?email=" + item.email)}
                                            >
                                                View
                                            </Button>
                                        </HStack>
                                    ))}
                                </OrderedList>
                            </div>
                        </Stack>
                    </Box>
                </Stack>
            </Container>
        </>
    );
}
