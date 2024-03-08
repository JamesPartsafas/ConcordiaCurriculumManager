import { useContext, useEffect, useState } from "react";
import { UserContext } from "../App";
import { Link, useNavigate } from "react-router-dom";
import { Box, Flex, Text } from "@chakra-ui/react";
import Button from "../components/Button";
import { BaseRoutes } from "../constants";
import GroupTable from "../components/GroupTable";
import { GetAllGroups, GroupDTO, MultiGroupResponseDTO } from "../services/group";

export default function ProfilePage() {
    const user = useContext(UserContext);
    const [myGroups, setMyGroups] = useState<GroupDTO[]>([]);
    const navigate = useNavigate();
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

    const groupsPerPage = 10;
    const indexOfLastGroup = currentPage * groupsPerPage;
    const indexOfFirstGroup = indexOfLastGroup - groupsPerPage;

    return (
        <div>
            <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                {user?.firstName + "'s"} Profile
            </Text>
            <Box maxW="5xl" m="auto">
                <Flex flexDirection="column">
                    <Text textAlign="left" fontSize="2xl" marginTop="7%" marginBottom="5">
                        First Name: {user?.firstName}
                    </Text>
                    <Text textAlign="left" fontSize="2xl" marginTop="7%" marginBottom="5">
                        Last Name: {user?.lastName}
                    </Text>
                    <Text textAlign="left" fontSize="2xl" marginTop="7%" marginBottom="5">
                        Email: {user?.email}
                    </Text>
                    <Button
                        style="primary"
                        variant="outline"
                        height="40px"
                        width="fit-content"
                        onClick={() => navigate(BaseRoutes.editProfileInfo)}
                    >
                        Edit Info and Password
                    </Button>
                </Flex>
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    {user?.firstName + "'s"} Group Affiliations
                </Text>
                <Box>
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
                            <Button
                                color="white"
                                backgroundColor={"#932439"}
                                variant="solid"
                                height="40px"
                                marginTop="7%"
                                marginBottom="5"
                            >
                                Back to Home Page
                            </Button>
                        </Link>
                    </Flex>
                </Box>
            </Box>
        </div>
    );
}
