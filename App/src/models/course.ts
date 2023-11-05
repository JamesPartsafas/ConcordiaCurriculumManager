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
    componentCodes: number[];
    dossierId: string;
    courseNotes: string;
    rationale: string;
    supportingFiles: object;
    resourceImplication: string;
}
