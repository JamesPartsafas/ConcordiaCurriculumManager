export interface AllCourseSettings {
    courseCareers: CourseCareer[];
    courseComponents: CourseComponent[];
    courseSubjects: unknown[];
}

export interface CourseCareer {
    careerCode: number;
    careerName: string;
}

export interface CourseComponent {
    componentCode: number;
    componentName: string;
}

export interface CourseComponents {
    componentCode: number;
    componentName: string;
    hours?: number;
}
export interface CourseCareer {
    careerCode: number;
    careerName: string;
}

export interface Course {
    subject: string;
    catalog: string;
    title: string;
    description: string;
    creditValue: string;
    preReqs: string;
    career: number;
    equivalentCourses: string;
    componentCodes: object;
    dossierId: string;
    courseNotes: string;
    rationale: string;
    supportingFiles: object;
    resourceImplication: string;
    courseID?: number;
}

export interface newCourse extends Course {
    id: string;
    createdDate: Date;
    modifiedDate: Date;
    version: number;
    published: boolean;
    courseState: number;
}

export interface ModifiedCourse extends Course {
    id: string;
    createdDate: Date;
    modifiedDate: Date;
    version: number;
    published: boolean;
    courseState: number;
}

export interface DeletedCourse extends Course {
    id: string;
    createdDate: Date;
    modifiedDate: Date;
    version: number;
    published: boolean;
    courseState: number;
}

export interface CourseRequest {
    id: string;
    dossierId: string;
    rationale: string;
    resourceImplication: string;
    comment: string;
    createdDate: Date;
    modifiedDate: Date;
}

export interface CourseCreationRequest extends CourseRequest {
    newCourse: newCourse;
    newCourseId: string;
}

export interface CourseModificationRequest extends CourseRequest {
    course: ModifiedCourse;
    courseId: string;
}

export interface CourseDeletionRequest extends CourseRequest {
    course: DeletedCourse;
    courseId: string;
}

export const componentMappings = {
    Conference: "CON",
    "Field Studies": "FLD",
    Fieldwork: "FWK",
    "Independent Study": "IND",
    Laboratory: "LAB",
    Lecture: "LEC",
    Modular: "MOD",
    Online: "ONL",
    "Practicum/Internship/Work-Term": "PRA",
    "Private Studies": "PST",
    Reading: "REA",
    Regular: "REG",
    Research: "RSC",
    Seminar: "SEM",
    Studio: "STU",
    "Thesis Research": "THE",
    Tutorial: "TUT",
    "Tutorial/Lab": "TL",
    Workshop: "WKS",
};
