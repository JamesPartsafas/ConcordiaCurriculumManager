import { useContext, useEffect, useState } from "react";
import { UserContext } from "../App";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import { Box, Flex, Text } from "@chakra-ui/react";
import Button from "../components/Button";
import { BaseRoutes } from "../constants";
import GroupTable from "../components/GroupTable";
import { GetAllGroups, GroupDTO, MultiGroupResponseDTO } from "../services/group";
import { UserDTO } from "../models/user";
import { AllUsersResponseDTO, getAllUsers, searchUsersByEmail } from "../services/user";
function useQuery() {
    return new URLSearchParams(useLocation().search);
}

export default function ProfilePage() {
    const user = useContext(UserContext);
    const query = useQuery(); // Use the useQuery function
    const email: string = query.get("email");
    const [foundUser, setFoundUser] = useState<UserDTO>();
    const [myGroups, setMyGroups] = useState<GroupDTO[]>([]);
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState<number>(1);

    useEffect(() => {
        const getUser = async () => {
            try {
                // If no email given assume is same as user context
                if (!email || email == "") {
                    const response: AllUsersResponseDTO = await searchUsersByEmail(user.email);
                    const users: UserDTO[] = response.data;

                    const altposition = users.map((user) => user.email).indexOf(user.email);
                    setFoundUser(users[altposition]);
                    console.log(users[altposition].email);
                } else {
                    const response: AllUsersResponseDTO = await searchUsersByEmail(email);
                    const users: UserDTO[] = response.data;
                    //Get index of found group
                    const position = users.map((user) => user.email).indexOf(email);
                    setFoundUser(users[position]);
                    {
                    }
                }
            } catch (error) {}
        };

        getUser();
    }, []);

    useEffect(() => {
        const fetchGroups = async () => {
            try {
                const response: MultiGroupResponseDTO = await GetAllGroups();
                const groups: GroupDTO[] = response.data;

                // Filter groups based on user's ID
                const userGroups = groups.filter((group) => group.members.some((member) => member.id === foundUser.id));

                setMyGroups(userGroups);
                setLoading(false);
            } catch (error) {
                console.error("Error fetching groups:", error);
            }
        };

        fetchGroups();
    }, [foundUser]);

    const groupsPerPage = 10;
    const indexOfLastGroup = currentPage * groupsPerPage;
    const indexOfFirstGroup = indexOfLastGroup - groupsPerPage;

    return (
        <div>
            <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                {foundUser?.firstName + "'s"} Profile
            </Text>
            <Box maxW="5xl" m="auto">
                <Flex flexDirection="column">
                    <Text textAlign="left" fontSize="2xl" marginTop="7%" marginBottom="5">
                        First Name: {foundUser?.firstName}
                    </Text>
                    <Text textAlign="left" fontSize="2xl" marginTop="7%" marginBottom="5">
                        Last Name: {foundUser?.lastName}
                    </Text>
                    <Text textAlign="left" fontSize="2xl" marginTop="7%" marginBottom="5">
                        Email: {foundUser?.email}
                    </Text>
                    {loading ? (
                        <Text>Loading...</Text>
                    ) : foundUser && foundUser.id == user.id ? (
                        <Button
                            style="primary"
                            variant="outline"
                            height="40px"
                            width="fit-content"
                            onClick={() => navigate(BaseRoutes.editProfileInfo)}
                        >
                            Edit Info and Password
                        </Button>
                    ) : (
                        <Text>This isn't your profile</Text>
                    )}
                </Flex>
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    {foundUser?.firstName + "'s"} Group Affiliations
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
