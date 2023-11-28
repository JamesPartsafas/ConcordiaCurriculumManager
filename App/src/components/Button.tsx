import { Button as ChakraButton } from "@chakra-ui/react";
import React from "react";

interface ButtonPropType {
    style?: "primary" | "secondary";
    variant?: "solid" | "outline";
    width?: string;
    height?: string;
    children: React.ReactNode;
    [key: string]: object | React.ReactNode;
}

const Button = React.forwardRef<HTMLButtonElement, ButtonPropType>(
    ({ style, variant, width, height, children, ...rest }: ButtonPropType, ref) => {
        let buttonColor = "blue"; // Default color
        let buttonVariantStyles = {};

        if (style === "primary") {
            buttonColor = "brandRed"; // Change to your primary color
        } else if (style === "secondary") {
            buttonColor = "brandBlue"; // Change to your secondary color
        }

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
            <ChakraButton ref={ref} {...buttonVariantStyles} width={width} height={height} {...rest}>
                {children}
            </ChakraButton>
        );
    }
);

Button.displayName = "Button";

export default Button;
