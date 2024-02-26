import { Course } from "./course";

export enum SchoolEnum {
    GinaCody = 0,
    ArtsAndScience = 1,
    FineArts = 2,
    JMSB = 3,
}

export enum CourseGroupingStateEnum {
    Accepted = 0,
    NewCourseGroupingProposal = 1,
    CourseGroupingChangeProposal = 2,
    CourseGroupingDeletionProposal = 3,
    Deleted = 4,
}

export enum GroupingTypeEnum {
    subGrouping = 0,
    optionalGrouping = 1,
}

export interface CourseIdentifierDTO {
    concordiaCourseId: number;
}

export interface CourseGroupingReferenceDTO {
    id?: string;
    parentGroupId?: string;
    childGroupCommonIdentifier: string;
    groupingType: GroupingTypeEnum;
}

export interface CourseGroupingReferenceInputDTO {
    childGroupCommonIdentifier: string;
    groupingType: GroupingTypeEnum;
}

export interface CourseGroupingDTO {
    id: string;
    commonIdentifier: string;
    name: string;
    requiredCredits: string;
    isTopLevel: boolean;
    school: SchoolEnum;
    description: string | null;
    notes: string | null;
    state: CourseGroupingStateEnum;
    version: number | null;
    published: boolean;
    subGroupingReferences: CourseGroupingReferenceDTO[];
    subGroupings: CourseGroupingDTO[];
    courseIdentifiers: CourseIdentifierDTO[];
    courses: Course[];
    createdDate: Date;
    modifiedDate: string;
}

export interface CourseGroupingRequestDTO {
    id: string;
    dossierId: string;
    rationale: string;
    resourceImplication: string;
    comment: string;
    conflict: string;
    courseGrouping: CourseGroupingDTO;
    requestType?: number;
}

export interface CourseGroupingModificationInputDTO {
    name: string;
    requiredCredits: string;
    isTopLevel: boolean;
    school: SchoolEnum;
    description: string | null;
    notes: string | null;
    subGroupingReferences: CourseGroupingReferenceInputDTO[];
    courseIdentifiers: CourseIdentifierDTO[];
    commonIdentifier: string;
}

export interface CourseGroupingInputDTO {
    name: string | null;
    requiredCredits: string | null;
    isTopLevel: boolean;
    school: SchoolEnum;
    description: string | null;
    notes: string | null;
    subGroupingReferences: CourseGroupingReferenceInputDTO[];
    courseIdentifiers: CourseIdentifierDTO[];
}

export interface CourseGroupingCreationRequestDTO {
    dossierId: string;
    rationale: string | null;
    resourceImplication: string | null;
    comment: string | null;
    courseGrouping: CourseGroupingInputDTO;
}

export interface CourseGroupingModificationRequestDTO {
    dossierId: string;
    rationale: string | null;
    resourceImplication: string | null;
    comment: string | null;
    courseGrouping: CourseGroupingModificationInputDTO;
}

export interface MultiCourseGroupingDTO {
    data: CourseGroupingDTO[];
}
