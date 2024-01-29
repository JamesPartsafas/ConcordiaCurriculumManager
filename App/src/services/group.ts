//This file is used to define group related types and apis
import axios from "axios";
import { UserDTO } from "../models/user";

//types
export interface GroupDTO {
    id: string;
    name: string;
    members: UserDTO[];
    groupMasters: UserDTO[];
}

export interface GroupCreateDTO {
    name: string;
}

export interface GroupResponseDTO {
    data: GroupDTO;
}

export interface MultiGroupResponseDTO {
    data: GroupDTO[];
}

//api calls
export function CreateGroupCall(dto: GroupCreateDTO): Promise<GroupResponseDTO> {
    return axios.post("/Group/CreateGroup", dto);
}

export function GetGroupByID(id: string): Promise<GroupResponseDTO> {
    return axios.get(`/Group/${id}`);
}

export function AddUserToGroup(gid: string, uid: string): Promise<void> {
    return axios.post("/Group/" + gid + "/users/" + uid);
}

export function RemoveUserFromGroup(gid: string, uid: string): Promise<void> {
    return axios.delete("/Group/" + gid + "/users/" + uid);
}

export function GetAllGroups(): Promise<MultiGroupResponseDTO> {
    return axios.get("/Group/GetAllGroups");
}

export function AddGroupMaster(gid: string, uid: string): Promise<void> {
    return axios.post("/Group/" + gid + "/masters/" + uid);
}

export function RemoveGroupMaster(gid: string, uid: string): Promise<void> {
    return axios.delete("/Group/" + gid + "/masters/" + uid);
}

export function UpdateGroup(id: string, dto: GroupCreateDTO): Promise<void> {
    return axios.put(`/Group/${id}`, dto);
}

export function DeleteGroup(id: string): Promise<void> {
    return axios.delete(`/Group/${id}`);
}
