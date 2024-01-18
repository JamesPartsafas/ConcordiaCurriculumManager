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
    setDeleteGroupId,
    setCurrentPage,
    currentPage,
    totalResults,
    useIcons,
    groupsPerPage,
}) {
    return (
        <TableContainer borderRadius="xl" boxShadow="xl" border="2px" maxH="500px" overflowY="hidden">
            <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                <Thead backgroundColor={"#e2e8f0"}>
                    <Tr display={"flex"}>
                        <Th minW={"200px"} maxW={"200px"}>
                            Group Name
                        </Th>
                        <Th minW={"200px"} maxW={"200px"}>
                            Applications to Approve
                        </Th>
                        <Th minW={"200px"} maxW={"200px"}>
                            Number of Members
                        </Th>
                        <Th minW={"200px"} maxW={"200px"}>
                            Delete Group
                        </Th>
                        <Th minW={"200px"} maxW={"200px"}>
                            Edit Group
                        </Th>
                        {useIcons && <Th width={"25%"}></Th>}
                    </Tr>
                </Thead>
                <Tbody>
                    {myGroups.slice(startIndex - 1, endIndex).map((group) => (
                        <Tr key={group.id} display={"flex"}>
                            <Td minW={"200px"} maxW={"200px"}>
                                {group.name}
                            </Td>
                            <Td minW={"200px"} maxW={"200px"}>
                                {0} {/* Placeholder for Applications to Approve */}
                            </Td>
                            <Td minW={"200px"} maxW={"200px"}>
                                {group.members?.length ?? 0}
                            </Td>
                            {useIcons && (
                                <>
                                    <Td width={"25%"}>
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
                                    <Td>
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
