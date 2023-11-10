import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import AddingUserToGroup from "../src/pages/addUserToGroup";

describe("Add User to Group Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = render(
            <BrowserRouter>
                <AddingUserToGroup />
            </BrowserRouter>
        );

        const labelNode = component.getByText("Search:");
        expect(labelNode).toBeDefined;
    });
});
