import PropTypes from "prop-types";
import { Button as ChakraButton } from "@chakra-ui/react";

function Button({ variant, width, height, children, ...rest }) {
    let buttonColor = "blue"; // Default color
    let buttonVariantStyles = {};

    buttonColor = "brandBlue"; // Change to your secondary color

    if (variant === "solid") {
        buttonVariantStyles = {
            bg: buttonColor,
            color: "white",
            _hover: { bg: `${buttonColor}100` },
        };
    } else if (variant === "outline") {
        buttonVariantStyles = {
            border: `1px solid`,
            borderColor: buttonColor,
            color: buttonColor,
            background: "transparent",
            _hover: { bg: `${buttonColor}600` },
        };
    }

    return (
        <ChakraButton type={"submit"} {...buttonVariantStyles} width={width} height={height} {...rest}>
            {children}
        </ChakraButton>
    );
}

Button.propTypes = {
    variant: PropTypes.oneOf(["solid", "outline"]), // New variant prop
    width: PropTypes.string,
    height: PropTypes.string,
    children: PropTypes.node.isRequired,
};

export default Button;
