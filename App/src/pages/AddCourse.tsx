import Header from "../shared/Header";
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
import { addCourse, getAllCourseSettings } from "../services/course";
import { AllCourseSettings, Course, CourseComponent, CourseComponents } from "../models/course";
import AutocompleteInput from "../components/Select";
import { showToast } from "./../utils/toastUtils"; // Import the utility function
import Button from "../components/Button";

export default function AddCourse() {
    const toast = useToast();
    const [errors, setErrors] = useState({
        courseSubject: false,
        courseCode: false,
        courseName: false,
        courseCredit: false,
    });
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
    const selectedComponentRef = useRef<HTMLSelectElement>(null);

    const handleChangeDepartment = (value: string) => {
        setDepartment(value);
    };
    const handleChangeCourseNumber = (e: React.ChangeEvent<HTMLInputElement>) => setCourseNumber(e.currentTarget.value);
    const handleChangeCourseName = (e: React.ChangeEvent<HTMLInputElement>) => setCourseName(e.currentTarget.value);
    const handleChangeCourseCredits = (e: React.ChangeEvent<HTMLInputElement>) =>
        setCourseCredits(e.currentTarget.value);
    const handleChangeCourseDescription = (e: React.ChangeEvent<HTMLTextAreaElement>) =>
        setCourseDescription(e.currentTarget.value);
    const handleChangeCourseRequesites = (e: React.ChangeEvent<HTMLTextAreaElement>) =>
        setCourseRequesites(e.currentTarget.value);
    const handleAddComponent = () => {
        console.log(selectedComponent);
        const selectedItem: CourseComponent = JSON.parse(selectedComponent) as CourseComponent;
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
    };
    const handleRemoveComponent = (index: number) => {
        setComponents([...components, courseComponents[index]]);
        setCourseComponents(courseComponents.filter((_component, componentIndex) => componentIndex !== index));
    };

    const handleSubmitCourse = () => {
        console.log(validateCourse());
        if (validateCourse()) return;
        else {
            const course: Course = {
                subject: department,
                catalog: "1",
                title: courseName,
                description: courseDescription,
                creditValue: courseCredits,
                preReqs: courseRequesites,
                career: 4,
                equivalentCourses: "",
                componentCodes: [],
                dossierId: "37581d9d-713f-475c-9668-23971b0e64d0",
            };
            console.log(course.creditValue);
            addCourse(course)
                .then(() => {
                    showToast(toast, "Success!", "Course added successfully.", "success");
                })
                .catch(() => {
                    showToast(toast, "Error!", "One or more validation errors occurred", "error");
                });
        }
    };
    const validateCourse = () => {
        console.log("course number", courseNumber);
        if (courseNumber == "") setErrors({ ...errors, courseCode: true });
        if (courseName == "") setErrors({ ...errors, courseName: true });
        if (department == "") setErrors({ ...errors, courseSubject: true });
        if (courseCredits == "") setErrors({ ...errors, courseCredit: true });
        // return true if there are errors
        return Object.values(errors).some((error) => error);
    };
    useEffect(() => {
        getAllCourseSettings()
            .then((res) => {
                setAllCourseSettings(res.data);
                setComponents(res.data.courseComponents);
            })
            .catch((err) => {
                showToast(toast, "Error!", err.message, "error");
            });
    }, []);
    return (
        // display if AllCourseSettings is not null

        <>
            {allCourseSettings && (
                <Box>
                    <Header></Header>
                    <form>
                        <Flex>
                            <Stack w="35%" p={8}>
                                <Stack>
                                    <Center>
                                        <Heading as="h1" size="2xl" color="brandRed">
                                            Add Course
                                        </Heading>
                                    </Center>
                                </Stack>
                                <Stack>
                                    <Stack>
                                        <FormControl isInvalid={errors.courseSubject}>
                                            <FormLabel m={0}>Subject</FormLabel>
                                            <AutocompleteInput
                                                options={allCourseSettings?.courseSubjects}
                                                onSelect={handleChangeDepartment}
                                                width="100%"
                                            />
                                            <FormErrorMessage>Subject is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <FormControl isInvalid={errors.courseCode}>
                                            <FormLabel m={0}>Course Code</FormLabel>
                                            <NumberInput>
                                                <NumberInputField
                                                    placeholder="Course Code"
                                                    pl="16px"
                                                    value={courseNumber}
                                                    onChange={handleChangeCourseNumber}
                                                />
                                            </NumberInput>
                                            <FormErrorMessage>Course code is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <FormControl isInvalid={errors.courseName}>
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
                                        <FormControl isInvalid={errors.courseCredit}>
                                            <FormLabel m={0}>Credits</FormLabel>
                                            <NumberInput max={10}>
                                                <NumberInputField
                                                    placeholder="Credits"
                                                    pl="16px"
                                                    value={courseCredits}
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
                                            Description
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
                            </Stack>
                            <Stack w="65%" p={8}>
                                <Stack>
                                    <Center>
                                        <Heading as="h2" size="xl" color="brandRed">
                                            Version Preview
                                        </Heading>
                                    </Center>
                                    <Stack>
                                        <Card>
                                            <CardBody>
                                                <Box bg={"gray.200"} p={2}>
                                                    <Heading size="xl">
                                                        {department} {courseNumber} {courseName}{" "}
                                                        {courseCredits === "" ? null : (
                                                            <Text display={"inline"}>
                                                                {"("}
                                                                {courseCredits} {" credits)"}{" "}
                                                            </Text>
                                                        )}
                                                    </Heading>
                                                    <Text>
                                                        <b>Description:</b>{" "}
                                                        {courseDescription
                                                            ? courseDescription
                                                            : "No description for this class"}
                                                    </Text>
                                                    <Text>
                                                        <b>Prerequisites and Corerequisites:</b>{" "}
                                                        {courseRequesites ? courseRequesites : "None"}
                                                    </Text>
                                                    <Text>
                                                        <b>Component(s):</b>{" "}
                                                        {courseComponents.length === 0
                                                            ? "Not Available"
                                                            : courseComponents.map(
                                                                  (component) =>
                                                                      component.componentName +
                                                                      " " +
                                                                      component.hours +
                                                                      " hour(s) per week. "
                                                              )}
                                                    </Text>
                                                </Box>
                                            </CardBody>
                                        </Card>
                                    </Stack>
                                </Stack>
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
                                                                {/* <Center w="35%">
                                                                <Text mr={2}>Hours:</Text>
                                                                <NumberInput
                                                                    max={10}
                                                                    display={"inline"}
                                                                    pr={0}
                                                                    w="50px"
                                                                    defaultValue={component.hours}
                                                                    onChange={(e) =>
                                                                        handleEditComponentHours(
                                                                            parseInt(e),
                                                                            index
                                                                        )
                                                                    }
                                                                >
                                                                    <NumberInputField pr={0} />
                                                                </NumberInput>
                                                            </Center> */}
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
                                    <Button
                                        type="primary"
                                        width="auto"
                                        height="50px"
                                        variant="solid"
                                        onClick={() => handleSubmitCourse()}
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
