import { useEffect, useState } from "react";
import { DossierDetailsDTO, DossierDetailsResponse, dossierStateToString } from "../../models/dossier";
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
} from "@chakra-ui/react";
import Button from "../../components/Button";
import { BaseRoutes } from "../../constants";
import { ArrowLeftIcon } from "@chakra-ui/icons";
import React from "react";

export default function DossierReview() {
    const { dossierId } = useParams();
    const [dossierDetails, setDossierDetails] = useState<DossierDetailsDTO | null>(null);
    const { isOpen: isOpenMessage, onOpen: onOpenMessage, onClose: onCloseMessage } = useDisclosure();
    const { isOpen: isOpenForward, onOpen: onOpenForward, onClose: onCloseForward } = useDisclosure();
    const { isOpen: isOpenReturn, onOpen: onOpenReturn, onClose: onCloseReturn } = useDisclosure();
    const { isOpen: isOpenReject, onOpen: onOpenReject, onClose: onCloseReject } = useDisclosure();
    //const [loading, setLoading] = useState<boolean>(false);
    const cancelRef = React.useRef();

    const navigate = useNavigate();

    useEffect(() => {
        requestDossierDetails(dossierId);
        //requestCourseSettings();
    }, [dossierId]);

    function requestDossierDetails(dossierId: string) {
        getDossierDetails(dossierId).then((res: DossierDetailsResponse) => {
            setDossierDetails(res.data);
        });
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
                                //isLoading={loading}
                                loadingText="Deleting"
                                // onClick={() => {
                                //     addMessage(message);
                                //     // onClose();
                                // }}
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
                            Forward Dossier
                        </AlertDialogHeader>

                        <AlertDialogBody>
                            Are you sure you want to forward this dossier to the next group in the approval pipeline?
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
                                //isLoading={loading}
                                loadingText="Deleting"
                                // onClick={() => {
                                //     forwardDossier(dossier);
                                //     // onClose();
                                // }}
                                ml={3}
                            >
                                Forward
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
                                //isLoading={loading}
                                loadingText="Deleting"
                                // onClick={() => {
                                //     returnDossier(dossier);
                                //     // onClose();
                                // }}
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
                                //isLoading={loading}
                                loadingText="Deleting"
                                // onClick={() => {
                                //     rejectDossier(dossier);
                                //     // onClose();
                                // }}
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
                                <Text>state: {dossierStateToString(dossierDetails)}</Text>
                                <Text>created: {dossierDetails?.createdDate?.toString()}</Text>
                                <Text>updated: {dossierDetails?.modifiedDate?.toString()}</Text>
                            </Stack>
                            <Stack direction="row" spacing={4} align="center" marginBottom={10}>
                                <Button
                                    background="brandBlue"
                                    variant="solid"
                                    onClick={() => {
                                        onOpenForward();
                                    }}
                                >
                                    Forward
                                </Button>
                                <Button
                                    background="brandGray"
                                    variant="solid"
                                    onClick={() => {
                                        onOpenReturn();
                                    }}
                                >
                                    Return
                                </Button>
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
                                        <Text align="center">Add Message</Text>
                                    </Heading>
                                </Center>
                                <Stack mb={2}>
                                    <Text as="span">
                                        ***You are a member of the{" "}
                                        <Text as="span" fontWeight={"bold"} fontSize={20}>
                                            Gina Cody School Faculty Council
                                        </Text>
                                        ***
                                    </Text>
                                </Stack>
                                <Stack>
                                    <Textarea
                                        // value={courseRequesites}
                                        // onChange={handleChangeCourseRequesites}
                                        placeholder="Add message to discussion board..."
                                        minH={"150px"}
                                    ></Textarea>
                                </Stack>
                                <Stack>
                                    <Button
                                        style="primary"
                                        width="auto"
                                        height="50px"
                                        variant="solid"
                                        // onClick={() => handleSubmitCourse()}
                                        // isLoading={isLoading}
                                        onClick={() => {
                                            onOpenMessage();
                                        }}
                                    >
                                        Submit
                                    </Button>
                                </Stack>
                            </Stack>
                            <Stack marginTop={16}>
                                <Center>
                                    <Heading as="h2" size="xl" color="brandRed">
                                        Discussion Board
                                    </Heading>
                                </Center>
                                <Stack>
                                    <Tabs>
                                        <TabList>
                                            <Tab>All Groups</Tab>
                                            <Tab>Department Curriculum Committee</Tab>
                                            <Tab>Department Council</Tab>
                                            <Tab>Gina Cody School Undergraduate Studies Committee</Tab>
                                            <Tab>Gina Cody School Faculty Council</Tab>
                                            <Tab>Academic Planning Committee</Tab>
                                            <Tab>School of Graduate Studies Curriculum Committee</Tab>
                                            <Tab>School of Graduate Studies Faculty Council</Tab>
                                            <Tab>Academic Planning Committee</Tab>
                                            <Tab>Concordia University Senate</Tab>
                                        </TabList>

                                        <TabPanels>
                                            <TabPanel>
                                                <Card>
                                                    <CardBody>
                                                        <Box bg={"gray.200"} p={2}>
                                                            <Text>
                                                                <b>Department Curriculum Committee</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>Joe User</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>2023-12-17</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                                                                sed do eiusmod tempor incididunt ut labore et dolore
                                                                magna aliqua. Ut enim ad minim veniam, quis nostrud
                                                                exercitation ullamco laboris nisi ut aliquip ex ea
                                                                commodo consequat. Duis aute irure dolor in
                                                                reprehenderit in voluptate velit esse cillum dolore eu
                                                                fugiat nulla pariatur. Excepteur sint occaecat cupidatat
                                                                non proident, sunt in culpa qui officia deserunt mollit
                                                                anim id est laborum.
                                                            </Text>
                                                            <Button marginTop={5}>
                                                                <ArrowLeftIcon marginRight={5} />
                                                                Reply
                                                            </Button>
                                                        </Box>
                                                    </CardBody>
                                                    <CardBody>
                                                        <Box bg={"gray.200"} p={2}>
                                                            <Text>
                                                                <b>Department Council</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>John User</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>2023-12-14</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                                                                sed do eiusmod tempor incididunt ut labore et dolore
                                                                magna aliqua. Ut enim ad minim veniam, quis nostrud
                                                                exercitation ullamco laboris nisi ut aliquip ex ea
                                                                commodo consequat. Duis aute irure dolor in
                                                                reprehenderit in voluptate velit esse cillum dolore eu
                                                                fugiat nulla pariatur. Excepteur sint occaecat cupidatat
                                                                non proident, sunt in culpa qui officia deserunt mollit
                                                                anim id est laborum.
                                                            </Text>
                                                            <Button marginTop={5}>
                                                                <ArrowLeftIcon marginRight={5} />
                                                                Reply
                                                            </Button>
                                                        </Box>
                                                    </CardBody>
                                                    <CardBody>
                                                        <Box bg={"gray.200"} p={2}>
                                                            <Text>
                                                                <b>Gina Cody School Faculty Council</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>Jane User</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>2023-12-13</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                                                                sed do eiusmod tempor incididunt ut labore et dolore
                                                                magna aliqua. Ut enim ad minim veniam, quis nostrud
                                                                exercitation ullamco laboris nisi ut aliquip ex ea
                                                                commodo consequat. Duis aute irure dolor in
                                                                reprehenderit in voluptate velit esse cillum dolore eu
                                                                fugiat nulla pariatur. Excepteur sint occaecat cupidatat
                                                                non proident, sunt in culpa qui officia deserunt mollit
                                                                anim id est laborum.
                                                            </Text>
                                                            <Button marginTop={5}>
                                                                <ArrowLeftIcon marginRight={5} />
                                                                Reply
                                                            </Button>
                                                        </Box>
                                                    </CardBody>
                                                </Card>
                                            </TabPanel>
                                            <TabPanel>
                                                <Card>
                                                    <CardBody>
                                                        <Box bg={"gray.200"} p={2}>
                                                            <Text>
                                                                <b>Department Curriculum Committee</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>Joe User</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>2023-12-17</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                                                                sed do eiusmod tempor incididunt ut labore et dolore
                                                                magna aliqua. Ut enim ad minim veniam, quis nostrud
                                                                exercitation ullamco laboris nisi ut aliquip ex ea
                                                                commodo consequat. Duis aute irure dolor in
                                                                reprehenderit in voluptate velit esse cillum dolore eu
                                                                fugiat nulla pariatur. Excepteur sint occaecat cupidatat
                                                                non proident, sunt in culpa qui officia deserunt mollit
                                                                anim id est laborum.
                                                            </Text>
                                                            <Button marginTop={5}>
                                                                <ArrowLeftIcon marginRight={5} />
                                                                Reply
                                                            </Button>
                                                        </Box>
                                                    </CardBody>
                                                </Card>
                                            </TabPanel>
                                            <TabPanel>
                                                <Card>
                                                    <CardBody>
                                                        <Box bg={"gray.200"} p={2}>
                                                            <Text>
                                                                <b>Department Council</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>John User</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>2023-12-14</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                                                                sed do eiusmod tempor incididunt ut labore et dolore
                                                                magna aliqua. Ut enim ad minim veniam, quis nostrud
                                                                exercitation ullamco laboris nisi ut aliquip ex ea
                                                                commodo consequat. Duis aute irure dolor in
                                                                reprehenderit in voluptate velit esse cillum dolore eu
                                                                fugiat nulla pariatur. Excepteur sint occaecat cupidatat
                                                                non proident, sunt in culpa qui officia deserunt mollit
                                                                anim id est laborum.
                                                            </Text>
                                                            <Button marginTop={5}>
                                                                <ArrowLeftIcon marginRight={5} />
                                                                Reply
                                                            </Button>
                                                        </Box>
                                                    </CardBody>
                                                </Card>
                                            </TabPanel>
                                            <TabPanel></TabPanel>
                                            <TabPanel>
                                                <Card>
                                                    <CardBody>
                                                        <Box bg={"gray.200"} p={2}>
                                                            <Text>
                                                                <b>Gina Cody School Faculty Council</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>Jane User</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                <b>2023-12-13</b>{" "}
                                                            </Text>
                                                            <Text>
                                                                Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                                                                sed do eiusmod tempor incididunt ut labore et dolore
                                                                magna aliqua. Ut enim ad minim veniam, quis nostrud
                                                                exercitation ullamco laboris nisi ut aliquip ex ea
                                                                commodo consequat. Duis aute irure dolor in
                                                                reprehenderit in voluptate velit esse cillum dolore eu
                                                                fugiat nulla pariatur. Excepteur sint occaecat cupidatat
                                                                non proident, sunt in culpa qui officia deserunt mollit
                                                                anim id est laborum.
                                                            </Text>
                                                            <Button marginTop={5}>
                                                                <ArrowLeftIcon marginRight={5} />
                                                                Reply
                                                            </Button>
                                                        </Box>
                                                    </CardBody>
                                                </Card>
                                            </TabPanel>
                                            <TabPanel></TabPanel>
                                            <TabPanel></TabPanel>
                                            <TabPanel></TabPanel>
                                            <TabPanel></TabPanel>
                                            <TabPanel></TabPanel>
                                        </TabPanels>
                                    </Tabs>
                                </Stack>
                            </Stack>
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
