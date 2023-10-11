# Data Seeder
---------------------------
## Operaton Modes

- Validate Seeding Data: run `npm run validateData`
    - Note that the validation only checks that the required fields are supplied. No relationship (e.g. FK constraint) validation happens.  
- Seed the database: run `npm run start`
    - Note that the script expects `ConnectionString` as an environment variable. Check `.env.Example` and create a `.env` with the value for `ConnectionString` before running the script. 

## Add More Data To Existing Files

Add the extra data to its respective json file under `src\data` directory. When you are done, validate the data to check that all the required fields are added.
that people naturally use in email.

## Add Data To a New Table

 1. Create a new JSON file under `src\data`. Make sure to follow the following format.
    - TableName (string): The name of the table(s) to be added. E.g., "Roles" or "Courses and CourseCourseComponent"
    - AutoGenerateCreatedDate (boolean): True to auto generate the created date or False to supply the created data manually.
    - AutoGenerateModifiedDate (boolean): True to auto generate the modified date or False to supply the modified data manually.
    - Data (array): An array of objects that should follow the actual data structure
 2. Create a new TS type under `src\types`. Make sure to implement `DataType` and provide an implementation to `CreateQuery`. The array of returned by that method should respect all the table constraints (e.g., have the Course insertion query before the CourseCourseComponent insertion query).
    - Note that the data validation only calls the constructor of your newly created type. Thus, make sure to validate the input to the constructor. The constructor **must** accept an object of type any.
 3. Edit `index.ts` under `src` to use `readFile` method to read the newly created JSON file. Also, add the newly read data to the array used as a parameter to `seedDataBase` method. Please make sure to respect the order or data in that array (e.g., have CourseComponent data before Course data). 