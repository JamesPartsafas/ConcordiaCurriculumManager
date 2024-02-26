import {
    FormControl,
    FormErrorMessage,
    FormLabel,
    Input,
    Modal,
    ModalBody,
    ModalCloseButton,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
    useToast,
} from "@chakra-ui/react";
import Button from "../../components/Button";
import { useForm } from "react-hook-form";
import AutocompleteInput from "../../components/Select";
import { AllCourseSettings, CourseDataResponse } from "../../models/course";
import { getCourseData } from "../../services/course";
import { showToast } from "../../utils/toastUtils";
import { BaseRoutes } from "../../constants";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

interface EditCourseModalProps {
    isOpen: boolean;
    onClose: () => void;
    allCourseSettings: AllCourseSettings | null;
    dossierId: string;
    onCourseSelect: (course: CourseDataResponse) => void;
}

interface EditCourseModalForm {
    subject: string;
    catalog: number;
}

export default function EditCourseModal(props: EditCourseModalProps) {
    const {
        register,
        handleSubmit,
        reset,
        setValue,
        clearErrors,
        formState: { errors },
    } = useForm<EditCourseModalForm>();

    const toast = useToast();
    const navigate = useNavigate();
    const [loading, setLoading] = useState<boolean>(false);

    function handleClose() {
        reset();
        props.onClose();
    }

    function handleChangeDepartment(val: string) {
        setValue("subject", val);
        clearErrors("subject");
    }

    function onSubmit(data: EditCourseModalForm) {
        setLoading(true);

        getCourseData(data.subject, data.catalog)
            .then(
                (res: CourseDataResponse) => {
                    setLoading(false);
                    props.onCourseSelect(res);
                    handleClose();
                },
                (rej) => {
                    setLoading(false);
                    showToast(toast, "Error!", rej.response != null ? rej.response.data : rej.message, "error");
                }
            )
            .catch((err) => {
                setLoading(false);
                showToast(toast, "Error!", err, "error");
            });
    }

    return (
        <Modal isOpen={props.isOpen} onClose={handleClose}>
            <form onSubmit={handleSubmit(onSubmit)}>
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>Course To Edit</ModalHeader>
                    <ModalCloseButton />

                    <ModalBody>
                        <FormLabel m={0} htmlFor="subject">
                            Department
                        </FormLabel>
                        <FormControl isInvalid={!!errors.subject}>
                            <AutocompleteInput
                                options={props?.allCourseSettings?.courseSubjects ?? []}
                                onSelect={handleChangeDepartment}
                                {...register("subject", { required: "Please enter a department." })}
                            />
                            <FormErrorMessage>Subject is required</FormErrorMessage>
                        </FormControl>

                        <FormControl isInvalid={!!errors.catalog}>
                            <FormLabel m={0} mt={2} htmlFor="catalog">
                                Course Number
                            </FormLabel>
                            <Input
                                id="catalog"
                                type="number"
                                {...register("catalog", {
                                    required: "Please enter a catalog number.",
                                    valueAsNumber: true,
                                })}
                            />
                            <FormErrorMessage>Catalog is required</FormErrorMessage>
                        </FormControl>
                    </ModalBody>

                    <ModalFooter>
                        <Button
                            style="primary"
                            variant="outline"
                            width="fit-content"
                            height="40px"
                            mr={3}
                            onClick={handleClose}
                        >
                            Close
                        </Button>

                        <Button
                            style="secondary"
                            type="submit"
                            //add type submit
                            isLoading={loading}
                            loadingText="Saving"
                            variant="solid"
                            width="fit-content"
                            minW="85px"
                            height="40px"
                            // isDisabled={!isDirty || !isValid}
                        >
                            Save
                        </Button>
                    </ModalFooter>
                </ModalContent>
            </form>
        </Modal>
    );
}
