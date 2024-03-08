import { useEffect, useState } from "react";
import logo from "../../assets/logo.png";
import { Container, Stack, Heading, Box, HStack, Text, OrderedList, ListItem, useToast } from "@chakra-ui/react";
import { useLocation, useNavigate } from "react-router-dom";
import Button from "../../components/Button";
import { GetGroupByID, GroupDTO, GroupResponseDTO, RemoveUserFromGroup } from "../../services/group";
import { BaseRoutes } from "../../constants";
import { UserDTO } from "../../models/user";

export default function RemovingUserFromGroup() {
    const navigate = useNavigate();
    const toast = useToast();
    const [locationState, setLocationState] = useState({ gid: "", name: "" });
    const [myGroup, setMyGroup] = useState<GroupDTO | null>(null);
    const [userList, setUserList] = useState<UserDTO[]>([]);
    const location = useLocation();

    function getMyGroup(gid: string) {
        console.log("Grabbing group info");
        GetGroupByID(gid)
            .then(
                (res: GroupResponseDTO) => {
                    setMyGroup(res.data);
                    setUserList(res.data.members);
                },
                (rej) => {
                    console.log(rej);
                    toast({
                        title: "Error",
                        description: "Failed to retrieve group information.",
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

    useEffect(() => {
        if (location.state) {
            const _state = location.state as { gid: string; name: string };
            setLocationState(_state);
            getMyGroup(location.state.gid);
        }
    }, [location]);

    useEffect(() => {
        const filteredUsers = userList.filter((user) => {
            if (myGroup.members.find((member) => member.id === user.id)) return true;

            return false;
        });
        setUserList(filteredUsers);
    }, [myGroup]);

    function removingUser(uid: string) {
        RemoveUserFromGroup(myGroup.id, uid)
            .then(
                () => {
                    console.log("User Removed");
                    getMyGroup(myGroup.id);
                    toast({
                        title: "Success",
                        description: "User removed successfully.",
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
                        description: "Failed to remove user.",
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
                <Stack>
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
                                Remove User from Group:
                            </Heading>
                            <Text fontSize={"2xl"} textAlign={"center"} mb={3}>
                                {locationState.name}
                            </Text>
                            <div id="item-list">
                                <OrderedList>
                                    {userList.map((item, index) => (
                                        <HStack justify="space-between" key={index}>
                                            <ListItem>{item.email}</ListItem>
                                            <Button
                                                style="primary"
                                                variant="outline"
                                                m={1}
                                                width="22%"
                                                height="40px"
                                                onClick={() => {
                                                    removingUser(item.id);
                                                }}
                                            >
                                                Remove
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
