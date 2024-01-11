import { useEffect, useState } from "react";
import { getDossierReport } from "../../services/dossier";
import { DossierReportDTO } from "../../models/dossier";
import { Heading, Center, Text, Card, CardHeader, CardBody, SimpleGrid, CardFooter, Button } from "@chakra-ui/react";

export default function DossierReport() {
    const [dossierReport, setDossierReport] = useState<DossierReportDTO | null>(null);
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
                    <Heading p={4}>Course Creation Requests:</Heading>
                    <SimpleGrid spacing={4} templateColumns="repeat(auto-fill, minmax(200px, 1fr))">
                        {dossierReport.courseCreationRequests.map((el, index) => {
                            return (
                                <Card key={index}>
                                    <CardHeader>
                                        <Heading size="md">
                                            {el.newCourse.subject} {el.newCourse.catalog}
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
                </>
            ) : null}
        </>
    );
}
