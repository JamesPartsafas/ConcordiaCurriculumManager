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
    Button,
    useDisclosure,
} from "@chakra-ui/react";
import { DeleteIcon, EditIcon, InfoIcon } from "@chakra-ui/icons";

import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../App";
import { DossierDTO, GetMyDossiersResponse, getMyDossiers } from "../../services/dossier";
import EditDossierModal from "./EditDossierModal";
import React from "react";

export default function Dossiers() {
    const user = useContext(UserContext);

    const [myDossiers, setMyDossiers] = useState<DossierDTO[]>([]);
    const [showEditDossierModal, setShowEditDossierModal] = useState<boolean>(false);
    const [selectedDossier, setSelectedDossier] = useState<DossierDTO | null>(null);

    const { isOpen, onOpen, onClose } = useDisclosure();
    const cancelRef = React.useRef();

    useEffect(() => {
        getAllDossiers();
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
        console.log(dossier);
    }

    function displayEditDossierModal() {
        setShowEditDossierModal(true);
    }

    function closeEditDossierModal() {
        setShowEditDossierModal(false);
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
                            Are you sure you want to delete <b>{selectedDossier.title}</b>? You can't undo this action
                            afterwards.
                        </AlertDialogBody>

                        <AlertDialogFooter>
                            <Button ref={cancelRef} onClick={onClose}>
                                Cancel
                            </Button>
                            <Button
                                colorScheme="red"
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

            <TableContainer borderRadius="xl" boxShadow="xl" maxW="5xl" m="auto" border="2px">
                <Table variant="simple" style={{ backgroundColor: "white" }}>
                    <Thead backgroundColor={"#e2e8f0"}>
                        <Tr display={"flex"}>
                            <Th flex={"2"}>Title</Th>
                            <Th flex={"4"}>Description</Th>
                            <Th flex={"1"}>Published</Th>
                            <Th flex={"1"}></Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        {myDossiers.map((dossier) => (
                            <Tr key={dossier.id} display={"flex"}>
                                <Td flex={"2"}>{dossier.title}</Td>
                                <Td flex={"4"}>
                                    <Text overflow="hidden" textOverflow="ellipsis" maxW={"300px"}>
                                        {dossier.description}
                                    </Text>
                                </Td>
                                <Td flex={"1"}>{dossier.published ? "Yes" : "No"}</Td>

                                <Td flex={"1"}>
                                    <IconButton
                                        aria-label="Delete"
                                        icon={<DeleteIcon />}
                                        backgroundColor={"#932439"}
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
                                        onClick={() => {
                                            setSelectedDossier(dossier);
                                            displayEditDossierModal();
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
                </Table>
            </TableContainer>
            {deleteAlertDialog()}
            {/* this is the Dossier Modal */}
            {showEditDossierModal && (
                <EditDossierModal
                    dossier={selectedDossier}
                    open={showEditDossierModal}
                    closeModal={closeEditDossierModal}
                />
            )}
        </>
    );
}
