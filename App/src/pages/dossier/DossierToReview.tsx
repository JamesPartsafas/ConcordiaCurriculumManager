import { Text, useDisclosure, Flex, Box, Container } from "@chakra-ui/react";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../App";
import { getDossierRequiredReview } from "../../services/dossier";
import { DossierDTO, GetMyDossiersResponse } from "../../models/dossier";
import { BaseRoutes } from "../../constants";
import { useNavigate } from "react-router-dom";
import Button from "../../components/Button";
import DossierTable from "../../components/DossierTable";
import { UserRoles } from "../../models/user";
import DossierModal from "./DossierModal";

export default function DossiersToReview() {
    const user = useContext(UserContext);
    const { onOpen } = useDisclosure();
    const navigate = useNavigate();

    const [myDossiers, setMyDossiers] = useState<DossierDTO[]>([]);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [selectedDossier, setSelectedDossier] = useState<DossierDTO | null>(null);
    const resultsPerPage = 5;
    const totalResults = myDossiers.length;
    const [dossierModalAction, setDossierModalTitle] = useState<"add" | "edit">();
    const [showDossierModal, setShowDossierModal] = useState<boolean>(false);

    const startIndex = myDossiers.length === 0 ? 0 : (currentPage - 1) * resultsPerPage + 1;
    const endIndex = Math.min(currentPage * resultsPerPage, totalResults);

    useEffect(() => {
        getAllDossiersRequired();
        console.log(user);
        console.log(selectedDossier);
    }, []);

    async function getAllDossiersRequired() {
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
    function displayDossierModal() {
        setShowDossierModal(true);
    }

    function closeDossierModal() {
        setShowDossierModal(false);
    }

    function handleNavigateToDossierDetails(dossierId: string) {
        navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId));
    }

    function handleNavigateToDossierReview(dossierId: string) {
        navigate(BaseRoutes.DossierReview.replace(":dossierId", dossierId));
    }

    function handleNavigateToDossierReport(dossierId: string) {
        navigate(BaseRoutes.DossierReport.replace(":dossierId", dossierId));
    }

    return (
        <div>
            <Box maxW="5xl" m="auto">
                <Container maxW={"5xl"} mt={5} pl={0}>
                    <Button
                        style="primary"
                        variant="outline"
                        height="40px"
                        width="fit-content"
                        onClick={() => navigate(BaseRoutes.Home)}
                    >
                        Return to Home
                    </Button>
                    <Button
                        style="primary"
                        variant="outline"
                        width="fit-content"
                        height="40px"
                        alignSelf="flex-end"
                        ml="2"
                        isDisabled={!user.roles.includes(UserRoles.Initiator)}
                        onClick={() => {
                            navigate(BaseRoutes.Dossiers);
                        }}
                    >
                        My Dossiers
                    </Button>
                    <Button
                        style="primary"
                        variant="outline"
                        height="40px"
                        width="fit-content"
                        ml="2"
                        onClick={() => navigate(BaseRoutes.DossierBrowser)}
                    >
                        Dossier Browser
                    </Button>
                </Container>
                <Flex flexDirection="column">
                    <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                        Dossiers Requiring Your Review
                    </Text>
                    <DossierTable
                        myDossiers={myDossiers}
                        startIndex={startIndex}
                        endIndex={endIndex}
                        onOpen={onOpen}
                        setDossierModalTitle={setDossierModalTitle}
                        setSelectedDossier={setSelectedDossier}
                        displayDossierModal={displayDossierModal}
                        handleNavigateToDossierDetails={handleNavigateToDossierDetails}
                        handleNavigateToDossierReview={handleNavigateToDossierReview}
                        handleNavigateToDossierReport={handleNavigateToDossierReport}
                        setCurrentPage={setCurrentPage}
                        currentPage={currentPage}
                        totalResults={totalResults}
                        useIcons={false}
                        reviewIcons={true}
                        editable={true}
                    />
                </Flex>
            </Box>
            {showDossierModal && (
                <DossierModal
                    action={dossierModalAction}
                    dossier={selectedDossier}
                    dossierList={myDossiers}
                    open={showDossierModal}
                    closeModal={closeDossierModal}
                />
            )}
        </div>
    );
}
