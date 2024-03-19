//this file is to define user related types and apis
import axios from "axios";
import jwt_decode from "jwt-decode";
import { User, UserRoles } from "../models/user";

//types
export interface AuthenticationResponse {
    data: {
        accessToken: string | null;
    };
}

export interface DecodedToken {
    id: string;
    fName: string;
    lName: string;
    email: string;
    roles: string[];
    iat: number;
    exp: number;
    iss: string;
    aud: string;
    group: string[] | null;
    groupMaster: string[] | null;
}

export interface LoginDTO {
    email: string | null;
    password: string | null;
}

export interface RegisterDTO {
    firstName: string | null;
    lastName: string | null;
    email: string | null;
    password: string | null;
}

//api calls with axios function style
export function login(dto: LoginDTO): Promise<AuthenticationResponse> {
    return axios.post("/Authentication/Login", dto).then((response) => {
        //save token to local storage then return the promise
        localStorage.setItem("token", response.data.accessToken);
        return response;
    });
}

export function RegisterUser(dto: RegisterDTO): Promise<AuthenticationResponse> {
    return axios.post("/Authentication/Register", dto).then((response) => {
        localStorage.setItem("token", response.data.accessToken);
        return response;
    });
}

export function logout(): Promise<void> {
    return axios.post("/Authentication/Logout").then(() => {
        //remove token from local storage
        localStorage.removeItem("token");
    });
}

export function decodeTokenToUser(accessToken: string) {
    const decodedToken = jwt_decode<DecodedToken>(accessToken);
    const user: User = {
        id: decodedToken.id,
        firstName: decodedToken.fName,
        lastName: decodedToken.lName,
        email: decodedToken.email,
        roles: decodedToken.roles,
        issuedAtTimestamp: decodedToken.iat,
        expiresAtTimestamp: decodedToken.exp,
        issuer: decodedToken.iss,
        audience: decodedToken.aud,
        groups: decodedToken.group,
        masteredGroups: decodedToken.groupMaster,
    };

    return user;
}

export function isAdmin(user: User): boolean {
    return user?.roles.includes(UserRoles.Admin);
}

export function isAdminOrGroupMaster(user: User): boolean {
    return isAdmin(user) || user.masteredGroups != null;
}

export function editProfile(dto: RegisterDTO): Promise<AuthenticationResponse> {
    return axios.post("/Authentication/EditProfileInfo", dto).then((response) => {
        localStorage.setItem("token", response.data.accessToken);
        return response;
    });
}
