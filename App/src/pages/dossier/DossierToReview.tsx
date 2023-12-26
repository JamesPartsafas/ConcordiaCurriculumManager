import { Text, useDisclosure, Flex, Box, useToast } from "@chakra-ui/react";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../App";
import { getDossierRequiredReview } from "../../services/dossier";
import React from "react";
import { DossierDTO, GetMyDossiersResponse } from "../../models/dossier";
import { BaseRoutes } from "../../constants";
import { useNavigate } from "react-router-dom";
import DossierTable from "../../components/DossierTable";

export default function DossiersToReview() {
    const user = useContext(UserContext);
    const toast = useToast(); // Use the useToast hook
    const { isOpen, onOpen, onClose } = useDisclosure();
    const navigate = useNavigate();

    const [myDossiers, setMyDossiers] = useState<DossierDTO[]>([]);
    const [showDossierModal, setShowDossierModal] = useState<boolean>(false);
    const [selectedDossier, setSelectedDossier] = useState<DossierDTO | null>(null);
    const [dossierModalAction, setDossierModalTitle] = useState<"add" | "edit">();
    const [loading, setLoading] = useState<boolean>(false);

    const [currentPage, setCurrentPage] = useState<number>(1);
    const resultsPerPage = 5;
    const totalResults = myDossiers.length;

    const startIndex = myDossiers.length === 0 ? 0 : (currentPage - 1) * resultsPerPage + 1;
    const endIndex = Math.min(currentPage * resultsPerPage, totalResults);

    const cancelRef = React.useRef();

    useEffect(() => {
        getAllDossiersRequired();
        console.log(user);
    }, []);

    function getAllDossiersRequired() {
        getDossierRequiredReview()
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

    function handleNavigateToDossierDetails(dossierId: string) {
        navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId));
    }
    function displayDossierModal() {
        setShowDossierModal(true);
    }

    return (
        <Box maxW="5xl" m="auto">
            <Flex flexDirection="column">
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    {user?.firstName + "'s"} Dossiers Under Review
                </Text>
                <DossierTable
                    myDossiers={myDossiers}
                    startIndex={startIndex}
                    endIndex={endIndex}
                    setSelectedDossier={setSelectedDossier}
                    onOpen={onOpen}
                    setDossierModalTitle={setDossierModalTitle}
                    displayDossierModal={displayDossierModal}
                    handleNavigateToDossierDetails={handleNavigateToDossierDetails}
                    setCurrentPage={setCurrentPage}
                    currentPage={currentPage}
                    totalResults={totalResults}
                />
            </Flex>
        </Box>
    );
}
