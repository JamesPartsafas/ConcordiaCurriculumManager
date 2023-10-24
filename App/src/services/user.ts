export interface User {
    firstName: string | null;
    lastName: string | null;
    email: string | null;
    roles: string[] | null;
    issuedAtTimestamp: number | null;
    expiresAtTimestamp: number;
    issuer: string | null;
    audience: string | null;
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
