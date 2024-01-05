import { GroupDTO } from "../services/group";
import { CourseCreationRequest, CourseDeletionRequest, CourseModificationRequest } from "./course";

export interface DossierDTO {
    id: string;
    initiatorId: string;
    title: string | null;
    description: string | null;
    state: DossierStateEnum;
}

export interface DossierDetailsDTO {
    id: string;
    initiatorId: string;
    title: string | null;
    description: string | null;
    state: DossierStateEnum;
    createdDate: Date;
    modifiedDate: Date;
    courseCreationRequests: CourseCreationRequest[];
    courseModificationRequests: CourseModificationRequest[];
    courseDeletionRequests: CourseDeletionRequest[];
    approvalStages: ApprovalStage[];
    discussion: DossierDiscussion;
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

export interface ApprovalStage {
    groupId: string;
    group: GroupDTO;
    stageIndex: number;
    isCurrentStage: boolean;
    isFinalStage: boolean;
}

export interface DossierDiscussion {
    dossierId: string;
    messages: DossierDiscussionMessage[];
}

export interface DossierDiscussionMessage {
    id: string;
    message: string;
    groupId: string;
    parentDiscussionMessageId: string;
    createdDate: Date;
    modifiedDate: Date;
}

export enum DossierStateEnum {
    Created = 0,
    InReview = 1,
    Rejected = 2,
    Approved = 3,
}

export function dossierStateToString(dossier: DossierDTO | DossierDetailsDTO | null): string {
    if (!dossier) return "Created";

    if (dossier.state === DossierStateEnum.InReview) return "In Review";
    if (dossier.state === DossierStateEnum.Rejected) return "Rejected";
    if (dossier.state === DossierStateEnum.Approved) return "Approved";

    return "Created";
}
