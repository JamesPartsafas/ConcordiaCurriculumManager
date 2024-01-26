import React from "react";
import { Box, Text } from "@chakra-ui/react";
import { diffWordsWithSpace } from "diff";

const detectChanges = (oldText, newText) => {
    const diffResult = diffWordsWithSpace(oldText, newText);
    const oldTextArray = [];
    const newTextArray = [];

    diffResult.forEach((part) => {
        if (part.added) {
            newTextArray.push({ value: part.value, type: "added" });
        } else if (part.removed) {
            oldTextArray.push({ value: part.value, type: "removed" });
        } else {
            oldTextArray.push({ value: part.value, type: "unchanged" });
            newTextArray.push({ value: part.value, type: "unchanged" });
        }
    });

    return { oldTextArray, newTextArray };
};

function StringDiff({ oldText, newText }: { oldText: string; newText: string }) {
    const { oldTextArray, newTextArray } = detectChanges(oldText, newText);
    console.log(detectChanges(oldText, newText));
    return (
        <Box>
            <Text as="div" color="black">
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
            <br />
            <Text as="div" color="black">
                {newTextArray.map((part, index) => (
                    <Text as="span" key={`new-${index}`} color={part.type === "added" ? "blue.500" : "black"}>
                        {part.value}
                    </Text>
                ))}
            </Text>
        </Box>
    );
}

export default StringDiff;
