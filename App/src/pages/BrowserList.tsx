import { Container, Stack, Text } from "@chakra-ui/react";
import Button from "../components/Button";
import { BaseRoutes } from "../constants";
import { useNavigate } from "react-router-dom";

export default function BrowserList() {
    const navigate = useNavigate();
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
                    <Text fontWeight="bold">Course Browser: </Text>
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
                    <Text fontWeight="bold">Course by Subject Browser: </Text>
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
                    <Text fontWeight="bold">Dossier Browser: </Text>
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
                    <Text fontWeight="bold">Curriculum by School Browser: </Text>
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
                    <Text fontWeight="bold">Curriculum by Name Browser: </Text>
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
                    <Text fontWeight="bold">Courses by Subject Browser: </Text>
                    <Button
                        style="primary"
                        variant="solid"
                        width="240px"
                        height="40px"
                        margin="0px"
                        //onClick={() => navigate(BaseRoutes.GroupingBySchool)}
                    >
                        View Course by Subject
                    </Button>
                </Stack>
                <Stack spacing={6} direction={"row"} marginTop="7%" marginBottom="5">
                    <Text fontWeight="bold">Group Browser: </Text>
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
            </Container>
        </div>
    );
}
