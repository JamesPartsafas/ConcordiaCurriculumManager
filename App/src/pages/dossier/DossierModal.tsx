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
import { DossierDTO, createDossierForUser } from "../../services/dossier";
import { useForm } from "react-hook-form";

interface DossierModalProps {
    open: boolean;
    dossier?: DossierDTO;
    modalTitle: string;
    dossierList?: DossierDTO[];
    closeModal: () => void;
}

interface DossierForm {
    title: string;
    description: string;
}

export default function DossierModal(props: DossierModalProps) {
    const {
        register,
        handleSubmit,
        formState: { isDirty, isValid },
    } = useForm<DossierForm>({
        defaultValues: {
            title: props.dossier?.title,
            description: props.dossier?.description,
        },
    });

    //this should call the edit or add dossier endpoint
    function onSubmit(data: DossierForm) {
        createDossierForUser(data).then(
            (res) => {
                props.dossierList?.push(res.data);
                props.closeModal();
                //display success Toast
            },
            () => {
                //display error Toast
            }
        );
    }

    return (
        <>
            <Modal isOpen={props.open} onClose={props.closeModal}>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <ModalOverlay />
                    <ModalContent>
                        <ModalHeader>{props.modalTitle}</ModalHeader>
                        <ModalCloseButton />

                        <ModalBody>
                            <FormLabel htmlFor="title">Title</FormLabel>
                            <Input
                                id="title"
                                type="text"
                                {...register("title", {
                                    required: true,
                                })}
                                defaultValue={props.dossier?.title}
                            />
                            <FormLabel htmlFor="description">Description</FormLabel>
                            <Textarea
                                id="description"
                                {...register("description", {
                                    required: true,
                                })}
                                defaultValue={props.dossier?.description}
                            />
                        </ModalBody>

                        <ModalFooter>
                            <Button colorScheme="blue" mr={3} onClick={props.closeModal}>
                                Close
                            </Button>

                            <Button type="submit" variant="ghost" isDisabled={!isDirty || !isValid}>
                                Save
                            </Button>
                        </ModalFooter>
                    </ModalContent>
                </form>
            </Modal>
        </>
    );
}
