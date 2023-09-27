import {
    Box,
    Button,
    Checkbox,
    Container,
    FormControl,
    Text,
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
import { useForm } from "react-hook-form";
import { AuthenticationResponse, login, LoginDTO } from "../services/auth";
import jwt_decode from "jwt-decode";
import { User } from "../services/user";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

interface LoginProps {
    setUser: (user: User | null) => void;
}

interface DecodedToken {
    fName: string;
    lName: string;
    email: string;
    roles: string[];
    iat: number;
    exp: number;
    iss: string;
    aud: string;
}

export default function Login({ setUser }: LoginProps) {
    const navigate = useNavigate();
    const { register, handleSubmit } = useForm<LoginDTO>();
    const [showPassword, setShowPassword] = useState(false);
    const [showError, setShowError] = useState(false);

    function toggleShowPassword() {
        setShowPassword(!showPassword);
    }

    function onSubmit(data: LoginDTO) {
        login(data)
            .then(
                (res: AuthenticationResponse) => {
                    console.log(res.data.accessToken);
                    //decode access token
                    //save access token in local storage
                    //save user in context
                    //redirect to home page
                    //code:
                    if (res.data.accessToken != null) {
                        const decodedToken = jwt_decode<DecodedToken>(res.data.accessToken);

                        const user: User = {
                            firstName: decodedToken.fName,
                            lastName: decodedToken.lName,
                            email: decodedToken.email,
                            roles: decodedToken.roles,
                            issuedAtTimestamp: decodedToken.iat,
                            expiresAtTimestamp: decodedToken.exp,
                            issuer: decodedToken.iss,
                            audience: decodedToken.aud,
                        };
                        setUser(user);
                        navigate("/");
                    }
                },
                (rej) => {
                    console.log(rej);
                    setShowError(true);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    return (
        <>
            <form onSubmit={handleSubmit(onSubmit)}>
                <Container maxW="lg" py={{ base: "12", md: "24" }} px={{ base: "0", sm: "8" }}>
                    <Stack spacing="8">
                        <Stack spacing="6">
                            <Image src={logo} width="50px" height="50px" margin="auto" />
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
                                    {showError && <Text color="red">Incorrect Credentials</Text>}
                                    <FormControl>
                                        <FormLabel htmlFor="email">Email</FormLabel>
                                        <Input
                                            id="email"
                                            type="email"
                                            {...register("email", {
                                                required: true,
                                                pattern: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                            })}
                                        />
                                        <FormLabel htmlFor="password">Password</FormLabel>
                                        <InputGroup>
                                            <Input
                                                id="password"
                                                type={showPassword ? "text" : "password"}
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
                                                    {showPassword ? "Hide" : "Show"}
                                                </Button>
                                            </InputRightElement>
                                        </InputGroup>
                                    </FormControl>
                                </Stack>
                                <HStack justify="space-between">
                                    <Checkbox defaultChecked>Remember me</Checkbox>
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
