import { useEffect, useState } from "react";
import logo from "../assets/logo.png";
import { Container, Stack, Heading, Box, FormControl, Input, FormLabel, HStack } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
import { AddUserToGroup, GetGroupByID, GroupDTO, GroupResponseDTO } from "../services/group";
import { useLocation } from "react-router-dom";
import { BaseRoutes } from "../constants";

export default function AddingUserToGroup() {
    const userList = [
        { id: "1", email: "User1@gmail.com", firstName: "User", lastName: "1" },
        { id: "2", email: "Dave@gmail.com", firstName: "Dave", lastName: "1" },
        { id: "3", email: "Joe@gmail.com", firstName: "Joe", lastName: "1" },
        { id: "4", email: "Billy@gmail.com", firstName: "Billy", lastName: "1" },
        { id: "5", email: "Benjamen@gmail.com", firstName: "Benjamen", lastName: "1" },
    ];
    const navigate = useNavigate();
    const location = useLocation();
    const [filteredList, setFilteredList] = useState(userList);
    const [myGroup, setMyGroup] = useState<GroupDTO | null>(null);
    const [locationState, setLocationState] = useState({ gid: "", name: "" });

    function getMyGroup(gid: string) {
        console.log("Grabbing group info");
        GetGroupByID(gid)
            .then((res: GroupResponseDTO) => {
                setMyGroup(res.data);
                console.log("this is the name" + res.data.name);
                console.log("this is the id " + res.data.id);
            })
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

    const filterBySearch = (event: { target: { value: string } }) => {
        const query = event.target.value;
        let updatedList = [...userList];
        updatedList = updatedList.filter((item) => {
            return item.email.toLowerCase().indexOf(query.toLowerCase()) !== -1;
        });
        setFilteredList(updatedList);
    };

    function addingUser(uid: string) {
        AddUserToGroup(myGroup.id, uid)
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
                            Add User to Group: {locationState.name}
                        </Heading>

                        <FormControl>
                            <FormLabel htmlFor="search-text">Search:</FormLabel>
                            <Input id="searcher" type="text" onChange={filterBySearch} />
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
                                            onClick={() => addingUser(item.id)}
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
