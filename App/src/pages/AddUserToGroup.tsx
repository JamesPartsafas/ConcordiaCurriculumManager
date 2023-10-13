import { useState } from "react";
import logo from "../assets/logo.png";
import { Container, Stack, Heading, Box, FormControl, Input, FormLabel, HStack } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";

export default function AddUserToGroup() {
    const userList = ["User1", "Dave", "Joe", "Billy", "Benjamen"];
    const navigate = useNavigate();

    const [filteredList, setFilteredList] = useState(userList);

    const filterBySearch = (event: { target: { value: any } }) => {
        const query = event.target.value;
        var updatedList = [...userList];
        updatedList = updatedList.filter((item) => {
            return item.toLowerCase().indexOf(query.toLowerCase()) !== -1;
        });
        setFilteredList(updatedList);
    };

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
                            Add User to Group
                        </Heading>

                        <FormControl>
                            <FormLabel htmlFor="search-text">Search:</FormLabel>
                            <Input id="groupName" type="text" onChange={filterBySearch} />
                        </FormControl>

                        <div id="item-list">
                            <ol>
                                {filteredList.map((item, index) => (
                                    <HStack justify="space-between" key={index}>
                                        <li>{item}</li>
                                        <Button
                                            type="primary"
                                            variant="outline"
                                            width="22%"
                                            height="40px"
                                            justifyContent={"flex-end"}
                                        >
                                            Select
                                        </Button>
                                    </HStack>
                                ))}
                            </ol>
                        </div>

                        <Button
                            type="primary"
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
