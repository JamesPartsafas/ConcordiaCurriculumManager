import { Spinner, Box } from "@chakra-ui/react";
import PropTypes from "prop-types";

/**
 * Resuable Loading Spinner Component with custom props
 * @param param0 size of the loading spinner (xs, sm, md, lg, xl, 2xl), color of the loading spinner (blue.500, brandRed, etc.), label of the loading spinner
 * @returns
 */
function LoadingSpinner({ size = "md", color = "blue.500", label = "Loading..." }) {
    return (
        <Box
            display="flex"
            alignItems="center"
            justifyContent="center"
            height="100vh" /* Set the height to cover the entire viewport */
            position="fixed" /* Position it as fixed to overlay other content */
            top="0"
            left="0"
            right="0"
            bottom="0"
            background="rgba(255, 255, 255, 0.8)" /* Add a semi-transparent background */
            zIndex="9999" /* Set a high z-index to make sure it's on top of other content */
        >
            <Spinner size={size} color={color} />
            <Box ml={2}>{label}</Box>
        </Box>
    );
}

LoadingSpinner.propTypes = {
    size: PropTypes.string,
    color: PropTypes.string,
    label: PropTypes.string,
};

export default LoadingSpinner;
