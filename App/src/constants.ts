export enum BaseRoutes {
    Home = "/",
    Login = "/login",
    Register = "/register",
    ResetPasswordEmail = "/resetPasswordEmail",
    Dossiers = "/dossiers",
    NotFound = "*",
    ComponentsList = "/components-list",
    NoData = "/NoData",
    browserList = "/browserList",
    coursesLeft = "/coursesLeft",
    Directories = "/directories",

    // Course related routes
    AddCourse = "/add-course/:dossierId",
    EditCourse = "/edit-course/:id/:dossierId",
    DeleteCourse = "/delete-course/:dossierId",
    DeleteCourseEdit = "/delete-course-edit/:dossierId",
    CourseBrowser = "/CourseBrowser",
    CourseDetails = "/CourseDetails",
    CourseBySubject = "/coursebysubjectbrowser",
    CoursesFromSubject = "/CoursesFromSubject",
    CourseHistory = "/course-history",

    // Course Grouping related routes
    DeleteCourseGrouping = "/delete-course-grouping/:dossierId",
    DeleteCourseGroupingEdit = "/delete-course-grouping-edit/:dossierId",
    GroupingBySchool = "/GetCourseBySchool",
    GroupingByName = "/GetCourseByName",
    CourseGrouping = "/CourseGrouping/:courseGroupingId",
    CreateCourseGrouping = "/add/CourseGrouping/:dossierId",
    EditCourseGrouping = "/edit/CourseGrouping/:dossierId/:courseGroupingId",

    // Groups related routes
    ManageableGroup = "/manageablegroup",
    Groups = "/groups",
    CreateGroup = "/creategroup",
    AddUserToGroup = "/addusertogroup",
    RemoveUserFromGroup = "/removeuserfromgroup",
    AddGroupMaster = "/addgroupmaster",
    RemoveGroupMaster = "/removegroupmaster",
    myGroups = "/myGroups",
    allGroups = "/allGroups",
    groupDetails = "/GroupDetails",

    // Dossier related routes
    DossierDetails = "/dossierdetails/:dossierId",
    DossierReview = "/dossierReview/:dossierId",
    DossierReport = "/dossierReport/:dossierId",
    DossierChangeLog = "/change-log",
    DossiersToReview = "/dossierstoreview",
    DossierBrowser = "/dossierbrowser",

    // User related routes
    profile = "/profile",
    editProfileInfo = "/editProfileInfo",
    userBrowser = "/userBrowser",
}
