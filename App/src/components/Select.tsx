import {
    AutoComplete,
    AutoCompleteInput,
    AutoCompleteList,
    AutoCompleteItem,
} from "@choc-ui/chakra-autocomplete";
import PropTypes from "prop-types";

function AutocompleteInput({ options, onSelect, width }) {
    const handleSelect = (value) => {
        onSelect(value);
    };
    return (
        <AutoComplete>
            <AutoCompleteInput width={width} />
            <AutoCompleteList>
                {options.map((option, index) => (
                    <AutoCompleteItem
                        key={`option-${index}`}
                        value={option}
                        textTransform="capitalize"
                        onClick={() => handleSelect(option)}
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
};
export default AutocompleteInput;
