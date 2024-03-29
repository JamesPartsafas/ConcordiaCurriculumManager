import { useContext, useEffect, useState } from "react";
import {
    DossierViewCountDTO,
    DossierViewResponseDTO,
    GetTopDossierViewingUser,
    GetTopHitHttpEndpoints,
    GetTopHttpStatusCodes,
    GetTopViewedDossiers,
    HttpEndpointCountDTO,
    HttpEndpointResponseDTO,
    HttpStatusCountDTO,
    HttpStatusResponseDTO,
    UserDossierViewedCountDTO,
    UserDossierViewedResponseDTO,
} from "../services/metrics";
import { Box, Flex, Spacer, Table, Tbody, Td, Text, Th, Thead, Tr } from "@chakra-ui/react";
import { Button as ChakraButton } from "@chakra-ui/react";

export default function Metrics() {
    const [httpEndpointCount, setHttpEndpointCount] = useState<HttpEndpointCountDTO[]>([]);
    const [httpStatusCount, setHttpStatusCount] = useState<HttpStatusCountDTO[]>([]);
    const [dossierViewCount, setDossierViewCount] = useState<DossierViewCountDTO[]>([]);
    const [userDossierViewedCount, setUserDossierViewedCount] = useState<UserDossierViewedCountDTO[]>([]);
    const [endpointIndex, setEndpointIndex] = useState<number>();
    const [statusIndex, setStatusIndex] = useState<number>();
    const [dossierViewIndex, setDossierViewIndex] = useState<number>();
    const [userDossierIndex, setUserDossierIndex] = useState<number>();

    function getHttpEndpoints(index: number) {
        GetTopHitHttpEndpoints(index)
            .then(
                (res: HttpEndpointResponseDTO) => {
                    setHttpEndpointCount(res.result);
                    setEndpointIndex(res.nextIndex);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    function getDossierViews(index: number) {
        GetTopViewedDossiers(index)
            .then(
                (res: DossierViewResponseDTO) => {
                    setDossierViewCount(res.result);
                    setDossierViewIndex(res.nextIndex);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    function getHttpStatuses(index: number) {
        GetTopHttpStatusCodes(index)
            .then(
                (res: HttpStatusResponseDTO) => {
                    setHttpStatusCount(res.result);
                    setStatusIndex(res.nextIndex);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    function getUserViewedDossiers(index: number) {
        GetTopDossierViewingUser(index)
            .then(
                (res: UserDossierViewedResponseDTO) => {
                    setUserDossierViewedCount(res.result);
                    setUserDossierIndex(res.nextIndex);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    useEffect(() => {
        getHttpEndpoints(0);
        getDossierViews(0);
        getHttpStatuses(0);
        getUserViewedDossiers(0);
        console.log("Initial Data Fetched");
    }, []);

    return (
        <div>
            <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                Available Metrics
            </Text>
            <Box>
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    Http Enpoint Count
                </Text>
                <Table variant="simple" size="md" borderRadius="xl" boxShadow="xl" border="2px">
                    <Thead backgroundColor={"#e2e8f0"}>
                        <Tr>
                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                Endpoint
                            </Th>
                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                FullPath
                            </Th>
                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                Count
                            </Th>
                        </Tr>
                        <Tbody>
                            {httpEndpointCount.slice(endpointIndex, endpointIndex + 5).map((endpoint) => (
                                <Tr>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {endpoint.endpoint}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {endpoint.fullpath}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {endpoint.count}
                                    </Td>
                                </Tr>
                            ))}
                        </Tbody>
                        <Tbody>
                            <Tr>
                                <Td height={20}>
                                    <Flex>
                                        <Text alignSelf="center">
                                            Showing {endpointIndex} to {endpointIndex + 5} of {httpEndpointCount.length}{" "}
                                            results
                                        </Text>
                                        <Spacer />
                                        <ChakraButton
                                            mr={4}
                                            p={4}
                                            variant="outline"
                                            onClick={() => getHttpEndpoints(endpointIndex - 5)}
                                            isDisabled={endpointIndex < 5}
                                        >
                                            Prev
                                        </ChakraButton>
                                        <ChakraButton
                                            p={4}
                                            variant="outline"
                                            onClick={() =>
                                                endpointIndex + 5 < httpEndpointCount.length
                                                    ? getHttpEndpoints(endpointIndex + 5)
                                                    : getHttpEndpoints(httpEndpointCount.length - 1)
                                            }
                                            isDisabled={endpointIndex + 5 >= httpEndpointCount.length}
                                        >
                                            Next
                                        </ChakraButton>
                                    </Flex>
                                </Td>
                            </Tr>
                        </Tbody>
                    </Thead>
                </Table>
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    Http Status Count
                </Text>
                <Table variant="simple" size="md" borderRadius="xl" boxShadow="xl" border="2px">
                    <Thead backgroundColor={"#e2e8f0"}>
                        <Tr>
                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                Status
                            </Th>

                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                Count
                            </Th>
                        </Tr>
                        <Tbody>
                            {httpStatusCount.slice(statusIndex, statusIndex + 5).map((status) => (
                                <Tr>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {status.httpstatus}
                                    </Td>

                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {status.count}
                                    </Td>
                                </Tr>
                            ))}
                        </Tbody>
                        <Tbody>
                            <Tr>
                                <Td height={20}>
                                    <Flex>
                                        <Text alignSelf="center">
                                            Showing {statusIndex} to {statusIndex + 5} of {httpEndpointCount.length}{" "}
                                            results
                                        </Text>
                                        <Spacer />
                                        <ChakraButton
                                            mr={4}
                                            p={4}
                                            variant="outline"
                                            onClick={() => getHttpStatuses(statusIndex - 5)}
                                            isDisabled={statusIndex < 5}
                                        >
                                            Prev
                                        </ChakraButton>
                                        <ChakraButton
                                            p={4}
                                            variant="outline"
                                            onClick={() =>
                                                statusIndex + 5 < httpEndpointCount.length
                                                    ? getHttpStatuses(statusIndex + 5)
                                                    : getHttpStatuses(httpEndpointCount.length - 1)
                                            }
                                            isDisabled={statusIndex + 5 >= httpEndpointCount.length}
                                        >
                                            Next
                                        </ChakraButton>
                                    </Flex>
                                </Td>
                            </Tr>
                        </Tbody>
                    </Thead>
                </Table>
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    Dossier Views
                </Text>
                <Table variant="simple" size="md" borderRadius="xl" boxShadow="xl" border="2px">
                    <Thead backgroundColor={"#e2e8f0"}>
                        <Tr>
                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                Endpoint
                            </Th>
                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                FullPath
                            </Th>
                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                Count
                            </Th>
                        </Tr>
                        <Tbody>
                            {httpEndpointCount.slice(endpointIndex, endpointIndex + 5).map((endpoint) => (
                                <Tr>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {endpoint.endpoint}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {endpoint.fullpath}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {endpoint.count}
                                    </Td>
                                </Tr>
                            ))}
                        </Tbody>
                        <Tbody>
                            <Tr>
                                <Td height={20}>
                                    <Flex>
                                        <Text alignSelf="center">
                                            Showing {endpointIndex} to {endpointIndex + 5} of {httpEndpointCount.length}{" "}
                                            results
                                        </Text>
                                        <Spacer />
                                        <ChakraButton
                                            mr={4}
                                            p={4}
                                            variant="outline"
                                            onClick={() => getHttpEndpoints(endpointIndex - 5)}
                                            isDisabled={endpointIndex < 5}
                                        >
                                            Prev
                                        </ChakraButton>
                                        <ChakraButton
                                            p={4}
                                            variant="outline"
                                            onClick={() =>
                                                endpointIndex + 5 < httpEndpointCount.length
                                                    ? getHttpEndpoints(endpointIndex + 5)
                                                    : getHttpEndpoints(httpEndpointCount.length - 1)
                                            }
                                            isDisabled={endpointIndex + 5 >= httpEndpointCount.length}
                                        >
                                            Next
                                        </ChakraButton>
                                    </Flex>
                                </Td>
                            </Tr>
                        </Tbody>
                    </Thead>
                </Table>
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    Users with Viewed Dossiers
                </Text>
                <Table variant="simple" size="md" borderRadius="xl" boxShadow="xl" border="2px">
                    <Thead backgroundColor={"#e2e8f0"}>
                        <Tr>
                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                Endpoint
                            </Th>
                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                FullPath
                            </Th>
                            <Th whiteSpace="nowrap" textAlign={"center"}>
                                Count
                            </Th>
                        </Tr>
                        <Tbody>
                            {httpEndpointCount.slice(endpointIndex, endpointIndex + 5).map((endpoint) => (
                                <Tr>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {endpoint.endpoint}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {endpoint.fullpath}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {endpoint.count}
                                    </Td>
                                </Tr>
                            ))}
                        </Tbody>
                        <Tbody>
                            <Tr>
                                <Td height={20}>
                                    <Flex>
                                        <Text alignSelf="center">
                                            Showing {endpointIndex} to {endpointIndex + 5} of {httpEndpointCount.length}{" "}
                                            results
                                        </Text>
                                        <Spacer />
                                        <ChakraButton
                                            mr={4}
                                            p={4}
                                            variant="outline"
                                            onClick={() => getHttpEndpoints(endpointIndex - 5)}
                                            isDisabled={endpointIndex < 5}
                                        >
                                            Prev
                                        </ChakraButton>
                                        <ChakraButton
                                            p={4}
                                            variant="outline"
                                            onClick={() =>
                                                endpointIndex + 5 < httpEndpointCount.length
                                                    ? getHttpEndpoints(endpointIndex + 5)
                                                    : getHttpEndpoints(httpEndpointCount.length - 1)
                                            }
                                            isDisabled={endpointIndex + 5 >= httpEndpointCount.length}
                                        >
                                            Next
                                        </ChakraButton>
                                    </Flex>
                                </Td>
                            </Tr>
                        </Tbody>
                    </Thead>
                </Table>
            </Box>
        </div>
    );
}
