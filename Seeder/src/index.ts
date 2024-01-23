import path from 'path';
import Role from './types/Role';
import User from './types/User';
import Group from './types/Group';
import { readFile, seedDataBase } from './util';
import CourseComponents from './types/CourseComponents';
import Course from './types/Course';
import CourseReference from './types/CourseReferences';
import CourseIdentifier from './types/CourseIdentifier';
import CourseGrouping from './types/CourseGrouping';

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

const courseReferencesJsonFilePath = path.resolve(__dirname, 'data', 'CourseReferences.json');
const courseReferencesData = readFile(courseReferencesJsonFilePath, CourseReference);

const groupsJsonFilePath = path.resolve(__dirname, 'data', 'Groups.json');
const groupsData = readFile(groupsJsonFilePath, Group);

const courseIdentifierJsonFilePath = path.resolve(__dirname, 'data', 'CourseIdentifiers.json');
const courseIdentifierData = readFile(courseIdentifierJsonFilePath, CourseIdentifier);

const courseGroupingJsonFilePath = path.resolve(__dirname, 'data', 'CourseGroupings.json');
const courseGroupingData = readFile(courseGroupingJsonFilePath, CourseGrouping);

if (!onlyValidateData) {
  seedDataBase(connectionString, [
    rolesData,
    usersData,
    courseComponentsData,
    coursesData,
    courseReferencesData,
    groupsData,
    courseIdentifierData,
    courseGroupingData
  ]);
} else {
  console.log('Seeding data is valid!');
}