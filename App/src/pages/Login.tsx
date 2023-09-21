import {
    Box,
    Button,
    Checkbox,
    Container,
    FormControl,
    FormLabel,
    Heading,
    HStack,
    Image,
    Input,
    InputGroup,
    InputRightElement,
    Stack,
} from "@chakra-ui/react";
import logo from "../assets/logo.png";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { AuthenticationResponse, login, LoginDTO } from "../services/auth";

export default function Login() {
    const { register, handleSubmit } = useForm<LoginDTO>();
    const [showPassword, setShowPassword] = useState(false);

    function toggleShowPassword() {
        setShowPassword(!showPassword);
    }

    function onSubmit(data: LoginDTO) {
        login(data)
            .then(
                (res: AuthenticationResponse) => {
                    console.log(res);
                    //deal with the access token here
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }
    return (
        <>
            <form onSubmit={handleSubmit(onSubmit)}>
                <Container
                    maxW="lg"
                    py={{ base: "12", md: "24" }}
                    px={{ base: "0", sm: "8" }}
                >
                    <Stack spacing="8">
                        <Stack spacing="6">
                            <Image
                                src={logo}
                                width="50px"
                                height="50px"
                                margin="auto"
                            />
                            <Heading textAlign="center" size="lg">
                                Log in to your account
                            </Heading>
                        </Stack>
                        <Box
                            py={{ base: "0", sm: "8" }}
                            px={{ base: "4", sm: "10" }}
                            bg={{ base: "transparent", sm: "bg.surface" }}
                            boxShadow={{ base: "none", sm: "2xl" }}
                            borderRadius={{ base: "none", sm: "xl" }}
                        >
                            <Stack spacing="6">
                                <Stack spacing="5">
                                    <FormControl>
                                        <FormLabel htmlFor="email">
                                            Email
                                        </FormLabel>
                                        <Input
                                            id="email"
                                            type="email"
                                            {...register("email", {
                                                required: true,
                                                pattern:
                                                    /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                            })}
                                        />
                                        <FormLabel htmlFor="password">
                                            Password
                                        </FormLabel>
                                        <InputGroup>
                                            <Input
                                                id="password"
                                                type={
                                                    showPassword
                                                        ? "text"
                                                        : "password"
                                                }
                                                {...register("password", {
                                                    required: true,
                                                })}
                                            />
                                            <InputRightElement width="4.5rem">
                                                <Button
                                                    h="1.75rem"
                                                    size="sm"
                                                    onClick={toggleShowPassword}
                                                >
                                                    {showPassword
                                                        ? "Hide"
                                                        : "Show"}
                                                </Button>
                                            </InputRightElement>
                                        </InputGroup>
                                    </FormControl>
                                </Stack>
                                <HStack justify="space-between">
                                    <Checkbox defaultChecked>
                                        Remember me
                                    </Checkbox>
                                    <Button variant="text" size="sm">
                                        Forgot password?
                                    </Button>
                                </HStack>
                                <Stack spacing="6">
                                    <Button
                                        backgroundColor="#932439"
                                        color="white"
                                        _hover={{ bg: "#7A1D2E" }}
                                        type="submit"
                                    >
                                        Sign in
                                    </Button>
                                </Stack>
                            </Stack>
                        </Box>
                    </Stack>
                </Container>
            </form>
        </>
    );
}
