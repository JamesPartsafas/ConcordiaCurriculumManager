import { BrowserRouter, Route, Routes } from "react-router-dom";

// 1. import `ChakraProvider` component
import { ChakraProvider } from "@chakra-ui/react";

import Home from "./pages/Home";
import NotFound from "./pages/NotFound";
import Login from "./pages/Login";
import { User } from "./services/user";
import { createContext, useState } from "react";
import AddCourse from "./pages/AddCourse";

export const UserContext = createContext<User | null>(null);

export function App() {
    const [user, setUser] = useState<User | null>(null);

    return (
        <>
            <UserContext.Provider value={user}>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/add-course" element={<AddCourse />} />
                    <Route
                        path="/login"
                        element={<Login setUser={setUser} />}
                    />
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
