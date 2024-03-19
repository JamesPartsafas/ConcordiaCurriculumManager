# ConcordiaCurriculumManager
A tool to facilitate curriculum and course modification for staff at Concordia University, built in partial requirements of SOEN 490.

The project contains an App folder for the frontend React project, built in Typescript, a Server folder for the backend ASP.NET project, built in C#, and a Seeder folder for a Node project which is used to seed data for the application, which is also built using Typescript. Setup instructions for each of the projects can be found in their respective folders or in the project's wiki. The CourseAdmin folder contains documentation surrounding the project.

- James Partsafas - 40170301 - jamespartsafas@gmail.com
- Eyad Al Sweidan - 40068386 - eyadsw2@gmail.com
- Ghaith Chrit - 40114180 - ghaithfinadnan@gmail.com
- Agnes Croteau - 26720927 - agnes.croteau@gmail.com
- Marc Eid - 40153034 - marc.mn.eid@gmail.com
- Kenny Phan - 40164827 - kennyphan100@gmail.com 
- Daniel Soldera - 40168674 - d.v.soldera@gmail.com
- Roger Sioufi - 40177472  - Rodg.sioufi@gmail.com
- Zi Hao Tan - 40174018 - zihaotan429@gmail.com
- Tony Yang - 40171440 - yangtony2000@gmail.com

## New Developer Instructions
As a new developer, there are multiple projects contained in this repository that must be set up to begin developing in a local environment: A backend project in the `Server` folder, and frontend project in the `App` folder, and a data seeder project in the `Seeder` folder. See below for instructions on setting each one up:

To set up the frontend project, refer to [the following document](https://github.com/JamesPartsafas/ConcordiaCurriculumManager/wiki/Front%E2%80%90End-Setup). Once the project is running, navigate to `http://localhost:4173` and verify that the login page is displayed to you.

To set up the backend project, refer to [the following document](https://github.com/JamesPartsafas/ConcordiaCurriculumManager/tree/main/Server).

Once the backend project is set up and running, close the server from within Visual Studio. Within psql, run the command `\dt` to verify that the tables for the project have been created by the migration that was run when setting up the backend project. Once that is verified, it is time to seed the base data for the project, such as courses, curriculums, and test users. Refer to [this document](https://github.com/JamesPartsafas/ConcordiaCurriculumManager/tree/main/Seeder) for further instructions. Once the seeder is run, verify within psql that the data is indeed present.

Finally, now that everything has been set up, start up the server and start up the frontend app. Navigate to `http://localhost:4173`, click on the Register button, and create a new account. You should be redirected to the home page. If you are on the home page, then the setup for your local development environment is complete. If not, please troubleshoot using the previous steps as needed.