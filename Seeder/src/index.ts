import path from 'path';
import Role from './types/Role';
import User from './types/User';
import { readFile, seedDataBase } from './util';
import CourseComponents from './types/CourseComponents';
import Course from './types/Course';

require('dotenv').config();
const onlyValidateData: boolean = process.argv[2] === '--validateData';
const connectionString: string = process.env.ConnectionString as string;

if (!onlyValidateData && (!connectionString || connectionString.trim() === '')) {
  throw new Error('Connection string cannot be empty or null');
}

const rolesJsonFilePath = path.resolve(__dirname, 'data', 'Roles.json');
const rolesData = readFile(rolesJsonFilePath, Role);

const usersJsonFilePath = path.resolve(__dirname, 'data', 'Users.json');
const usersData = readFile(usersJsonFilePath, User);

const courseComponentsJsonFilePath = path.resolve(__dirname, 'data', 'CourseComponents.json');
const courseComponentsData = readFile(courseComponentsJsonFilePath, CourseComponents);

const coursesJsonFilePath = path.resolve(__dirname, 'data', 'Courses.json');
const coursesData = readFile(coursesJsonFilePath, Course);

if (!onlyValidateData) {
  seedDataBase(connectionString, [rolesData, usersData, courseComponentsData, coursesData]);
} else {
  console.log('Seeding data is valid!');
}