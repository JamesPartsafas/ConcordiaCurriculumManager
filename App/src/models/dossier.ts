import { GroupDTO } from "../services/group";
import { Course, CourseCreationRequest, CourseDeletionRequest, CourseModificationRequest } from "./course";
import { CourseGroupingDTO, CourseGroupingRequestDTO } from "./courseGrouping";
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
    courseGroupingRequests: CourseGroupingRequestDTO[];
    approvalStages: ApprovalStage[];
    approvalHistories: ApprovalHistoryDTO[];
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

export interface ApprovalHistoryDTO {
    action: number;
    createdDate: Date;
    group: GroupDTO;
    groupId: string;
    orderIndex: number;
    user: UserDTO;
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
    voteCount: number;
    discussionMessageVotes: DiscussionMessageVote[];
    isDeleted: boolean;
}

export interface DiscussionMessageVote {
    discussionMessageId: string;
    userId: string;
    discussionMessageVoteValue: DiscussionMessageVoteEnum;
}

export enum DossierStateEnum {
    Created = 0,
    InReview = 1,
    Rejected = 2,
    Approved = 3,
}

export enum DiscussionMessageVoteEnum {
    Upvote = 0,
    Downvote = 1,
    NoVote = 2,
}

export interface DossierReportDTO {
    approvalStages: ApprovalStage[];
    courseCreationRequests: CourseCreationRequest[];
    courseModificationRequests: CourseModificationRequest[];
    courseDeletionRequests: CourseDeletionRequest[];
    courseGroupingRequests: CourseGroupingRequestDTO[];
    description: string | null;
    initiatiorId: string;
    oldCourses: Course[];
    state: number;
    title: string | null;
}

export interface ChangeLogDTO {
    courseCreationRequests: CourseCreationRequest[];
    courseModificationRequests: CourseModificationRequest[];
    courseDeletionRequests: CourseDeletionRequest[];
    courseGroupingRequests: CourseGroupingRequestDTO[];
    oldCourseGroupings: CourseGroupingDTO[];
    oldCourses: Course[];
}
export interface DossierReportResponse {
    data: DossierReportDTO;
}
export interface ChangeLogResponse {
    data: ChangeLogDTO;
}

export function dossierStateToString(dossier: DossierDTO | DossierDetailsDTO | null): string {
    if (!dossier) return "Created";

    if (dossier.state === DossierStateEnum.InReview) return "In Review";
    if (dossier.state === DossierStateEnum.Rejected) return "Rejected";
    if (dossier.state === DossierStateEnum.Approved) return "Approved";

    return "Created";
}
