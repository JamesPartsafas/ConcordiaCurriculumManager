import { AutoComplete, AutoCompleteInput, AutoCompleteList, AutoCompleteItem } from "@choc-ui/chakra-autocomplete";
import PropTypes from "prop-types";

/**
 * Reusable Autocomplete Input Component with custom props and styling
 * @param param0 options (array of options), onSelect (function to handle selected option), width (string)
 * @returns
 */
function AutocompleteInput({ options, onSelect, width, placeholder }) {
    const handleSelect = (value) => {
        onSelect(value);
    };
    return (
        <AutoComplete onChange={(vals) => handleSelect(vals)} openOnFocus>
            <AutoCompleteInput width={width} placeholder={placeholder} />
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
};
export default AutocompleteInput;
