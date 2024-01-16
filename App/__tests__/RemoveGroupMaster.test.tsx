import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import RemovingMasterFromGroup from "../src/pages/groups/RemoveGroupMaster";

describe("Add User to Group Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = render(
            <BrowserRouter>
                <RemovingMasterFromGroup />
            </BrowserRouter>
        );

        const labelNode = component.getByText("Remove Master from Group:");
        expect(labelNode).toBeDefined;
    });
});
