//for modal
import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    ModalCloseButton,
    Button,
    Text,
} from "@chakra-ui/react";
import { DossierDTO } from "../../services/dossier";

interface DossierModalProps {
    open: boolean;
    dossier: DossierDTO;
    closeModal: () => void;
}

export default function DossierModal(props: DossierModalProps) {
    console.log(props.dossier);
    return (
        <>
            <Modal isOpen={props.open} onClose={props.closeModal}>
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>Dossier</ModalHeader>
                    <ModalCloseButton />
                    <ModalBody>
                        <Text>ID: {props.dossier.id}</Text>
                        <Text>Title: {props.dossier.title}</Text>
                        <Text>Description: {props.dossier.description}</Text>
                        <Text>Published: {props.dossier.published ? "Yes" : "No"}</Text>
                    </ModalBody>

                    <ModalFooter>
                        <Button colorScheme="blue" mr={3} onClick={props.closeModal}>
                            Close
                        </Button>
                        <Button variant="ghost">Secondary Action</Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    );
}
