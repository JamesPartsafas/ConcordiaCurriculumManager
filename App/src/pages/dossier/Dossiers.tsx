import { Table, Thead, Tbody, Text, Tr, Th, Td, TableContainer, IconButton } from "@chakra-ui/react";
import { DeleteIcon, EditIcon } from "@chakra-ui/icons";

import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../App";
import { DossierDTO, GetMyDossiersResponse, getMyDossiers } from "../../services/dossier";
import DossierModal from "./DossierModal";

export default function Dossiers() {
    const user = useContext(UserContext);

    const [myDossiers, setMyDossiers] = useState<DossierDTO[]>([]);
    const [showDossierModal, setShowDossierModal] = useState<boolean>(false);
    const [selectedDossier, setSelectedDossier] = useState<DossierDTO | null>(null);

    useEffect(() => {
        getAllDossiers();
    }, []);

    function getAllDossiers() {
        getMyDossiers()
            .then(
                (res: GetMyDossiersResponse) => {
                    setMyDossiers(res.data);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    function displayDossierModal(dossier: DossierDTO) {
        setSelectedDossier(dossier);
        setShowDossierModal(true);
    }

    function closeModal() {
        setShowDossierModal(false);
    }

    return (
        <>
            <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                {user?.firstName + "'s"} Dossiers
            </Text>

            <TableContainer borderRadius="xl" boxShadow="xl" maxW="5xl" m="auto" border="2px">
                <Table variant="simple" style={{ backgroundColor: "white" }}>
                    <Thead backgroundColor={"#e2e8f0"}>
                        <Tr display={"flex"}>
                            <Th flex={"2"}>Title</Th>
                            <Th flex={"4"}>Description</Th>
                            <Th flex={"1"}>Published</Th>
                            <Th flex={"1"}></Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        {myDossiers.map((dossier) => (
                            <Tr
                                key={dossier.id}
                                onClick={() => {
                                    setShowDossierModal(true);
                                    displayDossierModal(dossier);
                                }}
                                display={"flex"}
                            >
                                <Td flex={"2"}>{dossier.title}</Td>
                                <Td flex={"4"}>
                                    <Text overflow="hidden" textOverflow="ellipsis" maxW={"300px"}>
                                        {dossier.description}
                                    </Text>
                                </Td>
                                <Td flex={"1"}>{dossier.published ? "Yes" : "No"}</Td>
                                <Td flex={"1"}>
                                    <IconButton
                                        aria-label="Delete"
                                        icon={<DeleteIcon />}
                                        backgroundColor={"transparent"}
                                        _hover={{ backgroundColor: "#932439" }}
                                    />
                                    <IconButton
                                        ml={2}
                                        aria-label="Edit"
                                        icon={<EditIcon />}
                                        backgroundColor={"transparent"}
                                        _hover={{ backgroundColor: "#0072a8" }}
                                    />
                                </Td>
                            </Tr>
                        ))}
                    </Tbody>
                </Table>
            </TableContainer>
            {/* this is the Dossier Modal */}
            {showDossierModal && (
                <DossierModal dossier={selectedDossier} open={showDossierModal} closeModal={closeModal} />
            )}
        </>
    );
}
