//This file is used to define group related types and apis
import axios from "axios";
import { User } from "./user";

//types
export interface GroupDTO {
    id: string | null;
    name: string | null;
    members: User[] | null;
}

export interface GroupCreateDTO {
    name: string | null;
}

export interface GroupResponseDTO {
    data: GroupDTO;
}

//api calls
export function CreateGroup(dto: GroupCreateDTO): Promise<GroupResponseDTO> {
    return axios.post("/Group/CreateGroup", dto);
}

export function GetGroupByID(id: string): Promise<GroupResponseDTO> {
    return axios.get("/Group/GetGroupById/" + id);
}

export function AddUserToGroup(gid: string, uid: string){
    return axios.post("/Group/")
}