export interface User {
    firstName: string | null;
    lastName: string | null;
    email: string | null;
    roles: string[] | null;
    issuedAtTimestamp: number | null;
    expiresAtTimestamp: number | null;
    issuer: string | null;
    audience: string | null;
}

export enum UserRoles {
    Initiator = "Initiator",
    Admin = "Admin",
    FacultyMember = "FacultyMember",
}
