import { BrowserRouter, Navigate, Route, Routes, useNavigate } from "react-router-dom";

// 1. import `ChakraProvider` component
import { ChakraProvider } from "@chakra-ui/react";

import Home from "./pages/Home";
import NotFound from "./pages/NotFound";
import Login from "./pages/Login";
import { User } from "./services/user";
import { createContext, useEffect, useState } from "react";
import Register from "./pages/Register";
import { decodeTokenToUser, logout } from "./services/auth";
import { BaseRoutes } from "./constants";
import axios from "axios";
import Dossiers from "./pages/dossier/Dossiers";

export const UserContext = createContext<User | null>(null);

export function App() {
    const [user, setUser] = useState<User | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        initializeUser();
    }, []);

    // Check for the token in localStorage.
    function initializeUser() {
        const token = localStorage.getItem("token");
        axios.defaults.headers.common["Authorization"] = `Bearer ${token}`; //set the token globally

        if (token != null) {
            const user: User = decodeTokenToUser(token);
            //if token expired logout, and clear token
            if (user.expiresAtTimestamp < Date.now()) {
                logout().then(() => {
                    navigate(BaseRoutes.Login);
                });
            }

            setUser(user);
        }
    }

    return (
        <>
            <UserContext.Provider value={user}>
                <Routes>
                    <Route path={BaseRoutes.Home} element={<Home />} />
                    <Route path={BaseRoutes.Login} element={<Login setUser={setUser} />} />
                    <Route path={BaseRoutes.Register} element={<Register setUser={setUser} />} />

                    <Route
                        path={BaseRoutes.Dossiers}
                        element={user != null ? <Dossiers /> : <Navigate to={BaseRoutes.Login} />}
                    />

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
            <ChakraProvider>
                <App />
            </ChakraProvider>
        </BrowserRouter>
    );
}
