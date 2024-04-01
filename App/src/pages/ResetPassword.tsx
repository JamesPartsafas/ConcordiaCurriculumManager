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
import { useLocation, useNavigate } from "react-router-dom";
import { useState } from "react";
import { showToast } from "../utils/toastUtils";
import { ResetPassword } from "../services/user";

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
    const [password, setPassword] = useState("");
    const [passwordError, setPasswordError] = useState(false);
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [confirmPassword, setConfirmPassword] = useState("");
    const [confirmPasswordError, setConfirmPasswordError] = useState(false);
    const query = useQuery();
    const token = query.get("token");

    function useQuery() {
        return new URLSearchParams(useLocation().search);
    }

    function onSubmit() {
        setFormSubmitted(true);
        setShowError(false);
        setConfirmPasswordError(false);

        if (password !== confirmPassword) {
            setConfirmPasswordError(true);
            showToast(toast, "Error!", "Passwords do not match.", "error");
            return; // Stop the form submission
        }

        const passwordResetDTO = {
            password: password,
        };
        ResetPassword(passwordResetDTO, token)
            .then(() => {
                showToast(toast, "Success!", "Password changed.", "success");
                navigate("/Login");
            })
            .catch((err) => {
                if (password == "") {
                    showToast(toast, "Error!", "Please a valid password.", "error");
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

    const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.currentTarget.value.length === 0) setPasswordError(true);
        else setPasswordError(false);
        setPassword(e.currentTarget.value);
    };

    return (
        <>
            <Container maxW="lg" py={{ base: "12", md: "24" }} px={{ base: "0", sm: "8" }}>
                <Stack spacing="8">
                    <Stack spacing="6">
                        <Image src={logo} width="50px" height="50px" margin="auto" />
                        <Heading textAlign="center" size="lg">
                            Change old password
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
                                <Text marginBottom={5}>Enter your new password</Text>
                                <FormControl isInvalid={passwordError && formSubmitted}>
                                    <FormLabel htmlFor="password" fontWeight={"bold"}>
                                        New Password
                                    </FormLabel>
                                    <Input
                                        id="password"
                                        type="password"
                                        {...register("password", {
                                            required: true,
                                            pattern: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                        })}
                                        onChange={handlePasswordChange}
                                        value={password}
                                    />
                                </FormControl>
                                <FormControl isInvalid={confirmPasswordError && formSubmitted}>
                                    <FormLabel htmlFor="confirmPassword" fontWeight={"bold"}>
                                        Confirm New Password
                                    </FormLabel>
                                    <Input
                                        id="confirmPassword"
                                        type="password"
                                        onChange={(e) => {
                                            if (e.currentTarget.value.length === 0) setConfirmPasswordError(true);
                                            else setConfirmPasswordError(false);
                                            setConfirmPassword(e.currentTarget.value);
                                        }}
                                        value={confirmPassword}
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
                                    Save
                                </Button>
                            </Stack>
                        </Stack>
                    </Box>
                </Stack>
            </Container>
        </>
    );
}
