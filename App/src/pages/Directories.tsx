import { useEffect, useState } from "react";
import { UserDTO } from "../models/user";
import { searchUsersByEmail, searchUsersByFirstname, searchUsersByLastname } from "../services/user";
import {
    Box,
    FormControl,
    FormLabel,
    Heading,
    IconButton,
    Input,
    Table,
    TableContainer,
    Tbody,
    Td,
    Th,
    Thead,
    Tr,
    Text,
} from "@chakra-ui/react";
import { useForm } from "react-hook-form";
import Button from "../components/Button";
import { EmailIcon, PhoneIcon } from "@chakra-ui/icons";

interface UserSeachDTO {
    firstName: string;
    lastName: string;
    email: string;
}

export default function Directories() {
    const [users, setUsers] = useState<UserDTO[]>([]);

    const { register, handleSubmit } = useForm<UserSeachDTO>();

    useEffect(() => {}, []);

    function getUsersByFirstName(firstName: string) {
        searchUsersByFirstname(firstName)
            .then(
                (response) => {
                    setUsers((prev) => {
                        const newUsers = response.data.filter(
                            (user) => !prev.some((prevUser) => prevUser.id === user.id)
                        );
                        return [...prev, ...newUsers];
                    });
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    function getUsersByLastName(lastName: string) {
        searchUsersByLastname(lastName)
            .then(
                (response) => {
                    console.log(response.data);
                    setUsers((prev) => {
                        const newUsers = response.data.filter(
                            (user) => !prev.some((prevUser) => prevUser.id === user.id)
                        );
                        return [...prev, ...newUsers];
                    });
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    function getUsersByEmail(email: string) {
        searchUsersByEmail(email)
            .then(
                (response) => {
                    console.log(response.data);
                    setUsers((prev) => {
                        const newUsers = response.data.filter(
                            (user) => !prev.some((prevUser) => prevUser.id === user.id)
                        );
                        return [...prev, ...newUsers];
                    });
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    function onSubmit(data: UserSeachDTO) {
        setUsers([]);
        if (data.firstName) {
            getUsersByFirstName(data.firstName);
        }
        if (data.lastName) {
            getUsersByLastName(data.lastName);
        }
        if (data.email) {
            getUsersByEmail(data.email);
        }
        if (!data.firstName && !data.lastName && !data.email) {
            setUsers(null);
        }
    }

    return (
        <>
            <Box width={"70%"} margin={"auto"} marginTop={10}>
                <Heading color={"brandRed"} mb={5}>
                    People Search
                </Heading>

                <form onSubmit={handleSubmit(onSubmit)}>
                    <FormControl>
                        <Box display={"flex"} mb={3}>
                            <FormLabel htmlFor="firstName" flexBasis={"10%"}>
                                First Name
                            </FormLabel>
                            <Input
                                id="firstName"
                                type="text"
                                {...register("firstName", {})}
                                width={"30%"}
                                flexShrink={"3"}
                            />
                        </Box>

                        <Box display={"flex"} mb={3}>
                            <FormLabel htmlFor="lastName" flexBasis={"10%"}>
                                Last Name
                            </FormLabel>
                            <Input
                                id="lastName"
                                type="text"
                                {...register("lastName", {})}
                                width={"30%"}
                                flexShrink={"3"}
                            />
                        </Box>

                        <Box display={"flex"}>
                            <FormLabel htmlFor="email" flexBasis={"10%"}>
                                Email
                            </FormLabel>
                            <Input id="email" type="text" {...register("email", {})} width={"30%"} flexShrink={"3"} />
                        </Box>

                        <Button style="primary" variant="outline" mt={3} type="submit">
                            Search
                        </Button>
                    </FormControl>
                </form>

                <Box mt={10}>
                    {users?.length === 0 && (
                        <Box backgroundColor={"brandBlue600"} width={"70%"} p={5}>
                            No results match your search criteria. Please try to simplify your search.
                        </Box>
                    )}
                    {users?.length > 0 && (
                        <Box>
                            <Text color={"brandRed"} mb={2} fontWeight={"bold"} fontSize={"lg"}>
                                People seach results
                            </Text>
                            <Text mb={4}>displaying {users.length} result(s)</Text>
                            <TableContainer width={"70%"}>
                                <Table>
                                    <Thead>
                                        <Tr>
                                            <Th>First Name</Th>
                                            <Th>Last Name</Th>
                                            <Th>Email</Th>
                                            <Th>Contact</Th>
                                        </Tr>
                                    </Thead>
                                    <Tbody>
                                        {users.map((user) => (
                                            <Tr key={user.id}>
                                                <Td>{user.firstName}</Td>
                                                <Td>{user.lastName}</Td>
                                                <Td>{user.email}</Td>
                                                <Td>
                                                    <IconButton
                                                        ml={2}
                                                        aria-label="Review"
                                                        icon={<EmailIcon />}
                                                        onClick={() => {
                                                            window.open("mailto:" + user.email);
                                                        }}
                                                    />
                                                    <IconButton
                                                        ml={2}
                                                        aria-label="Review"
                                                        icon={<PhoneIcon />}
                                                        isDisabled={true}
                                                    />
                                                </Td>
                                            </Tr>
                                        ))}
                                    </Tbody>
                                </Table>
                            </TableContainer>
                        </Box>
                    )}
                </Box>
            </Box>
        </>
    );
}
