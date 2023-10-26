import axios from "axios";
import { AllCourseSettings, Course } from "../models/course";

interface GetAllCourseSettingsResponse {
    data: AllCourseSettings;
}

const CourseAPIEndpoints = {
    GetAllCourseSettings: "/Course/GetAllCourseSettings",
    AddCourse: "/Course/InitiateCourseCreation",
};

export function getAllCourseSettings(): Promise<GetAllCourseSettingsResponse> {
    return axios.get(CourseAPIEndpoints.GetAllCourseSettings);
}

export function addCourse(course: Course): Promise<unknown> {
    return axios.post(CourseAPIEndpoints.AddCourse, course);
}
