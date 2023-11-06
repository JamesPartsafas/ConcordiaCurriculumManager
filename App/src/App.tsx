import { BrowserRouter, Navigate, Route, Routes, useNavigate } from "react-router-dom";

// 1. import `ChakraProvider` component
import { ChakraProvider } from "@chakra-ui/react";
// import CourseBrowser from "./pages/CourseBrowser";
import Home from "./pages/Home";
import NotFound from "./pages/NotFound";
import Login from "./pages/Login";
import { User } from "./models/user";
import { createContext, useState } from "react";
import Register from "./pages/Register";
import { decodeTokenToUser } from "./services/auth";
import AddCourse from "./pages/AddCourse";
import theme from "../theme"; // Import your custom theme
import ComponentsList from "./pages/ComponentsList";
import { LoadingProvider } from "./utils/loadingContext"; // Import the provider
import { BaseRoutes } from "./constants";
import axios from "axios";
import Dossiers from "./pages/dossier/Dossiers";
import DisplayManageableGroups from "./pages/ManageableGroups";
import EditCourse from "./pages/EditCourse";
import AddingUserToGroup from "./pages/addUserToGroup";
import RemovingUserFromGroup from "./pages/RemoveUserFromGroup";
import DeleteCourse from "./pages/DeleteCourse";

export const UserContext = createContext<User | null>(null);

export function App() {
    const [user, setUser] = useState<User | null>(initializeUser());
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(user != null ? true : false);
    const [isAdminorGroupMaster, setIsAdminorGroupMaster] = useState<boolean>(
        user != null ? user.roles.includes("Admin") || user.masteredGroups != null : false
    );
    const navigate = useNavigate();

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
                <Routes>
                    <Route
                        path={BaseRoutes.Home}
                        element={isLoggedIn == true ? <Home /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    {/* <Route
                        path={BaseRoutes.CourseBrowser}
                        element={isLoggedIn == true ? <CourseBrowser /> : <Navigate to={BaseRoutes.Login} />}
                    /> */}
                    <Route path={BaseRoutes.Login} element={<Login setUser={setUser} />} />
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
                        path={BaseRoutes.Dossiers}
                        element={isLoggedIn == true ? <Dossiers /> : <Navigate to={BaseRoutes.Login} />}
                    />

                    <Route
                        path={BaseRoutes.AddCourse}
                        element={isLoggedIn == true ? <AddCourse /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.EditCourse + "/:id"}
                        element={isLoggedIn == true ? <EditCourse /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.DeleteCourse}
                        element={isLoggedIn == true ? <DeleteCourse /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route path={BaseRoutes.ComponentsList} element={<ComponentsList />} />
                    {/* whenever none of the other routes match we show the not found page */}
                    <Route path={BaseRoutes.NotFound} element={<NotFound />} />
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
