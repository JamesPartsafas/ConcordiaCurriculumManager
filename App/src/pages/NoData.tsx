import { Container, Heading, Stack, Text } from "@chakra-ui/react";
import Button from "../components/Button";
//import { BaseRoutes } from "../constants";
import { useNavigate } from "react-router-dom";

export default function NoData() {
    const navigate = useNavigate();
    return (
        <>
            <Container maxW={"5xl"}>
                <Stack textAlign={"center"} align={"center"} spacing={{ base: 8, md: 10 }} py={{ base: 20, md: 28 }}>
                    <Heading fontWeight={600} fontSize={{ base: "3xl", sm: "4xl", md: "6xl" }} lineHeight={"110%"}>
                        204
                        <br />
                        <Text as={"span"} color={"brandRed"}>
                            No content
                        </Text>
                    </Heading>
                    <Text color={"gray.500"} maxW={"3xl"} fontSize="xl">
                        This does not currently exist in the database.
                    </Text>
                    <Button
                        style="primary"
                        variant="solid"
                        width="200px"
                        height="40px"
                        margin="0px"
                        onClick={() => navigate(-3)} // i have no clue why -3 here but it works.
                    >
                        Go back
                    </Button>
                </Stack>
            </Container>
        </>
    );
}
