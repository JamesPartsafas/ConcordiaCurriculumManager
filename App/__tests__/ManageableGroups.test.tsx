import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import DisplayManageableGroups from "../src/pages/ManageableGroups";

describe("Add User to Group Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = render(
            <BrowserRouter>
                <DisplayManageableGroups />
            </BrowserRouter>
        );

        const labelNode = component.getByText("Group Name");
        expect(labelNode).toBeDefined;
    });
});
