import globalPgq from 'pg-promise';
import { DataType } from './DataType';
import { CourseCourseComponent } from './CourseCourseComponent';

export default class CourseReference implements DataType {
  Id: string;
  CourseReferencingId: string;
  CourseReferencedId: string;
  State: number;
  CreatedDate: Date;
  ModifiedDate: Date;

  constructor(obj: any) {
    this.validateParameter(obj);
    this.Id = obj.Id;
    this.CourseReferencingId = obj.CourseReferencingId;
    this.CourseReferencedId = obj.CourseReferencedId;
    this.State = obj.State;
    this.CreatedDate = obj.CreatedDate;
    this.ModifiedDate = obj.ModifiedDate;
  }

  CreateQuery(task: globalPgq.ITask<{}>): Promise<null>[] {
    return [task.none('INSERT INTO "CourseReferences" ("Id", "CourseReferencingId", "CourseReferencedId", "State", "CreatedDate", "ModifiedDate") VALUES($1, $2, $3, $4, $5, $6) ON CONFLICT ("Id") DO NOTHING', [this.Id, this.CourseReferencingId, this.CourseReferencedId, this.State, this.CreatedDate.toISOString(), this.ModifiedDate.toISOString()])];
  }

  private validateParameter(obj: any) {
    if (obj === null || obj === undefined)
      throw new Error('Parameter is not defined.');
    if (typeof obj.Id !== 'string' || obj.Id.trim() === '')
      throw new Error(`Id must be a non-empty string. Passed value is of type ${typeof obj.Id}`);
    if (typeof obj.CourseReferencingId !== 'string' || obj.CourseReferencingId.trim() === '')
      throw new Error(`CourseReferencingId must be a non-empty string. Passed value is of type ${typeof obj.CourseReferencingId}`);
    if (typeof obj.CourseReferencedId !== 'string' || obj.CourseReferencedId.trim() === '')
      throw new Error(`CourseReferencedId must be a non-empty string. Passed value is of type ${typeof obj.CourseReferencedId}`);
    if (typeof obj.State !== 'number' || isNaN(obj.State))
      throw new Error(`State must be a number. Passed value is of type ${typeof obj.State}`);
    if (!(obj.CreatedDate instanceof Date))
      throw new Error(`CreatedDate must be a date. Passed value is of type ${typeof obj.CreatedDate}`);
    if (!(obj.ModifiedDate instanceof Date))
      throw new Error(`ModifiedDate must be a date. Passed value is of type ${typeof obj.ModifiedDate}`);
  }
}