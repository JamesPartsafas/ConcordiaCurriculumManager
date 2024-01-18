import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import RemoveUserFromGroup from "../src/pages/groups/RemoveUserFromGroup";

describe("Add User to Group Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = render(
            <BrowserRouter>
                <RemoveUserFromGroup />
            </BrowserRouter>
        );

        const labelNode = component.getByText("Remove User from Group:");
        expect(labelNode).toBeDefined;
    });
});
