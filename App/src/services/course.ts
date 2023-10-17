import axios from "axios";
import { AllCourseSettings } from "../models/course";

interface GetAllCourseSettingsResponse {
    data: AllCourseSettings;
}

const CourseAPIEndpoints = {
    GetAllCourseSettings: "/Course/GetAllCourseSettings",
    GetCourseDetails: "/Course/GetCourseDetails",
};

export function getAllCourseSettings(): Promise<GetAllCourseSettingsResponse> {
    return axios.get(CourseAPIEndpoints.GetAllCourseSettings);
}
