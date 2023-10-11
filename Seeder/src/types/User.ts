import globalPgq from 'pg-promise';
import { DataType } from './DataType';

export default class User implements DataType {
  Id: string;
  FirstName: string;
  LastName: string;
  Email: string;
  Password: string;
  Roles: string[];
  CreatedDate: Date;
  ModifiedDate: Date;

  constructor(obj: any) {
    this.validateParameter(obj);
    this.Id = obj.Id;
    this.FirstName = obj.FirstName;
    this.LastName = obj.LastName;
    this.Email = obj.Email;
    this.Password = obj.Password;
    this.Roles = obj.Roles;
    this.CreatedDate = obj.CreatedDate;
    this.ModifiedDate = obj.ModifiedDate;
  }

  CreateQuery(task: globalPgq.ITask<{}>): Promise<null>[] {
    const insertIntoUserTable = task.none('INSERT INTO "Users" ("Id", "FirstName", "LastName", "Email", "Password", "CreatedDate", "ModifiedDate") VALUES($1, $2, $3, $4, $5, $6, $7) ON CONFLICT ("Id") DO NOTHING', [this.Id, this.FirstName, this.LastName, this.Email, this.Password, this.CreatedDate.toISOString(), this.ModifiedDate.toISOString()]);
    const insertIntoUserRoleTable = this.Roles.map(role => task.none('INSERT INTO "RoleUser" ("RolesId", "UsersId") VALUES($1, $2) ON CONFLICT ("RolesId", "UsersId") DO NOTHING', [role, this.Id]));
    return [insertIntoUserTable, ...insertIntoUserRoleTable];
  }

  private validateParameter(obj: any) {
    if (obj === null || obj === undefined)
      throw new Error('Parameter is not defined.');
    if (typeof obj.Id !== 'string' || obj.Id.trim() === '')
      throw new Error(`Id must be a non-empty string. Passed value is of type ${typeof obj.Id}`);
    if (typeof obj.FirstName !== 'string' || obj.FirstName.trim() === '')
      throw new Error(`FirstName must be a non-empty string. Passed value is of type ${typeof obj.FirstName}`);
    if (typeof obj.LastName !== 'string' || obj.LastName.trim() === '')
      throw new Error(`LastName must be a non-empty string. Passed value is of type ${typeof obj.LastName}`);
    if (typeof obj.Email !== 'string' || obj.Email.trim() === '')
      throw new Error(`Email must be a non-empty string. Passed value is of type ${typeof obj.Email}`);
    if (typeof obj.Password !== 'string' || obj.Password.trim() === '')
      throw new Error(`Password must be a string. Passed value is of type ${typeof obj.Password}`);
    if (!Array.isArray(obj.Roles) || obj.Roles.some((i: any) => typeof i !== 'string'))
      throw new Error(`Roles must be a string array. Passed value is of type ${typeof obj.Roles}`);
    if (!(obj.CreatedDate instanceof Date))
      throw new Error(`CreatedDate must be a date. Passed value is of type ${typeof obj.CreatedDate}`);
    if (!(obj.ModifiedDate instanceof Date))
      throw new Error(`ModifiedDate must be a date. Passed value is of type ${typeof obj.ModifiedDate}`);
  }
}