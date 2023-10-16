import { Container, Table, Thead, Tbody, Tr, Th, Td } from "@chakra-ui/react";

export default function DisplayGroups() {
    const groupsData = [
        { groupName: "Software Engineering", applicationsToApprove: 2, numberOfMembers: 5 },
        { groupName: "Group 2", applicationsToApprove: 0, numberOfMembers: 3 },
        { groupName: "Group 3", applicationsToApprove: 1, numberOfMembers: 4 },
    ];

    return (
        <Container maxW="3xl" height="80vh" display="flex" alignItems="center" justifyContent="center">
            <div>
                <h1
                    style={{
                        textAlign: "center",
                        marginBottom: "20px",
                        fontWeight: "bold",
                        fontSize: "24px",
                        color: "#FF8888",
                    }}
                >
                    Group Information
                </h1>
                <Table variant="simple" size="lg">
                    <Thead>
                        <Tr>
                            <Th>Group Name</Th>
                            <Th whiteSpace="nowrap">Applications to Approve</Th>
                            <Th whiteSpace="nowrap">Number of Members</Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        {groupsData.map((group, index) => (
                            <Tr key={index}>
                                <Td whiteSpace="nowrap" padding="16px">
                                    {group.groupName}
                                </Td>
                                <Td whiteSpace="nowrap" padding="16px">
                                    {group.applicationsToApprove}
                                </Td>
                                <Td whiteSpace="nowrap" padding="16px">
                                    {group.numberOfMembers}
                                </Td>
                            </Tr>
                        ))}
                    </Tbody>
                </Table>
            </div>
        </Container>
    );
}
