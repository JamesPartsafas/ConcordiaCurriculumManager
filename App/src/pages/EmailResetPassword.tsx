import {
    Box,
    Button,
    Container,
    FormControl,
    Text,
    FormLabel,
    Heading,
    Image,
    Input,
    Stack,
    useToast,
} from "@chakra-ui/react";
import logo from "../assets/logo.png";
import { useForm } from "react-hook-form";
import { LoginDTO } from "../services/auth";
import { User } from "../models/user";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { showToast } from "../utils/toastUtils";
import { SendResetPasswordEmail } from "../services/user";

export interface LoginProps {
    setUser: (user: User | null) => void;
    setIsLoggedIn: (isLoggedIn: boolean) => void;
    setIsAdminOrGroupMaster: (isAdminOrGroupMaster: boolean) => void;
}

export default function EmailResetPassword() {
    const navigate = useNavigate();
    const { register } = useForm<LoginDTO>();
    const [showError, setShowError] = useState(false);
    const toast = useToast(); // Use the useToast hook
    const [email, setEmail] = useState("");
    const [emailError, setEmailError] = useState(false);
    const [formSubmitted, setFormSubmitted] = useState(false);

    function onSubmit() {
        setFormSubmitted(true);
        setShowError(false);
        const emailPasswordResetDTO = {
            email: email,
        };
        SendResetPasswordEmail(emailPasswordResetDTO)
            .then(() => {
                showToast(toast, "Success!", "Email sent.", "success");
                navigate("/");
            })
            .catch((err) => {
                if (email == "") {
                    showToast(toast, "Error!", "Please enter an email.", "error");
                } else {
                    showToast(
                        toast,
                        "Error!",
                        err.response ? err.response.data : "One or more validation errors occurred",
                        "error"
                    );
                }
            });
    }

    const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.currentTarget.value.length === 0) setEmailError(true);
        else setEmailError(false);
        setEmail(e.currentTarget.value);
    };

    return (
        <>
            <Container maxW="lg" py={{ base: "12", md: "24" }} px={{ base: "0", sm: "8" }}>
                <Stack spacing="8">
                    <Stack spacing="6">
                        <Image src={logo} width="50px" height="50px" margin="auto" />
                        <Heading textAlign="center" size="lg">
                            Reset your password
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
                                <Text marginBottom={5}>
                                    Enter your email address and we will send you a link to get back into your account
                                </Text>
                                <FormControl isInvalid={emailError && formSubmitted}>
                                    <FormLabel htmlFor="email" fontWeight={"bold"}>
                                        Email address
                                    </FormLabel>
                                    <Input
                                        id="email"
                                        type="email"
                                        {...register("email", {
                                            required: true,
                                            pattern: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                        })}
                                        onChange={handleEmailChange}
                                        value={email}
                                    />
                                </FormControl>
                            </Stack>
                            <Stack spacing="6">
                                <Button
                                    backgroundColor="#932439"
                                    color="white"
                                    _hover={{ bg: "#7A1D2E" }}
                                    type="submit"
                                    onClick={() => onSubmit()}
                                >
                                    Send Link
                                </Button>
                            </Stack>
                        </Stack>
                    </Box>
                </Stack>
            </Container>
        </>
    );
}
