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
    Button,
} from "@chakra-ui/react";
import {
    AutoComplete,
    AutoCompleteInput,
    AutoCompleteItem,
    AutoCompleteList,
} from "@choc-ui/chakra-autocomplete";
import { AddIcon } from "@chakra-ui/icons";
import { MinusIcon } from "@chakra-ui/icons";
import { useState, useRef } from "react";

export default function AddCourse() {
    const [department, setDepartment] = useState("");
    const [courseNumber, setCourseNumber] = useState("");
    const [courseName, setCourseName] = useState("");
    const [courseCredits, setCourseCredits] = useState("");
    const [courseDescription, setCourseDescription] = useState("");
    const [courseComponents, setCourseComponents] = useState<
        ICourseComponents[]
    >([]); // [ {type: "lecture", hours: 3}, {type: "lab", hours: 2} ]
    const [components, setComponents] = useState<string[]>([
        "Lecture",
        "Lab",
        "Tutorial",
    ]);
    const selectedComponentRef = useRef<HTMLSelectElement>(null);
    const handleChangeDepartment = (e: React.ChangeEvent<HTMLSelectElement>) =>
        setDepartment(e.currentTarget.value);
    const handleChangeCourseNumber = (e: React.ChangeEvent<HTMLInputElement>) =>
        setCourseNumber(e.currentTarget.value);
    const handleChangeCourseName = (e: React.ChangeEvent<HTMLInputElement>) =>
        setCourseName(e.currentTarget.value);
    const handleChangeCourseCredits = (
        e: React.ChangeEvent<HTMLInputElement>
    ) => setCourseCredits(e.currentTarget.value);
    const handleChangeCourseDescription = (
        e: React.ChangeEvent<HTMLTextAreaElement>
    ) => setCourseDescription(e.currentTarget.value);
    const handleAddComponent = (selectedComponent: string | undefined) => {
        setCourseComponents([
            ...courseComponents,
            { type: selectedComponent ? selectedComponent : "", hours: 3 },
        ]);
        setComponents(
            components.filter((component) => component !== selectedComponent)
        );
    };
    const handleEditComponentHours = (hours: number, index: number) => {
        courseComponents[index].hours = hours;
        setCourseComponents(courseComponents);
    };
    const handleRemoveComponent = (index: number) => {
        setComponents([...components, courseComponents[index].type]);
        setCourseComponents(
            courseComponents.filter(
                (_component, componentIndex) => componentIndex !== index
            )
        );
    };
    //define component model
    interface ICourseComponents {
        type: string;
        hours: number;
    }
    const courses = [
        "COMP 352",
        "COMP 232",
        "COMP 335",
        "COMP 248",
        "COMP 248",
    ];
    return (
        <>
            <Header></Header>
            <Flex>
                <Stack w="35%" p={8}>
                    <Stack>
                        <Center>
                            <Heading as="h1" size="2xl">
                                Add Course
                            </Heading>
                        </Center>
                    </Stack>
                    <Stack>
                        <FormControl id="email">
                            <Stack mb={4}>
                                <FormLabel m={0}>Department</FormLabel>
                                <Select
                                    placeholder="Select option"
                                    value={department}
                                    onChange={handleChangeDepartment}
                                >
                                    <option value="COMP">COMP</option>
                                    <option value="SOEN">SOEN</option>
                                    <option value="COEN">COEN</option>
                                </Select>
                            </Stack>
                            <Stack mb={4}>
                                <FormLabel m={0}>Course Number</FormLabel>
                                <NumberInput>
                                    <NumberInputField
                                        placeholder="Course Number"
                                        pl="16px"
                                        value={courseNumber}
                                        onChange={handleChangeCourseNumber}
                                    />
                                </NumberInput>
                            </Stack>
                            <Stack mb={4}>
                                <FormLabel m={0}>Name</FormLabel>
                                <Input
                                    placeholder="Name"
                                    value={courseName}
                                    onChange={handleChangeCourseName}
                                />
                            </Stack>
                            <Stack mb={4}>
                                <FormLabel m={0}>Credits</FormLabel>
                                <NumberInput max={10}>
                                    <NumberInputField
                                        placeholder="Credits"
                                        pl="16px"
                                        value={courseCredits}
                                        onChange={handleChangeCourseCredits}
                                    />
                                </NumberInput>
                            </Stack>
                        </FormControl>
                    </Stack>
                    <Stack>
                        <Center>
                            <Heading as="h2" size="2xl">
                                Components
                            </Heading>
                        </Center>
                        <Card>
                            <CardBody>
                                <Box>
                                    {courseComponents.map(
                                        (component, index) => {
                                            return (
                                                <Box
                                                    w="100%"
                                                    p={2}
                                                    mb={2}
                                                    rounded="10"
                                                    border="1px"
                                                    borderColor="gray.200"
                                                    key={component.type}
                                                >
                                                    <Flex>
                                                        <Center w="35%">
                                                            <Text>
                                                                {component.type}
                                                            </Text>
                                                        </Center>
                                                        <Center w="35%">
                                                            <Text mr={2}>
                                                                Hours:
                                                            </Text>
                                                            <NumberInput
                                                                max={10}
                                                                display={
                                                                    "inline"
                                                                }
                                                                pr={0}
                                                                w="50px"
                                                                defaultValue={
                                                                    component.hours
                                                                }
                                                                onChange={(e) =>
                                                                    handleEditComponentHours(
                                                                        parseInt(
                                                                            e
                                                                        ),
                                                                        index
                                                                    )
                                                                }
                                                            >
                                                                <NumberInputField
                                                                    pr={0}
                                                                />
                                                            </NumberInput>
                                                        </Center>
                                                        <Center w="30%">
                                                            <ButtonGroup
                                                                size="sm"
                                                                isAttached
                                                                variant="outline"
                                                                ml="10px"
                                                                onClick={() =>
                                                                    handleRemoveComponent(
                                                                        index
                                                                    )
                                                                }
                                                            >
                                                                <IconButton
                                                                    rounded="full"
                                                                    aria-label="Add to friends"
                                                                    icon={
                                                                        <MinusIcon />
                                                                    }
                                                                />
                                                            </ButtonGroup>
                                                        </Center>
                                                    </Flex>
                                                </Box>
                                            );
                                        }
                                    )}
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
                                                        ref={
                                                            selectedComponentRef
                                                        }
                                                    >
                                                        {components.map(
                                                            (component) => (
                                                                <option
                                                                    key={
                                                                        component
                                                                    }
                                                                    value={
                                                                        component
                                                                    }
                                                                >
                                                                    {component}
                                                                </option>
                                                            )
                                                        )}
                                                    </Select>
                                                </Center>
                                                <Center w="30%">
                                                    <ButtonGroup
                                                        size="sm"
                                                        isAttached
                                                        variant="outline"
                                                        ml="10px"
                                                        onClick={() =>
                                                            handleAddComponent(
                                                                selectedComponentRef
                                                                    .current
                                                                    ?.value
                                                            )
                                                        }
                                                    >
                                                        <IconButton
                                                            rounded="full"
                                                            aria-label="Add to friends"
                                                            icon={<AddIcon />}
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
                            <Heading as="h2" size="2xl">
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
                            <Heading as="h2" size="xl">
                                Version Preview
                            </Heading>
                        </Center>
                        <Stack>
                            <Card>
                                <CardBody>
                                    <Box bg={"gray.200"} p={2}>
                                        <Heading size="xl">
                                            {department} {courseNumber}{" "}
                                            {courseName}{" "}
                                            <Text display={"inline"}>
                                                {"("} {courseCredits}{" "}
                                                {" credits)"}{" "}
                                            </Text>
                                        </Heading>
                                        <Text>
                                            <b>Prerequisite/Corequesite:</b>
                                        </Text>
                                        <Text>
                                            <b>Description:</b>{" "}
                                            {courseDescription}
                                        </Text>
                                        <Text>
                                            <b>Component(s):</b>{" "}
                                            {courseComponents.map(
                                                (component) =>
                                                    component.type +
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
                            <Heading as="h2" size="xl">
                                Prerequestites/Corequesites
                            </Heading>
                        </Center>
                        <Stack>
                            <Flex justify="center" align="center" w="full">
                                <FormControl w="60">
                                    <FormLabel>Search for course</FormLabel>
                                    <AutoComplete openOnFocus>
                                        <AutoCompleteInput variant="filled" />
                                        <AutoCompleteList>
                                            {courses.map((courses, cid) => (
                                                <AutoCompleteItem
                                                    key={`option-${cid}`}
                                                    value={courses}
                                                    textTransform="capitalize"
                                                >
                                                    {courses}
                                                </AutoCompleteItem>
                                            ))}
                                        </AutoCompleteList>
                                    </AutoComplete>
                                </FormControl>
                                <Center h={"100%"} alignItems={"end"} ml={2}>
                                    <Button colorScheme="blue">
                                        Add Course
                                    </Button>
                                </Center>
                            </Flex>
                        </Stack>
                        <Card>
                            <CardBody>
                                <Box
                                    w="100%"
                                    p={2}
                                    mb={2}
                                    rounded="10"
                                    border="1px"
                                    borderColor="gray.200"
                                >
                                    <Flex justifyContent={"space-between"}>
                                        <Center w="20%">MATH 204</Center>
                                        <Center w="40%">
                                            <Select ref={selectedComponentRef}>
                                                {components.map((component) => (
                                                    <option
                                                        key={component}
                                                        value={component}
                                                    >
                                                        {component}
                                                    </option>
                                                ))}
                                            </Select>
                                        </Center>
                                    </Flex>
                                </Box>
                            </CardBody>
                        </Card>
                    </Stack>
                </Stack>
            </Flex>
        </>
    );
}
