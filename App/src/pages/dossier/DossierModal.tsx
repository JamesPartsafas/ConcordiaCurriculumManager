//for modal
import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    ModalCloseButton,
    FormLabel,
    Input,
    Textarea,
    useToast,
} from "@chakra-ui/react";
import { createDossierForUser, editDossier } from "../../services/dossier";
import { useForm } from "react-hook-form";
import { showToast } from "../../utils/toastUtils"; // Import the utility function
import Button from "../../components/Button";
import { useState } from "react";
import { DossierDTO, DossierDTOResponse } from "../../models/dossier";

interface DossierModalProps {
    open: boolean;
    dossier?: DossierDTO;
    action: "add" | "edit";
    dossierList?: DossierDTO[];
    closeModal: () => void;
}

interface DossierForm {
    title: string;
    description: string;
}

export default function DossierModal(props: DossierModalProps) {
    const toast = useToast(); // Use the useToast hook
    const [loading, setLoading] = useState<boolean>(false);

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
        setLoading(true);
        if (props.action === "add") {
            createDossierForUser(data).then(
                (res: DossierDTOResponse) => {
                    props.dossierList?.push(res.data);
                    props.closeModal();
                    showToast(toast, "Success!", "Dossier created.", "success");
                    setLoading(false);
                },
                () => {
                    showToast(toast, "Error!", "Dossier not created.", "error");
                    setLoading(false);
                }
            );
        }
        if (props.action === "edit") {
            const dossier: DossierDTO = {
                id: props.dossier?.id,
                title: data.title,
                description: data.description,
                initiatorId: props.dossier?.initiatorId,
                published: props.dossier?.published,
            };
            editDossier(dossier).then(
                () => {
                    showToast(toast, "Success!", "Dossier edited.", "success");
                    props.dossierList?.forEach((dossier) => {
                        if (dossier.id === props.dossier?.id) {
                            dossier.title = data.title;
                            dossier.description = data.description;
                        }
                    });
                    setLoading(false);
                    props.closeModal();
                },
                () => {
                    showToast(toast, "Error!", "Dossier not edited.", "error");
                    setLoading(false);
                }
            );
        }
    }

    return (
        <>
            <Modal isOpen={props.open} onClose={props.closeModal}>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <ModalOverlay />
                    <ModalContent>
                        <ModalHeader>{props.action == "add" ? "Add Dossier" : "Edit Dossier"}</ModalHeader>
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

                            <Button
                                style="secondary"
                                type="submit"
                                //add type submit
                                isLoading={loading}
                                loadingText="Saving"
                                variant="solid"
                                width="fit-content"
                                minW="85px"
                                height="40px"
                                isDisabled={!isDirty || !isValid}
                            >
                                Save
                            </Button>
                        </ModalFooter>
                    </ModalContent>
                </form>
            </Modal>
        </>
    );
}
