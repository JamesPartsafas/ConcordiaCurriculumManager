import { useEffect, useState } from "react";
import Button from "../components/Button";
import { useDisclosure } from "@chakra-ui/react";
import SearchCourseGrouping from "../components/CourseGrouping/SearchCourseGrouping";
import { GetCourseGrouping } from "../services/courseGrouping";

export default function CoursesLeft() {
    const [courseGrouping, setCourseGrouping] = useState(null);

    const {
        isOpen: isSearchCourseGroupingOpen,
        onOpen: onSearchCourseGroupingOpen,
        onClose: onSearchCourseGroupingClose,
    } = useDisclosure();

    useEffect(() => {}, []);

    function displaySearchCourseGroupModal() {
        return (
            <SearchCourseGrouping
                isOpen={isSearchCourseGroupingOpen}
                onClose={onSearchCourseGroupingClose}
                onSelectCourseGrouping={handleCourseGroupingSelect}
                isEdit={true}
            />
        );
    }

    function handleCourseGroupingSelect(courseGrouping) {
        requestCourseGrouping(courseGrouping.id);
    }

    function requestCourseGrouping(CourseGroupingId: string) {
        GetCourseGrouping(CourseGroupingId)
            .then(
                (response) => {
                    console.log(response.data);
                    setCourseGrouping(response.data);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    return (
        <div>
            {displaySearchCourseGroupModal()}

            <Button
                backgroundColor="brandGray500"
                _hover={{ bg: "brandGray" }}
                variant="solid"
                style="secondary"
                width="100%"
                onClick={() => {
                    onSearchCourseGroupingOpen();
                }}
            >
                Select Program
            </Button>
        </div>
    );
}
