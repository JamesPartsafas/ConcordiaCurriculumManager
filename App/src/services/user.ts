export interface User {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    roles: string[];
    issuedAtTimestamp: number;
    expiresAtTimestamp: number;
    issuer: string;
    audience: string;
}

export interface UserDTO {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
}

export enum UserRoles {
    Initiator = "Initiator",
    Admin = "Admin",
    FacultyMember = "FacultyMember",
}
