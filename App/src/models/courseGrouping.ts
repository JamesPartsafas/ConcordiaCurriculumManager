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

export interface CourseGroupingReferenceDTO {
    id?: string;
    parentGroupId?: string;
    childGroupCommonIdentifier: string;
    groupingType: GroupingTypeEnum;
}

export interface CourseIdentifierDTO {
    concordiaCourseId: number;
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

// export interface CourseGroupingRequestDTO {
//     id: string;
//     dossierId: string;
//     rationale: string;
//     resourceImplication: string;
//     comment: string;
//     conflict: string;
//     courseGrouping: CourseGroupingDTO;
// }

export interface CourseGroupingRequestInputDTO {
    name: string;
    requiredCredits: string;
    isTopLevel: boolean;
    school: SchoolEnum;
    description: string | null;
    notes: string | null;
    subGroupingReferences: CourseGroupingReferenceDTO[];
    courseIdentifiers: CourseIdentifierDTO[];
    commonIdentifier: string;
}

export interface CourseGroupingModificationRequestDTO {
    dossierId: string;
    rationale: string;
    resourceImplication: string;
    comment: string;
    courseGrouping: CourseGroupingRequestInputDTO;
}

export interface MultiCourseGroupingDTO {
    data: CourseGroupingDTO[];
}
