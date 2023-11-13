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
import { useParams } from "react-router-dom";
import { addCourse, getAllCourseSettings } from "../services/course";
import {
    AllCourseSettings,
    Course,
    CourseCareer,
    CourseComponent,
    CourseComponents,
    componentMappings,
} from "../models/course";
import { showToast } from "./../utils/toastUtils"; // Import the utility function
import Button from "../components/Button";
import { useForm } from "react-hook-form";
import { AutoComplete, AutoCompleteInput, AutoCompleteItem, AutoCompleteList } from "@choc-ui/chakra-autocomplete";

export default function AddCourse() {
    const toast = useToast();
    // Form managment and error handling states
    const [isLoading, toggleLoading] = useState(false);

    const [selectedComponent, setSelectedComponent] = useState<string>(
        '{"componentCode":0,"componentName":"Conference"}'
    );
    const [allCourseSettings, setAllCourseSettings] = useState<AllCourseSettings>(null);

    const [courseComponents, setCourseComponents] = useState<CourseComponents[]>([]); // [ {type: "lecture", hours: 3}, {type: "lab", hours: 2} ]
    const [components, setComponents] = useState<CourseComponents[]>([]);
    const [courseCareers, setCourseCareers] = useState<string[]>([]);
    const selectedComponentRef = useRef<HTMLSelectElement>(null);

    // File upload states
    const [fileName, setFileName] = useState("");
    const [fileContent, setFileContent] = useState("");
    const [supportingFiles, setSupportingFiles] = useState({});
    const { dossierId } = useParams();

    const {
        register,
        handleSubmit,
        watch,
        setValue,
        reset,
        formState: { isValid, errors },
    } = useForm<Course>({
        defaultValues: {
            title: null,
            subject: null,
            catalog: null,
            career: null,
        },
    });

    const courseName = watch("title");
    const courseCode = watch("catalog");
    const courseCredits = watch("creditValue");
    const courseDescription = watch("description");
    const courseRequesites = watch("preReqs");
    const department = watch("subject");
    const courseCareer = allCourseSettings?.courseCareers.find(
        (career) => career.careerCode === watch("career")
    ) as CourseCareer;
    const subject = watch("subject");

    const handleChangeCourseCareer = (value: string) => {
        // Find course carrer code
        const career = allCourseSettings?.courseCareers.find((career) => career.careerName === value);
        if (career) setValue("career", career?.careerCode, { shouldValidate: true });
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
    };
    const handleRemoveComponent = (index: number) => {
        setComponents([...components, courseComponents[index]]);
        setCourseComponents(courseComponents.filter((_component, componentIndex) => componentIndex !== index));
    };

    function onSubmit(data: Course) {
        if (!isValid) {
            showToast(toast, "Error!", "One or more validation errors occurred", "error");
            return;
        } else {
            toggleLoading(true);
            const course: Course = {
                subject: data.subject,
                catalog: data.catalog,
                title: data.title,
                description: data.description,
                creditValue: data.creditValue,
                preReqs: data.preReqs,
                career: data.career,
                equivalentCourses: "",
                componentCodes: getCourseComponentsObject(courseComponents),
                dossierId: dossierId,
                courseNotes: data.courseNotes,
                rationale: data.rationale,
                supportingFiles: supportingFiles,
                resourceImplication: data.resourceImplication,
            };
            addCourse(course)
                .then(() => {
                    showToast(toast, "Success!", "Course added successfully.", "success");
                    toggleLoading(false);
                    clearForm();
                    reset();
                })
                .catch((e) => {
                    showToast(toast, "Error!", e.response?.data.detail, "error");
                    toggleLoading(false);
                });
        }
    }
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
        setCourseComponents([]);
        setComponents([]);
        // setCourseCareers([]);
        setSelectedComponent('{"componentCode":0,"componentName":"Conference"}');
        setSupportingFiles({});
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
    return (
        // display if AllCourseSettings is not null
        <>
            {allCourseSettings && (
                <Box>
                    <Header></Header>
                    <form onSubmit={handleSubmit(onSubmit)}>
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
                                        <FormControl isInvalid={!!errors.career}>
                                            <FormLabel m={0}>Course Career</FormLabel>

                                            <AutoComplete
                                                onChange={(val) => {
                                                    handleChangeCourseCareer(val);
                                                }}
                                                openOnFocus
                                            >
                                                <AutoCompleteInput
                                                    width={"100%"}
                                                    placeholder={courseCareer ? courseCareer.careerName : ""}
                                                    register={register("career", { required: true })}
                                                />
                                                <AutoCompleteList>
                                                    {courseCareers.map((option, index) => (
                                                        <AutoCompleteItem
                                                            key={`option-${index}`}
                                                            value={option}
                                                            textTransform="capitalize"
                                                            _focus={{ bg: "brandRed100", color: "white" }}
                                                        >
                                                            {option}
                                                        </AutoCompleteItem>
                                                    ))}
                                                </AutoCompleteList>
                                            </AutoComplete>

                                            <FormErrorMessage>Career is required</FormErrorMessage>
                                        </FormControl>
                                        <FormControl isInvalid={!!errors.subject}>
                                            <FormLabel m={0}>Subject</FormLabel>

                                            <AutoComplete
                                                onChange={(val) => {
                                                    setValue("subject", val, { shouldValidate: true });
                                                }}
                                                openOnFocus
                                            >
                                                <AutoCompleteInput
                                                    width={"100%"}
                                                    placeholder={null}
                                                    value={subject ? subject : ""}
                                                    {...register("subject", { required: true })}
                                                />
                                                <AutoCompleteList>
                                                    {allCourseSettings?.courseSubjects.map((option, index) => (
                                                        <AutoCompleteItem
                                                            key={`option-${index}`}
                                                            value={option}
                                                            textTransform="capitalize"
                                                            _focus={{ bg: "brandRed100", color: "white" }}
                                                        >
                                                            {option}
                                                        </AutoCompleteItem>
                                                    ))}
                                                </AutoCompleteList>
                                            </AutoComplete>

                                            <FormErrorMessage>Subject is required</FormErrorMessage>
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <FormControl isInvalid={errors.catalog && true}>
                                            <FormLabel m={0}>Course Code</FormLabel>
                                            <Input
                                                placeholder="Course Code"
                                                pl="16px"
                                                {...register("catalog", { required: true })}
                                            />
                                            {errors.catalog && (
                                                <FormErrorMessage>Course code is required</FormErrorMessage>
                                            )}
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <FormControl isInvalid={errors.title && true}>
                                            <FormLabel m={0}>Name</FormLabel>
                                            <Input placeholder="Name" {...register("title", { required: true })} />
                                            {errors.title && (
                                                <FormErrorMessage>Course name is required</FormErrorMessage>
                                            )}
                                        </FormControl>
                                    </Stack>
                                    <Stack>
                                        <FormControl isInvalid={errors.creditValue && true}>
                                            <FormLabel m={0}>Credits</FormLabel>
                                            <Input
                                                type="number"
                                                placeholder="Credits"
                                                {...register("creditValue", { required: true })}
                                            />
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
                                        {/* <FormControl isInvalid={errors.description && true}> */}
                                        <Textarea
                                            placeholder="Enter course description..."
                                            minH={"200px"}
                                            {...register("description")}
                                        ></Textarea>
                                        {/* {errors.description && (
                                            <FormErrorMessage>Course description is required</FormErrorMessage>
                                        )} */}
                                        {/* </FormControl> */}
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
                                            placeholder="Enter resource implication..."
                                            minH={"100px"}
                                            {...register("resourceImplication", { required: false })}
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
                                                        {department} {courseCode} {courseName}{" "}
                                                        {courseCredits === "" ? null : (
                                                            <Text display={"inline"}>
                                                                {"("}
                                                                {courseCredits}{" "}
                                                                {parseInt(courseCredits) === 1 ? "credit)" : "credits)"}
                                                            </Text>
                                                        )}
                                                    </Heading>
                                                    <Text>
                                                        <b>Course Career:</b>{" "}
                                                        {courseCareer ? courseCareer.careerName : "Not specified"}
                                                    </Text>
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
                                            {...register("preReqs", { required: false })}
                                            placeholder="Enter course requirements..."
                                            minH={"200px"}
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
                                            {...register("rationale", { required: false })}
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
                                            {...register("courseNotes", { required: false })}
                                            placeholder="Enter course notes..."
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
                                        type="submit"
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
