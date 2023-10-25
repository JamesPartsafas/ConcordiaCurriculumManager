import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import AddUserToGroup from "../src/pages/AddUserToGroup";

describe("Add User to Group Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = render(
            <BrowserRouter>
                <AddUserToGroup />
            </BrowserRouter>
        );

        const labelNode = component.getByText("Search:");
        expect(labelNode).toBeDefined;
    });
});
