import { BrowserRouter, Navigate, Route, Routes, useNavigate } from "react-router-dom";

// 1. import `ChakraProvider` component
import { ChakraProvider } from "@chakra-ui/react";
import CourseBrowser from "./pages/CourseBrowser";
import Home from "./pages/Home";
import NotFound from "./pages/NotFound";
import Login from "./pages/Login";
import { User } from "./models/user";
import { createContext, useState } from "react";
import Register from "./pages/Register";
import { decodeTokenToUser, isAdmin, isAdminOrGroupMaster } from "./services/auth";
import AddCourse from "./pages/AddCourse";
import theme from "../theme"; // Import your custom theme
import ComponentsList from "./pages/ComponentsList";
import { LoadingProvider } from "./utils/loadingContext"; // Import the provider
import { BaseRoutes } from "./constants";
import axios from "axios";
import Dossiers from "./pages/dossier/Dossiers";
import DisplayManageableGroups from "./pages/groups/ManageableGroups";
import AddingUserToGroup from "./pages/groups/addUserToGroup";
import RemovingUserFromGroup from "./pages/groups/RemoveUserFromGroup";
import DeleteCourse from "./pages/DeleteCourse";
import DossierDetails from "./pages/dossier/DossierDetails";
import AddingMasterToGroup from "./pages/groups/AddGroupMaster";
import RemovingMasterFromGroup from "./pages/groups/RemoveGroupMaster";
import DossiersToReview from "./pages/dossier/DossierToReview";
import CreateGroup from "./pages/groups/CreateGroup";
import DeleteCourseEdit from "./pages/DeleteCourseEdit";
import CourseDetails from "./pages/CourseDetails";
import Header from "./shared/Header";
import DossierReview from "./pages/dossier/DossierReview";
import DossierReport from "./pages/dossier/DossierReport";
import NoData from "./pages/NoData";
import CourseGrouping from "./pages/CourseGrouping";
import DossierBrowser from "./pages/dossier/DossierBrowser";
import GroupingBySchool from "./pages/CourseGroupingBySchool";
import MyGroups from "./pages/groups/myGroups";
import DossierChangeLog from "./pages/dossier/DossierChangeLog";
import BrowserList from "./pages/BrowserList";

export const UserContext = createContext<User | null>(null);

export function App() {
    const navigate = useNavigate();
    const [user, setUser] = useState<User | null>(initializeUser());
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(user != null ? true : false);
    const [isAdminorGroupMaster, setIsAdminorGroupMaster] = useState<boolean>(
        user != null ? isAdminOrGroupMaster(user) : false
    );

    // Check for the token in localStorage.
    function initializeUser() {
        const token = localStorage.getItem("token");
        axios.defaults.headers.common["Authorization"] = `Bearer ${token}`; //set the token globally

        if (token != null) {
            const user: User = decodeTokenToUser(token);
            // if token expired logout, and clear token
            if (user.expiresAtTimestamp * 1000 < Date.now()) {
                localStorage.removeItem("token");
                navigate(BaseRoutes.Login);
                setIsLoggedIn(false);
                setIsAdminorGroupMaster(false);
            }

            return user;
        }
    }

    return (
        <>
            <UserContext.Provider value={user}>
                {isLoggedIn && (
                    <Header
                        setUser={setUser}
                        setIsLoggedIn={setIsLoggedIn}
                        setIsAdminOrGroupMaster={setIsAdminorGroupMaster}
                    ></Header>
                )}
                <Routes>
                    <Route
                        path={BaseRoutes.Home}
                        element={isLoggedIn == true ? <Home /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.CourseBrowser}
                        element={isLoggedIn == true ? <CourseBrowser /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.Login}
                        element={
                            <Login
                                setUser={setUser}
                                setIsLoggedIn={setIsLoggedIn}
                                setIsAdminOrGroupMaster={setIsAdminorGroupMaster}
                            />
                        }
                    />
                    <Route path={BaseRoutes.Register} element={<Register setUser={setUser} />} />
                    <Route
                        path={BaseRoutes.ManageableGroup}
                        element={
                            isAdminorGroupMaster == true ? (
                                <DisplayManageableGroups />
                            ) : (
                                <Navigate to={BaseRoutes.Login} />
                            )
                        }
                    />
                    <Route
                        path={BaseRoutes.AddUserToGroup}
                        element={
                            isAdminorGroupMaster == true ? <AddingUserToGroup /> : <Navigate to={BaseRoutes.Login} />
                        }
                    />
                    <Route
                        path={BaseRoutes.RemoveUserFromGroup}
                        element={
                            isAdminorGroupMaster == true ? (
                                <RemovingUserFromGroup />
                            ) : (
                                <Navigate to={BaseRoutes.Login} />
                            )
                        }
                    />
                    <Route
                        path={BaseRoutes.AddGroupMaster}
                        element={isAdmin(user) == true ? <AddingMasterToGroup /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.RemoveGroupMaster}
                        element={
                            isAdmin(user) == true ? <RemovingMasterFromGroup /> : <Navigate to={BaseRoutes.Login} />
                        }
                    />
                    <Route
                        path={BaseRoutes.Dossiers}
                        element={isLoggedIn == true ? <Dossiers /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.DossiersToReview}
                        element={isLoggedIn == true ? <DossiersToReview /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.DossierBrowser}
                        element={isLoggedIn == true ? <DossierBrowser /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.DossierReview}
                        element={isLoggedIn == true ? <DossierReview /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.DossierDetails}
                        element={isLoggedIn == true ? <DossierDetails /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.DossierReport}
                        element={isLoggedIn == true ? <DossierReport /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.DossierChangeLog}
                        element={isLoggedIn == true ? <DossierChangeLog /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.AddCourse}
                        element={isLoggedIn == true ? <AddCourse /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.EditCourse}
                        element={isLoggedIn == true ? <AddCourse /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.DeleteCourse}
                        element={isLoggedIn == true ? <DeleteCourse /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.CreateGroup}
                        element={isAdmin(user) == true ? <CreateGroup /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.DeleteCourseEdit}
                        element={isLoggedIn == true ? <DeleteCourseEdit /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.CourseDetails}
                        element={isLoggedIn == true ? <CourseDetails /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.GroupingBySchool}
                        element={isLoggedIn == true ? <GroupingBySchool /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.NoData}
                        element={isLoggedIn == true ? <NoData /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route path={BaseRoutes.ComponentsList} element={<ComponentsList />} />
                    <Route
                        path={"report"}
                        element={isLoggedIn ? <DossierReport /> : <Navigate to={BaseRoutes.Login} />}
                    />

                    <Route
                        path={BaseRoutes.CourseGrouping}
                        element={isLoggedIn == true ? <CourseGrouping /> : <Navigate to={BaseRoutes.Login} />}
                    />

                    {/* whenever none of the other routes match we show the not found page */}
                    <Route path={BaseRoutes.NotFound} element={<NotFound />} />

                    <Route
                        path={BaseRoutes.myGroups}
                        element={isLoggedIn == true ? <MyGroups /> : <Navigate to={BaseRoutes.Login} />}
                    />

                    <Route
                        path={BaseRoutes.browserList}
                        element={isLoggedIn == true ? <BrowserList /> : <Navigate to={BaseRoutes.Login} />}
                    />
                </Routes>
            </UserContext.Provider>
        </>
    );
}

export function WrappedApp() {
    return (
        <BrowserRouter>
            <ChakraProvider theme={theme}>
                <LoadingProvider>
                    <App />
                </LoadingProvider>
            </ChakraProvider>
        </BrowserRouter>
    );
}
