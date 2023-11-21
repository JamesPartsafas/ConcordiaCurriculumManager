import { useEffect, useState } from "react";
import logo from "../assets/logo.png";
import { Container, Stack, Heading, Box, HStack } from "@chakra-ui/react";
import { useLocation, useNavigate } from "react-router-dom";
import Button from "../components/Button";
import { GetGroupByID, GroupDTO, GroupResponseDTO, RemoveGroupMaster } from "../services/group";
import { BaseRoutes } from "../constants";
import { UserDTO } from "../models/user";

export default function RemovingMasterFromGroup() {
    const navigate = useNavigate();
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
                }
            )
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
                            Remove Master from Group: {locationState.name}
                        </Heading>
                        <div id="item-list">
                            <ol>
                                {userList.map((item, index) => (
                                    <HStack justify="space-between" key={index}>
                                        <li>{item.email}</li>
                                        <Button
                                            style="primary"
                                            variant="outline"
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
                            </ol>
                        </div>

                        <Button
                            style="primary"
                            variant="outline"
                            width="50%"
                            height="40px"
                            onClick={() => navigate(BaseRoutes.ManageableGroup)}
                        >
                            Back
                        </Button>
                    </Stack>
                </Box>
            </Stack>
        </Container>
    );
}
