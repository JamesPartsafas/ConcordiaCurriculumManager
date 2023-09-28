import { BrowserRouter, Route, Routes } from "react-router-dom";

// 1. import `ChakraProvider` component
import { ChakraProvider } from "@chakra-ui/react";

import Home from "./pages/Home";
import NotFound from "./pages/NotFound";
import Login from "./pages/Login";
import { User } from "./services/user";
import { createContext, useEffect, useState } from "react";
import Register from "./pages/Register";
import { DecodedToken } from "./services/auth";
import jwt_decode from "jwt-decode";

export const UserContext = createContext<User | null>(null);

export function App() {
    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        // Check for the token in localStorage
        const token = localStorage.getItem("token");

        if (token != null) {
            const decodedToken = jwt_decode<DecodedToken>(token);

            const user: User = {
                firstName: decodedToken.fName,
                lastName: decodedToken.lName,
                email: decodedToken.email,
                roles: decodedToken.roles,
                issuedAtTimestamp: decodedToken.iat,
                expiresAtTimestamp: decodedToken.exp,
                issuer: decodedToken.iss,
                audience: decodedToken.aud,
            };
            setUser(user);
        }
    }, []);

    return (
        <>
            <UserContext.Provider value={user}>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<Login setUser={setUser} />} />
                    <Route path="/register" element={<Register setUser={setUser} />} />
                    {/* whenever none of the other routes match we show the not found page */}
                    <Route path="*" element={<NotFound />} />
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
