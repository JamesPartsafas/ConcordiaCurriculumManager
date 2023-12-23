//This file is used to define user related apis that don't relate to authentication
import axios from "axios";
import { UserDTO } from "../models/user";

export interface AllUsersResponseDTO {
    data: UserDTO[];
}

export function getAllUsers(): Promise<AllUsersResponseDTO> {
    return axios.get("/Users/GetAllUsersAsync");
}

export function updateAllUsers(uid: string): Promise<AllUsersResponseDTO> {
    return axios.get("/Users/GetAllUsersAsync?lastId=" + uid);
}

export function searchUsersByEmail(email: string): Promise<AllUsersResponseDTO> {
    return axios.get("/Users/SearchUsersByEmail?email=" + email);
}
