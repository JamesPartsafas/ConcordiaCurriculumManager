import globalPgq from 'pg-promise';
import { DataType } from './DataType';

export default class Role implements DataType {
  Id: string;
  UserRole: number;
  CreatedDate: Date;
  ModifiedDate: Date;

  constructor(obj: any) {
    this.validateParameter(obj);
    this.Id = obj.Id;
    this.UserRole = obj.UserRole;
    this.CreatedDate = obj.CreatedDate;
    this.ModifiedDate = obj.ModifiedDate;
  }

  CreateQuery(task: globalPgq.ITask<{}>): Promise<null>[] {
    return [task.none('INSERT INTO "Roles" ("Id", "UserRole", "CreatedDate", "ModifiedDate") VALUES($1, $2, $3, $4) ON CONFLICT ("Id") DO NOTHING', [this.Id, this.UserRole, this.CreatedDate.toISOString(), this.ModifiedDate.toISOString()])];
  }

  private validateParameter(obj: any) {
    if (obj === null || obj === undefined)
      throw new Error('Parameter is not defined.');
    if (typeof obj.Id !== 'string' || obj.Id.trim() === '')
      throw new Error(`Id must be a non-empty string. Passed value is of type ${typeof obj.Id}`);
    if (typeof obj.UserRole !== 'number' || isNaN(obj.UserRole))
      throw new Error(`UserRole must be a number. Passed value is of type ${typeof obj.UserRole}`);
    if (!(obj.CreatedDate instanceof Date))
      throw new Error(`CreatedDate must be a date. Passed value is of type ${typeof obj.CreatedDate}`);
    if (!(obj.ModifiedDate instanceof Date))
      throw new Error(`ModifiedDate must be a date. Passed value is of type ${typeof obj.ModifiedDate}`);
  }
}