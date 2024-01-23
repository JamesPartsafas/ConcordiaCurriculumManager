import React from "react";
import { Table, Thead, Tbody, Tr, Th, Td, TableContainer, IconButton, Spacer, Flex, Text } from "@chakra-ui/react";
import { Button as ChakraButton } from "@chakra-ui/react";
import { DeleteIcon, EditIcon } from "@chakra-ui/icons";
import PropTypes from "prop-types";

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
};

function GroupTable({
    myGroups,
    startIndex,
    endIndex,
    setSelectedGroup,
    onOpen,
    // setDeleteGroupId,
    setCurrentPage,
    currentPage,
    // totalResults,
    useIcons,
    groupsPerPage,
}) {
    return (
        <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
            <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "fixed" }}>
                <Thead backgroundColor={"#e2e8f0"}>
                    <Tr display={"flex"}>
                        <Th flex="1">Group Name</Th>
                        <Th flex="1">Applications to Approve</Th>
                        <Th flex="1">Number of Members</Th>
                        <Th flex="1">Delete Group</Th>
                        <Th flex="1">Edit Group</Th>
                        {useIcons && <Th flex="1"></Th>}
                    </Tr>
                </Thead>
                <Tbody>
                    {myGroups.slice(startIndex - 1, endIndex).map((group) => (
                        <Tr key={group.id} display={"flex"}>
                            <Td flex="1">{group.name}</Td>
                            <Td flex="1">{0}</Td>
                            <Td flex="1">{group.members?.length ?? 0}</Td>
                            {useIcons && (
                                <>
                                    <Td flex="1">
                                        <IconButton
                                            aria-label="Delete"
                                            icon={<DeleteIcon />}
                                            backgroundColor={"#932439"}
                                            color={"white"}
                                            onClick={() => {
                                                setSelectedGroup(group);
                                                onOpen();
                                            }}
                                        />
                                    </Td>
                                    <Td flex="1">
                                        <IconButton
                                            ml={2}
                                            aria-label="Edit"
                                            icon={<EditIcon />}
                                            backgroundColor={"#0072a8"}
                                            color={"white"}
                                            onClick={() => {
                                                // edit code
                                            }}
                                        />
                                    </Td>
                                </>
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
        </TableContainer>
    );
}

export default GroupTable;
