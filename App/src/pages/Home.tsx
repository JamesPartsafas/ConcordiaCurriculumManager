import { Flex, Container, Heading, Stack, Text } from "@chakra-ui/react";
import { useContext } from "react";
import { UserContext } from "../App";
import Button from "../components/Button";
import { BaseRoutes } from "../constants";
import { useNavigate } from "react-router-dom";
import { isAdminOrGroupMaster } from "../services/auth";

export default function CallToActionWithIllustration() {
    const navigate = useNavigate();
    const user = useContext(UserContext);

    return (
        <>
            <Container maxW={"5xl"}>
                <Stack textAlign={"center"} align={"center"} spacing={{ base: 8, md: 10 }} py={{ base: 20, md: 28 }}>
                    <Heading fontWeight={600} fontSize={{ base: "3xl", sm: "4xl", md: "6xl" }} lineHeight={"110%"}>
                        Hello {user?.firstName},
                        <br />
                        <Text as={"span"} color={"brandRed"}>
                            Welcome to the Concordia Curriculum Manager!
                        </Text>
                    </Heading>
                    <Text color={"gray.500"} maxW={"3xl"} fontSize="xl">
                        Quick access to the Dossier List and Group List:
                    </Text>
                    <Stack spacing={6} direction={"row"}>
                        <Button
                            style="primary"
                            variant="solid"
                            width="200px"
                            height="40px"
                            margin="0px"
                            onClick={() => navigate(BaseRoutes.Dossiers)}
                        >
                            View Dossier List
                        </Button>
                        <Button
                            style="primary"
                            variant="outline"
                            width="200px"
                            margin="0px"
                            height="40px"
                            onClick={() => navigate(BaseRoutes.Dossiers)}
                        >
                            View Groups List
                        </Button>
                    </Stack>
                    {isAdminOrGroupMaster(user) && (
                        <Stack>
                            <Text color={"gray.500"} maxW={"3xl"}>
                                Manage Groups is reserved for admins only:
                            </Text>
                            <Button
                                style="secondary"
                                variant="solid"
                                width="240px"
                                height="40px"
                                margin="auto"
                                onClick={() => navigate(BaseRoutes.ManageableGroup)}
                            >
                                View Manageable Groups List
                            </Button>
                        </Stack>
                    )}
                    <Flex w={"full"}>
                        {/* <Illustration height={{ sm: "24rem", lg: "28rem" }} mt={{ base: 12, sm: 16 }} /> */}
                    </Flex>
                </Stack>
            </Container>
        </>
    );
}
