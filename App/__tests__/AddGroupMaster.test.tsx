import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import AddingMasterToGroup from "../src/pages/groups/AddGroupMaster";

describe("Add Master to Group Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = render(
            <BrowserRouter>
                <AddingMasterToGroup />
            </BrowserRouter>
        );

        const labelNode = component.getByText("Add Master to Group:");
        expect(labelNode).toBeDefined;
    });
});
