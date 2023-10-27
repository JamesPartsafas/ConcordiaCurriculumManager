import { useEffect, useState } from "react";
import { DossierDetailsDTO, DossierDetailsResponse } from "../../models/dossier";
import { getDossierDetails } from "../../services/dossier";
import { useParams } from "react-router-dom";

export default function DossierDetails() {
    const { dossierId } = useParams();
    const [dossierDetails, setDossierDetails] = useState<DossierDetailsDTO | null>(null);

    useEffect(() => {
        requestDossierDetails(dossierId);
    }, [dossierId]);

    function requestDossierDetails(dossierId: string) {
        getDossierDetails(dossierId).then((res: DossierDetailsResponse) => {
            setDossierDetails(res.data);
        });
    }

    return (
        <div>
            <p>{dossierDetails ? JSON.stringify(dossierDetails) : ""}</p>
        </div>
    );
}
