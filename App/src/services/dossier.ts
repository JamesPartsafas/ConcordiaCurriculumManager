//this file is to define Dossier related types and apis.

import axios from "axios";
import {
    DossierDTO,
    DossierDTOResponse,
    DossierDetailsResponse,
    DossierReportResponse,
    DossierStateEnum,
    GetMyDossiersResponse,
} from "../models/dossier";

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
    return axios.put(`/Dossier/EditDossier/${dto.id}`, dto);
}

export function getDossierDetails(id: string): Promise<DossierDetailsResponse> {
    return axios.get(`/Dossier/${id}`);
}

export function getDossierRequiredReview(): Promise<GetMyDossiersResponse> {
    return axios.get("/Dossier/GetDossiersRequiredReview");
}

export function getDossierReport(id: string): Promise<DossierReportResponse> {
    return axios.get(`/Dossier/GetDossierReportByDossierId/${id}`);
}

export function searchDossiersByTitle(title: string): Promise<GetMyDossiersResponse> {
    return axios.get("/Dossier/SearchDossiers?title=" + title);
}

export function searchDossiersByState(state: DossierStateEnum): Promise<GetMyDossiersResponse> {
    return axios.get("/Dossier/SearchDossiers?state=" + state);
}

export function searchDossiersByGuid(guid: string): Promise<GetMyDossiersResponse> {
    return axios.get("/Dossier/SearchDossiers?groupId=" + guid);
}
