//this file is to define Dossier related types and apis.

import axios from "axios";

export interface DossierDTO {
    id: string;
    initiatorId: string;
    title: string | null;
    description: string | null;
    published: boolean;
}

export interface DossierDTOResponse {
    data: DossierDTO;
}

export interface GetMyDossiersResponse {
    data: DossierDTO[];
}

export interface CreateDossierDTO {
    title: string | null;
    description: string | null;
}

//api calls with axios function style
export function getMyDossiers(): Promise<GetMyDossiersResponse> {
    return axios.get("/Dossier/GetDossiersByID");
}

export function createDossierForUser(dto: CreateDossierDTO): Promise<DossierDTOResponse> {
    return axios.post("/Dossier/CreateDossierForUser", dto);
}

export function deleteDossier(id: string): Promise<void> {
    return axios.delete(`/Dossier/DeleteDossier/${id}`);
}
