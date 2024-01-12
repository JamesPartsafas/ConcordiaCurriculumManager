import { useEffect, useState } from "react";
import logo from "../../assets/logo.png";
import { Container, Stack, Heading, Box, HStack, OrderedList, ListItem, Text } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import Button from "../../components/Button";
import { AddGroupMaster, GetGroupByID, GroupDTO, GroupResponseDTO } from "../../services/group";
import { useLocation } from "react-router-dom";
import { BaseRoutes } from "../../constants";
import { UserDTO, UserRoleCodes } from "../../models/user";

export default function AddingMasterToGroup() {
    const [users, setUsers] = useState<UserDTO[]>([]);
    const [nonMasters, setNonMasters] = useState<UserDTO[]>([]);
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
                setUsers(res.data.members);
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
        }
    }, [location]);

    useEffect(() => {
        const filteredUsers = users.filter((user) => {
            if (user.roles.find((role) => role.userRole === UserRoleCodes.Admin)) return false;
            if (myGroup.groupMasters.find((master) => master.id === user.id)) return false;

            return true;
        });
        setNonMasters(filteredUsers);
    }, [users, myGroup]);

    function addingUser(uid: string) {
        AddGroupMaster(myGroup.id, uid)
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
                                Add Master to Group:
                            </Heading>
                            <Text fontSize={"2xl"} textAlign={"center"} mb={3}>
                                {locationState.name}
                            </Text>

                            <div id="item-list">
                                <OrderedList>
                                    {nonMasters.map((item, index) => (
                                        <HStack justify="space-between" key={index}>
                                            <ListItem>{item.email}</ListItem>
                                            <Button
                                                style="primary"
                                                variant="outline"
                                                width="18%"
                                                m={1}
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
