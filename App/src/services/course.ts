import axios from "axios";
import { AllCourseSettings, Course } from "../models/course";

interface GetAllCourseSettingsResponse {
    data: AllCourseSettings;
}

interface CourseDeletionRequestDTO {
    dossierId: string;
    rationale: string;
    resourceImplication: string;
    subject: string;
    catalog: string;
}

const CourseAPIEndpoints = {
    GetAllCourseSettings: "/Course/GetAllCourseSettings",
    AddCourse: "/Course/InitiateCourseCreation",
    DeleteCourse: "/Course/InitiateCourseDeletion",
    DeleteCourseCreationRequest: "/Course/DeleteCourseCreationRequest",
    DeleteCourseModificationRequest: "/Course/DeleteCourseModificationRequest",
    DeleteCourseDeletionRequest: "/Course/DeleteCourseDeletionRequest",
};

export function getAllCourseSettings(): Promise<GetAllCourseSettingsResponse> {
    return axios.get(CourseAPIEndpoints.GetAllCourseSettings);
}

export function addCourse(course: Course): Promise<unknown> {
    return axios.post(CourseAPIEndpoints.AddCourse, course);
}

export function deleteCourse(courseDeletionRequest: CourseDeletionRequestDTO): Promise<unknown> {
    return axios.post(CourseAPIEndpoints.DeleteCourse, courseDeletionRequest);
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
