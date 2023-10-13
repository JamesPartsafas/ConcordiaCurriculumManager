// chakra-theme.js
import { extendTheme } from "@chakra-ui/react";

const theme = extendTheme({
    colors: {
        brandRed: "#912338",
        brandRed100: "#9a4a59",
        brandRed600: "#91233847",
        brandBlue: "#0072A8",
        brandBlue100: "#1987a7",
        brandBlue600: "#e6f4f8",
        brandGray: "#353535",
        brandGray500: "#6E6E6E",
        brandGray200: "#BCBCBC",
    },
    fonts: {
        heading: "Montserrat, sans-serif",
        body: "Roboto, sans-serif",
    },
    components: {
        Input: {
            defaultProps: {
                focusBorderColor: "brandRed100",
            },
        },
        Link: {
            baseStyle: {
                color: "brandBlue",
            },
        },
    },
});

export default theme;
