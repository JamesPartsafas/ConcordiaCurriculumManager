import globalPgq from 'pg-promise';
import { DataType } from './DataType';

export default class Group implements DataType {
  Id: string;
  Name: string;
  Members: string[];
  GroupMasters: string[];
  CreatedDate: Date;
  ModifiedDate: Date;

  constructor(obj: any) {
    this.validateParameter(obj);
    this.Id = obj.Id;
    this.Name = obj.Name;
    this.Members = obj.Members;
    this.GroupMasters = obj.GroupMasters;
    this.CreatedDate = obj.CreatedDate;
    this.ModifiedDate = obj.ModifiedDate;
  }

  CreateQuery(task: globalPg.ITask<{}>): Promise<null>[] {
    const insertIntoGroupTable = task.none(
      'INSERT INTO "Groups" ("Id", "Name", "CreatedDate", "ModifiedDate") VALUES($1, $2, $3, $4) ON CONFLICT ("Id") DO NOTHING',
      [this.Id, this.Name, this.CreatedDate.toISOString(), this.ModifiedDate.toISOString()]
    );

    const insertIntoGroupUserTable = this.Members.map(memberId => 
      task.none(
        'INSERT INTO "GroupUsers" ("GroupId", "UserId") VALUES($1, $2) ON CONFLICT ("GroupId", "UserId") DO NOTHING',
        [this.Id, memberId]
      )
    );

    const insertIntoGroupMastersTable = this.GroupMasters.map(masterId => 
      task.none(
        'INSERT INTO "GroupMasters" ("GroupId", "MasterId") VALUES($1, $2) ON CONFLICT ("GroupId", "MasterId") DO NOTHING',
        [this.Id, masterId]
      )
    );

    return [insertIntoGroupTable, ...insertIntoGroupUserTable, ...insertIntoGroupMastersTable];
  }

  private validateParameter(obj: any) {
    if (obj === null || obj === undefined)
      throw new Error('Parameter is not defined.');
    if (typeof obj.Id !== 'string' || obj.Id.trim() === '')
      throw new Error(`Id must be a non-empty string. Passed value is of type ${typeof obj.Id}`);
    if (typeof obj.Name !== 'string' || obj.Name.trim() === '')
      throw new Error(`Name must be a non-empty string. Passed value is of type ${typeof obj.Name}`);
    if (!Array.isArray(obj.Members) || obj.Members.some((i: any) => typeof i !== 'string'))
      throw new Error(`Members must be a string array of User IDs. Passed value is of type ${typeof obj.Members}`);
	if (!Array.isArray(obj.GroupMasters) || obj.GroupMasters.some((i: any) => typeof i !== 'string'))
      throw new Error(`GroupMasters must be a string array of User IDs. Passed value is of type ${typeof obj.GroupMasters}`);
    if (!(obj.CreatedDate instanceof Date))
      throw new Error(`CreatedDate must be a date. Passed value is of type ${typeof obj.CreatedDate}`);
    if (!(obj.ModifiedDate instanceof Date))
      throw new Error(`ModifiedDate must be a date. Passed value is of type ${typeof obj.ModifiedDate}`);
  }
}
