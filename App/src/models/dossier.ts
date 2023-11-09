import { CourseCreationRequest, CourseModificationRequest } from "./course";

export interface DossierDTO {
    id: string;
    initiatorId: string;
    title: string | null;
    description: string | null;
    published: boolean;
}

export interface DossierDetailsDTO {
    id: string;
    initiatorId: string;
    title: string | null;
    description: string | null;
    published: boolean;
    createdDate: Date;
    modifiedDate: Date;
    courseCreationRequests: CourseCreationRequest[];
    courseModificationRequests: CourseModificationRequest[];
}

export interface DossierDTOResponse {
    data: DossierDTO;
}

export interface GetMyDossiersResponse {
    data: DossierDTO[];
}

export interface DossierDetailsResponse {
    data: DossierDetailsDTO;
}
