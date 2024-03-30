import { useEffect, useState } from "react";
import { CourseGroupingDTO, CourseGroupingRequestDTO } from "../../models/courseGrouping";
import { Box, Heading, ListItem, OrderedList, Stack, Text } from "@chakra-ui/react";
import CourseGroupingDiffViewer from "../CourseDifference/CourseGroupingDifference";

export default function DossierReportCourseGrouping(props: {
    courseGrouping: CourseGroupingRequestDTO[];
    oldGroupings: CourseGroupingDTO[];
}) {
    const [creationRequests, setCreationRequests] = useState<CourseGroupingRequestDTO[]>([]);
    const [modificationRequest, setModificationRequests] = useState<CourseGroupingRequestDTO[]>([]);
    const [deletionRequests, setDeletionRequests] = useState<CourseGroupingRequestDTO[]>([]);

    const parseCourseGrouping = () => {
        if (!props.courseGrouping) return;
        props.courseGrouping.forEach((item) => {
            if (item.requestType === 0) {
                setCreationRequests((prev) => [...prev, item]);
            } else if (item.requestType == 1) {
                setModificationRequests((prev) => [...prev, item]);
            } else if (item.requestType == 2) {
                setDeletionRequests((prev) => [...prev, item]);
            }
        });
    };
    useEffect(() => {
        console.log(props.courseGrouping);
        if (creationRequests.length == 0) parseCourseGrouping();
    }, [props.courseGrouping]);

    const displayMapping = [
        { key: "state", title: "State" },
        { key: "version", title: "Version" },
        { key: "coursesCount", title: "Number of Courses" },
        { key: "createdDate", title: "Created Date" },
        { key: "modifiedDate", title: "Modified Date" },
        { key: "requiredCredits", title: "Required Credits" },
        { key: "description", title: "Description" },
        { key: "notes", title: "Notes" },
    ];
    function getSchoolName(school: number) {
        switch (school) {
            case 0:
                return "Gina Cody";
            case 1:
                return "Arts And Science";
            case 2:
                return "Fine Arts";
            case 3:
                return "JMSB";
            default:
                return "N/A";
        }
    }

    function getOldCourseGrouping(commonIdentifier: string) {
        return props.oldGroupings.find((grouping) => grouping.commonIdentifier === commonIdentifier);
    }
    return (
        <>
            {props.courseGrouping && (
                <>
                    <Stack>
                        <Heading fontSize="2xl" mb={4} mt={4}>
                            Course Grouping Creation Requests:
                        </Heading>
                        <OrderedList ml={12} mt={2}>
                            {creationRequests?.length === 0 && (
                                <Box backgroundColor={"brandRed600"} p={5} borderRadius={"xl"}>
                                    <Text fontSize="md">No course grouping creation requests.</Text>
                                </Box>
                            )}

                            {creationRequests.map((creationRequest, index) => (
                                <ListItem
                                    className="pageBreak"
                                    key={index}
                                    backgroundColor={"brandRed600"}
                                    p={5}
                                    borderRadius={"xl"}
                                    mb={2}
                                >
                                    <Text fontSize="md" marginBottom="3">
                                        <b>Course Group Name: </b>
                                        {creationRequest.courseGrouping.name ?? "N/A"}
                                    </Text>
                                    <Text fontSize="md" marginBottom="3">
                                        <b>Course Group School: </b>
                                        {getSchoolName(creationRequest.courseGrouping.school) ?? "N/A"}
                                    </Text>
                                    {displayMapping.map(({ key, title }) => (
                                        <Text fontSize="md" marginBottom="3" key={key}>
                                            <b>{title}: </b>
                                            {creationRequest.courseGrouping[key] ?? "N/A"}
                                        </Text>
                                    ))}
                                    <Text fontSize="md" marginBottom="3">
                                        <b>Courses: </b>
                                        {creationRequest.courseGrouping.courses.length == 0 && <span>N/A</span>}
                                        {creationRequest.courseGrouping.courses.map((course, index) => (
                                            <span key={index}>
                                                {course.subject}-{course.catalog}
                                                {index < creationRequest.courseGrouping.courses.length - 1 ? ", " : ""}
                                            </span>
                                        ))}
                                    </Text>
                                    <Text fontSize="md" marginBottom="3">
                                        <b>Sub-Groupings: </b>
                                        {creationRequest.courseGrouping.subGroupings.length == 0 && <span>N/A</span>}
                                        {creationRequest.courseGrouping.subGroupings.map((subGroup, index) => (
                                            <span key={subGroup.id}>
                                                {subGroup.name}
                                                {index < creationRequest.courseGrouping.subGroupings.length - 1
                                                    ? ", "
                                                    : ""}
                                            </span>
                                        ))}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b>Rationale:</b> <br />
                                        {creationRequest.rationale ?? "N/A"}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b> Ressource Implications:</b> <br />
                                        {creationRequest.resourceImplication ?? "N/A"}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b>Comments:</b> <br />
                                        {creationRequest.comment == "" ? "N/A" : creationRequest.comment}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b>Conflicts:</b> <br />
                                        {creationRequest.conflict == "" ? "N/A" : creationRequest.comment}
                                    </Text>
                                </ListItem>
                            ))}
                        </OrderedList>
                    </Stack>
                    <Stack>
                        <Heading fontSize="2xl" mb={4} mt={4}>
                            Course Grouping Modification Requests:
                        </Heading>
                        <OrderedList ml={12} mt={2}>
                            {modificationRequest?.length === 0 && (
                                <Box backgroundColor={"brandBlue600"} p={5} borderRadius={"xl"}>
                                    <Text fontSize="md">No course grouping creation requests.</Text>
                                </Box>
                            )}

                            {modificationRequest.map((modificationRequest, index) => (
                                <ListItem
                                    className="pageBreak"
                                    key={index}
                                    backgroundColor={"brandBlue600"}
                                    p={5}
                                    borderRadius={"xl"}
                                    mb={2}
                                >
                                    <Text fontSize="md" mb="3">
                                        <b>Course Group Name: </b>
                                        {modificationRequest.courseGrouping.name ?? "N/A"}
                                    </Text>
                                    <CourseGroupingDiffViewer
                                        oldGrouping={getOldCourseGrouping(
                                            modificationRequest.courseGrouping.commonIdentifier
                                        )}
                                        newGrouping={modificationRequest.courseGrouping}
                                    ></CourseGroupingDiffViewer>
                                    <Text fontSize="md" mb="3" mt={6}>
                                        <b>Rationale:</b> <br />
                                        {modificationRequest.rationale ?? "N/A"}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b> Ressource Implications:</b> <br />
                                        {modificationRequest.resourceImplication ?? "N/A"}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b>Comments:</b> <br />
                                        {modificationRequest.comment == "" ? "N/A" : modificationRequest.comment}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b>Conflicts:</b> <br />
                                        {modificationRequest.conflict == "" ? "N/A" : modificationRequest.comment}
                                    </Text>
                                </ListItem>
                            ))}
                        </OrderedList>
                    </Stack>
                    <Stack>
                        <Heading fontSize="2xl" mb={4} mt={4}>
                            Course Grouping Deletion Requests:
                        </Heading>
                        <OrderedList ml={12} mt={2}>
                            {deletionRequests?.length === 0 && (
                                <Box backgroundColor={"brandGray200"} p={5} borderRadius={"xl"}>
                                    <Text fontSize="md">No course grouping creation requests.</Text>
                                </Box>
                            )}

                            {deletionRequests.map((creationRequest, index) => (
                                <ListItem
                                    className="pageBreak"
                                    key={index}
                                    backgroundColor={"brandGray200"}
                                    p={5}
                                    borderRadius={"xl"}
                                    mb={2}
                                >
                                    <Text fontSize="md" marginBottom="3">
                                        <b>Course Group Name: </b>
                                        {creationRequest.courseGrouping.name ?? "N/A"}
                                    </Text>
                                    <Text fontSize="md" marginBottom="3">
                                        <b>Course Group School: </b>
                                        {getSchoolName(creationRequest.courseGrouping.school) ?? "N/A"}
                                    </Text>
                                    {displayMapping.map(({ key, title }) => (
                                        <Text fontSize="md" marginBottom="3" key={key}>
                                            <b>{title}: </b>
                                            {creationRequest.courseGrouping[key] ?? "N/A"}
                                        </Text>
                                    ))}
                                    <Text fontSize="md" marginBottom="3">
                                        <b>Courses: </b>
                                        {creationRequest.courseGrouping.courses.length == 0 && <span>N/A</span>}
                                        {creationRequest.courseGrouping.courses.map((course, index) => (
                                            <span key={index}>
                                                {course.subject}-{course.catalog}
                                                {index < creationRequest.courseGrouping.courses.length - 1 ? ", " : ""}
                                            </span>
                                        ))}
                                    </Text>
                                    <Text fontSize="md" marginBottom="3">
                                        <b>Sub-Groupings: </b>
                                        {creationRequest.courseGrouping.subGroupings.length == 0 && <span>N/A</span>}
                                        {creationRequest.courseGrouping.subGroupings.map((subGroup, index) => (
                                            <span key={subGroup.id}>
                                                {subGroup.name}
                                                {index < creationRequest.courseGrouping.subGroupings.length - 1
                                                    ? ", "
                                                    : ""}
                                            </span>
                                        ))}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b>Rationale:</b> <br />
                                        {creationRequest.rationale ?? "N/A"}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b> Ressource Implications:</b> <br />
                                        {creationRequest.resourceImplication ?? "N/A"}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b>Comments:</b> <br />
                                        {creationRequest.comment == "" ? "N/A" : creationRequest.comment}
                                    </Text>

                                    <Text fontSize="md" marginBottom="3">
                                        <b>Conflicts:</b> <br />
                                        {creationRequest.conflict == "" ? "N/A" : creationRequest.comment}
                                    </Text>
                                </ListItem>
                            ))}
                        </OrderedList>
                    </Stack>
                </>
            )}
        </>
    );
}
