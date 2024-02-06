import { Box, Flex, Select, TableContainer, Table, Text, Thead, Tr, Th, Tbody, Td } from "@chakra-ui/react";
import { useEffect, useRef, useState } from "react";
import { CourseGroupingDTO, MultiCourseGroupingDTO, SchoolEnum } from "../models/courseGrouping";
import Button from "../components/Button";
import { GetCourseGroupingBySchool } from "../services/courseGrouping";
export default function GroupingBySchool() {
    const selectedSchoolRef = useRef<HTMLSelectElement>(null);
    const [selectedSchool, setSelectedSchool] = useState<string>("GinaCody");
    const [changer, setChanger] = useState<SchoolEnum>(SchoolEnum.GinaCody);
    const [courseGroupings, setCourseGroupings] = useState<CourseGroupingDTO[]>([]);

    useEffect(() => {
        if (selectedSchool == "GinaCody") {
            setChanger(SchoolEnum.GinaCody);
        }
        if (selectedSchool == "ArtsAndScience") {
            setChanger(SchoolEnum.ArtsAndScience);
        }
        if (selectedSchool == "FineArts") {
            setChanger(SchoolEnum.FineArts);
        }
        if (selectedSchool == "JMSB") {
            setChanger(SchoolEnum.JMSB);
        }
    }, [selectedSchool]);

    function searchBySchool(input: SchoolEnum) {
        console.log(input);
        GetCourseGroupingBySchool(input)
            .then((res: MultiCourseGroupingDTO) => {
                console.log(JSON.stringify(res.data));
                setCourseGroupings(res.data);
                console.log(JSON.stringify(courseGroupings));
            })
            .catch((err) => {
                console.log(err);
            });
    }
    return (
        <div>
            <Box maxW="5xl" m="auto">
                <Flex flexDirection="column">
                    <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                        Curriculum by School
                    </Text>
                    <Select
                        ref={selectedSchoolRef}
                        value={selectedSchool}
                        onChange={(e) => setSelectedSchool(e.target.value)}
                    >
                        <option
                            key={0}
                            value={"GinaCody"} // Store the entire component object as a string
                        >
                            GinaCody
                        </option>
                        <option
                            key={1}
                            value={"ArtsAndScience"} // Store the entire component object as a string
                        >
                            ArtsAndScience
                        </option>
                        <option
                            key={2}
                            value={"FineArts"} // Store the entire component object as a string
                        >
                            FineArts
                        </option>
                        <option
                            key={3}
                            value={"JMSB"} // Store the entire component object as a string
                        >
                            JMSB
                        </option>
                    </Select>
                    <Button
                        mt={4}
                        mb={2}
                        style="secondary"
                        variant="outline"
                        width="22%"
                        height="40px"
                        onClick={() => {
                            searchBySchool(changer);
                        }}
                    >
                        Search
                    </Button>
                    <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
                        <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                            <Thead backgroundColor={"#e2e8f0"}>
                                <Tr display={"flex"}>
                                    <Th minW={"250px"} maxW={"250px"}>
                                        Name
                                    </Th>
                                    <Th minW={"450px"} maxW={"450px"}>
                                        Credits Required
                                    </Th>
                                    <Th minW={"120px"} maxW={"120px"}>
                                        State
                                    </Th>
                                    <Th width={"25%"}></Th>
                                </Tr>
                            </Thead>
                            <Tbody>
                                {courseGroupings.slice(0, courseGroupings.length).map((groupings) => (
                                    <Tr key={groupings.id} display={"flex"}>
                                        <Td minW={"250px"} maxW={"250px"}>
                                            {groupings.name}
                                        </Td>
                                        <Td minW={"450px"} maxW={"450px"}>
                                            <Text overflow="hidden" textOverflow="ellipsis" maxW={"500px"}>
                                                {groupings.requiredCredits}
                                            </Text>
                                        </Td>
                                        <Td minW={"120px"} maxW={"120px"}>
                                            {groupings.school}
                                        </Td>
                                    </Tr>
                                ))}
                            </Tbody>
                        </Table>
                    </TableContainer>
                </Flex>
            </Box>
        </div>
    );
}
