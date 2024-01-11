import {
    AbsoluteCenter,
    Box,
    Divider,
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
import { AddIcon, ArrowDownIcon, ArrowUpIcon, DeleteIcon } from "@chakra-ui/icons";
import { GetAllGroups, GroupDTO, MultiGroupResponseDTO } from "../../services/group";
import { submitDossierForReview } from "../../services/dossierreview";
import { showToast } from "../../utils/toastUtils";
import { useNavigate } from "react-router-dom";
import { BaseRoutes } from "../../constants";
import { group } from "console";

interface EditApprovalStagesModalProps {
    open: boolean;
    closeModal: () => void;
    dossierId: string;
}

interface ApprovalStage {
    group: GroupDTO;
    stageIndex: number;
}

export default function EditApprovalStagesModal(props: EditApprovalStagesModalProps) {
    const toast = useToast();
    const [loading, setLoading] = useState<boolean>(false);
    const [approvalStages, setApprovalStages] = useState<ApprovalStage[]>([]);
    const [otherStages, setOtherStages] = useState<ApprovalStage[]>([]);
    const navigate = useNavigate();

    const defaultGroupOrder = {
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
            const includedGroups = [];
            const excludedGroups = [];

            groups.data.forEach((group) => {
                if (defaultGroupOrder[group.name]) {
                    includedGroups.push(group);
                } else {
                    excludedGroups.push({ group: group, stageIndex: 0 });
                }
            });

            const sortedIncludedGroups = includedGroups
                .sort((a, b) => {
                    const orderA = defaultGroupOrder[a.name] || Number.MAX_VALUE;
                    const orderB = defaultGroupOrder[b.name] || Number.MAX_VALUE;
                    return orderA - orderB;
                })
                .map((group, index) => ({ group: group, stageIndex: index }));

            setApprovalStages(sortedIncludedGroups);
            setOtherStages(excludedGroups);
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

    function deleteStage(groupId: string) {
        setApprovalStages((prevStages) => {
            const stageIndex = prevStages.findIndex((stage) => stage.group.id === groupId);
            if (stageIndex === -1) return prevStages; // If not found, return the original array

            // Extract the stage to be deleted
            const [removedStage] = prevStages.splice(stageIndex, 1);

            // Update the deletedStages state
            setOtherStages((prevDeleted) => {
                // Check if the stage is already in the deleted stages
                if (prevDeleted.some((deletedStage) => deletedStage.group.id === groupId)) {
                    return prevDeleted;
                }
                return [...prevDeleted, removedStage];
            });

            return prevStages.map((stage, index) => ({
                ...stage,
                stageIndex: index,
            }));
        });
    }

    function addBackStage(groupId: string) {
        setOtherStages((prevDeleted) => {
            // Find the index of the stage with the given group id in the deleted stages
            const stageIndex = prevDeleted.findIndex((stage) => stage.group.id === groupId);
            if (stageIndex === -1) return prevDeleted; // If not found, return the original array

            const newDeleted = [...prevDeleted];
            const [addedBackStage] = newDeleted.splice(stageIndex, 1);

            // Add the stage back to the approval stages
            setApprovalStages((prevStages) => [...prevStages, { ...addedBackStage, stageIndex: prevStages.length }]);

            return newDeleted;
        });
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
                                    key={stage.group.id}
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
                                                deleteStage(stage.group.id);
                                            }}
                                        />
                                    </Box>
                                </Box>
                            ))}

                            <Box position="relative" padding="6">
                                <Divider />
                                <AbsoluteCenter bg="white" px="4">
                                    Other Groups
                                </AbsoluteCenter>
                            </Box>
                            {otherStages.map((stage) => (
                                <Box
                                    mt={2}
                                    mb={2}
                                    p={2}
                                    key={stage.group.id}
                                    borderRadius={"lg"}
                                    backgroundColor={"brandGray200"}
                                    display="flex"
                                    flexDirection="row"
                                    justifyContent="space-between"
                                >
                                    <Text alignSelf={"center"}>{stage.group.name}</Text>
                                    <Box>
                                        <IconButton
                                            aria-label="Add"
                                            icon={<AddIcon />}
                                            backgroundColor={"#0072a8"}
                                            color={"white"}
                                            justifySelf={"right"}
                                            mr={2}
                                            onClick={() => {
                                                addBackStage(stage.group.id);
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
