import { useEffect, useState } from "react";
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

import { AllCourseSettings, Course, CourseDataResponse } from "../../models/course";
import { getAllCourseSettings, getCourseHistory } from "../../services/course";
import SelectCourseModal from "../dossier/SelectCourseModal";
import CourseDiffViewer from "../../components/CourseDifference/CourseDiffViewer";
import CoursePreviewCard from "../../components/CoursePreviewCard";
import { useNavigate } from "react-router-dom";
import { BaseRoutes } from "../../constants";
import Button from "../../components/Button";
export default function CourseHistory() {
    const [courseHistory, setCourseHistory] = useState<Course[]>(null);
    const [oldVersion, setOldVersion] = useState<Course>(null);
    const [newVersion, setNewVersion] = useState<Course>(null);
    const [courseSubject, setCourseSubject] = useState<string>("COMP");
    const [courseCatalog, setCourseCatalog] = useState<number>(335);

    const [courseSettings, setCourseSettings] = useState<AllCourseSettings>(null);

    const navigate = useNavigate();

    const {
        isOpen: isCourseSelectionOpen,
        onOpen: onCourseSelectionOpen,
        onClose: onCourseSelectionClose,
    } = useDisclosure();

    useEffect(() => {
        requestCourseSettings();
        fetchCourseHistory(courseSubject, courseCatalog);
    }, []);
    function fetchCourseHistory(subject: string, catalog: number) {
        getCourseHistory(subject, catalog).then((res) => {
            console.log(res);
            setCourseHistory(res.data);
        });
    }
    function handleCourseSelect(res: CourseDataResponse) {
        setCourseSubject(res.data.subject);
        setCourseCatalog(parseInt(res.data.catalog));
        fetchCourseHistory(res.data.subject, parseInt(res.data.catalog));
    }

    function displaySelectCourseModal() {
        return (
            <SelectCourseModal
                isOpen={isCourseSelectionOpen}
                onClose={onCourseSelectionClose}
                allCourseSettings={courseSettings}
                onCourseSelect={handleCourseSelect}
                dossierId="1"
            ></SelectCourseModal>
        );
    }
    function requestCourseSettings() {
        getAllCourseSettings().then((res) => {
            setCourseSettings(res.data);
        });
    }
    const changeOldVersion = (index: string) => {
        setOldVersion(courseHistory[index]);
    };

    const changeNewVersion = (index: string) => {
        setNewVersion(courseHistory[index]);
    };
    return (
        <>
            {displaySelectCourseModal()}
            {courseHistory && (
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
                        <Heading color={"brandRed"}>Course History</Heading>
                    </Center>
                    <Center mb="6">
                        <Text mr="2">Select the course: </Text>
                        <Button
                            style="primary"
                            variant="solid"
                            height="40px"
                            width="fit-content"
                            onClick={onCourseSelectionOpen}
                        >
                            Select
                        </Button>
                    </Center>
                    <Center>
                        <Tabs variant="soft-rounded" colorScheme="gray">
                            <Center>
                                <TabList>
                                    {courseHistory.map((course) => (
                                        <Tab key={course.courseID}>Version {course.version}</Tab>
                                    ))}
                                </TabList>
                            </Center>

                            <TabPanels>
                                {courseHistory.map((course) => (
                                    <TabPanel key={course.courseID}>
                                        <CoursePreviewCard
                                            courseCareer={"UGCD"}
                                            courseDescription={course.description}
                                            coursePreReqs={course.preReqs}
                                            courseTitle={course.title}
                                            courseCreditValue={course.creditValue}
                                            courseEquivalentCourses={course.equivalentCourses}
                                            courseNotes={course.courseNotes}
                                            courseSubject={course.subject}
                                            courseCatalog={course.catalog}
                                            courseComponents={[]}
                                        ></CoursePreviewCard>
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
                            {courseHistory.map((course, index) => (
                                <option key={index} value={index}>
                                    Version {course.version}
                                </option>
                            ))}
                        </Select>
                        <Select placeholder="Select Version" onChange={(ev) => changeNewVersion(ev.target.value)}>
                            {courseHistory.map((course, index) => (
                                <option key={index} value={index}>
                                    Version {course.version}
                                </option>
                            ))}
                        </Select>
                    </Stack>
                    {oldVersion && newVersion && courseSettings && (
                        <Stack mb={8}>
                            <CourseDiffViewer
                                oldCourse={oldVersion}
                                newCourse={newVersion}
                                allCourseSettings={courseSettings}
                            ></CourseDiffViewer>
                        </Stack>
                    )}
                </>
            )}
        </>
    );
}
