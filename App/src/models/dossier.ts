import { CourseCreationRequest } from "./course";

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
    courseCreationRequests: CourseCreationRequest[]; //make a course creation request model
    courseModificationRequests: string; //make a course modification request model
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
