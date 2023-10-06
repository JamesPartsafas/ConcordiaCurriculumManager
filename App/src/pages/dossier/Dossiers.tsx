import { Table, Thead, Tbody, Text, Tr, Th, Td, TableContainer } from "@chakra-ui/react";

import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../App";
import { DossierDTO, GetMyDossiersResponse, getMyDossiers } from "../../services/dossier";
import DossierModal from "./DossierModal";
import { set } from "react-hook-form";

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
                    console.log(res.data);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }
    document.body.style.backgroundColor = "#932439";

    function displayDossierModal(dossier: DossierDTO) {
        setSelectedDossier(dossier);
        setShowDossierModal(true);
    }

    function closeModal() {
        setShowDossierModal(false);
    }

    return (
        <>
            <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%">
                My Dossiers
            </Text>

            <TableContainer style={{ backgroundColor: "white" }} borderRadius="xl" boxShadow="xl" maxW="5xl" m="auto">
                <Table variant="simple">
                    <Thead>
                        <Tr>
                            <Th>Title</Th>
                            <Th>Description</Th>
                            <Th>Published</Th>
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
                            >
                                <Td maxW="100px" overflow="hidden">
                                    {dossier.title}
                                </Td>
                                <Td maxW="200px">
                                    <Text maxW="100%" overflow="hidden" textOverflow="ellipsis">
                                        {dossier.description}
                                    </Text>
                                </Td>
                                <Td>{dossier.published ? "Yes" : "No"}</Td>
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
