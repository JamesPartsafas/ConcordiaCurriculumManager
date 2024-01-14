import { useContext, useEffect, useState } from "react";
import { ApprovalStage, DossierDetailsDTO, DossierDetailsResponse, dossierStateToString } from "../../models/dossier";
import { getDossierDetails } from "../../services/dossier";
import { Link, useNavigate, useParams } from "react-router-dom";
import {
    AlertDialog,
    AlertDialogBody,
    AlertDialogContent,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogOverlay,
    Box,
    Card,
    CardBody,
    Center,
    Flex,
    FormControl,
    FormErrorMessage,
    Heading,
    Kbd,
    Stack,
    Tab,
    TabList,
    TabPanel,
    TabPanels,
    Tabs,
    Text,
    Textarea,
    useDisclosure,
    useToast,
} from "@chakra-ui/react";
import Button from "../../components/Button";
import { BaseRoutes } from "../../constants";
import { ArrowLeftIcon } from "@chakra-ui/icons";
import React from "react";
import { forwardDossier, rejectDossier, returnDossier, reviewDossier } from "../../services/dossierreview";
import { showToast } from "../../utils/toastUtils";
import { UserContext } from "../../App";

export default function DossierReview() {
    const { dossierId } = useParams();
    const [dossierDetails, setDossierDetails] = useState<DossierDetailsDTO | null>(null);
    const { isOpen: isOpenMessage, onOpen: onOpenMessage, onClose: onCloseMessage } = useDisclosure();
    const { isOpen: isOpenForward, onOpen: onOpenForward, onClose: onCloseForward } = useDisclosure();
    const { isOpen: isOpenReturn, onOpen: onOpenReturn, onClose: onCloseReturn } = useDisclosure();
    const { isOpen: isOpenReject, onOpen: onOpenReject, onClose: onCloseReject } = useDisclosure();
    const cancelRef = React.useRef();
    const toast = useToast();
    const [message, setMessage] = useState("");
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [messageError, setMessageError] = useState(true);
    const [currentGroup, setCurrentGroup] = useState<ApprovalStage | null>(null);
    const [isGroupMaster, setIsGroupMaster] = useState(false);
    const [isPartOfCurrentStageGroup, setIsPartOfCurrentStageGroup] = useState(false);
    const user = useContext(UserContext);

    const navigate = useNavigate();

    useEffect(() => {
        requestDossierDetails(dossierId);
    }, [dossierId]);

    async function requestDossierDetails(dossierId: string) {
        const dossierDetailsData: DossierDetailsResponse = await getDossierDetails(dossierId);
        setDossierDetails(dossierDetailsData.data);
        const currentStageGroup = dossierDetailsData.data.approvalStages.filter((stage) => stage.isCurrentStage)[0];
        setCurrentGroup(currentStageGroup);
        setIsGroupMaster(user.masteredGroups?.includes(currentStageGroup?.groupId));
        setIsPartOfCurrentStageGroup(user.groups?.includes(currentStageGroup?.groupId));
    }

    function messageAlertDialog() {
        return (
            <AlertDialog isOpen={isOpenMessage} leastDestructiveRef={cancelRef} onClose={onCloseMessage}>
                <AlertDialogOverlay>
                    <AlertDialogContent>
                        <AlertDialogHeader fontSize="lg" fontWeight="bold">
                            Add Message
                        </AlertDialogHeader>

                        <AlertDialogBody>
                            Are you sure you want to submit this message to the discussion board? You can&apos;t undo
                            this action afterwards.
                        </AlertDialogBody>

                        <AlertDialogFooter>
                            <Button
                                style="secondary"
                                variant="outline"
                                width="fit-content"
                                height="40px"
                                ref={cancelRef}
                                onClick={onCloseMessage}
                            >
                                Cancel
                            </Button>
                            <Button
                                style="primary"
                                variant="solid"
                                width="fit-content"
                                height="40px"
                                onClick={() => {
                                    handleSubmitRequest();
                                    onCloseMessage();
                                    setMessage("");
                                }}
                                ml={3}
                            >
                                Submit
                            </Button>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialogOverlay>
            </AlertDialog>
        );
    }

    function forwardAlertDialog() {
        return (
            <AlertDialog isOpen={isOpenForward} leastDestructiveRef={cancelRef} onClose={onCloseForward}>
                <AlertDialogOverlay>
                    <AlertDialogContent>
                        <AlertDialogHeader fontSize="lg" fontWeight="bold">
                            {currentGroup?.isFinalStage ? "Accept Dossier" : "Forward Dossier"}
                        </AlertDialogHeader>

                        <AlertDialogBody>
                            {currentGroup?.isFinalStage
                                ? "Are you sure you want to accept this dossier? "
                                : "Are you sure you want to forward this dossier to the next group in the approval pipeline? "}
                            You can&apos;t undo this action afterwards.
                        </AlertDialogBody>

                        <AlertDialogFooter>
                            <Button
                                style="secondary"
                                variant="outline"
                                width="fit-content"
                                height="40px"
                                ref={cancelRef}
                                onClick={onCloseForward}
                            >
                                Cancel
                            </Button>
                            <Button
                                style="primary"
                                variant="solid"
                                width="fit-content"
                                height="40px"
                                onClick={() => {
                                    handleForwardDossier();
                                    navigate(BaseRoutes.DossiersToReview);
                                }}
                                ml={3}
                            >
                                {currentGroup?.isFinalStage ? "Accept" : "Forward"}
                            </Button>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialogOverlay>
            </AlertDialog>
        );
    }

    function returnAlertDialog() {
        return (
            <AlertDialog isOpen={isOpenReturn} leastDestructiveRef={cancelRef} onClose={onCloseReturn}>
                <AlertDialogOverlay>
                    <AlertDialogContent>
                        <AlertDialogHeader fontSize="lg" fontWeight="bold">
                            Return Dossier
                        </AlertDialogHeader>

                        <AlertDialogBody>
                            Are you sure you want to return this dossier to the previous group in the approval pipeline?
                            You can&apos;t undo this action afterwards.
                        </AlertDialogBody>

                        <AlertDialogFooter>
                            <Button
                                style="secondary"
                                variant="outline"
                                width="fit-content"
                                height="40px"
                                ref={cancelRef}
                                onClick={onCloseReturn}
                            >
                                Cancel
                            </Button>
                            <Button
                                style="primary"
                                variant="solid"
                                width="fit-content"
                                height="40px"
                                onClick={() => {
                                    handleReturnDossier();
                                    navigate(BaseRoutes.DossiersToReview);
                                }}
                                ml={3}
                            >
                                Return
                            </Button>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialogOverlay>
            </AlertDialog>
        );
    }

    function rejectAlertDialog() {
        return (
            <AlertDialog isOpen={isOpenReject} leastDestructiveRef={cancelRef} onClose={onCloseReject}>
                <AlertDialogOverlay>
                    <AlertDialogContent>
                        <AlertDialogHeader fontSize="lg" fontWeight="bold">
                            Reject Dossier
                        </AlertDialogHeader>

                        <AlertDialogBody>
                            Are you sure you want to reject this dossier? You can&apos;t undo this action afterwards.
                        </AlertDialogBody>

                        <AlertDialogFooter>
                            <Button
                                style="secondary"
                                variant="outline"
                                width="fit-content"
                                height="40px"
                                ref={cancelRef}
                                onClick={onCloseReject}
                            >
                                Cancel
                            </Button>
                            <Button
                                style="primary"
                                variant="solid"
                                width="fit-content"
                                height="40px"
                                onClick={() => {
                                    handleRejectDossier();
                                    navigate(BaseRoutes.DossiersToReview);
                                }}
                                ml={3}
                            >
                                Reject
                            </Button>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialogOverlay>
            </AlertDialog>
        );
    }

    const handleForwardDossier = () => {
        forwardDossier(dossierId)
            .then(() => {
                if (currentGroup?.isFinalStage) {
                    showToast(toast, "Success!", "Dossier successfully accepted.", "success");
                } else {
                    showToast(toast, "Success!", "Dossier successfully forwarded.", "success");
                }
            })
            .catch(() => {
                showToast(toast, "Error!", "One or more validation errors occurred", "error");
            });
    };

    const handleReturnDossier = () => {
        returnDossier(dossierId)
            .then(() => {
                showToast(toast, "Success!", "Dossier successfully returned.", "success");
            })
            .catch(() => {
                showToast(toast, "Error!", "One or more validation errors occurred", "error");
            });
    };

    const handleRejectDossier = () => {
        rejectDossier(dossierId)
            .then(() => {
                showToast(toast, "Success!", "Dossier successfully rejected.", "success");
            })
            .catch(() => {
                showToast(toast, "Error!", "One or more validation errors occurred", "error");
            });
    };

    const handleChangeMessage = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        if (e.currentTarget.value.length === 0) setMessageError(true);
        else setMessageError(false);
        setMessage(e.currentTarget.value);
    };

    const handleSubmitRequest = () => {
        setFormSubmitted(true);
        if (messageError) return;
        else {
            const dossierForReviewDTO = {
                message: message,
                groupId: currentGroup?.groupId,
            };

            reviewDossier(dossierId, dossierForReviewDTO)
                .then(() => {
                    showToast(toast, "Success!", "Message successfully sent.", "success");
                    requestDossierDetails(dossierId);
                })
                .catch((e) => {
                    if (e.response.status == 403) {
                        showToast(
                            toast,
                            "Error!",
                            "You need to be part of the current stage group in order to submit a message.",
                            "error"
                        );
                    } else {
                        showToast(toast, "Error!", "One or more validation errors occurred", "error");
                    }
                });
        }
    };

    return (
        <>
            <Box>
                <form>
                    <Button
                        style="primary"
                        variant="outline"
                        height="40px"
                        width="100px"
                        margin="20px"
                        onClick={() => navigate(BaseRoutes.Dossiers)} // TODO: change the link to navigate to the Dossiers Review Page
                    >
                        Back
                    </Button>
                    <Flex>
                        <Stack w="25%" p={8}>
                            <Stack marginBottom={10}>
                                <Heading
                                    color={"brandRed"}
                                    style={{
                                        marginLeft: "auto",
                                        marginRight: "auto",
                                        width: "fit-content",
                                    }}
                                >
                                    Dossier Review
                                </Heading>
                                <Heading color={"black"}>{dossierDetails?.title}</Heading>
                                <Kbd>{dossierDetails?.id}</Kbd>
                                <Text>{dossierDetails?.description}</Text>
                                <Text>
                                    <b>State:</b> {dossierStateToString(dossierDetails)}
                                </Text>
                                <Text>
                                    <b>Created: </b>
                                    {dossierDetails?.createdDate.toString().substring(0, 10)}{" "}
                                    {new Date(dossierDetails?.createdDate).getHours().toString()}:
                                    {new Date(dossierDetails?.createdDate).getMinutes().toString()}:
                                    {new Date(dossierDetails?.createdDate).getSeconds().toString()}
                                </Text>
                                <Text>
                                    <b>Updated: </b>
                                    {dossierDetails?.modifiedDate.toString().substring(0, 10)}{" "}
                                    {new Date(dossierDetails?.modifiedDate).getHours().toString()}:
                                    {new Date(dossierDetails?.modifiedDate).getMinutes().toString()}:
                                    {new Date(dossierDetails?.modifiedDate).getSeconds().toString()}
                                </Text>
                                {dossierDetails?.state == 3 ? (
                                    <Text fontWeight={"bold"} fontSize={20} color={"green"}>
                                        This dossier has been approved.
                                    </Text>
                                ) : dossierDetails?.state == 2 ? (
                                    <Text fontWeight={"bold"} fontSize={20} color={"red"}>
                                        This dossier has been rejected.
                                    </Text>
                                ) : (
                                    <Text>
                                        <b>Current Group:</b> {currentGroup?.group.name}
                                    </Text>
                                )}
                            </Stack>
                            {isGroupMaster && dossierDetails.state == 1 ? (
                                <Stack direction="row" spacing={4} align="center" marginBottom={10}>
                                    <Button
                                        background="brandBlue"
                                        variant="solid"
                                        onClick={() => {
                                            onOpenForward();
                                        }}
                                    >
                                        {currentGroup.isFinalStage ? "Accept Changes" : "Forward"}
                                    </Button>
                                    {currentGroup.stageIndex == 0 ? (
                                        ""
                                    ) : (
                                        <Button
                                            background="brandGray"
                                            variant="solid"
                                            onClick={() => {
                                                onOpenReturn();
                                            }}
                                        >
                                            Return
                                        </Button>
                                    )}

                                    <Button
                                        background="brandRed"
                                        variant="solid"
                                        onClick={() => {
                                            onOpenReject();
                                        }}
                                    >
                                        Reject
                                    </Button>
                                </Stack>
                            ) : (
                                ""
                            )}

                            <Stack>
                                <Heading margin="auto" size="xl" color="brandRed">
                                    Changes
                                </Heading>
                                <Stack mb={4}>
                                    <Heading size={"md"} color="brandBlue">
                                        Course Creation Requests:
                                    </Heading>
                                    {/* TODO: Change the links below to the Course Details Page */}
                                    {dossierDetails?.courseCreationRequests?.map((courseCreationRequest) => (
                                        <Text key={courseCreationRequest.id} as="u">
                                            <Link to={BaseRoutes.ManageableGroup}>
                                                {courseCreationRequest.newCourse?.subject}{" "}
                                                {courseCreationRequest.newCourse?.catalog}
                                            </Link>
                                        </Text>
                                    ))}
                                </Stack>
                                <Stack mb={4}>
                                    <Heading size={"md"} color="brandBlue">
                                        Course Modification Requests:
                                    </Heading>
                                    {/* TODO: Change the link below to the Course Details Page */}
                                    {dossierDetails?.courseModificationRequests?.map((courseModificationRequest) => (
                                        <Text key={courseModificationRequest.id} as="u">
                                            <Link to={BaseRoutes.ManageableGroup}>
                                                {courseModificationRequest.course?.subject}{" "}
                                                {courseModificationRequest.course?.catalog}
                                            </Link>
                                        </Text>
                                    ))}
                                </Stack>
                                <Stack mb={4}>
                                    <Heading size={"md"} color="brandBlue">
                                        Course Deletion Requests:
                                    </Heading>
                                    {/* TODO: Change the link below to the Course Details Page */}
                                    {dossierDetails?.courseDeletionRequests?.map((courseDeletionRequest) => (
                                        <Text key={courseDeletionRequest.id} as="u">
                                            <Link to={BaseRoutes.ManageableGroup}>
                                                {courseDeletionRequest.course?.subject}{" "}
                                                {courseDeletionRequest.course?.catalog}
                                            </Link>
                                        </Text>
                                    ))}
                                </Stack>
                            </Stack>
                        </Stack>
                        <Stack w="75%" p={8}>
                            <Stack>
                                <Center>
                                    <Heading as="h2" size="xl" color="brandRed">
                                        Discussion Board
                                    </Heading>
                                </Center>
                                <Stack>
                                    <Tabs defaultIndex={currentGroup?.stageIndex}>
                                        <TabList>
                                            <Tab>All Groups</Tab>
                                            {dossierDetails?.approvalStages
                                                ?.sort((a, b) => a.stageIndex - b.stageIndex)
                                                .map((stage) => <Tab key={stage.groupId}>{stage.group.name}</Tab>)}
                                        </TabList>

                                        <TabPanels>
                                            <TabPanel>
                                                {dossierDetails?.discussion.messages.map((message) => (
                                                    <Card key={message.id}>
                                                        {dossierDetails?.approvalStages
                                                            .filter((stage) => message.groupId == stage.groupId)
                                                            .map((filteredGroup) => (
                                                                <CardBody key={filteredGroup.groupId}>
                                                                    <Box bg={"gray.200"} p={2}>
                                                                        <Text>
                                                                            <b>{filteredGroup.group.name}</b>{" "}
                                                                        </Text>
                                                                        <Text>
                                                                            {message.createdDate
                                                                                .toString()
                                                                                .substring(0, 10)}{" "}
                                                                            {new Date(message.createdDate)
                                                                                .getHours()
                                                                                .toString()}
                                                                            :
                                                                            {new Date(message.createdDate)
                                                                                .getMinutes()
                                                                                .toString()}
                                                                            :
                                                                            {new Date(message.createdDate)
                                                                                .getSeconds()
                                                                                .toString()}
                                                                        </Text>
                                                                        <Text>{message.message}</Text>
                                                                        <Button marginTop={5}>
                                                                            <ArrowLeftIcon marginRight={5} />
                                                                            Reply
                                                                        </Button>
                                                                    </Box>
                                                                </CardBody>
                                                            ))}
                                                    </Card>
                                                ))}
                                            </TabPanel>
                                            {dossierDetails?.approvalStages?.map((stage) => (
                                                <TabPanel key={stage.groupId}>
                                                    <Card>
                                                        {dossierDetails?.discussion.messages
                                                            .filter((message) => stage.groupId == message.groupId)
                                                            .sort(
                                                                (a, b) =>
                                                                    new Date(a.createdDate).getTime() -
                                                                    new Date(b.createdDate).getTime()
                                                            )
                                                            .map((filteredMessage) => (
                                                                <CardBody key={filteredMessage.id}>
                                                                    <Box bg={"gray.200"} p={2}>
                                                                        <Text>
                                                                            <b>{stage.group.name}</b>{" "}
                                                                        </Text>
                                                                        <Text>
                                                                            {filteredMessage.createdDate
                                                                                .toString()
                                                                                .substring(0, 10)}{" "}
                                                                            {new Date(filteredMessage.createdDate)
                                                                                .getHours()
                                                                                .toString()}
                                                                            :
                                                                            {new Date(filteredMessage.createdDate)
                                                                                .getMinutes()
                                                                                .toString()}
                                                                            :
                                                                            {new Date(filteredMessage.createdDate)
                                                                                .getSeconds()
                                                                                .toString()}
                                                                        </Text>
                                                                        <Text>{filteredMessage.message}</Text>
                                                                        <Button marginTop={5}>
                                                                            <ArrowLeftIcon marginRight={5} />
                                                                            Reply
                                                                        </Button>
                                                                    </Box>
                                                                </CardBody>
                                                            ))}
                                                    </Card>
                                                </TabPanel>
                                            ))}
                                        </TabPanels>
                                    </Tabs>
                                </Stack>
                            </Stack>
                            {!isPartOfCurrentStageGroup || dossierDetails?.state == 2 || dossierDetails?.state == 3 ? (
                                ""
                            ) : (
                                <Stack marginTop={10}>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            <Text align="center">Add Message</Text>
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <FormControl isInvalid={messageError && formSubmitted}>
                                            <Textarea
                                                onChange={handleChangeMessage}
                                                value={message}
                                                placeholder={"Add message to discussion board..."}
                                                minH={"150px"}
                                            ></Textarea>
                                            <FormErrorMessage>Message cannot be empty.</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <Button
                                            style="primary"
                                            width="auto"
                                            height="50px"
                                            variant="solid"
                                            onClick={() => {
                                                onOpenMessage();
                                            }}
                                        >
                                            Submit
                                        </Button>
                                    </Stack>
                                </Stack>
                            )}
                        </Stack>
                    </Flex>
                </form>
            </Box>
            {messageAlertDialog()}
            {forwardAlertDialog()}
            {returnAlertDialog()}
            {rejectAlertDialog()}
        </>
    );
}
