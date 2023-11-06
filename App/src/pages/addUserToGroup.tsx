import { useEffect, useState } from "react";
import logo from "../assets/logo.png";
import { Container, Stack, Heading, Box, FormControl, Input, FormLabel, HStack } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
import { AddUserToGroup, GetGroupByID, GroupDTO, GroupResponseDTO } from "../services/group";
import { useLocation } from "react-router-dom";
import { BaseRoutes } from "../constants";
import { UserDTO, UserRoleCodes } from "../models/user";
import { AllUsersResponseDTO, updateAllUsers } from "../services/user";
import { getAllUsers } from "../services/user";

export default function AddingUserToGroup() {
    const [users, setUsers] = useState<UserDTO[]>([]);
    const [nonMembers, setNonMembers] = useState<UserDTO[]>([]);
    const navigate = useNavigate();
    const location = useLocation();
    const [myGroup, setMyGroup] = useState<GroupDTO | null>(null);
    const [locationState, setLocationState] = useState({ gid: "", name: "" });

    function getMyGroup(gid: string) {
        console.log("Grabbing group info");
        console.log(gid);
        GetGroupByID(gid)
            .then((res: GroupResponseDTO) => {
                setMyGroup(res.data);
            })
            .catch((err) => {
                console.log(err);
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
            });
    }

    function updateUsers(uid: string) {
        console.log("Updating user info");
        updateAllUsers(uid)
            .then((res: AllUsersResponseDTO) => {
                console.log(JSON.stringify(res, null, 2));
                setUsers(users.concat(res.data));
            })
            .catch((err) => {
                console.log(err);
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
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    return (
        <Container maxW="lg" py={{ base: "12", md: "24" }} px={{ base: "0", sm: "8" }}>
            <Stack spacing="8">
                <Box
                    py={{ base: "0", sm: "8" }}
                    px={{ base: "4", sm: "10" }}
                    bg={{ base: "transparent", sm: "bg.surface" }}
                    boxShadow={{ base: "none", sm: "2xl" }}
                    borderRadius={{ base: "none", sm: "xl" }}
                >
                    <Stack spacing="6">
                        <img src={logo} alt="Logo" width="50px" height="50px" style={{ margin: "auto" }} />
                        <Heading textAlign="center" size="lg">
                            Add User to Group: {locationState.name}
                        </Heading>

                        <FormControl>
                            <FormLabel htmlFor="search-text">Search:</FormLabel>
                            <Input id="searcher" type="text" />
                        </FormControl>

                        <div id="item-list">
                            <ol>
                                {nonMembers.map((item, index) => (
                                    <HStack justify="space-between" key={index}>
                                        <li>{item.email}</li>
                                        <Button
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
                            </ol>
                        </div>
                        <HStack justify="space-between">
                            <Button
                                style="primary"
                                variant="outline"
                                width="50%"
                                height="40px"
                                onClick={() => navigate(BaseRoutes.ManageableGroup)}
                            >
                                Back
                            </Button>

                            {nonMembers.length != 0 && (
                                <Button
                                    style="secondary"
                                    variant="outline"
                                    width="22%"
                                    height="40px"
                                    onClick={() => updateUsers(nonMembers[nonMembers.length - 1].id)}
                                >
                                    Next Page
                                </Button>
                            )}
                        </HStack>
                    </Stack>
                </Box>
            </Stack>
        </Container>
    );
}
