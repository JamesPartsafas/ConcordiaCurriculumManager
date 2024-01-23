import CreateGroup from "../src/pages/groups/CreateGroup";
import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";

describe("Create Group Test Case", () => {
    it("validate function should render elements", () => {
        const component = render(
            <BrowserRouter>
                <CreateGroup />
            </BrowserRouter>
        );

        // Use a custom function to find the label element
        const labelNode = component.getByText((content, element) => {
            // Check if the element has the expected class
            const hasExpectedClass = element.classList.contains("chakra-form__label");

            // Check if the content includes "Name"
            const includesExpectedText = content.includes("Name");

            // Return true if both conditions are met
            return hasExpectedClass && includesExpectedText;
        });

        expect(labelNode).toBeDefined();
    });
});
