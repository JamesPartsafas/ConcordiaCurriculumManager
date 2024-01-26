import { GroupDTO } from "../services/group";
import { Course, CourseCreationRequest, CourseDeletionRequest, CourseModificationRequest } from "./course";
import { UserDTO } from "./user";

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

export interface ApprovalStage {
    id: string;
    createdDate: Date;
    modifiedDate: Date;
    groupId: string;
    group: GroupDTO;
    dossierId: string;
    stageIndex: number;
    isCurrentStage: boolean;
    isFinalStage: boolean;
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

export interface DossierDiscussion {
    dossierId: string;
    messages: DossierDiscussionMessage[];
}

export interface DossierDiscussionMessage {
    id: string;
    author: UserDTO;
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

export interface DossierReportDTO {
    approvalStages: ApprovalStage[];
    courseCreationRequests: CourseCreationRequest[];
    courseModificationRequests: CourseModificationRequest[];
    courseDeletionRequests: CourseDeletionRequest[];
    description: string | null;
    initiatiorId: string;
    oldCourses: Course[];
    state: number;
    title: string | null;
}

export interface DossierReportResponse {
    data: DossierReportDTO;
}

export function dossierStateToString(dossier: DossierDTO | DossierDetailsDTO | null): string {
    if (!dossier) return "Created";

    if (dossier.state === DossierStateEnum.InReview) return "In Review";
    if (dossier.state === DossierStateEnum.Rejected) return "Rejected";
    if (dossier.state === DossierStateEnum.Approved) return "Approved";

    return "Created";
}
