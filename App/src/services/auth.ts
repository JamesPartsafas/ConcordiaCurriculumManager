//this file is to define user related types and apis
import axios from "axios";

//types
export interface AuthenticationResponse {
    data: {
        accessToken: string | null;
    };
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

export function register(dto: RegisterDTO): Promise<AuthenticationResponse> {
    return axios.post("/Authentication/Register", dto);
}

export function logout(): Promise<void> {
    return axios.post("/Authentication/Logout").then(() => {
        //remove token from local storage
        localStorage.removeItem("token");
    });
}
