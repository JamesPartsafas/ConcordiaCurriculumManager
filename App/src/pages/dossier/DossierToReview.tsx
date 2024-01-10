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

export default function DossiersToReview() {
    const user = useContext(UserContext);
    const { onOpen } = useDisclosure();
    const navigate = useNavigate();

    const [myDossiers, setMyDossiers] = useState<DossierDTO[]>([]);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const resultsPerPage = 5;
    const totalResults = myDossiers.length;

    const startIndex = myDossiers.length === 0 ? 0 : (currentPage - 1) * resultsPerPage + 1;
    const endIndex = Math.min(currentPage * resultsPerPage, totalResults);

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

    function handleNavigateToDossierReview(dossierId: string) {
        navigate(BaseRoutes.DossierReview.replace(":dossierId", dossierId));
    }

    return (
        <Box maxW="5xl" m="auto">
            <Container maxW={"5xl"} mt={5}>
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
                    isDisabled={!user.roles.includes(UserRoles.Initiator)}
                    onClick={() => {
                        navigate(BaseRoutes.Dossiers);
                    }}
                >
                    My Dossiers
                </Button>
            </Container>
            <Flex flexDirection="column">
                <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                    {user?.firstName + "'s"} Dossiers Under Review
                </Text>
                <DossierTable
                    myDossiers={myDossiers}
                    startIndex={startIndex}
                    endIndex={endIndex}
                    onOpen={onOpen}
                    handleNavigateToDossierDetails={handleNavigateToDossierDetails}
                    handleNavigateToDossierReview={handleNavigateToDossierReview}
                    setCurrentPage={setCurrentPage}
                    currentPage={currentPage}
                    totalResults={totalResults}
                    useIcons={false}
                    reviewIcons={true}
                />
            </Flex>
        </Box>
    );
}
