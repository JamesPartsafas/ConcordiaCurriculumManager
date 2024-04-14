import { useEffect, useState } from "react";
import { getCourseGroupingHistory } from "../../services/courseGrouping";
import {
    Center,
    Heading,
    Tab,
    TabList,
    TabPanel,
    TabPanels,
    Tabs,
    Select,
    Stack,
    Text,
    useDisclosure,
} from "@chakra-ui/react";
import CourseGroupingPreview from "../../components/CourseGrouping/CourseGroupingPreview";
import { CourseGroupingDTO } from "../../models/courseGrouping";
import CourseGroupingDiffViewer from "../../components/CourseDifference/CourseGroupingDifference";
import SearchCourseGrouping from "../../components/CourseGrouping/SearchCourseGrouping";
import Button from "../../components/Button";
import { useNavigate } from "react-router-dom";
import { BaseRoutes } from "../../constants";

export default function CourseGroupingHistory() {
    const [courseGroupingHistory, setCourseGroupingHistory] = useState<CourseGroupingDTO[]>(null);
    const [oldVersion, setOldVersion] = useState<CourseGroupingDTO>(null);
    const [newVersion, setNewVersion] = useState<CourseGroupingDTO>(null);
    const [courseGroupingId, setCourseGroupingId] = useState<string>("4a8a12cd-1555-4d69-931b-40bae1951c18");

    const navigate = useNavigate();

    const {
        isOpen: isSearchCourseGroupingOpen,
        onOpen: onSearchCourseGroupingOpen,
        onClose: onSearchCourseGroupingClose,
    } = useDisclosure();
    useEffect(() => {
        fetchCourseGroupingHistory(courseGroupingId);
    }, []);

    function fetchCourseGroupingHistory(commonID: string) {
        getCourseGroupingHistory(commonID).then((res) => {
            setCourseGroupingHistory(res.data);
        });
    }
    function handleCourseGroupingSelect(res) {
        setCourseGroupingId(res.commonIdentifier);
        fetchCourseGroupingHistory(res.commonIdentifier);
    }
    function displaySearchCourseGroupModal() {
        return (
            <SearchCourseGrouping
                isOpen={isSearchCourseGroupingOpen}
                onClose={onSearchCourseGroupingClose}
                onSelectCourseGrouping={handleCourseGroupingSelect}
                isEdit={false}
            />
        );
    }
    const changeOldVersion = (index: string) => {
        setOldVersion(courseGroupingHistory[index]);
    };

    const changeNewVersion = (index: string) => {
        setNewVersion(courseGroupingHistory[index]);
    };
    return (
        <>
            {displaySearchCourseGroupModal()}
            {courseGroupingHistory && (
                <>
                    <Button
                        style="primary"
                        variant="outline"
                        height="40px"
                        width="fit-content"
                        m="6"
                        onClick={() => navigate(BaseRoutes.Home)}
                    >
                        Return to Home
                    </Button>
                    <Center m={6}>
                        <Heading color={"brandRed"}>Course Grouping History</Heading>
                    </Center>
                    <Center mb="6">
                        <Text mr="2">Select the course grouping: </Text>
                        <Button
                            style="primary"
                            variant="solid"
                            height="40px"
                            width="fit-content"
                            onClick={onSearchCourseGroupingOpen}
                        >
                            Select
                        </Button>
                    </Center>
                    <Center>
                        <Tabs variant="soft-rounded" colorScheme="gray">
                            <Center>
                                <TabList>
                                    {courseGroupingHistory.map((courseGrouping) => (
                                        <Tab key={courseGrouping.id}>Version {courseGrouping.version}</Tab>
                                    ))}
                                </TabList>
                            </Center>

                            <TabPanels>
                                {courseGroupingHistory.map((courseGrouping) => (
                                    <TabPanel key={courseGrouping.id}>
                                        <CourseGroupingPreview courseGrouping={courseGrouping}></CourseGroupingPreview>
                                    </TabPanel>
                                ))}
                            </TabPanels>
                        </Tabs>
                    </Center>
                    <Center m={6}>
                        <Heading color={"brandRed"}>Compare Changes</Heading>
                    </Center>
                    <Stack w="60%" m="auto" mb="6">
                        <Text>Select Versions to compare</Text>
                        <Select placeholder="Select Version" onChange={(ev) => changeOldVersion(ev.target.value)}>
                            {courseGroupingHistory.map((courseGrouping, index) => (
                                <option key={index} value={index}>
                                    Version {courseGrouping.version}
                                </option>
                            ))}
                        </Select>
                        <Select placeholder="Select Version" onChange={(ev) => changeNewVersion(ev.target.value)}>
                            {courseGroupingHistory.map((courseGrouping, index) => (
                                <option key={index} value={index}>
                                    Version {courseGrouping.version}
                                </option>
                            ))}
                        </Select>
                    </Stack>
                    <Stack mb={8}>
                        <CourseGroupingDiffViewer
                            oldGrouping={oldVersion}
                            newGrouping={newVersion}
                        ></CourseGroupingDiffViewer>
                    </Stack>
                </>
            )}
        </>
    );
}
