import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "./pages/Home";
import NotFound from "./pages/NotFound";

export function App() {
    return (
        <>
            <Routes>
                <Route path="/" element={<Home />} />
                {/* whenever none of the other routes match we show the not found page */}
                <Route path="*" element={<NotFound />} />
            </Routes>
        </>
    );
}

export function WrappedApp() {
    return (
        <BrowserRouter>
            <App />
        </BrowserRouter>
    );
}
