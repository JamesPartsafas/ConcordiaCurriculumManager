import axios from "axios";
import { CourseGroupingDTO, CourseGroupingModificationRequestDTO, SchoolEnum } from "../models/courseGrouping";

interface GetCourseGroupingResponse {
    data: CourseGroupingDTO;
}

interface GetMultiCourseGroupingResponse {
    data: CourseGroupingDTO[];
}

const CourseGroupingAPIEndpoints = {
    GetCourseGrouping: "/CourseGrouping/GetCourseGrouping",
    GetGroupingBySchool: "/CourseGrouping/GetCourseGroupingsBySchoolNonRecursive",
    InitiateCourseGroupingDeletion: "/CourseGrouping/InitiateCourseGroupingDeletion",
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

export function InitiateCourseGroupingDeletion(
    dossierId: string,
    courseGroupingDeletionRequest: CourseGroupingModificationRequestDTO
): Promise<unknown> {
    return axios.post(
        `${CourseGroupingAPIEndpoints.InitiateCourseGroupingDeletion}/${dossierId}`,
        courseGroupingDeletionRequest
    );
}
