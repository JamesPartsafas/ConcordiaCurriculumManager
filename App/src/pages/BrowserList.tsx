import { Container, Stack, Text } from "@chakra-ui/react";
import Button from "../components/Button";
import { BaseRoutes } from "../constants";
import { useNavigate } from "react-router-dom";
import { UserContext } from "../App";
import { useContext } from "react";
import { isAdmin } from "../services/auth";

export default function BrowserList() {
    const navigate = useNavigate();
    const user = useContext(UserContext);
    return (
        <div>
            <Container maxW={"5xl"} mt={5} pl={0}>
                <Button
                    style="primary"
                    variant="outline"
                    height="40px"
                    width="fit-content"
                    onClick={() => navigate(BaseRoutes.Home)}
                >
                    Return to Home
                </Button>
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    Browser List
                </Text>
                <Stack spacing={6} direction={"row"} marginTop="7%" marginBottom="5">
                    <Text fontWeight="bold" width={250}>
                        Course Browser:{" "}
                    </Text>
                    <Button
                        style="primary"
                        variant="solid"
                        width="200px"
                        height="40px"
                        margin="0px"
                        onClick={() => navigate(BaseRoutes.CourseBrowser)}
                    >
                        View Course Browser
                    </Button>
                </Stack>
                <Stack spacing={6} direction={"row"} marginTop="7%" marginBottom="5">
                    <Text fontWeight="bold" width={250}>
                        Course by Subject Browser:{" "}
                    </Text>
                    <Button
                        style="primary"
                        variant="solid"
                        width="270px"
                        height="40px"
                        margin="0px"
                        onClick={() => navigate(BaseRoutes.CourseBySubject)}
                    >
                        View Course by Subject Browser
                    </Button>
                </Stack>
                <Stack spacing={6} direction={"row"} marginTop="7%" marginBottom="5">
                    <Text fontWeight="bold" width={250}>
                        Dossier Browser:{" "}
                    </Text>
                    <Button
                        style="primary"
                        variant="solid"
                        width="200px"
                        height="40px"
                        margin="0px"
                        onClick={() => navigate(BaseRoutes.DossierBrowser)}
                    >
                        View Dossier Browser
                    </Button>
                </Stack>
                <Stack spacing={6} direction={"row"} marginTop="7%" marginBottom="5">
                    <Text fontWeight="bold" width={250}>
                        Curriculum by School Browser:{" "}
                    </Text>
                    <Button
                        style="primary"
                        variant="solid"
                        width="240px"
                        height="40px"
                        margin="0px"
                        onClick={() => navigate(BaseRoutes.GroupingBySchool)}
                    >
                        View Curriculum by School
                    </Button>
                </Stack>
                <Stack spacing={6} direction={"row"} marginTop="7%" marginBottom="5">
                    <Text fontWeight="bold" width={250}>
                        Curriculum by Name Browser:{" "}
                    </Text>
                    <Button
                        style="primary"
                        variant="solid"
                        width="240px"
                        height="40px"
                        margin="0px"
                        onClick={() => navigate(BaseRoutes.GroupingByName)}
                    >
                        View Curriculum by Name
                    </Button>
                </Stack>
                <Stack spacing={6} direction={"row"} marginTop="7%" marginBottom="5">
                    <Text fontWeight="bold" width={250}>
                        Group Browser:{" "}
                    </Text>
                    <Button
                        style="primary"
                        variant="solid"
                        width="240px"
                        height="40px"
                        margin="0px"
                        onClick={() => navigate(BaseRoutes.allGroups)}
                    >
                        View Groups
                    </Button>
                </Stack>
                {isAdmin(user) && (
                    <Stack spacing={6} direction={"row"} marginTop="7%" marginBottom="5">
                        <Text fontWeight="bold">For Admins Only: User Browser: </Text>
                        <Button
                            style="secondary"
                            variant="solid"
                            width="240px"
                            height="40px"
                            margin="0px"
                            onClick={() => navigate(BaseRoutes.userBrowser)}
                        >
                            View User Profiles
                        </Button>
                    </Stack>
                )}
            </Container>
        </div>
    );
}
