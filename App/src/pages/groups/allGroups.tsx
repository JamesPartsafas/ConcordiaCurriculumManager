import React, { useState, useEffect } from "react";
import { Container, Box, Text, Button, Flex } from "@chakra-ui/react";
import { GetAllGroups, GroupDTO, MultiGroupResponseDTO } from "../../services/group";
import { BaseRoutes } from "../../constants";
import { Link } from "react-router-dom";
import GroupTable from "../../components/GroupTable";

export default function AllGroups() {
    const [myGroups, setMyGroups] = useState<GroupDTO[]>([]);
    const [loading, setLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState<number>(1);

    useEffect(() => {
        const fetchGroups = async () => {
            try {
                const response: MultiGroupResponseDTO = await GetAllGroups();
                const groups: GroupDTO[] = response.data;

                setMyGroups(groups);
                setLoading(false);
            } catch (error) {
                console.error("Error fetching groups:", error);
            }
        };

        fetchGroups();
    }, []);

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
                        All Groups
                    </h1>
                    {loading ? (
                        <Text>Loading...</Text>
                    ) : myGroups.length === 0 ? (
                        <Text>There are no Created Groups.</Text>
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
