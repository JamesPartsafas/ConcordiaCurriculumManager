import {
    Flex,
    Center,
    Text,
    Heading,
    FormControl,
    FormLabel,
    Input,
    Stack,
    NumberInput,
    NumberInputField,
    Select,
    Card,
    CardBody,
    Box,
    ButtonGroup,
    IconButton,
    Textarea,
    useToast,
    FormErrorMessage,
} from "@chakra-ui/react";
import { AddIcon } from "@chakra-ui/icons";
import { MinusIcon } from "@chakra-ui/icons";
import { useState, useRef, useEffect } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import {
    addCourse,
    editCourseCreationRequest,
    editCourseModificationRequest,
    getAllCourseSettings,
    modifyCourse,
} from "../services/course";
import {
    AllCourseSettings,
    Course,
    CourseCareer,
    CourseComponent,
    CourseComponents,
    EditCourseCreationRequestDTO,
    EditCourseModificationRequestDTO,
    componentMappings,
} from "../models/course";
import AutocompleteInput from "../components/Select";
import { showToast } from "./../utils/toastUtils"; // Import the utility function
import Button from "../components/Button";
import { BaseRoutes } from "../constants";
import CoursePreview from "../components/CoursePreview";
import CourseDiffViewer from "../components/CourseDifference/CourseDiffViewer";

export default function AddCourse() {
    const toast = useToast();
    const location = useLocation();
    const navigate = useNavigate();

    const state = location.state;
    const { pathname } = location;
    const isEditPage = pathname.includes("edit-course");
    const pageTitle = !isEditPage ? "Add Course" : "Edit Course";

    const [oldCourse, setOldCourse] = useState<Course>(null);
    const [newCourse, setNewCourse] = useState<Course>(null);
    // Form managment and error handling states
    const [isLoading, toggleLoading] = useState(false);
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [courseSubjectError, setCourseSubjectError] = useState(true);
    const [courseCodeError, setCourseCodeError] = useState(true);
    const [courseNameError, setCourseNameError] = useState(true);
    const [courseCreditError, setCourseCreditError] = useState(true);
    const [courseCareersError, setCourseCareersError] = useState(true);

    const [courseID, setCourseID] = useState<number>(null);
    const [selectedComponent, setSelectedComponent] = useState<string>(
        '{"componentCode":0,"componentName":"Conference"}'
    );
    const [allCourseSettings, setAllCourseSettings] = useState<AllCourseSettings>(null);
    const [department, setDepartment] = useState("");
    const [courseNumber, setCourseNumber] = useState("");
    const [courseName, setCourseName] = useState("");
    const [courseCredits, setCourseCredits] = useState("");
    const [courseDescription, setCourseDescription] = useState("");
    const [courseRequesites, setCourseRequesites] = useState("");
    const [courseComponents, setCourseComponents] = useState<CourseComponents[]>([]); // [ {type: "lecture", hours: 3}, {type: "lab", hours: 2} ]
    const [components, setComponents] = useState<CourseComponents[]>([]);
    const [courseCareer, setCouresCareer] = useState<CourseCareer>(null);
    const [courseCareers, setCourseCareers] = useState<string[]>([]);
    const selectedComponentRef = useRef<HTMLSelectElement>(null);
    const [rational, setRational] = useState("");
    const [courseNotes, setCourseNotes] = useState("");
    const [resourceImplication, setResourceImplication] = useState("");
    const [equivalentCourses, setEquivalentCourses] = useState("");
    const [comment, setComment] = useState("");

    // File upload states
    const [fileName, setFileName] = useState("");
    const [fileContent, setFileContent] = useState("");
    const [supportingFiles, setSupportingFiles] = useState({});
    const { dossierId } = useParams();

    const handleChangeCourseCareer = (value: string) => {
        if (value.length === 0) setCourseCareersError(true);
        else setCourseCareersError(false);
        // Find course carrer code
        const career = allCourseSettings?.courseCareers.find((career) => career.careerName === value);
        setCouresCareer(career);
        setNewCourse({
            ...newCourse,
            career: career.careerCode,
        });
    };
    const handleChangeDepartment = (value: string) => {
        if (value.length === 0) setCourseSubjectError(true);
        else setCourseSubjectError(false);
        setNewCourse({
            ...newCourse,
            subject: value,
        });
        setDepartment(value);
    };
    const handleChangeCourseNumber = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.currentTarget.value.length === 0) setCourseCodeError(true);
        else setCourseCodeError(false);
        setCourseNumber(e.currentTarget.value);
        setNewCourse({
            ...newCourse,
            catalog: e.currentTarget.value,
        });
    };
    const handleChangeCourseName = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.currentTarget.value.length === 0) setCourseNameError(true);
        else setCourseNameError(false);
        setCourseName(e.currentTarget.value);
        setNewCourse({
            ...newCourse,
            title: e.currentTarget.value,
        });
    };
    const handleChangeCourseCredits = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.currentTarget.value.length === 0) setCourseCreditError(true);
        else setCourseCreditError(false);
        setCourseCredits(e.currentTarget.value);
        setNewCourse({
            ...newCourse,
            creditValue: e.currentTarget.value,
        });
    };
    const handleChangeCourseDescription = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setCourseDescription(e.currentTarget.value);
        setNewCourse({
            ...newCourse,
            description: e.currentTarget.value,
        });
    };

    const handleChangeCourseRequesites = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setCourseRequesites(e.currentTarget.value);
        setNewCourse({
            ...newCourse,
            preReqs: e.currentTarget.value,
        });
    };
    const handleChangeResourceImplication = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setResourceImplication(e.currentTarget.value);
        setNewCourse({
            ...newCourse,
            resourceImplication: e.currentTarget.value,
        });
    };
    const handleChangeRational = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setRational(e.currentTarget.value);
        setNewCourse({
            ...newCourse,
            rationale: e.currentTarget.value,
        });
    };
    const handleChangeCourseNotes = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setCourseNotes(e.currentTarget.value);
        setNewCourse({
            ...newCourse,
            courseNotes: e.currentTarget.value,
        });
    };
    const handleAddComponent = () => {
        const selectedItem: CourseComponent = JSON.parse(selectedComponent) as CourseComponent;
        // check if course component already exists
        if (courseComponents.find((component) => component.componentCode === selectedItem.componentCode)) {
            showToast(toast, "Warning!", "Course component already exists", "warning");
            return;
        }
        // setSelectedComponent(JSON.parse(selectedComponent) as CourseComponent);
        if (selectedComponent === undefined) return;
        setCourseComponents([
            ...courseComponents,
            {
                componentName: selectedItem.componentName,
                componentCode: selectedItem.componentCode,
                hours: 3,
            },
        ]);
        setComponents(components.filter((component) => component !== selectedItem));
        setNewCourse({
            ...newCourse,
            componentCodes: {
                ...newCourse?.componentCodes,
                [componentMappings[selectedItem.componentName]]: 3,
            },
        });
    };
    const handleRemoveComponent = (index: number) => {
        setComponents([...components, courseComponents[index]]); // update list of components
        setCourseComponents(courseComponents.filter((_component, componentIndex) => componentIndex !== index));
        // Remove component with component name as key from component codes object
        setNewCourse({
            ...newCourse,
            componentCodes: Object.keys(newCourse.componentCodes).reduce((object, key) => {
                if (key !== componentMappings[courseComponents[index].componentName]) {
                    object[key] = newCourse.componentCodes[key];
                }
                return object;
            }, {}),
        });
    };
    const handleChangeEquivalentCourses = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setEquivalentCourses(e.currentTarget.value);
        setNewCourse({
            ...newCourse,
            equivalentCourses: e.currentTarget.value,
        });
    };

    const handleChangeComment = (e: React.ChangeEvent<HTMLTextAreaElement>) => setComment(e.currentTarget.value);
    const handleSubmitCourse = () => {
        setFormSubmitted(true);
        if (courseCodeError || courseCreditError || courseNameError || courseSubjectError || courseCareersError) {
            showToast(toast, "Error!", "One or more validation errors occurred", "error");
            return;
        } else {
            toggleLoading(true);
            const course: Course = {
                courseID: courseID,
                subject: department,
                catalog: courseNumber,
                title: courseName,
                description: courseDescription,
                creditValue: courseCredits,
                preReqs: courseRequesites,
                career: courseCareer.careerCode,
                equivalentCourses: equivalentCourses,
                componentCodes: getCourseComponentsObject(courseComponents),
                dossierId: dossierId,
                courseNotes: courseNotes,
                rationale: rational,
                supportingFiles: supportingFiles,
                resourceImplication: resourceImplication,
                comment: comment,
            };
            if (pathname.includes("add-course")) {
                addCourse(course)
                    .then(() => {
                        showToast(toast, "Success!", "Course added successfully.", "success");
                        toggleLoading(false);
                        clearForm();
                        setFormSubmitted(false);
                        navigate("/dossierdetails/" + dossierId);
                    })
                    .catch((e) => {
                        console.log(e);
                        showToast(toast, "Error!", e.response.data, "error");
                        toggleLoading(false);
                    });
            } else if (pathname.includes("edit-course")) editAction(course);
        }
    };
    const handleFileNameChange = (event) => {
        setFileName(event.target.value);
    };

    const handleFileChange = (event) => {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                setFileContent(reader.result as string);
            };
            reader.readAsDataURL(file);
        }
    };

    const handleSaveFile = () => {
        if (fileName && fileContent) {
            setSupportingFiles({
                ...supportingFiles,
                [fileName]: fileContent.split(",")[1], // Removes the base64 prefix
            });
            setFileName(""); // Reset the filename input
            setFileContent(""); // Reset the file content
        }
    };
    const handleEditComponentHours = (hours: number, index: number) => {
        //Check if hours is NaN
        if (isNaN(hours)) {
            hours = 0;
        }
        const newCourseComponents = [...courseComponents];
        newCourseComponents[index].hours = hours;
        setCourseComponents(newCourseComponents);
        setNewCourse({
            ...newCourse,
            componentCodes: {
                ...newCourse.componentCodes,
                [componentMappings[courseComponents[index].componentName]]: hours,
            },
        });
    };
    const getCourseComponentsObject = (courseComponents: CourseComponents[]) => {
        const courseComponentsObject = {};
        courseComponents.forEach((component) => {
            const enumValue = componentMappings[component.componentName];
            courseComponentsObject[enumValue] = component.hours;
        });
        return courseComponentsObject;
    };

    const clearForm = () => {
        setCourseSubjectError(true);
        setCourseCodeError(true);
        setCourseNameError(true);
        setCourseCreditError(true);
        setCourseCareersError(true);
        setDepartment("");
        setCourseNumber("");
        setCourseName("");
        setCourseCredits("");
        setCourseDescription("");
        setCourseRequesites("");
        setCourseComponents([]);
        setComponents([]);
        setCouresCareer(null);
        setCourseCareers([]);
        setSelectedComponent('{"componentCode":0,"componentName":"Conference"}');
        setRational("");
        setCourseNotes("");
        setResourceImplication("");
        setSupportingFiles({});
        setEquivalentCourses("");
    };

    const getCourseCarreer = (careerCode: number) => {
        const carrer = allCourseSettings?.courseCareers.find((career) => career.careerCode == careerCode);
        return carrer;
    };

    const editAction = (course: Course) => {
        if (state.api === "editCreationRequest") {
            //edit a creation request
            const creationRequestToEdit: EditCourseCreationRequestDTO = {
                ...course,
                id: state?.id,
            };
            editCourseCreationRequest(dossierId, creationRequestToEdit)
                .then(() => {
                    showToast(toast, "Success!", "Course creation request modified successfully.", "success");
                    toggleLoading(false);
                    navigate("/dossierdetails/" + dossierId);
                })
                .catch(() => {
                    showToast(toast, "Error!", "One or more validation errors occurred", "error");
                    toggleLoading(false);
                });
        } else if (state.api === "editModificationRequest") {
            //edit an existing modification request
            const modificationRequestToEdit: EditCourseModificationRequestDTO = {
                ...course,
                id: state?.id,
            };
            editCourseModificationRequest(dossierId, modificationRequestToEdit)
                .then(() => {
                    showToast(toast, "Success!", "Course modification request modified successfully.", "success");
                    toggleLoading(false);
                    navigate("/dossierdetails/" + dossierId);
                })
                .catch(() => {
                    showToast(toast, "Error!", "One or more validation errors occurred", "error");
                    toggleLoading(false);
                    navigate("/dossierdetails/" + dossierId);
                });
        } else {
            //create a new modification request
            modifyCourse(course)
                .then(() => {
                    showToast(toast, "Success!", "Course modified successfully.", "success");
                    toggleLoading(false);
                    navigate("/dossierdetails/" + dossierId);
                })
                .catch(() => {
                    showToast(toast, "Error!", "One or more validation errors occurred", "error");
                    toggleLoading(false);
                });
        }
    };
    const setCourseData = () => {
        const courseDetails: Course = {
            courseID: state?.courseID,
            subject: state?.subject,
            catalog: state?.catalog,
            title: state?.title,
            description: state?.description,
            creditValue: state?.creditValue,
            preReqs: state?.preReqs,
            career: state?.career,
            equivalentCourses: state?.equivalentCourses,
            componentCodes: state?.componentCodes,
            dossierId: dossierId,
            courseNotes: state?.courseNotes,
            rationale: state?.rationale || "",
            supportingFiles: state?.supportingFiles,
            resourceImplication: state?.resourceImplication || "",
            comment: state?.comment || "",
        };
        const courseDetails2: Course = {
            courseID: state?.courseID,
            subject: state?.subject,
            catalog: state?.catalog,
            title: state?.title,
            description: state?.description,
            creditValue: state?.creditValue,
            preReqs: state?.preReqs,
            career: state?.career,
            equivalentCourses: state?.equivalentCourses,
            componentCodes: state?.componentCodes,
            dossierId: dossierId,
            courseNotes: state?.courseNotes,
            rationale: state?.rationale || "",
            supportingFiles: state?.supportingFiles,
            resourceImplication: state?.resourceImplication || "",
            comment: state?.comment || "",
        };
        setOldCourse(courseDetails);
        setNewCourse(courseDetails2);
        // set values
        setCourseID(courseDetails?.courseID);
        setDepartment(courseDetails?.subject);
        setCourseName(courseDetails?.title);
        setCourseDescription(courseDetails?.description);
        setCourseCredits(courseDetails?.creditValue);
        setCourseRequesites(courseDetails?.preReqs);
        setCourseNumber(courseDetails?.catalog);
        setSupportingFiles(courseDetails?.supportingFiles);
        setEquivalentCourses(courseDetails?.equivalentCourses);
        setCourseNotes(courseDetails?.courseNotes);
        setRational(courseDetails?.rationale);
        setComment(courseDetails?.comment);
        setResourceImplication(courseDetails?.resourceImplication);
        setCouresCareer(getCourseCarreer(courseDetails?.career));
        // find course components in allCourseSettings that match component codes of coursdDetails
        const courseComponentsTemp = allCourseSettings?.courseComponents
            .filter((component) =>
                Object.keys(courseDetails?.componentCodes || {}).includes(componentMappings[component.componentName])
            )
            .map((filteredComponent) => {
                const code = componentMappings[filteredComponent.componentName];
                const hours = courseDetails.componentCodes[code];
                return {
                    ...filteredComponent,
                    hours: hours,
                };
            });
        setCourseComponents(courseComponentsTemp || []);
        // update errors
        setCourseSubjectError(false);
        setCourseNameError(false);
        setCourseCreditError(false);
        setCourseCodeError(false);
        setCourseCareersError(false);
    };
    useEffect(() => {
        getAllCourseSettings()
            .then((res) => {
                setAllCourseSettings(res.data);
                setComponents(res.data.courseComponents);
                const courseCareersTemp = res.data.courseCareers.map((career) => career.careerName);
                setCourseCareers(courseCareersTemp);
            })
            .catch((err) => {
                showToast(toast, "Error!", err.message, "error");
            });
    }, []);

    useEffect(() => {
        if (allCourseSettings && pathname.includes("edit-course")) setCourseData();
    }, [allCourseSettings]);
    return (
        // display if AllCourseSettings is not null
        <>
            {allCourseSettings && (
                <Box>
                    <Button
                        style="primary"
                        variant="outline"
                        width="100px"
                        height="40px"
                        ml={8}
                        mt={5}
                        onClick={() => navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId))}
                    >
                        Back
                    </Button>
                    <form>
                        {oldCourse && newCourse && isEditPage && (
                            <>
                                <CourseDiffViewer
                                    oldCourse={oldCourse}
                                    newCourse={newCourse}
                                    allCourseSettings={allCourseSettings}
                                ></CourseDiffViewer>
                            </>
                        )}
                        <Flex>
                            <Stack w="35%" p={8}>
                                <Stack>
                                    <Center>
                                        <Heading as="h1" size="2xl" color="brandRed">
                                            {pageTitle}
                                        </Heading>
                                    </Center>
                                </Stack>
                                <Stack>
                                    <Stack>
                                        <FormControl isInvalid={courseCareersError && formSubmitted}>
                                            <FormLabel m={0}>Course Career</FormLabel>
                                            <AutocompleteInput
                                                options={courseCareers}
                                                onSelect={handleChangeCourseCareer}
                                                width="100%"
                                                isDisabled={isEditPage}
                                                value={courseCareer ? courseCareer.careerName : "Select Course Career"}
                                            />
                                            <FormErrorMessage>Course Career is required</FormErrorMessage>
                                        </FormControl>
                                        <FormControl isInvalid={courseSubjectError && formSubmitted}>
                                            <FormLabel m={0}>Subject</FormLabel>
                                            <AutocompleteInput
                                                options={allCourseSettings?.courseSubjects}
                                                onSelect={handleChangeDepartment}
                                                width="100%"
                                                isDisabled={isEditPage}
                                                value={department ? department : "Select Subject"}
                                            />
                                            <FormErrorMessage>Subject is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <FormControl isInvalid={courseCodeError && formSubmitted}>
                                            <FormLabel m={0}>Course Code</FormLabel>
                                            <Input
                                                placeholder="Course Code"
                                                pl="16px"
                                                value={courseNumber}
                                                isDisabled={isEditPage}
                                                onChange={handleChangeCourseNumber}
                                            />
                                            <FormErrorMessage>Course code is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <FormControl isInvalid={courseNameError && formSubmitted}>
                                            <FormLabel m={0}>Name</FormLabel>
                                            <Input
                                                placeholder="Name"
                                                value={courseName}
                                                onChange={handleChangeCourseName}
                                            />
                                            <FormErrorMessage>Course name is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <FormControl isInvalid={courseCreditError && formSubmitted}>
                                            <FormLabel m={0}>Credits</FormLabel>
                                            <NumberInput value={courseCredits}>
                                                <NumberInputField
                                                    placeholder="Credits"
                                                    pl="16px"
                                                    onChange={handleChangeCourseCredits}
                                                />
                                            </NumberInput>
                                            <FormErrorMessage>Course credit is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            Course Description
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <Textarea
                                            value={courseDescription}
                                            onChange={handleChangeCourseDescription}
                                            placeholder="Enter course description..."
                                            minH={"200px"}
                                        ></Textarea>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            <Text align="center">Equivalent Courses</Text>
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <Textarea
                                            value={equivalentCourses}
                                            onChange={handleChangeEquivalentCourses}
                                            placeholder="Enter equivalent courses..."
                                            minH={"100px"}
                                        ></Textarea>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            Supporting Files
                                        </Heading>
                                    </Center>
                                    <Stack spacing={4}>
                                        <Input
                                            placeholder="Enter file name..."
                                            value={fileName}
                                            onChange={handleFileNameChange}
                                        />
                                        <Input type="file" onChange={handleFileChange} />
                                        <Button
                                            style="primary"
                                            width="auto"
                                            height="50px"
                                            variant="solid"
                                            onClick={handleSaveFile}
                                            isDisabled={!fileName || !fileContent}
                                        >
                                            Upload File
                                        </Button>
                                        <Stack>
                                            {Object.keys(supportingFiles).map((file, index) => (
                                                <Box
                                                    w="100%"
                                                    p={2}
                                                    mb={2}
                                                    rounded="10"
                                                    border="1px"
                                                    borderColor="gray.200"
                                                    key={index}
                                                >
                                                    <Flex>
                                                        <Center w="70%">
                                                            <Text textAlign="left" width="full" pl="4">
                                                                {file}
                                                            </Text>
                                                        </Center>
                                                        <Center w="30%">
                                                            <ButtonGroup
                                                                size="sm"
                                                                isAttached
                                                                variant="outline"
                                                                ml="10px"
                                                            >
                                                                <IconButton
                                                                    rounded="full"
                                                                    aria-label="Add to friends"
                                                                    icon={<MinusIcon />}
                                                                    onClick={() => {
                                                                        const newSupportingFiles = {
                                                                            ...supportingFiles,
                                                                        };
                                                                        delete newSupportingFiles[file];
                                                                        setSupportingFiles(newSupportingFiles);
                                                                    }}
                                                                />
                                                            </ButtonGroup>
                                                        </Center>
                                                    </Flex>
                                                </Box>
                                            ))}
                                        </Stack>
                                    </Stack>
                                </Stack>
                            </Stack>

                            <Stack w="65%" p={8}>
                                {!isEditPage && (
                                    <CoursePreview
                                        courseCareer={courseCareer?.careerName}
                                        courseDescription={courseDescription}
                                        coursePreReqs={courseRequesites}
                                        courseTitle={courseName}
                                        courseCreditValue={courseCredits}
                                        courseEquivalentCourses={equivalentCourses}
                                        courseNotes={courseNotes}
                                        courseSubject={department}
                                        courseCatalog={courseNumber}
                                        courseComponents={courseComponents}
                                    ></CoursePreview>
                                )}
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            Components
                                        </Heading>
                                    </Center>
                                    <Card>
                                        <CardBody>
                                            <Box>
                                                {courseComponents.map((component, index) => {
                                                    return (
                                                        <Box
                                                            w="100%"
                                                            p={2}
                                                            mb={2}
                                                            rounded="10"
                                                            border="1px"
                                                            borderColor="gray.200"
                                                            key={index}
                                                        >
                                                            <Flex>
                                                                <Center w="70%">
                                                                    <Text textAlign="left" width="full" pl="4">
                                                                        {component.componentName}
                                                                    </Text>
                                                                </Center>
                                                                <Center w="35%">
                                                                    <Text mr={2}>Hours:</Text>
                                                                    <NumberInput
                                                                        max={10}
                                                                        display={"inline"}
                                                                        pr={0}
                                                                        w="50px"
                                                                        defaultValue={component.hours}
                                                                        onChange={(e) =>
                                                                            handleEditComponentHours(parseInt(e), index)
                                                                        }
                                                                    >
                                                                        <NumberInputField pr={0} />
                                                                    </NumberInput>
                                                                </Center>
                                                                <Center w="30%">
                                                                    <ButtonGroup
                                                                        size="sm"
                                                                        isAttached
                                                                        variant="outline"
                                                                        ml="10px"
                                                                        onClick={() => handleRemoveComponent(index)}
                                                                    >
                                                                        <IconButton
                                                                            rounded="full"
                                                                            aria-label="Add to friends"
                                                                            icon={<MinusIcon />}
                                                                        />
                                                                    </ButtonGroup>
                                                                </Center>
                                                            </Flex>
                                                        </Box>
                                                    );
                                                })}
                                                {components.length === 0 ? null : (
                                                    <Box
                                                        w="100%"
                                                        p={2}
                                                        mb={2}
                                                        rounded="10"
                                                        border="1px"
                                                        borderColor="gray.200"
                                                    >
                                                        <Flex>
                                                            <Center w="70%">
                                                                <Select
                                                                    ref={selectedComponentRef}
                                                                    value={selectedComponent}
                                                                    onChange={(e) =>
                                                                        setSelectedComponent(e.target.value)
                                                                    }
                                                                >
                                                                    {components.map((component, index) => (
                                                                        <option
                                                                            key={index}
                                                                            value={JSON.stringify(component)} // Store the entire component object as a string
                                                                        >
                                                                            {component.componentName}
                                                                        </option>
                                                                    ))}
                                                                </Select>
                                                            </Center>
                                                            <Center w="30%">
                                                                <ButtonGroup
                                                                    size="sm"
                                                                    isAttached
                                                                    variant="outline"
                                                                    ml="10px"
                                                                >
                                                                    <IconButton
                                                                        rounded="full"
                                                                        aria-label="Add Course Component"
                                                                        icon={<AddIcon />}
                                                                        onClick={handleAddComponent}
                                                                    />
                                                                </ButtonGroup>
                                                            </Center>
                                                        </Flex>
                                                    </Box>
                                                )}
                                            </Box>
                                        </CardBody>
                                    </Card>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            <Text align="center">Prerequisites and Corerequisites</Text>
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <Textarea
                                            value={courseRequesites}
                                            onChange={handleChangeCourseRequesites}
                                            placeholder="Enter course requirements..."
                                            minH={"200px"}
                                        ></Textarea>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            <Text align="center">Resource Implication</Text>
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <Textarea
                                            value={resourceImplication}
                                            onChange={handleChangeResourceImplication}
                                            placeholder="Enter resource implication..."
                                            minH={"100px"}
                                        ></Textarea>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            <Text align="center">Rationale</Text>
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <Textarea
                                            value={rational}
                                            onChange={handleChangeRational}
                                            placeholder="Enter course rationale..."
                                            minH={"100px"}
                                        ></Textarea>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            <Text align="center">Notes</Text>
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <Textarea
                                            value={courseNotes}
                                            onChange={handleChangeCourseNotes}
                                            placeholder="Enter course notes..."
                                            minH={"100px"}
                                        ></Textarea>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            <Text align="center">Comments</Text>
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <Textarea
                                            value={comment}
                                            onChange={handleChangeComment}
                                            placeholder="Enter comments..."
                                            minH={"100px"}
                                        ></Textarea>
                                    </Stack>
                                </Stack>
                                <Stack>
                                    <Button
                                        style="primary"
                                        width="auto"
                                        height="50px"
                                        variant="solid"
                                        onClick={() => handleSubmitCourse()}
                                        isLoading={isLoading}
                                    >
                                        Submit
                                    </Button>
                                </Stack>
                            </Stack>
                        </Flex>
                    </form>
                </Box>
            )}
        </>
    );
}
