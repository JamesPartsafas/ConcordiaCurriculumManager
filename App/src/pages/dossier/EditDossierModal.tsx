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
    FormLabel,
    Input,
    Textarea,
} from "@chakra-ui/react";
import { DossierDTO } from "../../services/dossier";
import { useForm } from "react-hook-form";

interface DossierModalProps {
    open: boolean;
    dossier: DossierDTO;
    closeModal: () => void;
}

interface EditDossierForm {
    title: string;
    description: string;
}

export default function EditDossierModal(props: DossierModalProps) {
    const {
        register,
        handleSubmit,
        formState: { isDirty },
    } = useForm<EditDossierForm>({
        defaultValues: {
            title: props.dossier.title,
            description: props.dossier.description,
        },
    });

    //this should call the edit dossier endpoint
    function onSubmit(data: EditDossierForm) {
        console.log(data);
    }

    return (
        <>
            <form onSubmit={handleSubmit(onSubmit)}>
                <Modal isOpen={props.open} onClose={props.closeModal}>
                    <ModalOverlay />
                    <ModalContent>
                        <ModalHeader>Edit Dossier</ModalHeader>
                        <ModalCloseButton />

                        <ModalBody>
                            <FormLabel htmlFor="title">Title</FormLabel>
                            <Input
                                id="title"
                                type="text"
                                {...register("title", {
                                    required: true,
                                })}
                                defaultValue={props.dossier.title}
                            />
                            <FormLabel htmlFor="title">Description</FormLabel>
                            <Textarea
                                id="description"
                                {...register("description", {
                                    required: true,
                                })}
                                defaultValue={props.dossier.description}
                            />
                        </ModalBody>

                        <ModalFooter>
                            <Button colorScheme="blue" mr={3} onClick={props.closeModal}>
                                Close
                            </Button>

                            <Button variant="ghost" isDisabled={!isDirty}>
                                Save
                            </Button>
                        </ModalFooter>
                    </ModalContent>
                </Modal>
            </form>
        </>
    );
}
