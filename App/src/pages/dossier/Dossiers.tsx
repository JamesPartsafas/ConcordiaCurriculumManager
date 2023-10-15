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
} from "@chakra-ui/react";
import { Button as ChakraButton } from "@chakra-ui/react";
import { AddIcon, DeleteIcon, EditIcon, InfoIcon } from "@chakra-ui/icons";

import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../App";
import { DossierDTO, GetMyDossiersResponse, getMyDossiers } from "../../services/dossier";
import DossierModal from "./DossierModal";
import React from "react";
import Button from "../../components/Button";
import { UserRoles } from "../../services/user";

export default function Dossiers() {
    const user = useContext(UserContext);

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

    const { isOpen, onOpen, onClose } = useDisclosure();
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
                                type="secondary"
                                variant="outline"
                                width="fit-content"
                                height="40px"
                                ref={cancelRef}
                                onClick={onClose}
                            >
                                Cancel
                            </Button>
                            <Button
                                type="primary"
                                variant="solid"
                                width="fit-content"
                                height="40px"
                                isLoading={loading}
                                loadingText="Deleting"
                                onClick={() => {
                                    deleteDossier(selectedDossier);
                                    onClose();
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

    return (
        <>
            <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                {user?.firstName + "'s"} Dossiers
            </Text>

            <Box maxW="5xl" m="auto">
                <Flex flexDirection="column">
                    <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                        <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "fixed" }}>
                            <Thead backgroundColor={"#e2e8f0"}>
                                <Tr display={"flex"}>
                                    <Th minW={"200px"} maxW={"200px"}>
                                        Title
                                    </Th>
                                    <Th minW={"500px"} maxW={"500px"}>
                                        Description
                                    </Th>
                                    <Th minW={"120px"} maxW={"120px"}>
                                        Published
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
                                            {dossier.published ? "Yes" : "No"}
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
                                                onClick={() => console.log("view Dossier")}
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
                        <Button
                            leftIcon={<AddIcon />}
                            type="primary"
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
