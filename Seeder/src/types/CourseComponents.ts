import globalPgq from 'pg-promise';
import { DataType } from './DataType';

export default class CourseComponents implements DataType {
  Id: string;
  ComponentCode: number;
  ComponentName: string;
  CreatedDate: Date;
  ModifiedDate: Date;

  constructor(obj: any) {
    this.validateParameter(obj);
    this.Id = obj.Id;
    this.ComponentCode = obj.ComponentCode;
    this.ComponentName = obj.ComponentName;
    this.CreatedDate = obj.CreatedDate;
    this.ModifiedDate = obj.ModifiedDate;
  }

  CreateQuery(task: globalPgq.ITask<{}>): Promise<null>[] {
    return [task.none('INSERT INTO "CourseComponents" ("Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate") VALUES($1, $2, $3, $4, $5) ON CONFLICT ("Id") DO NOTHING', [this.Id, this.ComponentCode, this.ComponentName, this.CreatedDate.toISOString(), this.ModifiedDate.toISOString()])];
  }

  private validateParameter(obj: any) {
    if (obj === null || obj === undefined)
      throw new Error('Parameter is not defined.');
    if (typeof obj.Id !== 'string' || obj.Id.trim() === '')
      throw new Error(`Id must be a non-empty string. Passed value is of type ${typeof obj.Id}`);
    if (typeof obj.ComponentCode !== 'number' || isNaN(obj.ComponentCode))
      throw new Error(`ComponentCode must be a number. Passed value is of type ${typeof obj.ComponentCode}`);
    if (typeof obj.ComponentName !== 'string' || obj.ComponentName.trim() === '')
      throw new Error(`ComponentName must be a non-empty string. Passed value is of type ${typeof obj.ComponentName}`);
    if (!(obj.CreatedDate instanceof Date))
      throw new Error(`CreatedDate must be a date. Passed value is of type ${typeof obj.CreatedDate}`);
    if (!(obj.ModifiedDate instanceof Date))
      throw new Error(`ModifiedDate must be a date. Passed value is of type ${typeof obj.ModifiedDate}`);
  }
}