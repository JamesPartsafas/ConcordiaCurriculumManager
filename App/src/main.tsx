import React from "react";
import ReactDOM from "react-dom/client";
import { WrappedApp } from "./App.tsx";
import "./index.css";
import axios from "axios";

axios.defaults.baseURL = import.meta.env.VITE_API_URL;

ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
        <WrappedApp />
    </React.StrictMode>
);
