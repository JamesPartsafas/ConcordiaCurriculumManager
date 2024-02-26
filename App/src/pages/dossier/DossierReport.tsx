import { Badge, Box, Center, Container, Flex, Heading, ListItem, OrderedList, Spacer, Text } from "@chakra-ui/react";
import Button from "../../components/Button";
import { BaseRoutes } from "../../constants";
import { useNavigate, useParams } from "react-router-dom";
import { useContext, useEffect, useState } from "react";
import { DossierReportDTO, DossierReportResponse, DossierStateEnum } from "../../models/dossier";
import { getDossierReport } from "../../services/dossier";
import { AllCourseSettings, componentMappings } from "../../models/course";
import { getAllCourseSettings } from "../../services/course";
import CourseDiffViewer from "../../components/CourseDifference/CourseDiffViewer";
import { Divider } from "@chakra-ui/react";
import "../../assets/styles/print.css";
import { UserContext } from "../../App";
import { UserRoles } from "../../models/user";
import DossierReportCourseGrouping from "../../components/Dossiers/DossierReportCourseGrouping";

export default function DossierReport() {
    const navigate = useNavigate();
    const { dossierId } = useParams();
    const [dossierReport, setDossierReport] = useState<DossierReportDTO | null>(null);
    const [allCourseSettings, setAllCourseSettings] = useState<AllCourseSettings>(null);
    const user = useContext(UserContext);

    useEffect(() => {
        requestDossierReport(dossierId);
        requestAllCareerSettings();
    }, [dossierId]);

    async function requestDossierReport(dossierId: string) {
        const dossierReportData: DossierReportResponse = await getDossierReport(dossierId);
        setDossierReport(dossierReportData.data);
        console.log(dossierReportData.data);
    }

    async function requestAllCareerSettings() {
        const allCourseSettingsData: AllCourseSettings = (await getAllCourseSettings()).data;
        setAllCourseSettings(allCourseSettingsData);
    }
    function handlePrint() {
        window.print();
    }

    return (
        <div>
            <Container maxW={"90%"} mt={12} mb={2} className="printable-content">
                <Box mb={5}>
                    <Button
                        style="primary"
                        variant="outline"
                        height="40px"
                        width="fit-content"
                        onClick={() => navigate(BaseRoutes.Dossiers)}
                        className="non-printable-content"
                    >
                        My Dossiers
                    </Button>

                    <Button
                        style="primary"
                        variant="outline"
                        width="fit-content"
                        height="40px"
                        ml={2}
                        className="non-printable-content"
                        isDisabled={!user.roles.includes(UserRoles.Initiator)}
                        onClick={() => {
                            navigate(BaseRoutes.DossiersToReview);
                        }}
                    >
                        Dossiers To Review
                    </Button>

                    <Button
                        style="primary"
                        variant="outline"
                        width="fit-content"
                        height="40px"
                        className="non-printable-content"
                        ml={2}
                        isDisabled={!user.roles.includes(UserRoles.Initiator)}
                        onClick={() => {
                            navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId));
                        }}
                    >
                        Dossiers Details
                    </Button>

                    <Button
                        style="primary"
                        variant="outline"
                        width="fit-content"
                        height="40px"
                        className="non-printable-content"
                        ml={2}
                        isDisabled={dossierReport?.state !== DossierStateEnum.InReview}
                        onClick={() => {
                            navigate(BaseRoutes.DossierReview.replace(":dossierId", dossierId));
                        }}
                    >
                        Dossier Review
                    </Button>
                </Box>

                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginBottom="1">
                    {dossierReport?.title}
                </Text>

                <Container textAlign={"center"}>
                    {dossierReport?.state === DossierStateEnum.Approved && (
                        <Badge colorScheme="green" fontSize="md" variant={"solid"} marginBottom="2">
                            Approved
                        </Badge>
                    )}

                    {dossierReport?.state === DossierStateEnum.Rejected && (
                        <Badge colorScheme="red" fontSize="md" variant={"solid"} marginBottom="2">
                            Rejected
                        </Badge>
                    )}

                    {dossierReport?.state === DossierStateEnum.InReview && (
                        <Badge colorScheme="yellow" fontSize="md" variant={"solid"} marginBottom="2">
                            Under Review
                        </Badge>
                    )}

                    {dossierReport?.state === DossierStateEnum.Created && (
                        <Badge fontSize="md" variant={"solid"} marginBottom="2">
                            Created
                        </Badge>
                    )}
                </Container>

                <Text fontSize="xl" marginBottom="2" style={{ height: "100%" }}>
                    <b>Description of Dossier:</b>
                </Text>
                <Box p={"8px 16px"} mb={3} width={"100%"} backgroundColor={"gray.100"} borderRadius={"lg"}>
                    {dossierReport?.description}
                </Box>

                <Box mb={8}>
                    <Box display={"flex"} gap={1}>
                        <Text fontSize="xl" mb={2}>
                            <b>Approval Stages: </b>
                        </Text>
                        <Text alignSelf={"center"}>
                            {dossierReport?.state === DossierStateEnum.Created && "(Not Submitted)"}
                        </Text>
                    </Box>
                    <OrderedList>
                        {dossierReport?.approvalStages
                            .sort((a, b) => (a.stageIndex - b.stageIndex > 0 ? 1 : -1))
                            .map((stage, index) => (
                                <ListItem ml={12} mb={2} key={index}>
                                    <Box fontSize="md">
                                        <Text>
                                            {" "}
                                            <b>{stage.group.name}</b> {stage.isCurrentStage ? "(Current Stage)" : ""}
                                        </Text>
                                    </Box>
                                </ListItem>
                            ))}
                    </OrderedList>
                </Box>
                <Center>
                    <Heading fontSize="3xl" mb={4} mt={4} color="brandRed">
                        Course Requests
                    </Heading>
                </Center>
                <Heading fontSize="2xl" mb={4} mt={4}>
                    Course Creation Requests:
                </Heading>

                <OrderedList ml={12} mt={2} mb={10}>
                    {dossierReport?.courseCreationRequests?.length === 0 && (
                        <Box backgroundColor={"brandRed600"} p={5} borderRadius={"xl"}>
                            <Text fontSize="md">No course creation requests.</Text>
                        </Box>
                    )}

                    {dossierReport?.courseCreationRequests?.map((courseCreationRequest, index) => (
                        <ListItem
                            className="pageBreak"
                            key={index}
                            backgroundColor={"brandRed600"}
                            p={5}
                            borderRadius={"xl"}
                            mb={2}
                        >
                            <div>
                                <Flex mb={6} gap={12} flexWrap={"wrap"}>
                                    <Box>
                                        <Text fontSize="md">
                                            <b>Course Career:</b>{" "}
                                            {allCourseSettings?.courseCareers.find(
                                                (career) => career.careerCode === courseCreationRequest.newCourse.career
                                            ).careerName ?? "N/A"}
                                        </Text>

                                        <Text fontSize="md">
                                            <b>Course Subject:</b> {courseCreationRequest.newCourse.subject ?? "N/A"}
                                        </Text>

                                        <Text fontSize="md">
                                            <b>Course Name:</b> {courseCreationRequest.newCourse.title ?? "N/A"}
                                        </Text>

                                        <Text fontSize="md" marginBottom="1">
                                            <b>Course Code:</b> {courseCreationRequest.newCourse.catalog ?? "N/A"}
                                        </Text>

                                        <Text fontSize="md">
                                            <b>Credits:</b> {courseCreationRequest.newCourse.creditValue ?? "N/A"}
                                        </Text>
                                    </Box>
                                    <Spacer />
                                    <Spacer />
                                    <Box>
                                        <Text fontSize="md" marginBottom="1">
                                            <b>Component(s):</b>{" "}
                                            {allCourseSettings?.courseComponents
                                                .filter((component) =>
                                                    Object.keys(
                                                        courseCreationRequest?.newCourse.componentCodes || {}
                                                    ).includes(componentMappings[component.componentName])
                                                )
                                                .map((filteredComponent) => {
                                                    const code = componentMappings[filteredComponent.componentName];
                                                    const hours = courseCreationRequest.newCourse.componentCodes[code];
                                                    return `${filteredComponent.componentName} ${hours} hour(s) per week`;
                                                })
                                                .join(", ")}
                                        </Text>

                                        <Text fontSize="md" marginBottom="1">
                                            <b>Equivalent Courses:</b>{" "}
                                            {courseCreationRequest.newCourse.equivalentCourses ?? "N/A"}
                                        </Text>

                                        <Text fontSize="md" marginBottom="3">
                                            <b>Prerequisites or Corequisites:</b>{" "}
                                            {courseCreationRequest.newCourse.preReqs == ""
                                                ? "N/A"
                                                : courseCreationRequest.newCourse.preReqs}
                                        </Text>
                                    </Box>
                                </Flex>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Course Description:</b> <br />{" "}
                                    {courseCreationRequest.newCourse.description ?? "N/A"}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Rationale:</b> <br />
                                    {courseCreationRequest.newCourse.rationale ?? "N/A"}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b> Resource Implication:</b> <br />
                                    {courseCreationRequest.newCourse.resourceImplication ?? "N/A"}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Comments:</b> <br />
                                    {courseCreationRequest.comment == "" ? "N/A" : courseCreationRequest.comment}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Conflicts:</b> <br />
                                    {courseCreationRequest.conflict == "" ? "N/A" : courseCreationRequest.comment}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Notes:</b> <br />
                                    {courseCreationRequest.newCourse.courseNotes == ""
                                        ? "N/A"
                                        : courseCreationRequest.newCourse.courseNotes}
                                </Text>
                            </div>
                        </ListItem>
                    ))}
                </OrderedList>

                <Heading fontSize="2xl" mb={4} mt={4}>
                    Course Deletion Requests:
                </Heading>

                <OrderedList ml={12} mt={2}>
                    {dossierReport?.courseDeletionRequests?.length === 0 && (
                        <Box backgroundColor={"brandGray200"} p={5} borderRadius={"xl"}>
                            <Text fontSize="md">No course deletion requests.</Text>
                        </Box>
                    )}

                    {dossierReport?.courseDeletionRequests.map((courseCreationRequest, index) => (
                        <ListItem
                            className="pageBreak"
                            key={index}
                            backgroundColor={"brandGray200"}
                            p={5}
                            borderRadius={"xl"}
                            mb={2}
                        >
                            <div>
                                <Flex mb={6} gap={12} flexWrap={"wrap"}>
                                    <Box>
                                        <Text fontSize="md">
                                            <b>Course Career:</b>{" "}
                                            {allCourseSettings?.courseCareers.find(
                                                (career) => career.careerCode === courseCreationRequest.course.career
                                            ).careerName ?? "N/A"}
                                        </Text>

                                        <Text fontSize="md">
                                            <b>Course Subject:</b> {courseCreationRequest.course.subject ?? "N/A"}
                                        </Text>

                                        <Text fontSize="md">
                                            <b>Course Name:</b> {courseCreationRequest.course.title ?? "N/A"}
                                        </Text>

                                        <Text fontSize="md" marginBottom="1">
                                            <b>Course Code:</b> {courseCreationRequest.course.catalog ?? "N/A"}
                                        </Text>

                                        <Text fontSize="md">
                                            <b>Credits:</b> {courseCreationRequest.course.creditValue ?? "N/A"}
                                        </Text>
                                    </Box>
                                    <Spacer />
                                    <Spacer />
                                    <Box>
                                        <Text fontSize="md" marginBottom="1">
                                            <b>Component(s):</b>{" "}
                                            {allCourseSettings?.courseComponents
                                                .filter((component) =>
                                                    Object.keys(
                                                        courseCreationRequest?.course.componentCodes || {}
                                                    ).includes(componentMappings[component.componentName])
                                                )
                                                .map((filteredComponent) => {
                                                    const code = componentMappings[filteredComponent.componentName];
                                                    const hours = courseCreationRequest.course.componentCodes[code];
                                                    return `${filteredComponent.componentName} ${hours} hour(s) per week`;
                                                })
                                                .join(", ")}
                                        </Text>

                                        <Text fontSize="md" marginBottom="1">
                                            <b>Equivalent Courses:</b>{" "}
                                            {courseCreationRequest.course.equivalentCourses ?? "N/A"}
                                        </Text>

                                        <Text fontSize="md" marginBottom="3">
                                            <b>Prerequisites or Corequisites:</b>{" "}
                                            {courseCreationRequest.course.preReqs == ""
                                                ? "N/A"
                                                : courseCreationRequest.course.preReqs}
                                        </Text>
                                    </Box>
                                </Flex>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Course Description:</b> <br />{" "}
                                    {courseCreationRequest.course.description ?? "N/A"}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Rationale:</b> <br />
                                    {courseCreationRequest.course.rationale ?? "N/A"}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b> Ressource Implications:</b> <br />
                                    {courseCreationRequest.course.resourceImplication ?? "N/A"}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Comments:</b> <br />
                                    {courseCreationRequest.comment == "" ? "N/A" : courseCreationRequest.comment}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Conflicts:</b> <br />
                                    {courseCreationRequest.conflict == "" ? "N/A" : courseCreationRequest.comment}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Notes:</b> <br />
                                    {courseCreationRequest.course.courseNotes == ""
                                        ? "N/A"
                                        : courseCreationRequest.course.courseNotes}
                                </Text>
                            </div>
                        </ListItem>
                    ))}
                </OrderedList>

                <Heading fontSize="2xl" mb={4} mt={4}>
                    Course Modification Requests:
                </Heading>

                <OrderedList ml={12} mt={2}>
                    {dossierReport?.courseModificationRequests?.length === 0 && (
                        <Box backgroundColor={"brandBlue600"} p={5} borderRadius={"xl"}>
                            <Text fontSize="md">No course modification requests.</Text>
                        </Box>
                    )}

                    {dossierReport?.courseModificationRequests?.map((courseModificationRequest, index) => (
                        <ListItem
                            className="pageBreak"
                            key={index}
                            backgroundColor={"brandBlue600"}
                            p={5}
                            borderRadius={"xl"}
                            mb={2}
                        >
                            <>
                                <Heading size={"md"} mb={2}>
                                    {courseModificationRequest.course.subject}{" "}
                                    {courseModificationRequest.course.catalog} {courseModificationRequest.course.title}
                                </Heading>
                                <Text fontSize="md" marginBottom="3">
                                    <b> Ressource Implications: </b>
                                    {courseModificationRequest.resourceImplication == ""
                                        ? "N/A"
                                        : courseModificationRequest.resourceImplication}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Comments: </b>
                                    {courseModificationRequest.comment == ""
                                        ? "N/A"
                                        : courseModificationRequest.comment}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Conflicts: </b>
                                    {courseModificationRequest.conflict == ""
                                        ? "N/A"
                                        : courseModificationRequest.comment}
                                </Text>
                                <Text fontSize="md" marginBottom="3">
                                    <b>Rationale: </b>
                                    {courseModificationRequest.rationale == ""
                                        ? "N/A"
                                        : courseModificationRequest.rationale}
                                </Text>
                                <CourseDiffViewer
                                    oldCourse={dossierReport?.oldCourses[index]}
                                    newCourse={courseModificationRequest.course}
                                    allCourseSettings={allCourseSettings}
                                ></CourseDiffViewer>
                                <Divider />
                            </>
                        </ListItem>
                    ))}
                </OrderedList>
                <Center>
                    <Heading fontSize="3xl" mb={4} mt={4} color="brandRed" className="breakBefore">
                        Course Grouping Requests
                    </Heading>
                </Center>
                <DossierReportCourseGrouping
                    courseGrouping={dossierReport?.courseGroupingRequests}
                ></DossierReportCourseGrouping>
            </Container>

            <Button
                style="primary"
                variant="outline"
                height="40px"
                width="fit-content"
                onClick={handlePrint}
                m={14}
                className="non-printable-content"
            >
                Print Dossier Report
            </Button>
            <div className="divFooter">Date Printed: {Date()}</div>
        </div>
    );
}
