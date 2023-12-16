import {
    Table,
    Thead,
    Tbody,
    Text,
    Tr,
    Th,
    Td,
    TableContainer,
    IconButton,
    AlertDialog,
    AlertDialogOverlay,
    AlertDialogContent,
    AlertDialogHeader,
    AlertDialogBody,
    AlertDialogFooter,
    useDisclosure,
    Tfoot,
    Spacer,
    Flex,
    Box,
    Tooltip,
    useToast,
} from "@chakra-ui/react";
import { Button as ChakraButton, Container } from "@chakra-ui/react";
import { AddIcon, DeleteIcon, EditIcon, InfoIcon } from "@chakra-ui/icons";

import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../App";
import { deleteDossierById, getMyDossiers } from "../../services/dossier";
import DossierModal from "./DossierModal";
import React from "react";
import Button from "../../components/Button";
import { UserRoles } from "../../models/user";
import { showToast } from "../../utils/toastUtils";
import { DossierDTO, GetMyDossiersResponse, dossierStateToString } from "../../models/dossier";
import { BaseRoutes } from "../../constants";
import { useNavigate } from "react-router-dom";

export default function Dossiers() {
    const user = useContext(UserContext);
    const toast = useToast(); // Use the useToast hook
    const { isOpen, onOpen, onClose } = useDisclosure();
    const navigate = useNavigate();

    const [myDossiers, setMyDossiers] = useState<DossierDTO[]>([]);
    const [showDossierModal, setShowDossierModal] = useState<boolean>(false);
    const [selectedDossier, setSelectedDossier] = useState<DossierDTO | null>(null);
    const [dossierModalAction, setDossierModalTitle] = useState<"add" | "edit">();
    const [loading, setLoading] = useState<boolean>(false);

    const [currentPage, setCurrentPage] = useState<number>(1);
    const resultsPerPage = 5;
    const totalResults = myDossiers.length;

    const startIndex = myDossiers.length === 0 ? 0 : (currentPage - 1) * resultsPerPage + 1;
    const endIndex = Math.min(currentPage * resultsPerPage, totalResults);

    const cancelRef = React.useRef();

    useEffect(() => {
        getAllDossiers();
        console.log(user);
    }, []);

    function getAllDossiers() {
        getMyDossiers()
            .then(
                (res: GetMyDossiersResponse) => {
                    setMyDossiers(res.data);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    // endpoint not implemented yet
    function deleteDossier(dossier: DossierDTO) {
        setLoading(true);
        console.log(dossier);
        deleteDossierById(dossier.id).then(
            () => {
                myDossiers.splice(myDossiers.indexOf(dossier), 1);
                showToast(toast, "Success!", "Dossier deleted.", "success");
                setLoading(false);
                onClose();
            },
            () => {
                showToast(toast, "Error!", "Dossier not deleted.", "error");
                setLoading(false);
                onClose();
            }
        );
    }

    function displayDossierModal() {
        setShowDossierModal(true);
    }

    function closeDossierModal() {
        setShowDossierModal(false);
    }

    function deleteAlertDialog() {
        return (
            <AlertDialog isOpen={isOpen} leastDestructiveRef={cancelRef} onClose={onClose}>
                <AlertDialogOverlay>
                    <AlertDialogContent>
                        <AlertDialogHeader fontSize="lg" fontWeight="bold">
                            Delete Dossier
                        </AlertDialogHeader>

                        <AlertDialogBody>
                            Are you sure you want to delete <b>{selectedDossier?.title}</b>? You can&apos;t undo this
                            action afterwards.
                        </AlertDialogBody>

                        <AlertDialogFooter>
                            <Button
                                style="secondary"
                                variant="outline"
                                width="fit-content"
                                height="40px"
                                ref={cancelRef}
                                onClick={onClose}
                            >
                                Cancel
                            </Button>
                            <Button
                                style="primary"
                                variant="solid"
                                width="fit-content"
                                height="40px"
                                isLoading={loading}
                                loadingText="Deleting"
                                onClick={() => {
                                    deleteDossier(selectedDossier);
                                    // onClose();
                                }}
                                ml={3}
                            >
                                Delete
                            </Button>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialogOverlay>
            </AlertDialog>
        );
    }

    function handleNavigateToDossierDetails(dossierId: string) {
        navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId));
    }

    return (
        <>
            <Container maxW={"5xl"} mt={5}>
                <Button
                    style="primary"
                    variant="outline"
                    height="40px"
                    width="100px"
                    onClick={() => navigate(BaseRoutes.Home)}
                >
                    Back
                </Button>
            </Container>
            <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                {user?.firstName + "'s"} Dossiers
            </Text>

            <Box maxW="5xl" m="auto">
                <Flex flexDirection="column">
                    <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                        <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                            <Thead backgroundColor={"#e2e8f0"}>
                                <Tr display={"flex"}>
                                    <Th minW={"200px"} maxW={"200px"}>
                                        Title
                                    </Th>
                                    <Th minW={"500px"} maxW={"500px"}>
                                        Description
                                    </Th>
                                    <Th minW={"120px"} maxW={"120px"}>
                                        State
                                    </Th>
                                    <Th width={"25%"}></Th>
                                </Tr>
                            </Thead>
                            <Tbody>
                                {myDossiers.slice(startIndex - 1, endIndex).map((dossier) => (
                                    <Tr key={dossier.id} display={"flex"}>
                                        <Td minW={"200px"} maxW={"200px"}>
                                            {dossier.title}
                                        </Td>
                                        <Td minW={"500px"} maxW={"500px"}>
                                            <Text overflow="hidden" textOverflow="ellipsis" maxW={"500px"}>
                                                {dossier.description}
                                            </Text>
                                        </Td>
                                        <Td minW={"120px"} maxW={"120px"}>
                                            {dossierStateToString(dossier)}
                                        </Td>

                                        <Td width={"25%"}>
                                            <IconButton
                                                aria-label="Delete"
                                                icon={<DeleteIcon />}
                                                backgroundColor={"#932439"}
                                                color={"white"}
                                                onClick={() => {
                                                    setSelectedDossier(dossier);
                                                    onOpen();
                                                }}
                                            />
                                            <IconButton
                                                ml={2}
                                                aria-label="Edit"
                                                icon={<EditIcon />}
                                                backgroundColor={"#0072a8"}
                                                color={"white"}
                                                onClick={() => {
                                                    setSelectedDossier(dossier);
                                                    setDossierModalTitle("edit");
                                                    displayDossierModal();
                                                }}
                                            />
                                            <IconButton
                                                ml={2}
                                                aria-label="Edit"
                                                icon={<InfoIcon />}
                                                onClick={() => {
                                                    setSelectedDossier(dossier);
                                                    handleNavigateToDossierDetails(dossier.id);
                                                }}
                                            />
                                        </Td>
                                    </Tr>
                                ))}
                            </Tbody>
                            <Tfoot>
                                <Tr>
                                    <Td height={20}>
                                        <Flex>
                                            <Text alignSelf="center">
                                                Showing {startIndex} to {endIndex} of {myDossiers.length} results
                                            </Text>
                                            <Spacer />
                                            <ChakraButton
                                                mr={4}
                                                p={4}
                                                variant="outline"
                                                onClick={() => setCurrentPage(currentPage - 1)}
                                                isDisabled={startIndex == 1}
                                            >
                                                Previous
                                            </ChakraButton>
                                            <ChakraButton
                                                p={4}
                                                variant="outline"
                                                onClick={() => setCurrentPage(currentPage + 1)}
                                                isDisabled={endIndex == totalResults}
                                            >
                                                Next
                                            </ChakraButton>
                                        </Flex>
                                    </Td>
                                </Tr>
                            </Tfoot>
                        </Table>
                    </TableContainer>

                    <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                        {user?.firstName + "'s"} Dossiers Under Review
                    </Text>

                    <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                        <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                            <Thead backgroundColor={"#e2e8f0"}>
                                <Tr display={"flex"}>
                                    <Th minW={"200px"} maxW={"200px"}>
                                        Title
                                    </Th>
                                    <Th minW={"500px"} maxW={"500px"}>
                                        Description
                                    </Th>
                                    <Th minW={"120px"} maxW={"120px"}>
                                        State
                                    </Th>
                                    <Th width={"25%"}></Th>
                                </Tr>
                            </Thead>
                            <Tbody>
                                {myDossiers.slice(startIndex - 1, endIndex).map((dossier) => (
                                    <Tr key={dossier.id} display={"flex"}>
                                        <Td minW={"200px"} maxW={"200px"}>
                                            {dossier.title}
                                        </Td>
                                        <Td minW={"500px"} maxW={"500px"}>
                                            <Text overflow="hidden" textOverflow="ellipsis" maxW={"500px"}>
                                                {dossier.description}
                                            </Text>
                                        </Td>
                                        <Td minW={"120px"} maxW={"120px"}>
                                            {dossierStateToString(dossier)}
                                        </Td>

                                        <Td width={"25%"}>
                                            <IconButton
                                                aria-label="Delete"
                                                icon={<DeleteIcon />}
                                                backgroundColor={"#932439"}
                                                color={"white"}
                                                onClick={() => {
                                                    setSelectedDossier(dossier);
                                                    onOpen();
                                                }}
                                            />
                                            <IconButton
                                                ml={2}
                                                aria-label="Edit"
                                                icon={<EditIcon />}
                                                backgroundColor={"#0072a8"}
                                                color={"white"}
                                                onClick={() => {
                                                    setSelectedDossier(dossier);
                                                    setDossierModalTitle("edit");
                                                    displayDossierModal();
                                                }}
                                            />
                                            <IconButton
                                                ml={2}
                                                aria-label="Edit"
                                                icon={<InfoIcon />}
                                                onClick={() => {
                                                    setSelectedDossier(dossier);
                                                    handleNavigateToDossierDetails(dossier.id);
                                                }}
                                            />
                                        </Td>
                                    </Tr>
                                ))}
                            </Tbody>
                            <Tfoot>
                                <Tr>
                                    <Td height={20}>
                                        <Flex>
                                            <Text alignSelf="center">
                                                Showing {startIndex} to {endIndex} of {myDossiers.length} results
                                            </Text>
                                            <Spacer />
                                            <ChakraButton
                                                mr={4}
                                                p={4}
                                                variant="outline"
                                                onClick={() => setCurrentPage(currentPage - 1)}
                                                isDisabled={startIndex == 1}
                                            >
                                                Previous
                                            </ChakraButton>
                                            <ChakraButton
                                                p={4}
                                                variant="outline"
                                                onClick={() => setCurrentPage(currentPage + 1)}
                                                isDisabled={endIndex == totalResults}
                                            >
                                                Next
                                            </ChakraButton>
                                        </Flex>
                                    </Td>
                                </Tr>
                            </Tfoot>
                        </Table>
                    </TableContainer>

                    <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                        Dossiers Requiring {user?.firstName + "'s"} Review
                    </Text>

                    <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                        <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                            <Thead backgroundColor={"#e2e8f0"}>
                                <Tr display={"flex"}>
                                    <Th minW={"200px"} maxW={"200px"}>
                                        Title
                                    </Th>
                                    <Th minW={"500px"} maxW={"500px"}>
                                        Description
                                    </Th>
                                    <Th minW={"120px"} maxW={"120px"}>
                                        State
                                    </Th>
                                    <Th width={"25%"}></Th>
                                </Tr>
                            </Thead>
                            <Tbody>
                                {myDossiers.slice(startIndex - 1, endIndex).map((dossier) => (
                                    <Tr key={dossier.id} display={"flex"}>
                                        <Td minW={"200px"} maxW={"200px"}>
                                            {dossier.title}
                                        </Td>
                                        <Td minW={"500px"} maxW={"500px"}>
                                            <Text overflow="hidden" textOverflow="ellipsis" maxW={"500px"}>
                                                {dossier.description}
                                            </Text>
                                        </Td>
                                        <Td minW={"120px"} maxW={"120px"}>
                                            {dossierStateToString(dossier)}
                                        </Td>

                                        <Td width={"25%"}>
                                            <IconButton
                                                aria-label="Delete"
                                                icon={<DeleteIcon />}
                                                backgroundColor={"#932439"}
                                                color={"white"}
                                                onClick={() => {
                                                    setSelectedDossier(dossier);
                                                    onOpen();
                                                }}
                                            />
                                            <IconButton
                                                ml={2}
                                                aria-label="Edit"
                                                icon={<EditIcon />}
                                                backgroundColor={"#0072a8"}
                                                color={"white"}
                                                onClick={() => {
                                                    setSelectedDossier(dossier);
                                                    setDossierModalTitle("edit");
                                                    displayDossierModal();
                                                }}
                                            />
                                            <IconButton
                                                ml={2}
                                                aria-label="Edit"
                                                icon={<InfoIcon />}
                                                onClick={() => {
                                                    setSelectedDossier(dossier);
                                                    handleNavigateToDossierDetails(dossier.id);
                                                }}
                                            />
                                        </Td>
                                    </Tr>
                                ))}
                            </Tbody>
                            <Tfoot>
                                <Tr>
                                    <Td height={20}>
                                        <Flex>
                                            <Text alignSelf="center">
                                                Showing {startIndex} to {endIndex} of {myDossiers.length} results
                                            </Text>
                                            <Spacer />
                                            <ChakraButton
                                                mr={4}
                                                p={4}
                                                variant="outline"
                                                onClick={() => setCurrentPage(currentPage - 1)}
                                                isDisabled={startIndex == 1}
                                            >
                                                Previous
                                            </ChakraButton>
                                            <ChakraButton
                                                p={4}
                                                variant="outline"
                                                onClick={() => setCurrentPage(currentPage + 1)}
                                                isDisabled={endIndex == totalResults}
                                            >
                                                Next
                                            </ChakraButton>
                                        </Flex>
                                    </Td>
                                </Tr>
                            </Tfoot>
                        </Table>
                    </TableContainer>

                    <Tooltip
                        label="Only Initiators can create dossiers"
                        isDisabled={user.roles.includes(UserRoles.Initiator)}
                    >
                        <span style={{ alignSelf: "flex-end", width: "fit-content", height: "fit-content" }}>
                            <Button
                                leftIcon={<AddIcon />}
                                style="primary"
                                variant="solid"
                                width="100px"
                                height="40px"
                                mt="2"
                                alignSelf="flex-end"
                                isDisabled={!user.roles.includes(UserRoles.Initiator)}
                                onClick={() => {
                                    setSelectedDossier(null);
                                    setDossierModalTitle("add");
                                    displayDossierModal();
                                }}
                            >
                                Add
                            </Button>
                        </span>
                    </Tooltip>
                </Flex>
            </Box>

            {deleteAlertDialog()}
            {/* this is the Dossier Modal */}
            {showDossierModal && (
                <DossierModal
                    action={dossierModalAction}
                    dossier={selectedDossier}
                    dossierList={myDossiers}
                    open={showDossierModal}
                    closeModal={closeDossierModal}
                />
            )}
        </>
    );
}
