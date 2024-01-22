import { Container, Text } from "@chakra-ui/react";
import Button from "../../components/Button";
import { BaseRoutes } from "../../constants";
import { useNavigate, useParams } from "react-router-dom";
import { UserContext } from "../../App";
import { useContext, useEffect, useState } from "react";
import { DossierDetailsDTO, DossierDetailsResponse, dossierStateToString } from "../../models/dossier";
import { getDossierDetails } from "../../services/dossier";
import { UserRoles } from "../../models/user";

export default function DossierReport() {
    const navigate = useNavigate();
    const user = useContext(UserContext);
    const { dossierId } = useParams();
    const [dossierDetails, setDossierDetails] = useState<DossierDetailsDTO | null>(null);
    useEffect(() => {
        requestDossierDetails(dossierId);
    }, [dossierId]);

    async function requestDossierDetails(dossierId: string) {
        const dossierDetailsData: DossierDetailsResponse = await getDossierDetails(dossierId);
        setDossierDetails(dossierDetailsData.data);
    }
    return (
        <div>
            <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                {user?.firstName + "'s"} Dossier Report of {dossierDetails?.title}
            </Text>
            <Container maxW={"5xl"} mt={5}>
                <Button
                    style="primary"
                    variant="outline"
                    width="fit-content"
                    height="40px"
                    ml="2"
                    isDisabled={!user.roles.includes(UserRoles.Initiator)}
                    onClick={() => {
                        navigate(BaseRoutes.DossiersToReview);
                    }}
                >
                    Dossiers To Review
                </Button>
                <Button
                    style="primary"
                    variant="outline"
                    height="40px"
                    width="fit-content"
                    ml="2"
                    onClick={() => navigate(BaseRoutes.Dossiers)}
                >
                    My Dossiers
                </Button>
                <Button
                    style="primary"
                    variant="outline"
                    height="40px"
                    width="fit-content"
                    ml="2"
                    onClick={() => navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId))}
                >
                    Dossier Details
                </Button>
                {dossierStateToString(dossierDetails) != "Created" && (
                    <Button
                        style="primary"
                        variant="outline"
                        height="40px"
                        width="fit-content"
                        ml="2"
                        onClick={() => navigate(BaseRoutes.DossierReview.replace(":dossierId", dossierId))}
                    >
                        Dossier Review
                    </Button>
                )}
            </Container>
        </div>
    );
}
