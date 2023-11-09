import globalPgq from 'pg-promise';
import { DataType } from './DataType';
import { CourseCourseComponent } from './CourseCourseComponent';

export default class Course implements DataType {
  Id: string;
  CourseID: string;
  Subject: string;
  Catalog: string;
  Title: string;
  Description: string;
  CreditValue: string;
  PreReqs: string;
  Career: number;
  EquivalentCourses: string;
  CourseState: number;
  Version: number;
  Published: boolean;
  CourseCourseComponents: CourseCourseComponent[];
  CreatedDate: Date;
  ModifiedDate: Date;

  constructor(obj: any) {
    this.validateParameter(obj);
    this.Id = obj.Id;
    this.CourseID = obj.CourseID;
    this.Subject = obj.Subject;
    this.Catalog = obj.Catalog;
    this.Title = obj.Title;
    this.Description = obj.Description;
    this.CreditValue = obj.CreditValue;
    this.PreReqs = obj.PreReqs;
    this.Career = obj.Career;
    this.EquivalentCourses = obj.EquivalentCourses;
    this.CourseState = obj.CourseState;
    this.Version = obj.Version;
    this.Published = obj.Published;
    this.CourseCourseComponents = obj.CourseCourseComponents;
    this.CreatedDate = obj.CreatedDate;
    this.ModifiedDate = obj.ModifiedDate;
  }

  CreateQuery(task: globalPgq.ITask<{}>): Promise<null>[] {
    const insertIntoCourse = task.none('INSERT INTO "Courses" ("Id", "CourseID", "Subject", "Catalog", "Title", "Description", "CreditValue", "PreReqs", "Career", "EquivalentCourses", "CourseState", "Version", "Published", "CreatedDate", "ModifiedDate") VALUES($1, $2, $3, $4, $5, $6, $7, $8, $9, $10, $11, $12, $13, $14, $15) ON CONFLICT ("Id") DO NOTHING', [this.Id, this.CourseID, this.Subject, this.Catalog, this.Title, this.Description, this.CreditValue, this.PreReqs, this.Career, this.EquivalentCourses, this.CourseState, this.Version, this.Published, this.CreatedDate.toISOString(), this.ModifiedDate.toISOString()]);
    const insertIntoCourseCourseComponents = this.CourseCourseComponents.map(c => task.none('INSERT INTO "CourseCourseComponents" ("Id", "CourseId", "ComponentCode", "CreatedDate", "ModifiedDate") VALUES($1, $2, $3, $4, $5) ON CONFLICT("Id") DO NOTHING', [c.Id, this.Id, c.ComponentCode, this.CreatedDate.toISOString(), this.ModifiedDate.toISOString()]));
    return [insertIntoCourse, ...insertIntoCourseCourseComponents];
  }

  private validateParameter(obj: any) {
    if (obj === null || obj === undefined)
      throw new Error('Parameter is not defined.');
    if (typeof obj.Id !== 'string' || obj.Id.trim() === '')
      throw new Error(`Id must be a non-empty string. Passed value is of type ${typeof obj.Id}`);
    if (typeof obj.CourseID !== 'string' || obj.CourseID.trim() === '')
      throw new Error(`CourseID must be a non-empty string. Passed value is of type ${typeof obj.CourseID}`);
    if (typeof obj.Subject !== 'string' || obj.Subject.trim() === '')
      throw new Error(`Subject must be a non-empty string. Passed value is of type ${typeof obj.Subject}`);
    if (typeof obj.Catalog !== 'string' || obj.Catalog.trim() === '')
      throw new Error(`Catalog must be a non-empty string. Passed value is of type ${typeof obj.Catalog}`);
    if (typeof obj.Title !== 'string' || obj.Title.trim() === '')
      throw new Error(`Title must be a non-empty string. Passed value is of type ${typeof obj.Title}`);
    if (typeof obj.Description !== 'string')
      throw new Error(`Description must be a string. Passed value is of type ${typeof obj.Description}`);
    if (typeof obj.CreditValue !== 'string' || obj.CreditValue.trim() === '')
      throw new Error(`CreditValue must be a non-empty string. Passed value is of type ${typeof obj.CreditValue}`);
    if (typeof obj.PreReqs !== 'string')
      throw new Error(`PreReqs must be a string. Passed value is of type ${typeof obj.PreReqs}`);
    if (typeof obj.Career !== 'number' || isNaN(obj.Career))
      throw new Error(`Career must be a number. Passed value is of type ${typeof obj.Career}`);
    if (typeof obj.EquivalentCourses !== 'string')
      throw new Error(`EquivalentCourses must be a string. Passed value is of type ${typeof obj.EquivalentCourses}`);
    if (typeof obj.CourseState !== 'number' || isNaN(obj.CourseState))
      throw new Error(`CourseState must be a number. Passed value is of type ${typeof obj.CourseState}`);
    if (typeof obj.Version !== 'number' || isNaN(obj.Version))
      throw new Error(`Version must be a number. Passed value is of type ${typeof obj.Version}`);
    if (typeof obj.Published !== 'boolean')
      throw new Error(`Published must be a boolean. Passed value is of type ${typeof obj.Published}`);
    if (!Array.isArray(obj.CourseCourseComponents) || obj.CourseCourseComponents.some((c: any) => (typeof c.Id !== 'string' || c.Id.trim() === '')))
      throw new Error(`CourseCourseComponents must be an array of CourseCourseComponent. Passed value is of type ${typeof obj.CourseCourseComponent}`);
    if (!(obj.CreatedDate instanceof Date))
      throw new Error(`CreatedDate must be a date. Passed value is of type ${typeof obj.CreatedDate}`);
    if (!(obj.ModifiedDate instanceof Date))
      throw new Error(`ModifiedDate must be a date. Passed value is of type ${typeof obj.ModifiedDate}`);
  }
}