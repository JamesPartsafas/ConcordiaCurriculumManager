import {
    Box,
    IconButton,
    Modal,
    ModalBody,
    ModalCloseButton,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
    Text,
    useToast,
} from "@chakra-ui/react";
import Button from "../../components/Button";
import { useEffect, useState } from "react";
import { ArrowDownIcon, ArrowUpIcon, DeleteIcon } from "@chakra-ui/icons";
import { GetAllGroups, MultiGroupResponseDTO } from "../../services/group";
import { submitDossierForReview } from "../../services/dossierreview";
import { showToast } from "../../utils/toastUtils";
import { useNavigate } from "react-router-dom";
import { BaseRoutes } from "../../constants";

interface EditApprovalStagesModalProps {
    open: boolean;
    closeModal: () => void;
    dossierId: string;
}

export default function EditApprovalStagesModal(props: EditApprovalStagesModalProps) {
    const toast = useToast();
    const [loading, setLoading] = useState<boolean>(false);
    const [approvalStages, setApprovalStages] = useState([]);
    const navigate = useNavigate();

    const groupOrder = {
        "Department Curriculum Committee": 1,
        "Department Consul": 2,
        "Faculty Undergraduate Committee": 3,
        "Faculty Council": 4,
        "APC Committee": 5,
        Senate: 6,
    };

    useEffect(() => {
        initializeApprovalStages();
    }, []);

    function initializeApprovalStages() {
        GetAllGroups().then((groups: MultiGroupResponseDTO) => {
            console.log(groups.data);
            const sortedGroups = groups.data
                .sort((a, b) => {
                    const orderA = groupOrder[a.name] || Number.MAX_VALUE;
                    const orderB = groupOrder[b.name] || Number.MAX_VALUE;
                    return orderA - orderB;
                })
                .map((group, index) => ({ group: group, stageIndex: index }));

            setApprovalStages(sortedGroups);
        });
    }

    function swapStages(index1, index2) {
        setApprovalStages((prevStages) => {
            const newStages = [...prevStages];
            [newStages[index1], newStages[index2]] = [newStages[index2], newStages[index1]];
            return newStages.map((stage, index) => ({ ...stage, stageIndex: index }));
        });
    }

    function addReviewGroups() {
        submitDossierForReview(props?.dossierId, {
            dossierId: props?.dossierId,
            groupIds: [...approvalStages.map((stage) => stage.group.id)],
        }).then(
            () => {
                setLoading(false);
                props.closeModal();
                showToast(toast, "Success!", "Dossier submitted for review.", "success");
                navigate(BaseRoutes.Dossiers);
            },
            () => {
                showToast(toast, "Error!", "Failed to submit dossier for review.", "error");
                setLoading(false);
            }
        );
    }

    function onSubmit() {
        setLoading(true);
        addReviewGroups();
    }

    return (
        <>
            <Modal isOpen={props.open} onClose={props.closeModal} size={"2xl"}>
                <form>
                    <ModalOverlay />
                    <ModalContent>
                        <ModalHeader>Edit Dossier Approval Pipeline</ModalHeader>
                        <ModalCloseButton />

                        <ModalBody>
                            {approvalStages?.map((stage) => (
                                <Box
                                    mt={2}
                                    mb={2}
                                    p={2}
                                    key={stage.stageIndex}
                                    borderRadius={"lg"}
                                    backgroundColor={"brandGray200"}
                                    display="flex"
                                    flexDirection="row"
                                    justifyContent="space-between"
                                >
                                    <Text alignSelf={"center"}>{stage.group.name}</Text>

                                    <Box>
                                        <IconButton
                                            aria-label="Down"
                                            icon={<ArrowDownIcon />}
                                            backgroundColor={"#0072a8"}
                                            color={"white"}
                                            justifySelf={"right"}
                                            mr={2}
                                            isDisabled={stage.stageIndex === approvalStages.length - 1}
                                            onClick={() => {
                                                swapStages(stage.stageIndex, stage.stageIndex + 1);
                                            }}
                                        />

                                        <IconButton
                                            aria-label="Down"
                                            icon={<ArrowUpIcon />}
                                            backgroundColor={"#0072a8"}
                                            color={"white"}
                                            justifySelf={"right"}
                                            mr={2}
                                            isDisabled={stage.stageIndex === 0}
                                            onClick={() => {
                                                swapStages(stage.stageIndex - 1, stage.stageIndex);
                                            }}
                                        />
                                        <IconButton
                                            aria-label="Delete"
                                            icon={<DeleteIcon />}
                                            backgroundColor={"#932439"}
                                            color={"white"}
                                            onClick={() => {
                                                setApprovalStages((prevStages) => {
                                                    const newStages = [...prevStages];
                                                    newStages.splice(stage.stageIndex, 1);
                                                    return newStages.map((stage, index) => ({
                                                        ...stage,
                                                        stageIndex: index,
                                                    }));
                                                });
                                            }}
                                        />
                                    </Box>
                                </Box>
                            ))}
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
                                loadingText="Submitting"
                                variant="solid"
                                width="fit-content"
                                minW="85px"
                                height="40px"
                                onClick={onSubmit}
                            >
                                Submit Dossier for Review
                            </Button>
                        </ModalFooter>
                    </ModalContent>
                </form>
            </Modal>
        </>
    );
}
