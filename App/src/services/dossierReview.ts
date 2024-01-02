import axios from "axios";

export enum Groups{
    DepartmentCurriculumCommittee = "cc11d4d5-20f8-45c2-abee-8aa5271ea014",
    DepartmentConcul = "d82cd640-0cec-4bc3-8421-86adca26ff01",
    FacultyUndergraduateCommittee = "5b8b7e96-67ae-406f-a953-01c3f8ffd014",
    FacultyCouncil = "b5e7845a-3f4d-45a4-a99a-34b0bdf7c963",
    APCCommittee = "79fdbfac-6560-48eb-98e3-3c653a687985",
    Senate = "8e2f3cd8-9876-4512-9dc7-96f16ff5f5c0"
}

export interface dossierReviewDTO{
    dossierID: string;
    groupIDs: string[];

}


export function submitDossierForReview(dossierID: string, dto: dossierReviewDTO): Promise<void> {
    return axios.post("/DossierReview/SubmitDossierForReview/" + dossierID, dto);
}