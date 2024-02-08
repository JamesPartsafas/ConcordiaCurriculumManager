// MyGroups.js
import React, { useState, useEffect, useContext } from "react";
import { Container, Box, Text, Button, Flex } from "@chakra-ui/react";
import { GetAllGroups, GroupDTO, MultiGroupResponseDTO } from "../../services/group";
import { UserContext } from "../../App";
import GroupTable from "../../components/GroupTable";
import { BaseRoutes } from "../../constants";
import { Link } from "react-router-dom";

export default function MyGroups() {
    const [myGroups, setMyGroups] = useState<GroupDTO[]>([]);
    const user = useContext(UserContext);
    const [loading, setLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState<number>(1);

    useEffect(() => {
        const fetchGroups = async () => {
            try {
                const response: MultiGroupResponseDTO = await GetAllGroups();
                const groups: GroupDTO[] = response.data;

                // Filter groups based on user's ID
                const userGroups = groups.filter((group) => group.members.some((member) => member.id === user.id));

                setMyGroups(userGroups);
                setLoading(false);
            } catch (error) {
                console.error("Error fetching groups:", error);
            }
        };

        fetchGroups();
    }, [user]);

    const groupsPerPage = 5;
    const indexOfLastGroup = currentPage * groupsPerPage;
    const indexOfFirstGroup = indexOfLastGroup - groupsPerPage;

    return (
        <Box overflowX="auto" width="100%" height="100vh" padding="2vw">
            <Container
                maxW="5xl"
                height="80vh"
                display="flex"
                alignItems="center"
                justifyContent="center"
                mt={10}
                mb={10}
            >
                <Box>
                    <h1
                        style={{
                            textAlign: "center",
                            marginBottom: "20px",
                            fontWeight: "bold",
                            fontSize: "24px",
                            color: "brandRed",
                        }}
                    >
                        My Groups
                    </h1>

                    {loading ? (
                        <Text>Loading...</Text>
                    ) : myGroups.length === 0 ? (
                        <Text>You have no groups at the moment.</Text>
                    ) : (
                        <GroupTable
                            myGroups={myGroups}
                            startIndex={indexOfFirstGroup + 1}
                            endIndex={indexOfLastGroup}
                            setCurrentPage={setCurrentPage}
                            currentPage={currentPage}
                            showManageMembers={false}
                            totalResults={myGroups.length}
                            groupsPerPage={groupsPerPage}
                        />
                    )}

                    <Flex justify="center" mt={4}>
                        <Link to={BaseRoutes.Home}>
                            <Button color="white" backgroundColor={"#932439"} variant="solid" height="40px">
                                Back to Home Page
                            </Button>
                        </Link>
                    </Flex>
                </Box>
            </Container>
        </Box>
    );
}
