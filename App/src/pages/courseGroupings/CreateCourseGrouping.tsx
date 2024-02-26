import { useEffect, useState } from "react";
import { useForm, useFieldArray, Controller } from "react-hook-form";
import {
    CourseGroupingCreationRequestDTO,
    CourseGroupingDTO,
    CourseGroupingRequestDTO,
    CourseIdentifierDTO,
    SchoolEnum,
} from "../../models/courseGrouping"; // Adjust the import path as needed
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
    useToast,
    useDisclosure,
} from "@chakra-ui/react";
import {
    EditCourseGroupingCreation,
    GetCourseGrouping,
    InitiateCourseGroupingCreation,
} from "../../services/courseGrouping";
import { MinusIcon } from "@chakra-ui/icons";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { BaseRoutes } from "../../constants";
import { showToast } from "../../utils/toastUtils";
import EditCourseModal from "../dossier/SelectCourseModal";
import { getAllCourseSettings } from "../../services/course";
import { AllCourseSettings, CourseDataResponse } from "../../models/course";

export default function CreateCourseGrouping() {
    const location = useLocation();
    const toast = useToast(); // Use the useToast hook
    const { dossierId, courseGroupingId } = useParams();

    const navigate = useNavigate();
    const state: { CourseGroupingRequest: CourseGroupingRequestDTO; api: string } = location.state;

    const [selectedItem, setSelectedItem] = useState(null);
    const [courseGrouping, setCourseGrouping] = useState<CourseGroupingDTO>();
    const [loading, setLoading] = useState<boolean>(false);
    const [courseSettings, setCourseSettings] = useState<AllCourseSettings>(null);
    const [courseIdentifiers, setCourseIdentifiers] = useState<CourseIdentifierDTO[]>([]);

    const {
        isOpen: isCourseSelectionOpen,
        onOpen: onCourseSelectionOpen,
        onClose: onCourseSelectionClose,
    } = useDisclosure();

    const {
        control,
        handleSubmit,
        formState: { errors, isDirty },
    } = useForm<CourseGroupingRequestDTO>({
        defaultValues: {
            ...state.CourseGroupingRequest,
        },
    });

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

    // to calm lint down for now
    courseGrouping;
    removeCourse;

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

    useEffect(() => {
        requestCourseGroupingById(courseGroupingId);
        requestCourseSettings();
    }, [dossierId, courseGroupingId]);

    function requestCourseGroupingById(Id: string) {
        GetCourseGrouping(Id).then((response) => {
            setCourseGrouping(response.data);

            response.data.courses.forEach((course) => {
                const courseIdentifier = {
                    concordiaCourseId: course.courseID,
                    subject: course.subject,
                    catalog: parseInt(course.catalog),
                };

                setCourseIdentifiers((prev) => [...prev, courseIdentifier]);
            });
        });
    }

    function requestCourseSettings() {
        getAllCourseSettings().then((res) => {
            setCourseSettings(res.data);
        });
    }

    function requestEditCourseGroupingCreation(
        dossierId: string,
        requestId: string,
        courseGroupingCreationRequest: CourseGroupingCreationRequestDTO
    ) {
        setLoading(true);
        EditCourseGroupingCreation(dossierId, requestId, courseGroupingCreationRequest)
            .then(
                () => {
                    showToast(toast, "Success!", "Course grouping creation request edited.", "success");
                    setLoading(false);
                    navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId));
                },
                () => {
                    showToast(toast, "Error!", "Course grouping creation request could not be edited.", "error");
                    setLoading(false);
                }
            )
            .catch(() => {
                showToast(toast, "Error!", "Course grouping creation request could not be edited.", "error");
                setLoading(false);
            });
    }

    function requestInitiateCourseGroupingCreation(
        dossierId: string,
        courseGroupingCreationRequest: CourseGroupingCreationRequestDTO
    ) {
        setLoading(true);
        InitiateCourseGroupingCreation(dossierId, courseGroupingCreationRequest)
            .then(
                () => {
                    showToast(toast, "Success!", "Course grouping creation request initiated.", "success");
                    setLoading(false);
                    navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId));
                },
                () => {
                    showToast(toast, "Error!", "Course grouping creation request could not be initiated.", "error");
                    setLoading(false);
                }
            )
            .catch(() => {
                showToast(toast, "Error!", "Course grouping creation request could not be initiated.", "error");
                setLoading(false);
            });
    }

    function onSubmit(data) {
        data.dossierId = dossierId;
        data.courseGrouping.school = Number(data.courseGrouping.school);

        //TODO: need to check which api to call based on the state.api
        if (state?.api === "editGroupingCreationRequest") {
            requestEditCourseGroupingCreation(dossierId, state.CourseGroupingRequest.id, data);
        } else {
            requestInitiateCourseGroupingCreation(dossierId, data);
        }
    }

    const handleAddClick = () => {
        if (selectedItem) {
            appendSubGroup({ childGroupCommonIdentifier: selectedItem, groupingType: 0 });
            setSelectedItem(null); // Reset selection after adding
        }
    };

    function displaySelectCourseModal() {
        return (
            <EditCourseModal
                isOpen={isCourseSelectionOpen}
                onClose={onCourseSelectionClose}
                allCourseSettings={courseSettings}
                dossierId={dossierId}
                onCourseSelect={handleCourseSelect}
            ></EditCourseModal>
        );
    }

    function handleCourseSelect(res: CourseDataResponse) {
        appendCourse({
            concordiaCourseId: res.data.courseID,
            subject: res.data.subject,
            catalog: parseInt(res.data.catalog),
        });
    }

    return (
        <>
            {displaySelectCourseModal()}

            <Stack m={4}>
                <Center m={4}>
                    <Heading>
                        {state?.api === "editGroupingCreationRequest" ? "Edit Course Grouping" : "Add Course Grouping"}
                    </Heading>
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
                                    <Button
                                        onClick={() => {
                                            onCourseSelectionOpen();
                                        }}
                                    >
                                        Add
                                    </Button>
                                </HStack>
                                {courseFields.map((field, index) => (
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
                                                {field.subject ||
                                                    courseIdentifiers.find(
                                                        (c) => c.concordiaCourseId === field.concordiaCourseId
                                                    )?.subject}{" "}
                                                {field.catalog ||
                                                    courseIdentifiers.find(
                                                        (c) => c.concordiaCourseId === field.concordiaCourseId
                                                    )?.catalog}{" "}
                                                -{" " + field.concordiaCourseId}
                                            </div>
                                            <ButtonGroup
                                                size="sm"
                                                isAttached
                                                variant="outline"
                                                ml="10px"
                                                onClick={() => removeCourse(index)}
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
                            <Button mt={4} isLoading={loading} loadingText="Submit" isDisabled={!isDirty} type="submit">
                                Submit
                            </Button>
                        </Stack>
                    </HStack>
                </form>
            </Stack>
        </>
    );
}
