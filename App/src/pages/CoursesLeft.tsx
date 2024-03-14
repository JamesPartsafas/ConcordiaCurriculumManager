import { useEffect, useState } from "react";
import Button from "../components/Button";
import { Box, Heading, useDisclosure } from "@chakra-ui/react";
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
                    if (response.data.isTopLevel) {
                        setCourseGrouping(response.data);
                    }
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
        <>
            {displaySearchCourseGroupModal()}

            <Box width="30%" margin={"auto"} marginTop={5}>
                <Button
                    backgroundColor="brandRed"
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
            </Box>

            <Box
                mt={10}
                mb={10}
                backgroundColor={"brandRed"}
                minH={"100px"}
                display="flex"
                flexDirection={"row"}
                alignItems="center"
            >
                <Box w={"70%"} margin={"auto"} py={3}>
                    <Heading size={"3xl"} color={"white"}>
                        {courseGrouping?.isTopLevel
                            ? " Degree Requirements for " + courseGrouping?.name
                            : "Select a Program"}
                    </Heading>
                </Box>
            </Box>
        </>
    );
}
