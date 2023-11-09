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
