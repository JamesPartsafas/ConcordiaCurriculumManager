import axios from "axios";
import { AllCourseSettings, Course, CourseDataResponse } from "../models/course";

interface GetAllCourseSettingsResponse {
    data: AllCourseSettings;
}

interface CourseDeletionRequestDTO {
    dossierId: string;
    rationale: string;
    resourceImplication: string;
    comment: string;
    subject: string;
    catalog: string;
}

interface EditCourseDeletionRequestDTO {
    dossierId: string;
    rationale: string;
    resourceImplication: string;
    comment: string;
    id: string;
}

const CourseAPIEndpoints = {
    GetAllCourseSettings: "/Course/GetAllCourseSettings",
    AddCourse: "/Course/InitiateCourseCreation",
    ModifyCourse: "Course/InitiateCourseModification",
    DeleteCourse: "/Course/InitiateCourseDeletion",
    DeleteCourseCreationRequest: "/Course/DeleteCourseCreationRequest",
    DeleteCourseModificationRequest: "/Course/DeleteCourseModificationRequest",
    DeleteCourseDeletionRequest: "/Course/DeleteCourseDeletionRequest",
    GetCourseDeletionRequest: "/Course/GetCourseDeletionRequest",
    GetCourseCreationRequest: "/Course/GetCourseCreationRequest",
    GetCourseModificationRequest: "/Course/GetCourseModificationRequest",
    EditCourseDeletionRequest: "/Course/EditCourseDeletionRequest",
    EditCourseCreationRequest: "/Course/EditCourseCreationRequest",
    EditCourseModificationRequest: "/Course/EditCourseModificationRequest",
    GetCourseData: "/Course/GetCourseData",
};

export function getAllCourseSettings(): Promise<GetAllCourseSettingsResponse> {
    return axios.get(CourseAPIEndpoints.GetAllCourseSettings);
}

export function addCourse(course: Course): Promise<unknown> {
    return axios.post(CourseAPIEndpoints.AddCourse, course);
}

export function modifyCourse(dossierId: string, course: Course): Promise<unknown> {
    return axios.post(`${CourseAPIEndpoints.ModifyCourse}/${dossierId}`, course);
}

export function deleteCourse(dossierId: string, courseDeletionRequest: CourseDeletionRequestDTO): Promise<unknown> {
    return axios.post(`${CourseAPIEndpoints.DeleteCourse}/${dossierId}`, courseDeletionRequest);
}

export function deleteCourseCreationRequest(dossierId: string, courseRequestId: string): Promise<unknown> {
    return axios.delete(`${CourseAPIEndpoints.DeleteCourseCreationRequest}/${dossierId}/${courseRequestId}`);
}

export function deleteCourseModificationRequest(dossierId: string, courseRequestId: string): Promise<unknown> {
    return axios.delete(`${CourseAPIEndpoints.DeleteCourseModificationRequest}/${dossierId}/${courseRequestId}`);
}

export function deleteCourseDeletionRequest(dossierId: string, courseRequestId: string): Promise<unknown> {
    return axios.delete(`${CourseAPIEndpoints.DeleteCourseDeletionRequest}/${dossierId}/${courseRequestId}`);
}

export function getCourseDeletionRequest(courseRequestId: string): Promise<unknown> {
    return axios.get(`${CourseAPIEndpoints.GetCourseDeletionRequest}/${courseRequestId}`);
}

export function getCourseCreationRequest(courseRequestId: string): Promise<unknown> {
    return axios.get(`${CourseAPIEndpoints.GetCourseCreationRequest}/${courseRequestId}`);
}

export function getCourseModificationRequest(courseRequestId: string): Promise<unknown> {
    return axios.get(`${CourseAPIEndpoints.GetCourseModificationRequest}/${courseRequestId}`);
}

export function editCourseDeletionRequest(
    dossierId: string,
    courseDeletionRequest: EditCourseDeletionRequestDTO
): Promise<unknown> {
    return axios.put(`${CourseAPIEndpoints.EditCourseDeletionRequest}/${dossierId}`, courseDeletionRequest);
}

export function getCourseData(subject: string, catalog: number): Promise<CourseDataResponse> {
    return axios.get(`${CourseAPIEndpoints.GetCourseData}/${subject}/${catalog}`);
}

export function editCourseCreationRequest(dossierId: string, course: Course) {
    return axios.put(`${CourseAPIEndpoints.EditCourseCreationRequest}/${dossierId}`, course);
}

export function editCourseModificationRequest(dossierId: string, course: Course) {
    return axios.put(`${CourseAPIEndpoints.EditCourseModificationRequest}/${dossierId}`, course);
}
