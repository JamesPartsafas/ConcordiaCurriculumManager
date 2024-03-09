import { useContext, useState } from "react";
import { useLoading } from "../utils/loadingContext";
import {
    Box,
    Checkbox,
    Container,
    FormControl,
    FormLabel,
    Text,
    HStack,
    Heading,
    Input,
    InputGroup,
    InputRightElement,
    Stack,
    useToast,
    Image,
} from "@chakra-ui/react";
import logo from "../assets/logo.png";
import Button from "../components/Button";
import { BaseRoutes } from "../constants";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { AuthenticationResponse, RegisterDTO, decodeTokenToUser, editProfile, logout } from "../services/auth";
import { showToast } from "../utils/toastUtils";
import { User } from "../models/user";
import { UserContext } from "../App";

export default function EditProfileInfo() {
    const navigate = useNavigate();
    const user = useContext(UserContext);
    const { register, handleSubmit } = useForm<RegisterDTO>();
    const [showPassword, setShowPassword] = useState(false);
    const [showError, setShowError] = useState(false);
    const { toggleLoading } = useLoading(); // Use the useLoading hook
    const toast = useToast(); // Use the useToast hook

    const handleLoadingButtonClick = () => {
        toggleLoading();
        setTimeout(() => {
            toggleLoading();
        }, 1000);
    };

    function toggleShowPassword() {
        setShowPassword(!showPassword);
    }

    function onSubmit(data: RegisterDTO) {
        handleLoadingButtonClick();
        editProfile(data)
            .then(
                (res: AuthenticationResponse) => {
                    if (res.data.accessToken != null) {
                        logout().then(
                            () => {
                                navigate(BaseRoutes.Login);
                                showToast(toast, "Success!", "You have successfully changed your profile.", "success");
                            },
                            (rej) => {
                                showToast(toast, "Error!", rej.message, "error");
                            }
                        );
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
                                Edit Account Info
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
                                    {showError && <Text color="red">Invalid Credentials</Text>}
                                    <FormControl>
                                        <FormLabel htmlFor="firstName">First Name</FormLabel>
                                        <Input
                                            id="firstName"
                                            type="firstName"
                                            placeholder={user.firstName}
                                            {...register("firstName", {
                                                required: true,
                                            })}
                                        />
                                        <FormLabel htmlFor="lastName">Last Name</FormLabel>
                                        <Input
                                            id="lastName"
                                            type="lastName"
                                            placeholder={user.lastName}
                                            {...register("lastName", {
                                                required: true,
                                            })}
                                        />
                                        <FormLabel htmlFor="email">Email</FormLabel>
                                        <Input
                                            id="email"
                                            type="email"
                                            placeholder={user.email}
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
                                                <Button h="1.75rem" size="sm" onClick={toggleShowPassword}>
                                                    {showPassword ? "Hide" : "Show"}
                                                </Button>
                                            </InputRightElement>
                                        </InputGroup>
                                    </FormControl>
                                </Stack>
                                <HStack justify="space-between">
                                    <Button variant="text" size="sm" onClick={() => navigate(BaseRoutes.profile)}>
                                        Back to Profile
                                    </Button>
                                </HStack>
                                <Stack spacing="6">
                                    <Button
                                        backgroundColor="#932439"
                                        color="white"
                                        _hover={{ bg: "#7A1D2E" }}
                                        type="submit"
                                    >
                                        Submit Changes
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
