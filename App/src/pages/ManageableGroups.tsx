import { useState } from "react";
import {
    Container,
    Table,
    Thead,
    Tbody,
    Tr,
    Th,
    Td,
    AlertDialog,
    AlertDialogBody,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogContent,
    AlertDialogOverlay,
} from "@chakra-ui/react";
import Button from "../components/Button";
import { Link } from "react-router-dom";
import { isAdmin } from "../services/auth";
import { GetAllGroups, GroupDTO, MultiGroupResponseDTO } from "../services/group";
import { useContext, useEffect, useRef } from "react";
import { BaseRoutes } from "../constants";
import { UserContext } from "../App";

export default function DisplayManageableGroups() {
    const [myGroups, setMyGroups] = useState<GroupDTO[]>([]);
    const [deleteGroupId, setDeleteGroupId] = useState<string | null>(null);
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
        // Add logic to delete the group here
        console.log(`Deleting group with ID: ${groupId}`);
        // You can call the API or perform any other actions here
        // ...

        // Clear the deleteGroupId after the action is performed
        setDeleteGroupId(null);
    };

    return (
        <div>
            <Container maxW="3xl" height="80vh" display="flex" alignItems="center" justifyContent="center">
                <div>
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
                    <Table variant="simple" size="lg">
                        <Thead>
                            <Tr>
                                <Th whiteSpace="nowrap">Group Name</Th>
                                <Th whiteSpace="nowrap">Applications to Approve</Th>
                                <Th whiteSpace="nowrap">Number of Members</Th>
                                <Th whiteSpace="nowrap">Manage Members</Th>
                                {isAdmin(user) && <Th whiteSpace="nowrap">Manage Masters</Th>}
                                {isAdmin(user) && <Th whiteSpace="nowrap">Edit Group</Th>}
                                {isAdmin(user) && <Th whiteSpace="nowrap">Delete Group</Th>}
                            </Tr>
                        </Thead>
                        <Tbody>
                            {myGroups.map((group, index) => (
                                <Tr key={index}>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {group.name}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {0}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                        {group.members?.length ?? 0}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px">
                                        <Link
                                            to={BaseRoutes.AddUserToGroup}
                                            state={{ gid: group.id, name: group.name }}
                                        >
                                            <Button style="primary" variant="outline" width="50%" height="40px">
                                                Add
                                            </Button>
                                        </Link>
                                        {(group.members?.length || 0) !== 0 && (
                                            <Link
                                                to={BaseRoutes.RemoveUserFromGroup}
                                                state={{ gid: group.id, name: group.name }}
                                            >
                                                <Button style="primary" variant="outline" width="50%" height="40px">
                                                    Remove
                                                </Button>
                                            </Link>
                                        )}
                                    </Td>
                                    {isAdmin(user) && (
                                        <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                            <Link
                                                to={BaseRoutes.AddGroupMaster}
                                                state={{ gid: group.id, name: group.name }}
                                            >
                                                <Button style="primary" variant="outline" width="50%" height="40px">
                                                    Add
                                                </Button>
                                            </Link>
                                            {(group.groupMasters?.length || 0) !== 0 && (
                                                <Link
                                                    to={BaseRoutes.RemoveGroupMaster}
                                                    state={{ gid: group.id, name: group.name }}
                                                >
                                                    <Button style="primary" variant="outline" width="50%" height="40px">
                                                        Remove
                                                    </Button>
                                                </Link>
                                            )}
                                        </Td>
                                    )}
                                    {isAdmin(user) && (
                                        <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                            <Link to={BaseRoutes.ManageableGroup}>
                                                <Button style="primary" variant="outline" width="50%" height="40px">
                                                    Edit {/**for Future Edit Group Name */}
                                                </Button>
                                            </Link>
                                        </Td>
                                    )}
                                    {isAdmin(user) && (
                                        <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                                            <Link to={BaseRoutes.ManageableGroup}>
                                                <Button
                                                    style="primary"
                                                    variant="outline"
                                                    width="50%"
                                                    height="40px"
                                                    onClick={() => setDeleteGroupId(group.id)}
                                                >
                                                    Delete {/**for Future Edit Group Name */}
                                                </Button>
                                            </Link>
                                        </Td>
                                    )}
                                </Tr>
                            ))}
                        </Tbody>
                    </Table>
                    {isAdmin(user) && (
                        <Link to={BaseRoutes.CreateGroup}>
                            <Button style="primary" variant="solid" width="100%" height="40px">
                                Create Group
                            </Button>
                        </Link>
                    )}
                    <Link to={BaseRoutes.Home}>
                        <Button style="primary" variant="solid" width="100%" height="40px">
                            Back to Home Page
                        </Button>
                    </Link>
                    <AlertDialog
                        isOpen={deleteGroupId !== null}
                        leastDestructiveRef={cancelRef}
                        onClose={() => setDeleteGroupId(null)}
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
                </div>
            </Container>
        </div>
    );
}
