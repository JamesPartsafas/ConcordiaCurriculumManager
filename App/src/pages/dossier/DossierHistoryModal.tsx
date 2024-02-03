import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    ModalCloseButton,
} from "@chakra-ui/react";
import { Table, Thead, Tbody, Tr, Th, Td, TableContainer } from "@chakra-ui/react";
import Button from "../../components/Button";
import { ApprovalHistoryDTO } from "../../models/dossier";
import { ArrowBackIcon, ArrowForwardIcon, CheckCircleIcon, DeleteIcon } from "@chakra-ui/icons";

interface DossierModalProps {
    open: boolean;
    closeModal: () => void;
    approvalHistories: ApprovalHistoryDTO[];
}

export default function DossierModal(props: DossierModalProps) {
    function displayAction(action) {
        switch (action) {
            case 0:
                return (
                    <>
                        <ArrowForwardIcon color="brandBlue" /> Forwarded
                    </>
                );
            case 1:
                return (
                    <>
                        <ArrowBackIcon color="brandGray" /> Returned
                    </>
                );
            case 2:
                return (
                    <>
                        <DeleteIcon color="brandRed" /> Rejected
                    </>
                );
            case 3:
                return (
                    <>
                        <CheckCircleIcon color="green" /> Accepted
                    </>
                );
        }
    }

    return (
        <>
            <Modal size={"5xl"} isOpen={props.open} onClose={props.closeModal}>
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>History Log</ModalHeader>
                    <ModalCloseButton />

                    <ModalBody>
                        <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                            <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                                <Thead backgroundColor={"#e2e8f0"}>
                                    <Tr display={"flex"}>
                                        <Th minW={"200px"} maxW={"300px"}>
                                            Date
                                        </Th>
                                        <Th minW={"300px"} maxW={"300px"}>
                                            Group
                                        </Th>
                                        <Th minW={"300px"} maxW={"300px"}>
                                            User
                                        </Th>
                                        <Th minW={"100px"} maxW={"200px"}>
                                            Action
                                        </Th>
                                    </Tr>
                                </Thead>
                                <Tbody>
                                    {props.approvalHistories
                                        .sort((a, b) => a.orderIndex - b.orderIndex)
                                        .map((history, idx) => (
                                            <Tr key={idx} display={"flex"}>
                                                <Td minW={"200px"} maxW={"300px"}>
                                                    {history.createdDate.toString().substring(0, 10)} {""}
                                                    {new Date(history.createdDate).getHours().toString()}:
                                                    {new Date(history.createdDate).getMinutes().toString()}:
                                                    {new Date(history.createdDate).getSeconds().toString()}
                                                </Td>
                                                <Td minW={"300px"} maxW={"300px"}>
                                                    {history.group.name}
                                                </Td>
                                                <Td minW={"300px"} maxW={"300px"}>
                                                    {history.user.firstName} {history.user.lastName}
                                                </Td>
                                                <Td minW={"100px"} maxW={"200px"}>
                                                    {displayAction(history.action)}
                                                </Td>
                                            </Tr>
                                        ))}
                                </Tbody>
                            </Table>
                        </TableContainer>
                    </ModalBody>

                    <ModalFooter>
                        <Button
                            style="primary"
                            variant="outline"
                            width="fit-content"
                            height="40px"
                            mr={3}
                            onClick={props.closeModal}
                        >
                            Close
                        </Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    );
}
