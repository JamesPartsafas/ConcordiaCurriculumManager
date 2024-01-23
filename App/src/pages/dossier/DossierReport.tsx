import { Container, ListItem, OrderedList, Text, UnorderedList } from "@chakra-ui/react";
import Button from "../../components/Button";
import { BaseRoutes } from "../../constants";
import { useNavigate, useParams } from "react-router-dom";
import { UserContext } from "../../App";
import { useContext, useEffect, useState } from "react";
import { DossierDetailsDTO, DossierDetailsResponse } from "../../models/dossier";
import { getDossierDetails } from "../../services/dossier";
import { AllCourseSettings } from "../../models/course";
import { getAllCourseSettings } from "../../services/course";

export default function DossierReport() {
    const navigate = useNavigate();
    const user = useContext(UserContext);
    const { dossierId } = useParams();
    const [dossierDetails, setDossierDetails] = useState<DossierDetailsDTO | null>(null);
    const [allCourseSettings, setAllCourseSettings] = useState<AllCourseSettings>(null);

    useEffect(() => {
        requestDossierDetails(dossierId);
        requestAllCareerSettings();
    }, [dossierId]);

    async function requestDossierDetails(dossierId: string) {
        const dossierDetailsData: DossierDetailsResponse = await getDossierDetails(dossierId);
        setDossierDetails(dossierDetailsData.data);
    }

    async function requestAllCareerSettings() {
        const allCourseSettingsData: AllCourseSettings = (await getAllCourseSettings()).data;
        setAllCourseSettings(allCourseSettingsData);
    }

    return (
        <div>
            <Container maxW={"70%"} mt={5} mb={2}>
                <Button
                    style="primary"
                    variant="outline"
                    height="40px"
                    width="fit-content"
                    onClick={() => navigate(BaseRoutes.Home)}
                >
                    Return to Home
                </Button>

                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginBottom="5">
                    {dossierDetails?.title}
                </Text>

                <Text fontSize="xl" marginBottom="5">
                    <b>Description of Dossier:</b> {dossierDetails?.description}
                </Text>

                <Text fontSize="xl" as="b">
                    Course Creation Requests:
                </Text>

                <OrderedList ml={12} mt={2}>
                    {dossierDetails?.courseCreationRequests.map((courseCreationRequest, index) => (
                        <ListItem key={index}>
                            <div key={index}>
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

                                <Text fontSize="md" marginBottom="1">
                                    <b>Credits:</b> {courseCreationRequest.newCourse.creditValue ?? "N/A"}
                                </Text>

                                <Text fontSize="md" marginBottom="1">
                                    <b>Component(s):</b>{" "}
                                    {courseCreationRequest.conflict == "" ? "N/A" : courseCreationRequest.comment} - TO
                                    FIX
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

                                <Text fontSize="md" marginBottom="3">
                                    <b>Course Description:</b> <br />{" "}
                                    {courseCreationRequest.newCourse.description ?? "N/A"}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b>Rationale:</b> <br />
                                    {courseCreationRequest.newCourse.rationale ?? "N/A"}
                                </Text>

                                <Text fontSize="md" marginBottom="3">
                                    <b> Ressource Implications:</b> <br />
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
            </Container>
        </div>
    );
}
