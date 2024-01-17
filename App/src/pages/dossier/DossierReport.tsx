import { useEffect, useState } from "react";
import { getDossierReport } from "../../services/dossier";
import { DossierReportDTO } from "../../models/dossier";
import {
    Heading,
    Center,
    Text,
    Card,
    CardHeader,
    CardBody,
    SimpleGrid,
    CardFooter,
    Button,
    Box,
} from "@chakra-ui/react";

export default function DossierReport() {
    const [dossierReport, setDossierReport] = useState<DossierReportDTO | null>(null);
    const items = [
        { title: "Course Creation Requests:", key: "courseCreationRequests" },
        { title: "Course Deletion Requests:", key: "courseDeletionRequests" },
        { title: "Course Modification Requests:", key: "courseModificationRequests" },
    ];

    useEffect(() => {
        getDossierReport("37581d9d-713f-475c-9668-23971b0e64d0")
            .then((res) => {
                setDossierReport(res.data);
                console.log(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    }, []);
    return (
        <>
            {dossierReport ? (
                <>
                    <Center mt={4}>
                        <Heading>{dossierReport.title}</Heading>
                    </Center>
                    <Center>
                        <Text color={"gray.600"} p={2}>
                            {dossierReport.description}
                        </Text>
                    </Center>
                    {items.map((el, index) => {
                        return (
                            <Box key={index}>
                                <Heading p={4}>{el.title}</Heading>
                                <SimpleGrid spacing={4} templateColumns="repeat(auto-fill, minmax(200px, 1fr))">
                                    {dossierReport[el.key].map((el, index) => {
                                        return (
                                            <Card key={index}>
                                                <CardHeader>
                                                    <Heading size="md">
                                                        {el.newCourse ? el.newCourse.subject : el.course.subject}{" "}
                                                        {el.newCourse ? el.newCourse.catalog : el.course.catalog}
                                                    </Heading>
                                                </CardHeader>
                                                <CardBody py={2}>
                                                    <Text>
                                                        <b>Comment:</b> {el.comment ? el.comment : "No comment"}
                                                    </Text>
                                                </CardBody>
                                                <CardFooter>
                                                    <Button>View</Button>
                                                </CardFooter>
                                            </Card>
                                        );
                                    })}
                                </SimpleGrid>
                            </Box>
                        );
                    })}
                </>
            ) : null}
        </>
    );
}
