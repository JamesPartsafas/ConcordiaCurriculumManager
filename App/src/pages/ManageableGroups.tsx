import { Container, Table, Thead, Tbody, Tr, Th, Td } from "@chakra-ui/react";
import Button from "../components/Button";
import { Link } from "react-router-dom";
import { GetAllGroups } from "../services/group";
import { GroupDTO, MultiGroupResponseDTO } from "../services/group";
import { useEffect, useState } from "react";
import { BaseRoutes } from "../constants";
import Header from "../shared/Header";

export default function DisplayManageableGroups() {
    const [myGroups, setMyGroups] = useState<GroupDTO[]>([]);
    useEffect(() => {
        AllGroups();
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
                                <Th whiteSpace="nowrap">Edit Group</Th>
                                <Th whiteSpace="nowrap">Delete Group</Th>
                            </Tr>
                        </Thead>
                        <Tbody>
                            {myGroups.map((group, index) => (
                                <Tr key={index}>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign={"center"}>
                                        {group.name}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign={"center"}>
                                        {0}
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px" textAlign={"center"}>
                                        {group.members?.length}
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
                                        <Link
                                            to={BaseRoutes.RemoveUserFromGroup}
                                            state={{ gid: group.id, name: group.name }}
                                        >
                                            <Button style="primary" variant="outline" width="50%" height="40px">
                                                Remove
                                            </Button>
                                        </Link>
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px">
                                        <Link to={BaseRoutes.ManageableGroup}>
                                            <Button style="primary" variant="outline" width="50%" height="40px">
                                                Edit
                                            </Button>
                                        </Link>
                                    </Td>
                                    <Td whiteSpace="nowrap" padding="16px">
                                        <Link to={BaseRoutes.ManageableGroup}>
                                            <Button style="primary" variant="outline" width="50%" height="40px">
                                                Delete
                                            </Button>
                                        </Link>
                                    </Td>
                                </Tr>
                            ))}
                        </Tbody>
                    </Table>
                    <Link to={BaseRoutes.CreateGroup}>
                        <Button style="primary" variant={"solid"} width="100%" height="40px">
                            Create Group
                        </Button>
                    </Link>
                </div>
            </Container>
        </div>
    );
}
