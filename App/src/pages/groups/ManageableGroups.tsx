import React, { useState, useEffect, useContext, useRef } from "react";
import {
    Container,
    Box,
    Button,
    AlertDialog,
    AlertDialogBody,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogContent,
    AlertDialogOverlay,
    Flex,
} from "@chakra-ui/react";
import { Link } from "react-router-dom";
import { isAdmin } from "../../services/auth";
import { GetAllGroups, GroupDTO, MultiGroupResponseDTO } from "../../services/group";
import { BaseRoutes } from "../../constants";
import { UserContext } from "../../App";
import GroupTable from "../../components/GroupTable";

export default function DisplayManageableGroups() {
    const [myGroups, setMyGroups] = useState<GroupDTO[]>([]);
    const [deleteGroupId, setDeleteGroupId] = useState<string | null>(null);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const groupsPerPage = 5;
    const user = useContext(UserContext);
    const cancelRef = useRef();

    useEffect(() => {
        GetAllGroups()
            .then(
                (res: MultiGroupResponseDTO) => {
                    const groups: GroupDTO[] = res.data;
                    if (isAdmin(user)) {
                        setMyGroups(groups);
                    } else {
                        setMyGroups(groups.filter((group) => user.masteredGroups.includes(group.id)));
                    }
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }, [user]);

    const onDeleteGroup = (groupId) => {
        console.log(`Deleting group with ID: ${groupId}`);
        // Incomplete yet
        setDeleteGroupId(null);
    };

    const indexOfLastGroup = currentPage * groupsPerPage;
    const indexOfFirstGroup = indexOfLastGroup - groupsPerPage;

    return (
        <Box overflowX="auto" width="100%" height="100vh" padding="2vw">
            <Container
                maxW="5xl"
                height="80vh"
                display="flex"
                alignItems="center"
                justifyContent="center"
                mt={10}
                mb={10}
            >
                <Box>
                    <h1
                        style={{
                            textAlign: "center",
                            marginBottom: "20px",
                            fontWeight: "bold",
                            fontSize: "24px",
                            color: "brandRed",
                        }}
                    >
                        Group Information
                    </h1>

                    {/* Replace the table rendering with the new GroupTable component */}
                    <GroupTable
                        myGroups={myGroups}
                        startIndex={indexOfFirstGroup + 1}
                        endIndex={indexOfLastGroup}
                        setSelectedGroup={(group) => setDeleteGroupId(group.id)}
                        onOpen={() => console.log("Open dialog")}
                        setDeleteGroupId={setDeleteGroupId}
                        setCurrentPage={setCurrentPage}
                        currentPage={currentPage}
                        totalResults={myGroups.length}
                        useIcons={isAdmin(user)}
                        groupsPerPage={groupsPerPage}
                    />

                    <Flex justify="center" mt={4}>
                        <Link to={BaseRoutes.CreateGroup}>
                            <Button color="white" backgroundColor={"#932439"} variant="solid" height="40px">
                                Create Group
                            </Button>
                        </Link>
                    </Flex>

                    <Flex justify="center" mt={4}>
                        <Link to={BaseRoutes.Home}>
                            <Button color="white" backgroundColor={"#932439"} variant="solid" height="40px">
                                Back to Home Page
                            </Button>
                        </Link>
                    </Flex>

                    <AlertDialog
                        isOpen={deleteGroupId !== null}
                        leastDestructiveRef={cancelRef}
                        onClose={() => setDeleteGroupId(null)}
                        size="sm"
                    >
                        <AlertDialogOverlay>
                            <AlertDialogContent>
                                <AlertDialogHeader fontSize="lg" fontWeight="bold">
                                    Delete Group
                                </AlertDialogHeader>
                                <AlertDialogBody>Are you sure? This action cannot be undone.</AlertDialogBody>
                                <AlertDialogFooter>
                                    <Button ref={cancelRef} onClick={() => setDeleteGroupId(null)}>
                                        Cancel
                                    </Button>
                                    <Button
                                        colorScheme="red"
                                        onClick={() => {
                                            onDeleteGroup(deleteGroupId);
                                        }}
                                        ml={3}
                                    >
                                        Delete
                                    </Button>
                                </AlertDialogFooter>
                            </AlertDialogContent>
                        </AlertDialogOverlay>
                    </AlertDialog>
                </Box>
            </Container>
        </Box>
    );
}
