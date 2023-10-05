import fs from 'fs';
import globalPgq from 'pg-promise';
import { DataType, JsonData } from './types/DataType';

export const readFile = <T extends DataType>(path: string, constructor: new (arg: any) => T) => {
  const jsonContent = fs.readFileSync(path, 'utf-8');
  const jsonData: JsonData<T> = JSON.parse(jsonContent);

  if (jsonData.TableName === undefined
    || jsonData.AutoGenerateCreatedDate === undefined
    || jsonData.AutoGenerateModifiedDate === undefined
    || jsonData.Data === undefined
    || !Array.isArray(jsonData.Data)) {
    throw new Error(`JSON file [${path}] is not valid`)
  }

  const data: T[] = jsonData.Data.map(item => {
    if (!item.CreatedDate && jsonData.AutoGenerateCreatedDate) {
      item.CreatedDate = new Date();
    }

    if (!item.ModifiedDate && jsonData.AutoGenerateModifiedDate) {
      item.ModifiedDate = new Date();
    }

    return new constructor(item);
  });

  jsonData.Data = data;
  return jsonData;
}

const seedTable = async <T extends DataType>(t: globalPgq.ITask<{}>, json: JsonData<T>) => {
  try {
    const queries = json.Data.map(item => item.CreateQuery(t)).flat();
    await t.batch(queries);
    console.log(`${json.TableName} table(s) was/were seeded successfully!`);
  } catch (error) {
    console.error(`Error while seeding ${json.TableName} table(s):`, error);
    throw error;
  }
}

export const seedDataBase = async (connectionString: string, jsonDataArray: JsonData<DataType>[]) => {
  const pgp: globalPgq.IMain = globalPgq();

  try {
    const db = pgp(connectionString);
    await db.tx(async (t: globalPgq.ITask<{}>) => {
      for (const jsonData of jsonDataArray) {
        await seedTable(t, jsonData);
      }
    });
  } catch (error) {
    console.error('Error while seeding the database:', error);
    throw error;
  } finally {
    pgp.end();
  }
}