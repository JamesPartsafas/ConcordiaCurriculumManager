import globalPgq from 'pg-promise';
import { DataType } from './DataType';

type CourseGroupingReference = {
    Id: string
    ChildGroupCommonIdentifier: string
    GroupingType: number
}

type CourseGroupingCourseIdentifier = {
    CourseGroupingId: string
    CourseIdentifiersId: string
}

export default class CourseGrouping implements DataType {
  Id: string;
  CommonIdentifier: string;
  Name: string;
  RequiredCredits: string;
  IsTopLevel: boolean;
  School: number;
  Description: string;
  Notes: string;
  State: number;
  Version: number;
  Published: boolean;
  CourseGroupingReferences: CourseGroupingReference[];
  CourseGroupingCourseIdentifier: CourseGroupingCourseIdentifier[];
  CreatedDate: Date;
  ModifiedDate: Date;

  constructor(obj: any) {
    this.validateParameter(obj);
    this.Id = obj.Id;
    this.CommonIdentifier = obj.CommonIdentifier;
    this.Name = obj.Name;
    this.RequiredCredits = obj.RequiredCredits;
    this.IsTopLevel = obj.IsTopLevel;
    this.School = obj.School;
    this.Description = obj.Description;
    this.Notes = obj.Notes;
    this.State = obj.State;
    this.Version = obj.Version;
    this.Published = obj.Published;
    this.CourseGroupingReferences = obj.CourseGroupingReferences;
    this.CourseGroupingCourseIdentifier = obj.CourseGroupingCourseIdentifier;
    this.CreatedDate = obj.CreatedDate;
    this.ModifiedDate = obj.ModifiedDate;
  }

  CreateQuery(task: globalPgq.ITask<{}>): Promise<null>[] {
    const insertIntoCourseGrouping = task.none('INSERT INTO "CourseGroupings" ("Id", "CommonIdentifier", "Name", "RequiredCredits", "IsTopLevel", "School", "Description", "Notes", "State", "Version", "Published", "CreatedDate", "ModifiedDate") VALUES($1, $2, $3, $4, $5, $6, $7, $8, $9, $10, $11, $12, $13) ON CONFLICT ("Id") DO NOTHING', [this.Id, this.CommonIdentifier, this.Name, this.RequiredCredits, this.IsTopLevel, this.School, this.Description, this.Notes, this.State, this.Version, this.Published, this.CreatedDate.toISOString(), this.ModifiedDate.toISOString()]);
    const insertIntoCourseGroupingReferences = this.CourseGroupingReferences.map(c => task.none('INSERT INTO "CourseGroupingReferences" ("Id", "ParentGroupId", "ChildGroupCommonIdentifier", "CreatedDate", "ModifiedDate", "GroupingType") VALUES($1, $2, $3, $4, $5, $6) ON CONFLICT ("Id") DO NOTHING', [c.Id, this.Id, c.ChildGroupCommonIdentifier, this.CreatedDate.toISOString(), this.ModifiedDate.toISOString(), c.GroupingType]))
    const insertIntoCourseGroupingCourseIdentifier = this.CourseGroupingCourseIdentifier.map(c => task.none('INSERT INTO "CourseGroupingCourseIdentifier" ("CourseGroupingId", "CourseIdentifiersId") VALUES($1, $2)', [c.CourseGroupingId, c.CourseIdentifiersId]))

    return [insertIntoCourseGrouping, ...insertIntoCourseGroupingReferences, ...insertIntoCourseGroupingCourseIdentifier];
  }

  private validateParameter(obj: any) {
    if (obj === null || obj === undefined)
      throw new Error('Parameter is not defined.');
    if (typeof obj.Id !== 'string' || obj.Id.trim() === '')
      throw new Error(`Id must be a non-empty string. Passed value is of type ${typeof obj.Id}`);
    if (typeof obj.CommonIdentifier !== 'string' || obj.CommonIdentifier.trim() === '')
      throw new Error(`CommonIdentifier must be a non-empty string. Passed value is of type ${typeof obj.CommonIdentifier}`);
    if (typeof obj.Name !== 'string' || obj.Name.trim() === '')
      throw new Error(`Name must be a non-empty string. Passed value is of type ${typeof obj.Name}`);
    if (typeof obj.RequiredCredits !== 'string' || obj.RequiredCredits.trim() === '')
      throw new Error(`RequiredCredits must be a non-empty string. Passed value is of type ${typeof obj.RequiredCredits}`);
    if (typeof obj.IsTopLevel !== 'boolean')
      throw new Error(`IsTopLevel must be a boolean. Passed value is of type ${typeof obj.IsTopLevel}`);
    if (typeof obj.School !== 'number')
      throw new Error(`School must be a number. Passed value is of type ${typeof obj.School}`);
    if (typeof obj.Description !== 'string')
      throw new Error(`Description must be a string. Passed value is of type ${typeof obj.Description}`);
    if (typeof obj.Notes !== 'string')
      throw new Error(`Notes must be a string. Passed value is of type ${typeof obj.Notes}`);
    if (typeof obj.State !== 'number' || isNaN(obj.State))
      throw new Error(`State must be a number. Passed value is of type ${typeof obj.State}`);
    if (typeof obj.Version !== 'number' || isNaN(obj.Version))
      throw new Error(`Version must be a number. Passed value is of type ${typeof obj.Version}`);
    if (typeof obj.Published !== 'boolean')
      throw new Error(`Published must be a boolean. Passed value is of type ${typeof obj.Published}`);
    if (!Array.isArray(obj.CourseGroupingReferences) || obj.CourseGroupingReferences.some((c: any) => (typeof c.Id !== 'string' || c.Id.trim() === '')))
      throw new Error(`CourseGroupingReferences must be an array of CourseGroupingReference. Passed value is of type ${typeof obj.CourseGroupingReferences}`);
    if (!Array.isArray(obj.CourseGroupingCourseIdentifier) || obj.CourseGroupingCourseIdentifier.some((c: any) => (typeof c.CourseGroupingId !== 'string' || c.CourseGroupingId.trim() === '')))
      throw new Error(`CourseGroupingCourseIdentifier must be an array of CourseGroupingCourseIdentifier. Passed value is of type ${typeof obj.CourseGroupingCourseIdentifier}`);
    if (!(obj.CreatedDate instanceof Date))
      throw new Error(`CreatedDate must be a date. Passed value is of type ${typeof obj.CreatedDate}`);
    if (!(obj.ModifiedDate instanceof Date))
      throw new Error(`ModifiedDate must be a date. Passed value is of type ${typeof obj.ModifiedDate}`);
  }
}