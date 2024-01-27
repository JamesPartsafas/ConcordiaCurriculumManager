import axios from "axios";
import { CourseGroupingDTO } from "../models/courseGrouping";

interface GetCourseGroupingResponse {
    data: CourseGroupingDTO;
}

const CourseGroupingAPIEndpoints = {
    GetCourseGrouping: "/CourseGrouping/GetCourseGrouping",
};

export function GetCourseGrouping(courseGroupingId: string): Promise<GetCourseGroupingResponse> {
    return axios.get(`${CourseGroupingAPIEndpoints.GetCourseGrouping}/${courseGroupingId}`);
}
