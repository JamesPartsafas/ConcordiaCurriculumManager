import { useEffect, useState } from "react";
import logo from "../../assets/logo.png";
import { Container, Stack, Heading, Box, HStack, Text, OrderedList, ListItem } from "@chakra-ui/react";
import { useLocation, useNavigate } from "react-router-dom";
import Button from "../../components/Button";
import { GetGroupByID, GroupDTO, GroupResponseDTO, RemoveGroupMaster } from "../../services/group";
import { BaseRoutes } from "../../constants";
import { UserDTO } from "../../models/user";
import { useToast } from "@chakra-ui/react";

export default function RemovingMasterFromGroup() {
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
                    setUserList(res.data.groupMasters);
                },
                (rej) => {
                    console.log(rej);
                    toast({
                        title: "Error",
                        description: "Failed to retrieve group information.",
                        status: "error",
                        duration: 5000,
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
            if (myGroup.groupMasters.find((member) => member.id === user.id)) return true;

            return false;
        });
        setUserList(filteredUsers);
    }, [myGroup]);

    function removingUser(uid: string) {
        RemoveGroupMaster(myGroup.id, uid)
            .then(
                () => {
                    console.log("User Removed");
                    getMyGroup(myGroup.id);
                    toast({
                        title: "Success",
                        description: "Master removed successfully.",
                        status: "success",
                        duration: 5000,
                        isClosable: true,
                        position: "top-right",
                    });
                },
                (rej) => {
                    console.log(rej);
                    toast({
                        title: "Error",
                        description: "Failed to remove master.",
                        status: "error",
                        duration: 5000,
                        isClosable: true,
                        position: "top-right",
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
                    isClosable: true,
                });
            });
    }

    return (
        <>
            <Button
                style="primary"
                variant="outline"
                ml={"10%"}
                mt={5}
                height="40px"
                onClick={() => navigate(BaseRoutes.ManageableGroup)}
            >
                Back
            </Button>
            <Container maxW="lg" py={{ base: "12", md: "24" }} px={{ base: "0", sm: "8" }}>
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
                                Remove Master from Group:
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
                                                width="22%"
                                                m={1}
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
