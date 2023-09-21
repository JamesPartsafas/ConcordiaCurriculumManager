import { BrowserRouter, Route, Routes } from "react-router-dom";

// 1. import `ChakraProvider` component
import { ChakraProvider } from "@chakra-ui/react";

import Home from "./pages/Home";
import NotFound from "./pages/NotFound";
import Login from "./pages/Login";

export function App() {
    return (
        <>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                {/* whenever none of the other routes match we show the not found page */}
                <Route path="*" element={<NotFound />} />
            </Routes>
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
