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
    roles: { userRole: UserRoleCodes }[];
}

export enum UserRoles {
    Initiator = "Initiator",
    Admin = "Admin",
    FacultyMember = "FacultyMember",
}

export enum UserRoleCodes {
    Initiator = 0,
    Admin = 1,
    FacultyMember = 2,
}
