import { Container, Table, Thead, Tbody, Tr, Th, Td } from "@chakra-ui/react";
import Button from "../components/Button";
import { useNavigate } from "react-router-dom";
import { GetAllGroups } from "../services/group";
import { GroupDTO, MultiGroupResponseDTO } from "../services/group";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../App";

export default function DisplayManageableGroups() {
    const groupsData = [
        { groupName: "Software Engineering", applicationsToApprove: 2, numberOfMembers: 5 },
        { groupName: "Group 2", applicationsToApprove: 0, numberOfMembers: 3 },
        { groupName: "Group 3", applicationsToApprove: 1, numberOfMembers: 4 },
    ];
    const [myGroups, setMyGroups] = useState<GroupDTO[]>([]);
    const user = useContext(UserContext);
    const navigate = useNavigate();
    useEffect(() => {
        AllGroups();
        console.log(user);
    }, []);

    function AllGroups() {
        GetAllGroups()
            .then(
                (res: MultiGroupResponseDTO) => {
                    setMyGroups(res.data);
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
                            <Th whiteSpace="nowrap">Manage Members</Th>
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
                                <Td whiteSpace="nowrap" padding="16px">
                                    <Button
                                        type="primary"
                                        variant="outline"
                                        width="50%"
                                        height="40px"
                                        onClick={() => navigate("/addusertogroup")}
                                    >
                                        Add
                                    </Button>

                                    <Button
                                        type="primary"
                                        variant="outline"
                                        width="50%"
                                        height="40px"
                                        onClick={() => navigate("/removeuserfromgroup")}
                                    >
                                        Remove
                                    </Button>
                                </Td>
                            </Tr>
                        ))}
                    </Tbody>
                </Table>
            </div>
        </Container>
    );
}
