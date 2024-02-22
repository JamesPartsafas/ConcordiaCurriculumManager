import axios from "axios";
import {
    CourseGroupingCreationRequestDTO,
    CourseGroupingDTO,
    CourseGroupingModificationRequestDTO,
    CourseGroupingRequestDTO,
    SchoolEnum,
} from "../models/courseGrouping";

interface GetCourseGroupingResponse {
    data: CourseGroupingDTO;
}

interface GetMultiCourseGroupingResponse {
    data: CourseGroupingDTO[];
}

interface CourseGroupingCreationResponse {
    data: CourseGroupingRequestDTO;
}

const CourseGroupingAPIEndpoints = {
    GetCourseGrouping: "/CourseGrouping/GetCourseGrouping",
    GetGroupingBySchool: "/CourseGrouping/GetCourseGroupingsBySchoolNonRecursive",
    InitiateCourseGroupingCreation: "/CourseGrouping/InitiateCourseGroupingCreation",
    InitiateCourseGroupingModification: "/CourseGrouping/InitiateCourseGroupingModification",
    InitiateCourseGroupingDeletion: "/CourseGrouping/InitiateCourseGroupingDeletion",
    EditCourseGroupingCreation: "/CourseGrouping/EditCourseGroupingCreation",
    EditCourseGroupingModification: "/CourseGrouping/EditCourseGroupingModification",
    EditCourseGroupingDeletion: "/CourseGrouping/EditCourseGroupingDeletion",
    DeleteCourseGroupingRequest: "/CourseGrouping/DeleteCourseGroupingRequest",
};

export function GetCourseGrouping(courseGroupingId: string): Promise<GetCourseGroupingResponse> {
    return axios.get(`${CourseGroupingAPIEndpoints.GetCourseGrouping}/${courseGroupingId}`);
}

export function GetCourseGroupingBySchool(school: SchoolEnum): Promise<GetMultiCourseGroupingResponse> {
    return axios.get("/CourseGrouping/GetCourseGroupingsBySchool/" + school);
}

export function GetCourseGroupingByName(name: string): Promise<GetMultiCourseGroupingResponse> {
    return axios.get("/CourseGrouping/SearchCourseGroupingsByName?name=" + name);
}

export function InitiateCourseGroupingCreation(dossierId: string, courseGroupingCreationRequest: CourseGroupingCreationRequestDTO): Promise<CourseGroupingCreationResponse> {
    return axios.post(`${CourseGroupingAPIEndpoints.InitiateCourseGroupingCreation}/${dossierId}`, courseGroupingCreationRequest);
}

export function InitiateCourseGroupingModification(
    dossierId: string,
    courseGroupingModificationRequest: CourseGroupingModificationRequestDTO
): Promise<CourseGroupingCreationResponse> {
    return axios.post(
        `${CourseGroupingAPIEndpoints.InitiateCourseGroupingModification}/${dossierId}`,
        courseGroupingModificationRequest
    );
}

export function InitiateCourseGroupingDeletion(
    dossierId: string,
    courseGroupingDeletionRequest: CourseGroupingModificationRequestDTO
): Promise<unknown> {
    return axios.post(
        `${CourseGroupingAPIEndpoints.InitiateCourseGroupingDeletion}/${dossierId}`,
        courseGroupingDeletionRequest
    );
}

export function EditCourseGroupingCreation(
    dossierId: string,
    requestId: string,
    courseGroupingCreationRequest: CourseGroupingCreationRequestDTO
): Promise<CourseGroupingCreationResponse> {
    return axios.put(
        `${CourseGroupingAPIEndpoints.EditCourseGroupingCreation}/${dossierId}/${requestId}`,
        courseGroupingCreationRequest
    );
}

export function EditCourseGroupingModification(
    dossierId: string,
    requestId: string,
    courseGroupingModificationRequest: CourseGroupingModificationRequestDTO
): Promise<CourseGroupingCreationResponse> {
    return axios.put(
        `${CourseGroupingAPIEndpoints.EditCourseGroupingModification}/${dossierId}/${requestId}`,
        courseGroupingModificationRequest
    );
}

export function EditCourseGroupingDeletion(
    dossierId: string,
    requestId: string,
    courseGroupingDeletionRequest: CourseGroupingModificationRequestDTO
): Promise<unknown> {
    return axios.put(
        `${CourseGroupingAPIEndpoints.EditCourseGroupingDeletion}/${dossierId}/${requestId}`,
        courseGroupingDeletionRequest
    );
}

export function DeleteCourseGroupingRequest(dossierId: string, requestId: string): Promise<unknown> {
    return axios.delete(`${CourseGroupingAPIEndpoints.DeleteCourseGroupingRequest}/${dossierId}/${requestId}`);
}
