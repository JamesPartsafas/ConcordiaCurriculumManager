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

export interface MultiGroupResponseDTO {
    data: GroupDTO[];
}

//api calls
export function CreateGroupCall(dto: GroupCreateDTO): Promise<GroupResponseDTO> {
    return axios.post("/Groups/CreateGroup", dto);
}

export function GetGroupByID(id: string): Promise<GroupResponseDTO> {
    return axios.get("/Groups/GetGroupById/" + id);
}

export function AddUserToGroup(gid: string, uid: string){
    return axios.post("/Groups/" + gid + "/users/" + uid);
}

export function RemoveUserFromGroup(gid: string, uid: string){
    return axios.post("/Groups/"+ gid + "/users/" + uid);
}

export function GetAllGroups(): Promise<MultiGroupResponseDTO>{
    return axios.get("/Groups/GetAllGroups")
     
}