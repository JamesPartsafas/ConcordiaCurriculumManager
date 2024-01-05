import { useEffect, useState } from "react";
import { DossierDetailsDTO, DossierDetailsResponse, dossierStateToString } from "../../models/dossier";
import { getDossierDetails } from "../../services/dossier";
import { useNavigate, useParams } from "react-router-dom";
import {
    Box,
    ButtonGroup,
    Card,
    CardBody,
    CardFooter,
    Container,
    Divider,
    Heading,
    Kbd,
    SimpleGrid,
    Stack,
    Text,
    Textarea,
    useDisclosure,
    useToast,
} from "@chakra-ui/react";
import {
    AllCourseSettings,
    CourseCreationRequest,
    CourseCreationRequestDTOResponse,
    CourseDeletionRequest,
    CourseModificationRequest,
    CourseModificationRequestDTOResponse,
} from "../../models/course";
import {
    deleteCourseCreationRequest,
    deleteCourseDeletionRequest,
    deleteCourseModificationRequest,
    getAllCourseSettings,
    getCourseCreationRequest,
    getCourseModificationRequest,
} from "../../services/course";
import Button from "../../components/Button";
import { BaseRoutes } from "../../constants";
import { showToast } from "../../utils/toastUtils";
import DeleteAlert from "../../shared/DeleteAlert";
import EditCourseModal from "./EditCourseModal";
import { ArrowBackIcon } from "@chakra-ui/icons";
import EditApprovalStagesModal from "./EditApprovalStagesModal";

export default function DossierDetails() {
    const { dossierId } = useParams();
    const toast = useToast(); // Use the useToast hook
    const [dossierDetails, setDossierDetails] = useState<DossierDetailsDTO | null>(null);
    const [courseSettings, setCourseSettings] = useState<AllCourseSettings>(null);
    const { isOpen, onOpen, onClose } = useDisclosure();
    const { isOpen: isEditOpen, onOpen: onEditOpen, onClose: onEditClose } = useDisclosure();
    const [selectedCourseCreationRequest, setSelectedCourseCreationRequest] = useState<CourseCreationRequest>(null);
    const [selectedCourseModificationRequest, setSelectedCourseModificationRequest] =
        useState<CourseModificationRequest>(null);
    const [selectedCourseDeletionRequest, setSelectedCourseDeletionRequest] = useState<CourseDeletionRequest>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [showApprovalStagesModal, setShowApprovalStagesModal] = useState<boolean>(false);

    const navigate = useNavigate();

    useEffect(() => {
        requestDossierDetails(dossierId);
        requestCourseSettings();
        requestCourseSettings();
    }, [dossierId]);

    function requestDossierDetails(dossierId: string) {
        getDossierDetails(dossierId).then((res: DossierDetailsResponse) => {
            setDossierDetails(res.data);
            // console.log(res.data.approvalStages.sort((a, b) => a.stageIndex - b.stageIndex > 0 ? 1 : -1));
        });
    }

    function requestCourseSettings() {
        getAllCourseSettings().then((res) => {
            setCourseSettings(res.data);
        });
    }

    function deleteRequestAlert() {
        if (selectedCourseCreationRequest) {
            return (
                <DeleteAlert
                    isOpen={isOpen}
                    onClose={handleOnClose}
                    loading={loading}
                    headerTitle="Delete Course Creation Request"
                    title={selectedCourseCreationRequest?.newCourse.title}
                    item={selectedCourseCreationRequest}
                    onDelete={deleteCreationRequest}
                />
            );
        } else if (selectedCourseModificationRequest) {
            return (
                <DeleteAlert
                    isOpen={isOpen}
                    onClose={handleOnClose}
                    loading={loading}
                    headerTitle="Delete Course Modification Request"
                    title={selectedCourseModificationRequest?.course.title}
                    item={selectedCourseModificationRequest}
                    onDelete={deleteModificationRequest}
                />
            );
        } else if (selectedCourseDeletionRequest) {
            return (
                <DeleteAlert
                    isOpen={isOpen}
                    onClose={handleOnClose}
                    loading={loading}
                    headerTitle="Delete Course Deletion Request"
                    title={selectedCourseDeletionRequest?.course.title}
                    item={selectedCourseDeletionRequest}
                    onDelete={deleteDeletionRequest}
                />
            );
        }
    }

    function handleOnClose() {
        setSelectedCourseCreationRequest(null);
        setSelectedCourseModificationRequest(null);
        setSelectedCourseDeletionRequest(null);
        onClose();
    }

    function deleteCreationRequest(courseCreationRequest: CourseCreationRequest) {
        setLoading(true);
        deleteCourseCreationRequest(dossierId, courseCreationRequest.id)
            .then(
                () => {
                    showToast(toast, "Success!", "Course creation request deleted.", "success");
                    setDossierDetails((prevDetails) => {
                        // Create a new array without the deleted course creation request
                        const updatedRequests = prevDetails.courseCreationRequests.filter(
                            (c) => c.id !== courseCreationRequest.id
                        );
                        // Return a new object for the state with the updated array
                        return { ...prevDetails, courseCreationRequests: updatedRequests };
                    });
                    setLoading(false);
                    setSelectedCourseCreationRequest(null);
                },
                () => {
                    showToast(toast, "Error!", "Course creation request could not be deleted.", "error");
                    setLoading(false);
                    setSelectedCourseCreationRequest(null);
                }
            )
            .catch(() => {
                showToast(toast, "Error!", "Course creation request could not be deleted.", "error");
                setLoading(false);
                setSelectedCourseCreationRequest(null);
            });
    }

    function deleteModificationRequest(courseModificationRequest: CourseModificationRequest) {
        setLoading(true);
        deleteCourseModificationRequest(dossierId, courseModificationRequest.id)
            .then(
                () => {
                    showToast(toast, "Success!", "Course Modification request deleted.", "success");
                    setDossierDetails((prevDetails) => {
                        // Create a new array without the deleted course creation request
                        const updatedRequests = prevDetails.courseModificationRequests.filter(
                            (c) => c.id !== courseModificationRequest.id
                        );
                        // Return a new object for the state with the updated array
                        return { ...prevDetails, courseModificationRequests: updatedRequests };
                    });
                    setLoading(false);
                    setSelectedCourseModificationRequest(null);
                },
                () => {
                    showToast(toast, "Error!", "Course modification request could not be deleted.", "error");
                    setLoading(false);
                    setSelectedCourseModificationRequest(null);
                }
            )
            .catch(() => {
                showToast(toast, "Error!", "Course modification request could not be deleted.", "error");
                setLoading(false);
                setSelectedCourseModificationRequest(null);
            });
    }

    function deleteDeletionRequest(courseDeletionRequest: CourseDeletionRequest) {
        setLoading(true);
        deleteCourseDeletionRequest(dossierId, courseDeletionRequest.id)
            .then(
                () => {
                    showToast(toast, "Success!", "Course deletion request deleted.", "success");
                    setDossierDetails((prevDetails) => {
                        // Create a new array without the deleted course creation request
                        const updatedRequests = prevDetails.courseDeletionRequests.filter(
                            (c) => c.id !== courseDeletionRequest.id
                        );
                        // Return a new object for the state with the updated array
                        return { ...prevDetails, courseDeletionRequests: updatedRequests };
                    });
                    setLoading(false);
                    setSelectedCourseDeletionRequest(null);
                },
                () => {
                    showToast(toast, "Error!", "Course deletion request could not be deleted.", "error");
                    setLoading(false);
                    setSelectedCourseDeletionRequest(null);
                }
            )
            .catch(() => {
                showToast(toast, "Error!", "Course deletion request could not be deleted.", "error");
                setLoading(false);
                setSelectedCourseDeletionRequest(null);
            });
    }

    function createModificationRequest() {
        return (
            <EditCourseModal
                isOpen={isEditOpen}
                onClose={onEditClose}
                allCourseSettings={courseSettings}
                dossierId={dossierId}
            ></EditCourseModal>
        );
    }

    function editCourseCreationRequest(creationRequest: CourseCreationRequest) {
        getCourseCreationRequest(creationRequest.id).then((res: CourseCreationRequestDTOResponse) => {
            const creationRequestToEdit = {
                ...res.data.newCourse,
                id: res.data.id,
            };

            navigate(
                BaseRoutes.EditCourse.replace(":id", creationRequest.newCourse.catalog.toString()).replace(
                    ":dossierId",
                    dossierId
                ),
                { state: { ...creationRequestToEdit, api: "editCreationRequest" } }
            );
        });
    }

    function editCourseModificationRequest(modificationRequest: CourseModificationRequest) {
        getCourseModificationRequest(modificationRequest.id).then((res: CourseModificationRequestDTOResponse) => {
            const modificationRequestToEdit = {
                ...res.data.course,
                id: res.data.id,
            };

            navigate(
                BaseRoutes.EditCourse.replace(":id", modificationRequest.course.catalog.toString()).replace(
                    ":dossierId",
                    dossierId
                ),
                { state: { ...modificationRequestToEdit, api: "editModificationRequest" } }
            );
        });
    }

    function dispaylayApprovalStagesModal() {
        setShowApprovalStagesModal(true);
    }
    function closeApprovalStagesModal() {
        setShowApprovalStagesModal(false);
    }

    return (
        <>
            {deleteRequestAlert()}
            {createModificationRequest()}
            <Container maxW={"70%"} mt={5} mb={2}>
                <Button
                    style="primary"
                    variant="outline"
                    height="40px"
                    width="100px"
                    onClick={() => navigate(BaseRoutes.Dossiers)}
                >
                    Back
                </Button>
                <div style={{ margin: "auto", width: "fit-content" }}>
                    <Heading color={"brandRed"}>{dossierDetails?.title}</Heading>
                    <Kbd>{dossierDetails?.id}</Kbd>
                    <Text>{dossierDetails?.description}</Text>
                    <Text>state: {dossierStateToString(dossierDetails)}</Text>
                    <Text>created: {dossierDetails?.createdDate?.toString()}</Text>
                    <Text>updated: {dossierDetails?.modifiedDate?.toString()}</Text>
                </div>
                <Box backgroundColor={"brandRed"} m={"auto"} mt={5} p="3" borderRadius={"lg"} minH={"400px"}>
                    <Heading size={"md"} color={"white"} textAlign={"center"} mb={2}>
                        Course Creation Requests
                    </Heading>
                    <SimpleGrid
                        templateColumns="repeat(auto-fill, minmax(200px, 400px))"
                        spacing={4}
                        justifyContent={"center"}
                    >
                        {dossierDetails?.courseCreationRequests?.map((courseCreationRequest) => (
                            <Card key={courseCreationRequest.id} boxShadow={"xl"} maxW={"400px"}>
                                <CardBody>
                                    <Stack spacing="4">
                                        <Heading size="md" color={"brandRed"}>
                                            {courseCreationRequest.newCourse?.title}
                                        </Heading>
                                        <Stack>
                                            <Kbd width={"fit-content"}>
                                                Course ID: {courseCreationRequest.newCourse?.courseID}
                                            </Kbd>
                                            <Kbd width={"fit-content"}>
                                                Subject: {courseCreationRequest.newCourse?.subject}
                                            </Kbd>
                                            <Kbd width={"fit-content"}>
                                                Catalog: {courseCreationRequest.newCourse?.catalog}
                                            </Kbd>
                                        </Stack>
                                        <Textarea
                                            isReadOnly
                                            variant={"filled"}
                                            value={courseCreationRequest.newCourse?.description}
                                        />
                                        <Stack>
                                            <Text>Credits: {courseCreationRequest.newCourse?.creditValue}</Text>
                                            <Text>Prerequisites: {courseCreationRequest.newCourse?.preReqs}</Text>
                                            <Text>
                                                Equivalent Courses:{" "}
                                                {courseCreationRequest.newCourse.equivalentCourses === null ||
                                                courseCreationRequest.newCourse?.equivalentCourses === ""
                                                    ? "N/A"
                                                    : courseCreationRequest.newCourse?.equivalentCourses}
                                            </Text>
                                            <Text>
                                                Career:
                                                {" " +
                                                    courseSettings?.courseCareers.find(
                                                        (courseCareer) =>
                                                            courseCareer.careerCode ===
                                                            courseCreationRequest.newCourse?.career
                                                    )?.careerName}
                                            </Text>
                                        </Stack>
                                    </Stack>
                                </CardBody>
                                <Divider />
                                <CardFooter>
                                    <ButtonGroup spacing="2">
                                        <Button
                                            variant="solid"
                                            style="primary"
                                            onClick={() => {
                                                editCourseCreationRequest(courseCreationRequest);
                                            }}
                                        >
                                            Edit
                                        </Button>
                                        <Button
                                            variant="outline"
                                            style="secondary"
                                            onClick={() => {
                                                setSelectedCourseCreationRequest(courseCreationRequest);
                                                onOpen();
                                            }}
                                        >
                                            Delete
                                        </Button>
                                    </ButtonGroup>
                                </CardFooter>
                            </Card>
                        ))}
                    </SimpleGrid>

                    <Divider marginTop={10} marginBottom={2} />
                    <Button
                        backgroundColor="brandRed100"
                        _hover={{ bg: "brandRed600" }}
                        variant="solid"
                        style="secondary"
                        width="100%"
                        onClick={() => {
                            navigate(BaseRoutes.AddCourse.replace(":dossierId", dossierId));
                        }}
                    >
                        Add Creation Request
                    </Button>
                </Box>
                <Box backgroundColor="brandBlue" m={"auto"} mt={5} p="3" borderRadius={"lg"} minH={"400px"}>
                    <Heading size={"md"} color={"white"} textAlign={"center"} mb={2}>
                        Course Modification Requests
                    </Heading>
                    <SimpleGrid
                        templateColumns="repeat(auto-fill, minmax(200px, 400px))"
                        spacing={4}
                        justifyContent={"center"}
                    >
                        {dossierDetails?.courseModificationRequests?.map((courseModificationRequest) => (
                            <Card key={courseModificationRequest.id} boxShadow={"xl"}>
                                <CardBody>
                                    <Stack spacing="4">
                                        <Heading size="md" color={"brandBlue"}>
                                            {courseModificationRequest.course?.title}
                                        </Heading>
                                        <Stack>
                                            <Kbd width={"fit-content"}>
                                                Course ID: {courseModificationRequest.course?.courseID}
                                            </Kbd>
                                            <Kbd width={"fit-content"}>
                                                Subject: {courseModificationRequest.course?.subject}
                                            </Kbd>
                                            <Kbd width={"fit-content"}>
                                                Catalog: {courseModificationRequest.course?.catalog}
                                            </Kbd>
                                        </Stack>
                                        <Textarea
                                            isReadOnly
                                            variant={"filled"}
                                            value={courseModificationRequest.course?.description}
                                        />
                                        <Stack>
                                            <Text>Credits: {courseModificationRequest.course?.creditValue}</Text>
                                            <Text>Prerequisites: {courseModificationRequest.course?.preReqs}</Text>
                                            <Text>
                                                Equivalent Courses:{" "}
                                                {courseModificationRequest.course.equivalentCourses === null ||
                                                courseModificationRequest.course?.equivalentCourses === ""
                                                    ? "N/A"
                                                    : courseModificationRequest.course?.equivalentCourses}
                                            </Text>
                                            <Text>
                                                Career:{" "}
                                                {" " +
                                                    courseSettings?.courseCareers.find(
                                                        (courseCareer) =>
                                                            courseCareer.careerCode ===
                                                            courseModificationRequest.course?.career
                                                    )?.careerName}
                                            </Text>
                                        </Stack>
                                    </Stack>
                                </CardBody>
                                <Divider />
                                <CardFooter>
                                    <ButtonGroup spacing="2">
                                        <Button
                                            variant="solid"
                                            style="secondary"
                                            onClick={() => {
                                                editCourseModificationRequest(courseModificationRequest);
                                            }}
                                        >
                                            Edit
                                        </Button>
                                        <Button
                                            variant="outline"
                                            style="primary"
                                            onClick={() => {
                                                setSelectedCourseModificationRequest(courseModificationRequest);
                                                onOpen();
                                            }}
                                        >
                                            Delete
                                        </Button>
                                    </ButtonGroup>
                                </CardFooter>
                            </Card>
                        ))}
                    </SimpleGrid>
                    <Divider marginTop={10} marginBottom={2} />

                    <Button
                        backgroundColor="brandBlue100"
                        _hover={{ bg: "brandBlue" }}
                        variant="solid"
                        style="primary"
                        width="100%"
                        onClick={() => {
                            onEditOpen();
                        }}
                    >
                        Add Modification Request
                    </Button>
                </Box>
                <Box backgroundColor="brandGray" m={"auto"} mt={5} p="3" borderRadius={"lg"} minH={"400px"}>
                    <Heading size={"md"} color={"white"} textAlign={"center"} mb={2}>
                        Course Deletion Requests
                    </Heading>
                    <SimpleGrid
                        templateColumns="repeat(auto-fill, minmax(200px, 400px))"
                        spacing={4}
                        justifyContent={"center"}
                    >
                        {dossierDetails?.courseDeletionRequests?.map((courseDeletionRequest) => (
                            <Card key={courseDeletionRequest.id} boxShadow={"xl"}>
                                <CardBody>
                                    <Stack spacing="4">
                                        <Heading size="md" color={"brandBlue"}>
                                            {courseDeletionRequest.course?.title}
                                        </Heading>
                                        <Stack>
                                            <Kbd width={"fit-content"}>
                                                Course ID: {courseDeletionRequest.course?.courseID}
                                            </Kbd>
                                            <Kbd width={"fit-content"}>
                                                Subject: {courseDeletionRequest.course?.subject}
                                            </Kbd>
                                            <Kbd width={"fit-content"}>
                                                Catalog: {courseDeletionRequest.course?.catalog}
                                            </Kbd>
                                        </Stack>
                                        <Textarea
                                            isReadOnly
                                            variant={"filled"}
                                            value={courseDeletionRequest.course?.description}
                                        />
                                        <Stack>
                                            <Text>Credits: {courseDeletionRequest.course?.creditValue}</Text>
                                            <Text>Prerequisites: {courseDeletionRequest.course?.preReqs}</Text>
                                            <Text>
                                                Equivalent Courses:{" "}
                                                {courseDeletionRequest.course.equivalentCourses === null ||
                                                courseDeletionRequest.course?.equivalentCourses === ""
                                                    ? "N/A"
                                                    : courseDeletionRequest.course?.equivalentCourses}
                                            </Text>
                                            <Text>
                                                Career:{" "}
                                                {" " +
                                                    courseSettings?.courseCareers.find(
                                                        (courseCareer) =>
                                                            courseCareer.careerCode ===
                                                            courseDeletionRequest.course?.career
                                                    )?.careerName}
                                            </Text>
                                        </Stack>
                                    </Stack>
                                </CardBody>
                                <Divider />
                                <CardFooter>
                                    <ButtonGroup spacing="2">
                                        <Button
                                            variant="solid"
                                            style="primary"
                                            onClick={() => {
                                                navigate(BaseRoutes.DeleteCourseEdit.replace(":dossierId", dossierId), {
                                                    state: { key: courseDeletionRequest },
                                                });
                                            }}
                                        >
                                            Edit
                                        </Button>
                                        <Button
                                            variant="outline"
                                            style="primary"
                                            onClick={() => {
                                                setSelectedCourseDeletionRequest(courseDeletionRequest);
                                                onOpen();
                                            }}
                                        >
                                            Delete
                                        </Button>
                                    </ButtonGroup>
                                </CardFooter>
                            </Card>
                        ))}
                    </SimpleGrid>
                    <Divider marginTop={10} marginBottom={2} />

                    <Button
                        backgroundColor="brandGray500"
                        _hover={{ bg: "brandGray" }}
                        variant="solid"
                        style="secondary"
                        width="100%"
                        onClick={() => {
                            navigate(BaseRoutes.DeleteCourse.replace(":dossierId", dossierId));
                        }}
                    >
                        Add Deletion Request
                    </Button>
                </Box>

                <Box
                    mt={2}
                    p={2}
                    borderRadius={"lg"}
                    border={"2px"}
                    borderColor={"brandGray200"}
                    display="flex"
                    flexDirection="column"
                    justifyContent="space-between"
                >
                    <Heading mb={2}>Approval Stages</Heading>

                    <Box>
                        {dossierDetails?.approvalStages
                            ?.sort((a, b) => (a.stageIndex - b.stageIndex > 0 ? 1 : -1))
                            .map((stage) => (
                                <Box key={stage.stageIndex}>
                                    <Text>
                                        {stage.group.name} {stage.isCurrentStage ? <ArrowBackIcon /> : ""}
                                    </Text>
                                </Box>
                            ))}
                    </Box>
                    <Button
                        mt={2}
                        alignSelf={"flex-end"}
                        variant="outline"
                        style="primary"
                        onClick={() => {
                            dispaylayApprovalStagesModal();
                        }}
                        isDisabled={dossierDetails?.approvalStages.length !== 0}
                    >
                        Edit
                    </Button>
                </Box>

                {showApprovalStagesModal && (
                    <EditApprovalStagesModal
                        open={showApprovalStagesModal}
                        closeModal={closeApprovalStagesModal}
                        dossierId={dossierId}
                    />
                )}
            </Container>
        </>
    );
}
