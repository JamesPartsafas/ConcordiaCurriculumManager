import { BrowserRouter, Route, Routes } from "react-router-dom";

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
import AddUserToGroup from "./pages/AddUserToGroup";
import RemoveUserFromGroup from "./pages/RemoveUserFromGroup";

export const UserContext = createContext<User | null>(null);

export function App() {
    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        // Check for the token in localStorage
        const token = localStorage.getItem("token");

        if (token != null) {
            const user: User = decodeTokenToUser(token);
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
                    <Route path="/creategroup" element={<CreateGroup />} />
                    <Route path="/regulargroups" element={<DisplayGroups />} />
                    <Route path="/managegroups" element={<DisplayManageableGroups />} />
                    <Route path="/addtogroup" element={<AddUserToGroup />} />
                    <Route path="/removefromgroup" element={<RemoveUserFromGroup />} />
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
