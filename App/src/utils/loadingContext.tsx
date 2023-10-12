import { createContext, useContext, useState } from "react";
import PropTypes from "prop-types";
import LoadingSpinner from "../components/LoadingSpinner";

type LoadingContextType = {
    isLoading: boolean;
    toggleLoading: () => void;
};

// Create a context with an initial default value
const LoadingContext = createContext<LoadingContextType | undefined>(undefined);

// Create a provider component
export function LoadingProvider({ children }) {
    const [isLoading, setIsLoading] = useState(false);

    const toggleLoading = () => {
        setIsLoading((prevLoading) => !prevLoading);
    };

    return (
        <LoadingContext.Provider value={{ isLoading, toggleLoading }}>
            {isLoading && <LoadingSpinner size="xl" color="brandRed" label="Loading..." />}
            {children}
        </LoadingContext.Provider>
    );
}
LoadingProvider.propTypes = {
    children: PropTypes.node.isRequired,
};
export function useLoading() {
    const context = useContext(LoadingContext);
    if (context === undefined) {
        throw new Error("useLoading must be used within a LoadingProvider");
    }
    return context;
}
