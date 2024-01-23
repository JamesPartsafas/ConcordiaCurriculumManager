import { Container, Text } from "@chakra-ui/react";
import Button from "../../components/Button";
import { BaseRoutes } from "../../constants";
import { useNavigate, useParams } from "react-router-dom";
import { UserContext } from "../../App";
import { useContext, useEffect, useState } from "react";
import { DossierDetailsDTO, DossierDetailsResponse } from "../../models/dossier";
import { getDossierDetails } from "../../services/dossier";

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
                    height="40px"
                    width="fit-content"
                    onClick={() => navigate(BaseRoutes.Home)}
                >
                    Return to Home
                </Button>
            </Container>
        </div>
    );
}
