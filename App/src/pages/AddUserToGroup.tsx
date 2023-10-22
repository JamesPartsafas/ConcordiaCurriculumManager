import { useState } from "react";
import logo from "../assets/logo.png";
import { Container, Stack, Heading, Box, FormControl, Input, FormLabel, HStack } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
import { AddUserToGroup, GetGroupByID, GroupDTO, GroupResponseDTO } from "../services/group";
import { useForm } from "react-hook-form";

export default function AddingUserToGroup(gid: string) {
    const userList = ["User1", "Dave", "Joe", "Billy", "Benjamen"];
    const navigate = useNavigate();

    const [filteredList, setFilteredList] = useState(userList);
    const [myGroup, setMyGroup] = useState<GroupDTO | null>(null);

    const filterBySearch = (event: { target: { value: string } }) => {
        const query = event.target.value;
        let updatedList = [...userList];
        updatedList = updatedList.filter((item) => {
            return item.toLowerCase().indexOf(query.toLowerCase()) !== -1;
        });
        setFilteredList(updatedList);
    };

    function getMyGroup(gid: string) {
        GetGroupByID(gid)
            .then(
                (res: GroupResponseDTO) => {
                    setMyGroup(res.data);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }
    getMyGroup(gid);

    function addingUser(uid: string) {
        AddUserToGroup(myGroup.id, uid);
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
                            Add User to Group: {myGroup.name}
                        </Heading>

                        <FormControl>
                            <FormLabel htmlFor="search-text">Search:</FormLabel>
                            <Input id="searcher" type="text" onChange={filterBySearch} />
                        </FormControl>

                        <div id="item-list">
                            <ol>
                                {filteredList.map((item, index) => (
                                    <HStack justify="space-between" key={index}>
                                        <li>{item}</li>
                                        <Button
                                            style="primary"
                                            variant="outline"
                                            width="22%"
                                            height="40px"
                                            justifyContent={"flex-end"}
                                            onClick={addingUser(item)}
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
                            onClick={() => navigate("/manageablegroup")}
                        >
                            Back
                        </Button>
                    </Stack>
                </Box>
            </Stack>
        </Container>
    );
}
