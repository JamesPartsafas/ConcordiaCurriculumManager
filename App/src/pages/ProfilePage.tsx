import { useContext } from "react";
import { UserContext } from "../App";
import { useNavigate } from "react-router-dom";
import { Box, Flex, Text } from "@chakra-ui/react";
import Button from "../components/Button";
import { BaseRoutes } from "../constants";

export default function ProfilePage() {
    const user = useContext(UserContext);
    const navigate = useNavigate();

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
                </Flex>
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    {user?.firstName + "'s"} Group Affiliations
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
            </Box>
        </div>
    );
}
