import { useEffect, useState } from "react";
import logo from "../assets/logo.png";
import { Container, Stack, Heading, Box, FormControl, Input, FormLabel, HStack } from "@chakra-ui/react";
import { useLocation, useNavigate } from "react-router-dom";
import Button from "../components/Button";
import { GetGroupByID, GroupDTO, GroupResponseDTO, RemoveUserFromGroup } from "../services/group";
import { BaseRoutes } from "../constants";

export default function RemovingUserFromGroup() {
    const navigate = useNavigate();
    const [locationState, setLocationState] = useState({ gid: "", name: "" });
    const [myGroup, setMyGroup] = useState<GroupDTO | null>(null);
    const location = useLocation();

    function getMyGroup(gid: string) {
        console.log("Grabbing group info");
        GetGroupByID(gid)
            .then(
                (res: GroupResponseDTO) => {
                    setMyGroup(res.data);
                    console.log(res.data.name);
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
            console.log(locationState.gid);
            getMyGroup(locationState.gid);
        }
    }, [location]);
    const userList = myGroup.members;
    const [filteredList, setFilteredList] = useState(userList);
    const filterBySearch = (event: { target: { value: string } }) => {
        const query = event.target.value;

        let updatedList = [...userList];

        updatedList = updatedList.filter((item) => {
            return item.email.toLowerCase().indexOf(query.toLowerCase()) !== -1;
        });

        setFilteredList(updatedList);
    };

    function removingUser(uid: string) {
        RemoveUserFromGroup(myGroup.id, uid)
            .then(
                () => {
                    console.log("User Added");
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
                            Remove User from Group: {locationState.name}
                        </Heading>

                        <FormControl>
                            <FormLabel htmlFor="search-text">Search:</FormLabel>
                            <Input id="groupName" type="text" onChange={filterBySearch} />
                        </FormControl>

                        <div id="item-list">
                            <ol>
                                {filteredList.map((item, index) => (
                                    <HStack justify="space-between" key={index}>
                                        <li>{item.email}</li>
                                        <Button
                                            style="primary"
                                            variant="outline"
                                            width="22%"
                                            height="40px"
                                            justifyContent={"flex-end"}
                                            onClick={() => removingUser(item.email)}
                                        >
                                            Select
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
