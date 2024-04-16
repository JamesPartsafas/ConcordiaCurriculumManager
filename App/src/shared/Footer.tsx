import {
    Box,
    Flex,
    Text,
    IconButton,
    Stack,
    Collapse,
    Icon,
    Popover,
    PopoverTrigger,
    PopoverContent,
    useColorModeValue,
    useBreakpointValue,
    useDisclosure,
    useToast,
    Image,
} from "@chakra-ui/react";
import Button from "../components/Button";
import logo from "../assets/logo.png";
import { HamburgerIcon, CloseIcon, ChevronDownIcon, ChevronRightIcon } from "@chakra-ui/icons";
import { useNavigate } from "react-router-dom";
import { logout } from "../services/auth";
import { BaseRoutes } from "../constants";
import { User } from "../models/user";
import { showToast } from "../utils/toastUtils";
import { Link } from "react-router-dom";

export interface FooterProps {
    setUser: (user: User | null) => void;
    setIsLoggedIn: (isLoggedIn: boolean) => void;
    setIsAdminOrGroupMaster: (isAdminOrGroupMaster: boolean) => void;
}

export default function Footer(props: FooterProps) {
    const { isOpen, onToggle } = useDisclosure();
    const navigate = useNavigate();
    const toast = useToast(); // Use the useToast hook

    function logOut() {
        logout().then(
            () => {
                props.setUser(null);
                props.setIsLoggedIn(false);
                props.setIsAdminOrGroupMaster(false);
                navigate(BaseRoutes.Login);
                showToast(toast, "Success!", "You have successfully logged out.", "success");
            },
            (rej) => {
                showToast(toast, "Error!", rej.message, "error");
            }
        );
    }

    return (
        <Box className="non-printable-content">
            <Flex
                position="fixed"
                bottom={0}
                width="100%"
                bg={useColorModeValue("white", "gray.800")}
                color={useColorModeValue("gray.600", "white")}
                minH={"10px"}
                py={{ base: 2 }}
                px={{ base: 4 }}
                borderTop={1}
                borderStyle={"solid"}
                borderColor={useColorModeValue("gray.200", "gray.900")}
                align={"center"}
                justifyContent="space-between"
            >
                <footer>
                    <Flex flex={{ base: 1 }} justify={{ base: "center", md: "start" }}>
                        <Text
                            textAlign={useBreakpointValue({ base: "center", md: "left" })}
                            fontFamily={"heading"}
                            color={useColorModeValue("gray.800", "white")}
                        >
                            Useful links :  
                        </Text>
                        <Flex direction="column">
                            <Link>Link 1</Link>
                            <Link>Link 2</Link>
                        </Flex>
                    </Flex>
                </footer>
                <Text>Copyright © 2024 JamesPart</Text>
            </Flex>
        </Box>
    );
}