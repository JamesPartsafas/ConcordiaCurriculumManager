import globalPgq from 'pg-promise';
import { DataType } from './DataType';

export default class CourseIdentifier implements DataType {
  Id: string;
  ConcordiaCourseId: string;
  CreatedDate: Date;
  ModifiedDate: Date;

  constructor(obj: any) {
    this.validateParameter(obj);
    this.Id = obj.Id;
    this.ConcordiaCourseId = obj.ConcordiaCourseId;
    this.CreatedDate = obj.CreatedDate;
    this.ModifiedDate = obj.ModifiedDate;
  }

  CreateQuery(task: globalPgq.ITask<{}>): Promise<null>[] {
    return [task.none('INSERT INTO "CourseIdentifiers" ("Id", "ConcordiaCourseId", "CreatedDate", "ModifiedDate") VALUES($1, $2, $3, $4) ON CONFLICT ("Id") DO NOTHING', [this.Id, this.ConcordiaCourseId, this.CreatedDate.toISOString(), this.ModifiedDate.toISOString()])];
  }

  private validateParameter(obj: any) {
    if (obj === null || obj === undefined)
      throw new Error('Parameter is not defined.');
    if (typeof obj.Id !== 'string' || obj.Id.trim() === '')
      throw new Error(`Id must be a non-empty string. Passed value is of type ${typeof obj.Id}`);
    if (typeof obj.ConcordiaCourseId !== 'string' || obj.ConcordiaCourseId.trim() === '')
      throw new Error(`ConcordiaCourseId must be a non-empty string. Passed value is of type ${typeof obj.ConcordiaCourseId}`);
    if (!(obj.CreatedDate instanceof Date))
      throw new Error(`CreatedDate must be a date. Passed value is of type ${typeof obj.CreatedDate}`);
    if (!(obj.ModifiedDate instanceof Date))
      throw new Error(`ModifiedDate must be a date. Passed value is of type ${typeof obj.ModifiedDate}`);
  }
}