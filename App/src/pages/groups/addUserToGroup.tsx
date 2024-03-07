import { useEffect, useState } from "react";
import logo from "../../assets/logo.png";
import {
    Container,
    Stack,
    Heading,
    Box,
    FormControl,
    Input,
    FormLabel,
    HStack,
    Text,
    ListItem,
    OrderedList,
    useToast,
} from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import Button from "../../components/Button";
import { AddUserToGroup, GetGroupByID, GroupDTO, GroupResponseDTO } from "../../services/group";
import { useLocation } from "react-router-dom";
import { BaseRoutes } from "../../constants";
import { UserDTO, UserRoleCodes } from "../../models/user";
import { AllUsersResponseDTO, searchUsersByEmail } from "../../services/user";
import { getAllUsers } from "../../services/user";

export default function AddingUserToGroup() {
    const [users, setUsers] = useState<UserDTO[]>([]);
    const [nonMembers, setNonMembers] = useState<UserDTO[]>([]);
    const [searchInput, setSearchInput] = useState<string>();
    const navigate = useNavigate();
    const location = useLocation();
    const [myGroup, setMyGroup] = useState<GroupDTO | null>(null);
    const [locationState, setLocationState] = useState({ gid: "", name: "" });
    const handleChange = (event) => setSearchInput(event.target.value);
    const toast = useToast(); // Initialize useToast hook

    function getMyGroup(gid: string) {
        console.log("Grabbing group info");
        console.log(gid);
        GetGroupByID(gid)
            .then((res: GroupResponseDTO) => {
                setMyGroup(res.data);
            })
            .catch((err) => {
                console.log(err);
                toast({
                    title: "Error",
                    description: "Failed to retrieve group information.",
                    status: "error",
                    duration: 5000,
                    position: "top-right",
                    isClosable: true,
                });
            });
    }

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
        if (location.state) {
            const _state = location.state as { gid: string; name: string };
            setLocationState(_state);
            getMyGroup(location.state.gid);
            getUsers();
        }
    }, [location]);

    useEffect(() => {
        console.log("Non Memebers being updated.");
        const filteredUsers = users.filter((user) => {
            if (user.roles.find((role) => role.userRole === UserRoleCodes.Admin)) return false;
            if (myGroup.members.find((member) => member.id === user.id)) return false;

            return true;
        });
        setNonMembers(filteredUsers);
    }, [users, myGroup]);

    function addingUser(uid: string) {
        AddUserToGroup(myGroup.id, uid)
            .then(
                () => {
                    getMyGroup(myGroup.id);
                    toast({
                        title: "Success",
                        description: "User added to group successfully.",
                        status: "success",
                        duration: 5000,
                        position: "top-right",
                        isClosable: true,
                    });
                },
                (rej) => {
                    console.log(rej);
                    toast({
                        title: "Error",
                        description: "Failed to add user to group.",
                        status: "error",
                        duration: 5000,
                        position: "top-right",
                        isClosable: true,
                    });
                }
            )
            .catch((err) => {
                console.log(err);
                toast({
                    title: "Error",
                    description: "An unexpected error occurred.",
                    status: "error",
                    duration: 5000,
                    position: "top-right",
                    isClosable: true,
                });
            });
    }

    return (
        <>
            <Button
                style="primary"
                variant="outline"
                height="40px"
                ml={"10%"}
                mt={5}
                onClick={() => navigate(BaseRoutes.ManageableGroup)}
            >
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
                                Add User to Group:
                            </Heading>
                            <Text fontSize={"2xl"} textAlign={"center"} mb={3}>
                                {locationState.name}
                            </Text>

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
                                    {nonMembers.map((item, index) => (
                                        <HStack justify="space-between" key={index}>
                                            <ListItem>{item.email}</ListItem>

                                            <Button
                                                m={1}
                                                style="primary"
                                                variant="outline"
                                                width="18%"
                                                height="40px"
                                                onClick={() => addingUser(item.id)}
                                            >
                                                Add
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
