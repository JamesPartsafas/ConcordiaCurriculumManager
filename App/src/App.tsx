import { BrowserRouter, Navigate, Route, Routes, useNavigate } from "react-router-dom";

// 1. import `ChakraProvider` component
import { ChakraProvider } from "@chakra-ui/react";

import Home from "./pages/Home";
import NotFound from "./pages/NotFound";
import Login from "./pages/Login";
import { User } from "./services/user";
import { createContext, useState } from "react";
import Register from "./pages/Register";
import CreateGroup from "./pages/CreateGroup";
import DisplayGroups from "./pages/RegularGroups";
import { decodeTokenToUser } from "./services/auth";
import DisplayManageableGroups from "./pages/ManageableGroups";
import AddUserToGroup from "./pages/AddUserToGroup";
import RemoveUserFromGroup from "./pages/RemoveUserFromGroup";
import { BaseRoutes } from "./constants";
import axios from "axios";
import AddCourse from "./pages/AddCourse";
import theme from "../theme.js"; // Import your custom theme
import ComponentsList from "./pages/ComponentsList";
import { LoadingProvider } from "./utils/loadingContext"; // Import the provider
import { BaseRoutes } from "./constants";
import axios from "axios";
import Dossiers from "./pages/dossier/Dossiers";

export const UserContext = createContext<User | null>(null);

export function App() {
    const [user, setUser] = useState<User | null>(initializeUser());
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(user != null ? true : false);
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

    return (
        <>
            <UserContext.Provider value={user}>
                <Routes>
                    <Route path={BaseRoutes.Home} element={<Home />} />
                    <Route path={BaseRoutes.Login} element={<Login setUser={setUser} />} />
                    <Route path={BaseRoutes.Register} element={<Register setUser={setUser} />} />
                    <Route path={BaseRoutes.Groups} element={<DisplayGroups />} />
                    <Route path={BaseRoutes.CreateGroup} element={<CreateGroup />} />
                    <Route path={BaseRoutes.ManageableGroup} element={<DisplayManageableGroups />} />
                    <Route path={BaseRoutes.AddUserToGroup} element={<AddUserToGroup />} />
                    <Route path={BaseRoutes.RemoveUserFromGroup} element={<RemoveUserFromGroup />} />

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
