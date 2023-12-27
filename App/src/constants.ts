export enum BaseRoutes {
    Home = "/",
    Login = "/login",
    Register = "/register",
    Dossiers = "/dossiers",
    NotFound = "*",
    AddCourse = "/add-course/:dossierId",
    EditCourse = "/edit-course/:id/:dossierId",
    DeleteCourse = "/delete-course/:dossierId",
    DeleteCourseEdit = "/delete-course-edit/:dossierId",
    ComponentsList = "/components-list",
    CourseBrowser = "/CourseBrowser",
    ManageableGroup = "/manageablegroup",
    Groups = "/groups",
    CreateGroup = "/creategroup",
    AddUserToGroup = "/addusertogroup",
    RemoveUserFromGroup = "/removeuserfromgroup",
    DossierDetails = "/dossierdetails/:dossierId",
    DossierReview = "/dossierreview/:dossierId",
    AddGroupMaster = "/addgroupmaster",
    RemoveGroupMaster = "/removegroupmaster",
    CourseDetails = "/CourseDetails",
}
