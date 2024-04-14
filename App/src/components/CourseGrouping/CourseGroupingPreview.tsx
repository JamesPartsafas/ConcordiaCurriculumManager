import { Badge, Card, CardBody, CardHeader, Heading, Text } from "@chakra-ui/react";
import { CourseGroupingDTO, SchoolEnumArray } from "../../models/courseGrouping";

interface CourseGroupingPreviewProps {
    courseGrouping: CourseGroupingDTO;
}

export default function CourseGroupingPreview(prop: CourseGroupingPreviewProps) {
    const parseDate = (date: string) => {
        return new Date(date).toLocaleDateString();
    };
    return (
        <>
            <Card align="center">
                <CardHeader>
                    <Heading size="md"> {prop.courseGrouping.name}</Heading>
                    <Text textAlign={"center"} color={"gray"}>
                        Modified at: {parseDate(prop.courseGrouping.modifiedDate)}
                    </Text>
                </CardHeader>
                <CardBody>
                    <Text>
                        <b>Required Credits: </b>
                        {prop.courseGrouping.requiredCredits} Credits
                    </Text>
                    <Text>
                        <b>School: </b>
                        {SchoolEnumArray[prop.courseGrouping.school].label}
                    </Text>
                    <Text>
                        <b>Description: </b>
                        {prop.courseGrouping.description}
                    </Text>
                    <Text>
                        <b>Notes: </b>
                        {prop.courseGrouping.notes || "None"}
                    </Text>
                    <Text>
                        <b>Top Level: </b>
                        {prop.courseGrouping.isTopLevel ? "Yes" : "No"}
                    </Text>
                    <Text>
                        <b>Courses: </b>
                        {prop.courseGrouping.courses.length > 0 ? "..." : "None"}
                    </Text>
                    <Text>
                        <b>Subgroupings: </b>
                        {prop.courseGrouping.subGroupingReferences.length > 0 ? "..." : "None"}
                    </Text>
                    <Text>
                        <b>Status: </b>
                        {prop.courseGrouping.published ? (
                            <Badge colorScheme="green">Published</Badge>
                        ) : (
                            <Badge colorScheme="red">Unpublished</Badge>
                        )}
                    </Text>
                </CardBody>
            </Card>
        </>
    );
}
