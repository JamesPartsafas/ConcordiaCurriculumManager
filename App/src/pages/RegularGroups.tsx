import { Container, Table, Thead, Tbody, Tr, Th, Td } from "@chakra-ui/react";
import { useContext, useEffect, useState } from "react";
import { GetAllGroups, GroupDTO, MultiGroupResponseDTO } from "../services/group";
import { UserContext } from "../App";
import Header from "../shared/Header";

export default function RegularGroups() {
    const [myGroups, setMyGroups] = useState<GroupDTO[]>([]);
    const user = useContext(UserContext);
    useEffect(() => {
        AllGroups();
    }, []);

    function AllGroups() {
        GetAllGroups()
            .then(
                (res: MultiGroupResponseDTO) => {
                    setMyGroups(res.data);
                    setMyGroups(res.data.filter(isMemberOfGroup));
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }
    function isMemberOfGroup(GroupDTO: GroupDTO): boolean {
        return GroupDTO.members.map((member) => member.id).includes(user.id);
    }
    return (
        <div>
            <Header></Header>
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
                            {myGroups.map((group, index) => (
                                <Tr key={index}>
                                    <Td whiteSpace="nowrap" padding="16px">
                                        {group.name}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px">
                                        {0}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px">
                                        {group.members.length}
                                    </Td>
                                </Tr>
                            ))}
                        </Tbody>
                    </Table>
                </div>
            </Container>
        </div>
    );
}
