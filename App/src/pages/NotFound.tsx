import { Container, Heading, Stack, Text } from "@chakra-ui/react";
import Button from "../components/Button";
import { BaseRoutes } from "../constants";
import { useNavigate } from "react-router-dom";

export default function NotFound() {
    const navigate = useNavigate();
    return (
        <>
            <Container maxW={"5xl"}>
                <Stack textAlign={"center"} align={"center"} spacing={{ base: 8, md: 10 }} py={{ base: 20, md: 28 }}>
                    <Heading fontWeight={600} fontSize={{ base: "3xl", sm: "4xl", md: "6xl" }} lineHeight={"110%"}>
                        404
                        <br />
                        <Text as={"span"} color={"brandRed"}>
                            Page not found
                        </Text>
                    </Heading>
                    <Text color={"gray.500"} maxW={"3xl"} fontSize="xl">
                        You may want to go back to the home page:
                    </Text>
                    <Button
                        style="primary"
                        variant="solid"
                        width="200px"
                        height="40px"
                        margin="0px"
                        onClick={() => navigate(BaseRoutes.Home)}
                    >
                        Go home
                    </Button>
                </Stack>
            </Container>
        </>
    );
}
