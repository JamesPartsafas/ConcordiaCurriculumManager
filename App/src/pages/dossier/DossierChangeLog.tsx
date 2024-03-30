import { Box, Center, Container, Flex, Heading, ListItem, OrderedList, Spacer, Text, useToast } from "@chakra-ui/react";
import Button from "../../components/Button";
import { BaseRoutes } from "../../constants";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { ChangeLogDTO } from "../../models/dossier";
import { changeLogPublishCourse, getChangesAcrossAllDossiers } from "../../services/dossier";
import { AllCourseSettings, componentMappings } from "../../models/course";
import { getAllCourseSettings } from "../../services/course";
import CourseDiffViewer from "../../components/CourseDifference/CourseDiffViewer";
import { Divider } from "@chakra-ui/react";
import "../../assets/styles/print.css";
import { Link } from "react-router-dom";
import { showToast } from "../../utils/toastUtils";
import { useContext } from "react";
import { isAdminOrGroupMaster } from "../../services/auth";
import { UserContext } from "../../App";
import DossierReportCourseGrouping from "../../components/Dossiers/DossierReportCourseGrouping";

export default function DossierChangeLog() {
    const toast = useToast();
    const user = useContext(UserContext);
    const navigate = useNavigate();
    const [dossierReport, setDossierReport] = useState<ChangeLogDTO | null>(null);
    const [allCourseSettings, setAllCourseSettings] = useState<AllCourseSettings>(null);

    useEffect(() => {
        requestAllCareerSettings();
        getChangesAcrossAllDossiers()
            .then((response) => {
                setDossierReport(response.data);
            })
            .catch((error) => {
                console.error(error);
            });
    }, []);

    async function requestAllCareerSettings() {
        const allCourseSettingsData: AllCourseSettings = (await getAllCourseSettings()).data;
        setAllCourseSettings(allCourseSettingsData);
    }
    function handlePrint() {
        window.print();
    }

    const publishCourse = (subject: string, catalog: string) => {
        changeLogPublishCourse(subject, catalog)
            .then(() => {
                showToast(toast, "Success!", "Course has been published.", "success");
                getChangesAcrossAllDossiers()
                    .then((response) => {
                        setDossierReport(response.data);
                    })
                    .catch((error) => {
                        showToast(toast, "Error!", error.message, "error");
                    });
            })
            .catch((error) => {
                showToast(toast, "Error!", error.response.data, "error");
            });
    };

    return (
        dossierReport && (
            <div>
                <Container maxW={"90%"} mt={5} mb={2} className="printable-content">
                    <Box mb={5}>
                        <Button
                            style="primary"
                            variant="outline"
                            height="40px"
                            width="fit-content"
                            onClick={() => navigate(BaseRoutes.Home)}
                            className="non-printable-content"
                        >
                            Back
                        </Button>
                    </Box>

                    <Center>
                        <Heading fontSize="5xl" mb={4} mt={4}>
                            Change Log
                        </Heading>
                    </Center>

                    <Heading fontSize="4xl" mb={4} mt={4}>
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
                                    <Link
                                        to={BaseRoutes.DossierDetails.replace(
                                            ":dossierId",
                                            courseCreationRequest.dossierId
                                        )}
                                    >
                                        <Text
                                            _hover={{ textDecoration: "underline" }}
                                            color="brandRed"
                                            fontSize="xl"
                                            className="non-printable-content"
                                        >
                                            Visit Related Dossier
                                        </Text>
                                    </Link>
                                    <Flex mb={6} gap={12} flexWrap={"wrap"}>
                                        <Box>
                                            <Text fontSize="md">
                                                <b>Course Career:</b>{" "}
                                                {allCourseSettings?.courseCareers.find(
                                                    (career) =>
                                                        career.careerCode === courseCreationRequest.newCourse.career
                                                ).careerName ?? "N/A"}
                                            </Text>

                                            <Text fontSize="md">
                                                <b>Course Subject:</b>{" "}
                                                {courseCreationRequest.newCourse.subject ?? "N/A"}
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
                                                        const hours =
                                                            courseCreationRequest.newCourse.componentCodes[code];
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
                                {isAdminOrGroupMaster(user) && (
                                    <Button
                                        style="primary"
                                        variant="outline"
                                        height="40px"
                                        width="fit-content"
                                        onClick={() =>
                                            publishCourse(
                                                courseCreationRequest.newCourse.subject,
                                                courseCreationRequest.newCourse.catalog
                                            )
                                        }
                                        className="non-printable-content"
                                    >
                                        Publish
                                    </Button>
                                )}
                            </ListItem>
                        ))}
                    </OrderedList>

                    <Heading size="xl">Course Deletion Requests:</Heading>

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
                                    <Link
                                        to={BaseRoutes.DossierDetails.replace(
                                            ":dossierId",
                                            courseCreationRequest.dossierId
                                        )}
                                    >
                                        <Text
                                            _hover={{ textDecoration: "underline" }}
                                            color="brandRed"
                                            fontSize="xl"
                                            className="non-printable-content"
                                        >
                                            Visit Related Dossier
                                        </Text>
                                    </Link>
                                    <Flex mb={6} gap={12} flexWrap={"wrap"}>
                                        <Box>
                                            <Text fontSize="md">
                                                <b>Course Career:</b>{" "}
                                                {allCourseSettings?.courseCareers.find(
                                                    (career) =>
                                                        career.careerCode === courseCreationRequest.course.career
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
                                        <b> Resource Implications:</b> <br />
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
                                {isAdminOrGroupMaster(user) && (
                                    <Button
                                        style="primary"
                                        variant="outline"
                                        height="40px"
                                        width="fit-content"
                                        onClick={() =>
                                            publishCourse(
                                                courseCreationRequest.course.subject,
                                                courseCreationRequest.course.catalog
                                            )
                                        }
                                        className="non-printable-content"
                                    >
                                        Publish
                                    </Button>
                                )}
                            </ListItem>
                        ))}
                    </OrderedList>

                    <Heading fontSize="4xl" mb={4} mt={4}>
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
                                    <Link
                                        to={BaseRoutes.DossierDetails.replace(
                                            ":dossierId",
                                            courseModificationRequest.dossierId
                                        )}
                                    >
                                        <Text
                                            _hover={{ textDecoration: "underline" }}
                                            color="brandRed"
                                            fontSize="xl"
                                            className="non-printable-content"
                                        >
                                            Visit Related Dossier
                                        </Text>
                                    </Link>
                                    <Heading size={"md"} mb={2}>
                                        {courseModificationRequest.course.subject}{" "}
                                        {courseModificationRequest.course.catalog}{" "}
                                        {courseModificationRequest.course.title}
                                    </Heading>
                                    <Text fontSize="md" marginBottom="3">
                                        <b> Resource Implications: </b>
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
                                {isAdminOrGroupMaster(user) && (
                                    <Button
                                        style="primary"
                                        variant="outline"
                                        height="40px"
                                        width="fit-content"
                                        onClick={() =>
                                            publishCourse(
                                                courseModificationRequest.course.subject,
                                                courseModificationRequest.course.catalog
                                            )
                                        }
                                        className="non-printable-content"
                                    >
                                        Publish
                                    </Button>
                                )}
                            </ListItem>
                        ))}
                    </OrderedList>
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
                    Print Change Log
                </Button>
                <div className="divFooter">Date Printed: {Date()}</div>
            </div>
        )
    );
}
