//This file is used to define metrics related types and apis
import axios from "axios";
import { DossierDTO } from "../models/dossier";
import { UserDTO } from "../models/user";

//types
export interface DossierViewCountDTO
{
    guid: string;
    dossier: DossierDTO;
    count: number; 
}

export interface DossierViewResponseDTO
{
    result: DossierViewCountDTO[];
    nextIndex: number;
}

export interface HttpEndpointCountDTO
{
    controller: string;
    endpoint: string;
    fullPath: string;
    count: number;
}

export interface HttpEndpointResponseDTO
{
    result: HttpEndpointCountDTO[];
    nextIndex: number;
}

export interface HttpStatusCountDTO{
    count: number;
    httpStatus: number;
}

export interface HttpStatusResponseDTO
{
    result: HttpStatusCountDTO[];
    nextIndex: number;
}

export interface UserDossierViewedCountDTO
{
    guid: string;
    user: UserDTO;
    count: number; 
}

export interface UserDossierViewedResponseDTO
{
    result: UserDossierViewedCountDTO[];
    nextIndex: number
}

//api calls
 export function GetTopHttpStatusCodes(fromIndex: number)//: Promise<HttpStatusResponseDTO>
{
    return axios.get("/Metrics/GetTopHttpStatusCodes?fromIndex="+fromIndex +"&startDate=");
}

export function GetTopHitHttpEndpoints(fromIndex: number)/*: Promise<HttpEndpointResponseDTO>*/
{
    console.log(fromIndex)
    return axios.get("/Metrics/GetTopHitHttpEndpoints?fromIndex="+fromIndex);
}

export function GetTopViewedDossiers(fromIndex: number)//: Promise<DossierViewResponseDTO>
{
    return axios.get("/Metrics/GetTopViewedDossier?fromIndex="+fromIndex);
}

export function GetTopDossierViewingUser(fromIndex: number)//: Promise<UserDossierViewedResponseDTO>
{
    return axios.get("/Metrics/GetTopDossierViewingUser?fromIndex="+fromIndex);
}
