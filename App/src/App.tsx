import { BrowserRouter, Navigate, Route, Routes, useNavigate } from "react-router-dom";

// 1. import `ChakraProvider` component
import { ChakraProvider } from "@chakra-ui/react";

import Home from "./pages/Home";
import NotFound from "./pages/NotFound";
import Login from "./pages/Login";
import { User } from "./services/user";
import { createContext, useEffect, useState } from "react";
import Register from "./pages/Register";
import CreateGroup from "./pages/CreateGroup";
import DisplayGroups from "./pages/RegularGroups";
import { decodeTokenToUser } from "./services/auth";
import DisplayManageableGroups from "./pages/ManageableGroups";
import { BaseRoutes } from "./constants";
import axios from "axios";
import AddCourse from "./pages/AddCourse";
import theme from "../theme"; // Import your custom theme
import ComponentsList from "./pages/ComponentsList";
import { LoadingProvider } from "./utils/loadingContext"; // Import the provider
import Dossiers from "./pages/dossier/Dossiers";
import AddingUserToGroup from "./pages/AddUserToGroup";
import RemovingUserFromGroup from "./pages/RemoveUserFromGroup";

export const UserContext = createContext<User | null | undefined>(null);

export function App() {
    const [user, setUser] = useState<User | null | undefined>();
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(user != null ? true : false);
    // const [isAdmin, setIsAdmin] = useState<boolean>(user.roles.includes("Admin") ? true : false);
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
            }
            return user;
        }
    }

    useEffect(() => {
        initializeUser();
        return () => localStorage.removeItem("token");
    }, []);

    return (
        <>
            <UserContext.Provider value={user}>
                <Routes>
                    <Route
                        path={BaseRoutes.Home}
                        element={isLoggedIn == true ? <Home /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route path={BaseRoutes.Login} element={<Login setUser={setUser} />} />
                    <Route path={BaseRoutes.Register} element={<Register setUser={setUser} />} />
                    <Route
                        path={BaseRoutes.Groups}
                        element={isLoggedIn == true ? <DisplayGroups /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.CreateGroup}
                        element={isLoggedIn == true ? <CreateGroup /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.ManageableGroup}
                        element={isLoggedIn == true ? <DisplayManageableGroups /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.AddUserToGroup}
                        element={isLoggedIn == true ? <AddingUserToGroup /> : <Navigate to={BaseRoutes.Login} />}
                    />
                    <Route
                        path={BaseRoutes.RemoveUserFromGroup}
                        element={isLoggedIn == true ? <RemovingUserFromGroup /> : <Navigate to={BaseRoutes.Login} />}
                    />

                    <Route
                        path={BaseRoutes.Dossiers}
                        element={isLoggedIn == true ? <Dossiers /> : <Navigate to={BaseRoutes.Login} />}
                    />

                    <Route
                        path={BaseRoutes.AddCourse}
                        element={isLoggedIn == true ? <AddCourse /> : <Navigate to={BaseRoutes.Login} />}
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
