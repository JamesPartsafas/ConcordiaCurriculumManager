//this file is to define user related types and apis
import axios from "axios";
import { User } from "./user";

//types
export interface AuthenticationResponse {
    data: {
        accessToken: string | null;
    };
}

export interface LoginProps {
    setUser: (user: User | null) => void;
}

export interface DecodedToken{
    fName: string;
    lName: string;
    email: string;
    roles: string[];
    iat: number;
    exp: number;
    iss: string;
    aud: string;
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
    return axios.post("/Authentication/Login", dto);
}

export function RegisterUser(dto: RegisterDTO): Promise<AuthenticationResponse> {
    return axios.post("/Authentication/Register", dto,  { headers: { 'Content-Type': 'application/x.ccm.authentication.create+json;v=1' } });
}

export function logout(): Promise<void> {
    return axios.post("/Authentication/Logout");
}
