import { AutoComplete, AutoCompleteInput, AutoCompleteList, AutoCompleteItem } from "@choc-ui/chakra-autocomplete";
import PropTypes from "prop-types";
import { useEffect, useState } from "react";

/**
 * Reusable Autocomplete Input Component with custom props and styling
 * @param param0 options (array of options), onSelect (function to handle selected option), width (string)
 * @returns
 */
function AutocompleteInput({ options, onSelect, width, value, isDisabled }) {
    const [inputValue, setInputValue] = useState("");

    const handleSelect = (value) => {
        onSelect(value);
    };
    useEffect(() => {
        // Update the local state when the value prop changes
        setInputValue(value);
    }, [value]);
    return (
        <AutoComplete onChange={(vals) => handleSelect(vals)} openOnFocus>
            <AutoCompleteInput width={width} placeholder={inputValue} isDisabled={isDisabled} />
            <AutoCompleteList>
                {options.map((option, index) => (
                    <AutoCompleteItem
                        key={`option-${index}`}
                        value={option}
                        textTransform="capitalize"
                        _focus={{ bg: "brandRed100", color: "white" }}
                    >
                        {option}
                    </AutoCompleteItem>
                ))}
            </AutoCompleteList>
        </AutoComplete>
    );
}
AutocompleteInput.propTypes = {
    options: PropTypes.array.isRequired,
    onSelect: PropTypes.func.isRequired,
    width: PropTypes.string,
    placeholder: PropTypes.string,
    value: PropTypes.string,
    isDisabled: !PropTypes.bool.isRequired,
};
export default AutocompleteInput;
