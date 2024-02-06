import React from "react";
import { Text } from "@chakra-ui/react";
import PropTypes from "prop-types";

function StringDiff({ oldTextArray }) {
    return (
        <Text as="span" color="black">
            {oldTextArray.map((part, index) => (
                <Text
                    as="span"
                    key={`old-${index}`}
                    textDecoration={part.type === "removed" ? "line-through" : "none"}
                    color={part.type === "removed" ? "red.500" : "black"}
                >
                    {part.value}
                </Text>
            ))}
        </Text>
    );
}
StringDiff.propTypes = {
    oldTextArray: PropTypes.array.isRequired,
};
export default StringDiff;
