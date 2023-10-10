import globalPgq from 'pg-promise';

export interface JsonData<T extends DataType> {
  TableName: string,
  AutoGenerateCreatedDate: boolean,
  AutoGenerateModifiedDate: boolean,
  Data: T[]
}

export interface DataType {
  Id: string,
  CreatedDate: Date,
  ModifiedDate: Date,
  CreateQuery(task: globalPgq.ITask<{}>): Promise<null>[]
}