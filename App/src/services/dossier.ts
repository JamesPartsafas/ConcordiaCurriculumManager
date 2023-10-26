//this file is to define Dossier related types and apis.

import axios from "axios";
import { DossierDTO, DossierDTOResponse, GetMyDossiersResponse } from "../models/dossier";

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

export function deleteDossierById(id: string): Promise<void> {
    return axios.delete(`/Dossier/DeleteDossier/${id}`);
}

export function editDossier(dto: DossierDTO) {
    return axios.put("/Dossier/EditDossier", dto);
}
