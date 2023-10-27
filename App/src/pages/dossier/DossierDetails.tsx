import { useEffect, useState } from "react";
import { DossierDetailsDTO, DossierDetailsResponse } from "../../models/dossier";
import { getDossierDetails } from "../../services/dossier";
import { useParams } from "react-router-dom";
import {
    Box,
    Button,
    ButtonGroup,
    Card,
    CardBody,
    CardFooter,
    Divider,
    Heading,
    Kbd,
    SimpleGrid,
    Stack,
    Text,
    Textarea,
} from "@chakra-ui/react";

export default function DossierDetails() {
    const { dossierId } = useParams();
    const [dossierDetails, setDossierDetails] = useState<DossierDetailsDTO | null>(null);

    useEffect(() => {
        requestDossierDetails(dossierId);
    }, [dossierId]);

    function requestDossierDetails(dossierId: string) {
        getDossierDetails(dossierId).then((res: DossierDetailsResponse) => {
            setDossierDetails(res.data);
            console.log(res.data);
        });
    }

    return (
        <>
            <div style={{ margin: "auto", width: "fit-content" }}>
                <Heading color={"brandRed"}>{dossierDetails?.title}</Heading>
                <Kbd>{dossierDetails?.id}</Kbd>
                <Text>{dossierDetails?.description}</Text>
                <Text>published: {dossierDetails?.published ? "yes" : "no"}</Text>
                <Text>created: {dossierDetails?.createdDate.toString()}</Text>
                <Text>updated: {dossierDetails?.modifiedDate.toString()}</Text>
            </div>

            <Box backgroundColor={"brandRed"} m={"auto"} mt={5} p="3" width={"70%"} borderRadius={"lg"} minH={"400px"}>
                <Heading size={"md"} color={"white"} textAlign={"center"} mb={2}>
                    Course Creation Requests
                </Heading>
                <SimpleGrid
                    templateColumns="repeat(auto-fill, minmax(200px, 400px))"
                    spacing={4}
                    justifyContent={"center"}
                >
                    {dossierDetails?.courseCreationRequests?.map((courseCreationRequest) => (
                        <Card key={courseCreationRequest.id} boxShadow={"xl"}>
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
                                        <Text>Career: {courseCreationRequest.newCourse?.career}</Text>
                                    </Stack>
                                    <Stack alignSelf={"end"} alignItems={"baseline"}>
                                        <Text>Course State: {courseCreationRequest.newCourse?.courseState}</Text>
                                        <Text>
                                            Version: <Kbd>{courseCreationRequest.newCourse?.version}</Kbd>
                                        </Text>
                                    </Stack>
                                </Stack>
                            </CardBody>
                            <Divider />
                            <CardFooter>
                                <ButtonGroup spacing="2">
                                    <Button variant="solid" colorScheme="blue">
                                        View
                                    </Button>
                                    <Button variant="ghost" colorScheme="blue">
                                        Delete
                                    </Button>
                                </ButtonGroup>
                            </CardFooter>
                        </Card>
                    ))}
                </SimpleGrid>
            </Box>
            <Box backgroundColor="brandBlue" m={"auto"} mt={5} p="3" width={"70%"} borderRadius={"lg"} minH={"400px"}>
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
                                        <Text>Career: {courseModificationRequest.course?.career}</Text>
                                    </Stack>
                                    <Stack alignSelf={"end"} alignItems={"baseline"}>
                                        <Text>Course State: {courseModificationRequest.course?.courseState}</Text>
                                        <Text>
                                            Version: <Kbd>{courseModificationRequest.course?.version}</Kbd>
                                        </Text>
                                    </Stack>
                                </Stack>
                            </CardBody>
                            <Divider />
                            <CardFooter>
                                <ButtonGroup spacing="2">
                                    <Button variant="solid" colorScheme="blue">
                                        View
                                    </Button>
                                    <Button variant="ghost" colorScheme="blue">
                                        Delete
                                    </Button>
                                </ButtonGroup>
                            </CardFooter>
                        </Card>
                    ))}
                </SimpleGrid>
            </Box>
        </>
    );
}
