import { Text } from "@chakra-ui/react";
import PropTypes from "prop-types";

function StringDiff({ newTextArray }) {
    return (
        <Text as="span" color="black">
            {newTextArray.map((part, index) => (
                <Text as="span" key={`new-${index}`} color={part.type === "added" ? "blue.500" : "black"}>
                    {part.value}
                </Text>
            ))}
        </Text>
    );
}
StringDiff.propTypes = {
    newTextArray: PropTypes.array.isRequired,
};
export default StringDiff;
