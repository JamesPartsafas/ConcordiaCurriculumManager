import React, { useEffect, useState } from "react";
import { useForm, useFieldArray, SubmitHandler, Controller } from "react-hook-form";
import { CourseGroupingDTO, CourseGroupingRequestDTO, SchoolEnum } from "../../models/courseGrouping"; // Adjust the import path as needed
import {
    Button,
    Center,
    FormControl,
    FormErrorMessage,
    FormLabel,
    Heading,
    Input,
    Textarea,
    Checkbox,
    Select,
    Stack,
    HStack,
    Box,
    ButtonGroup,
    IconButton,
} from "@chakra-ui/react";
import AutocompleteInput from "../../components/Select";
import { GetCourseGroupingBySchool, InitiateCourseGroupingCreation } from "../../services/courseGrouping";
import { MinusIcon } from "@chakra-ui/icons";

export default function CreateCourseGrouping() {
    const {
        register,
        control,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<CourseGroupingRequestDTO>();

    const {
        fields: subGroupFields,
        append: appendSubGroup,
        remove: removeSubGroup,
    } = useFieldArray({
        control,
        name: "courseGrouping.subGroupingReferences",
    });

    const {
        fields: courseFields,
        append: appendCourse,
        remove: removeCourse,
    } = useFieldArray({
        control,
        name: "courseGrouping.courseIdentifiers",
    });

    const autoCompleteOptions = ["INDI Courses", "Engineering Core Courses", "Fine Arts Core"];
    const [selectedItem, setSelectedItem] = useState(null);
    const [courseGrouping, setCourseGrouping] = useState<CourseGroupingDTO[]>();

    const fieldConfigurations = [
        {
            name: "courseGrouping.name",
            label: "Course Grouping Name",
            placeholder: "Enter the course grouping name",
            isRequired: true,
            type: "input",
        },
        {
            name: "courseGrouping.requiredCredits",
            label: "Required Credits",
            placeholder: "Enter required credits",
            isRequired: false,
            type: "input",
        },
        {
            name: "courseGrouping.description",
            label: "Description",
            placeholder: "Enter description",
            isRequired: false,
            type: "textarea",
        },
        {
            name: "courseGrouping.notes",
            label: "Notes",
            placeholder: "Enter notes",
            isRequired: false,
            type: "textarea",
        },
        {
            name: "courseGrouping.isTopLevel",
            label: "Top Level",
            placeholder: "This course group is a top level course group",
            isRequired: false,
            type: "checkbox",
        },
        {
            name: "courseGrouping.school", // Assuming 'school' is a dropdown field
            label: "School",
            isRequired: true,
            type: "select",
            options: [
                { value: SchoolEnum.GinaCody, label: "Gina Cody" },
                { value: SchoolEnum.ArtsAndScience, label: "Arts and Science" },
                { value: SchoolEnum.FineArts, label: "Fine Arts" },
                { value: SchoolEnum.JMSB, label: "JMSB" },
                // Add other options as needed
            ],
        },
        { name: "rationale", label: "Rationale", placeholder: "Enter Rational", isRequired: false, type: "textarea" },
        {
            name: "resourceImplication",
            label: "Resource Implication",
            placeholder: "Enter Resource Implication",
            isRequired: false,
            type: "textarea",
        },
        {
            name: "comment",
            label: "Comment",
            placeholder: "Enter Comment",
            isRequired: false,
            type: "textarea",
        },
    ];
    const onSubmit: SubmitHandler<CourseGroupingRequestDTO> = (data) => {
        data.dossierId = "37581d9d-713f-475c-9668-23971b0e64d0";
        data.courseGrouping.school = Number(data.courseGrouping.school);
        InitiateCourseGroupingCreation("37581d9d-713f-475c-9668-23971b0e64d0", data).then((response) => {
            console.log(response.data);
        });
        // Submit your form data
    };
    const handleAddClick = () => {
        if (selectedItem) {
            appendSubGroup({ childGroupCommonIdentifier: selectedItem, groupingType: 0 });
            setSelectedItem(null); // Reset selection after adding
        }
    };
    useEffect(() => {
        GetCourseGroupingBySchool(SchoolEnum.GinaCody).then((response) => {
            setCourseGrouping(response.data);
            console.log(response.data);
        });
    }, []);
    return (
        <>
            <Stack m={4}>
                <Center m={4}>
                    <Heading>Add Course Grouping</Heading>
                </Center>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <HStack>
                        <Stack width="50%">
                            {fieldConfigurations.map(({ name, label, placeholder, isRequired, type, options }) => (
                                <FormControl key={name} isInvalid={!!errors.courseGrouping?.[name]} p={2}>
                                    <FormLabel htmlFor={name}>{label}</FormLabel>
                                    <Controller
                                        name={name}
                                        control={control}
                                        rules={isRequired ? { required: `${label} is required` } : {}}
                                        render={({ field }) => {
                                            switch (type) {
                                                case "textarea":
                                                    return <Textarea {...field} id={name} placeholder={placeholder} />;
                                                case "checkbox":
                                                    return (
                                                        <>
                                                            <Checkbox
                                                                {...field}
                                                                id={name}
                                                                isChecked={field.value}
                                                                pt={1}
                                                                mr={2}
                                                            />
                                                            <span>{placeholder}</span>
                                                        </>
                                                    );
                                                case "select":
                                                    return (
                                                        <Select
                                                            {...field}
                                                            id={name}
                                                            placeholder={`Select ${label.toLowerCase()}`}
                                                        >
                                                            {options.map((option) => (
                                                                <option key={option.value} value={option.value}>
                                                                    {option.label}
                                                                </option>
                                                            ))}
                                                        </Select>
                                                    );
                                                default:
                                                    return <Input {...field} id={name} placeholder={placeholder} />;
                                            }
                                        }}
                                    />
                                    <FormErrorMessage>
                                        {errors.courseGrouping?.[name] && errors.courseGrouping[name].message}
                                    </FormErrorMessage>
                                </FormControl>
                            ))}
                        </Stack>
                        <Stack width="50%" alignSelf="baseline">
                            <FormControl border="1px solid" borderRadius={5} p={2} borderColor={"gray.200"}>
                                <FormLabel htmlFor="subGroupingReferences">Select the Sub Course Grouping:</FormLabel>
                                <HStack>
                                    <AutocompleteInput
                                        options={autoCompleteOptions}
                                        onSelect={setSelectedItem} // Use setSelectedItem directly here
                                        width="100%"
                                        placeholder="Select Sub Grouping"
                                    />
                                    <Button onClick={handleAddClick}>Add</Button>
                                </HStack>
                                {subGroupFields.map((field, index) => (
                                    <Box key={field.id} mt={2}>
                                        <HStack
                                            justifyContent="space-between"
                                            border="1px solid"
                                            borderRadius={5}
                                            p={2}
                                            borderColor={"gray.200"}
                                        >
                                            <div>
                                                {index + 1 + ". "}
                                                {field.childGroupCommonIdentifier}
                                            </div>
                                            <ButtonGroup
                                                size="sm"
                                                isAttached
                                                variant="outline"
                                                ml="10px"
                                                onClick={() => removeSubGroup(index)}
                                            >
                                                <IconButton
                                                    rounded="full"
                                                    aria-label="Add to friends"
                                                    icon={<MinusIcon />}
                                                />
                                            </ButtonGroup>
                                        </HStack>
                                    </Box>
                                ))}
                            </FormControl>
                            <FormControl border="1px solid" borderRadius={5} p={2} borderColor={"gray.200"}>
                                <FormLabel htmlFor="subGroupingReferences">Select the courses:</FormLabel>
                                <HStack>
                                    <AutocompleteInput
                                        options={autoCompleteOptions}
                                        onSelect={setSelectedItem} // Use setSelectedItem directly here
                                        width="100%"
                                        placeholder="Select Sub Grouping"
                                    />
                                    <Button onClick={handleAddClick}>Add</Button>
                                </HStack>
                                {subGroupFields.map((field, index) => (
                                    <Box key={field.id} mt={2}>
                                        <HStack
                                            justifyContent="space-between"
                                            border="1px solid"
                                            borderRadius={5}
                                            p={2}
                                            borderColor={"gray.200"}
                                        >
                                            <div>
                                                {index + 1 + ". "}
                                                {field.childGroupCommonIdentifier}
                                            </div>
                                            <ButtonGroup
                                                size="sm"
                                                isAttached
                                                variant="outline"
                                                ml="10px"
                                                onClick={() => removeSubGroup(index)}
                                            >
                                                <IconButton
                                                    rounded="full"
                                                    aria-label="Add to friends"
                                                    icon={<MinusIcon />}
                                                />
                                            </ButtonGroup>
                                        </HStack>
                                    </Box>
                                ))}
                            </FormControl>
                            <Button mt={4} isLoading={isSubmitting} type="submit">
                                Submit
                            </Button>
                        </Stack>
                    </HStack>
                </form>
            </Stack>
        </>
    );
}
