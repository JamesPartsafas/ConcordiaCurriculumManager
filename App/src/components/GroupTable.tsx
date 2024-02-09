import React from "react";
import { Link } from "react-router-dom";
import { isAdmin } from "../services/auth";
import { Button, Table, Thead, Tbody, Tr, Th, Td, Spacer, Flex, Text } from "@chakra-ui/react";
import { Button as ChakraButton } from "@chakra-ui/react";
import PropTypes from "prop-types";
import { BaseRoutes } from "../constants";
import { UserContext } from "../App";
import { useContext } from "react";

GroupTable.propTypes = {
    myGroups: PropTypes.array,
    startIndex: PropTypes.number,
    endIndex: PropTypes.number,
    setSelectedGroup: PropTypes.func,
    onOpen: PropTypes.func,
    setDeleteGroupId: PropTypes.func,
    setCurrentPage: PropTypes.func,
    currentPage: PropTypes.number,
    totalResults: PropTypes.number,
    useIcons: PropTypes.bool,
    groupsPerPage: PropTypes.number,
    onEditGroup: PropTypes.func,
    showManageMembers: PropTypes.bool,
};

function GroupTable({
    myGroups,
    startIndex,
    endIndex,
    onOpen,
    setCurrentPage,
    currentPage,
    groupsPerPage,
    onEditGroup,
    setDeleteGroupId,
    showManageMembers = true,
}) {
    const handleEditGroup = (groupId) => {
        onEditGroup(groupId);
        onOpen();
    };

    const onDeleteGroup = (groupId) => {
        setDeleteGroupId(groupId);
    };

    const user = useContext(UserContext);

    return (
        <Table variant="simple" size="md" borderRadius="xl" boxShadow="xl" border="2px">
            <Thead backgroundColor={"#e2e8f0"}>
                <Tr>
                    <Th whiteSpace="nowrap" textAlign={"center"}>
                        Group Name
                    </Th>
                    <Th whiteSpace="nowrap" textAlign={"center"}>
                        Number of Members
                    </Th>
                    {showManageMembers && (
                        <Th whiteSpace="nowrap" textAlign={"center"}>
                            Manage Members
                        </Th>
                    )}
                    {isAdmin(user) && (
                        <Th whiteSpace="nowrap" textAlign={"center"}>
                            Manage Masters
                        </Th>
                    )}
                    {isAdmin(user) && (
                        <Th whiteSpace="nowrap" textAlign={"center"}>
                            Delete Group
                        </Th>
                    )}
                    {isAdmin(user) && (
                        <Th whiteSpace="nowrap" textAlign={"center"}>
                            Edit Group
                        </Th>
                    )}
                </Tr>
            </Thead>
            <Tbody>
                {myGroups.slice(startIndex - 1, endIndex).map((group) => (
                    <Tr key={group.id}>
                        <Td whiteSpace="nowrap" padding="17px" textAlign="center">
                            {group.name}
                        </Td>
                        <Td whiteSpace="nowrap" padding="16px" textAlign="center">
                            {group.members?.length ?? 0}
                        </Td>
                        {showManageMembers && (
                            <Td whiteSpace="nowrap" padding="16px">
                                <Link to={BaseRoutes.AddUserToGroup} state={{ gid: group.id, name: group.name }}>
                                    <Button colorScheme="primary" variant="outline" width="25%" height="40px" mr={1}>
                                        Add
                                    </Button>
                                </Link>
                                {(group.members?.length || 0) !== 0 && (
                                    <Link
                                        to={BaseRoutes.RemoveUserFromGroup}
                                        state={{ gid: group.id, name: group.name }}
                                    >
                                        <Button colorScheme="primary" variant="outline" width="43%" height="40px">
                                            Remove
                                        </Button>
                                    </Link>
                                )}
                            </Td>
                        )}
                        {isAdmin(user) && (
                            <Td whiteSpace="nowrap" padding="16px">
                                <Link to={BaseRoutes.AddGroupMaster} state={{ gid: group.id, name: group.name }}>
                                    <Button colorScheme="primary" variant="outline" width="25%" height="40px" mr={1}>
                                        Add
                                    </Button>
                                </Link>
                                {(group.members?.length || 0) !== 0 && (
                                    <Link to={BaseRoutes.RemoveGroupMaster} state={{ gid: group.id, name: group.name }}>
                                        <Button colorScheme="primary" variant="outline" width="43%" height="40px">
                                            Remove
                                        </Button>
                                    </Link>
                                )}
                            </Td>
                        )}
                        {isAdmin(user) && (
                            <Td whiteSpace="nowrap" padding="16px">
                                <Link to={BaseRoutes.ManageableGroup}>
                                    <Button
                                        colorScheme="primary"
                                        variant="outline"
                                        width="50%"
                                        height="40px"
                                        onClick={() => onDeleteGroup(group.id)}
                                    >
                                        Delete
                                    </Button>
                                </Link>
                            </Td>
                        )}
                        {isAdmin(user) && (
                            <Td whiteSpace="nowrap" padding="16px">
                                <Link to={BaseRoutes.ManageableGroup}>
                                    <Button
                                        colorScheme="primary"
                                        variant="outline"
                                        width="50%"
                                        height="40px"
                                        onClick={() => handleEditGroup(group.id)}
                                    >
                                        Edit
                                    </Button>
                                </Link>
                            </Td>
                        )}
                    </Tr>
                ))}
            </Tbody>
            <Tbody>
                <Tr>
                    <Td height={20}>
                        <Flex>
                            <Text alignSelf="center">
                                Showing {startIndex} to {endIndex} of {myGroups.length} results
                            </Text>
                            <Spacer />
                            <ChakraButton
                                mr={4}
                                p={4}
                                variant="outline"
                                onClick={() => setCurrentPage(currentPage - 1)}
                                isDisabled={startIndex === 1}
                            >
                                Prev
                            </ChakraButton>
                            <ChakraButton
                                p={4}
                                variant="outline"
                                onClick={() =>
                                    setCurrentPage((prevPage) =>
                                        prevPage * groupsPerPage >= myGroups.length ? prevPage : prevPage + 1
                                    )
                                }
                                isDisabled={endIndex >= myGroups.length}
                            >
                                Next
                            </ChakraButton>
                        </Flex>
                    </Td>
                </Tr>
            </Tbody>
        </Table>
    );
}

export default GroupTable;
