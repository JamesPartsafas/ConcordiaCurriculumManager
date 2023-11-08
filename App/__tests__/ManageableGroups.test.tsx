import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import DisplayManageableGroups from "../src/pages/ManageableGroups";
import { UserContext } from "../src/App";
import { User } from "../src/models/user";

describe("Add User to Group Test Case", () => {
    it("validate function should render all elements ", () => {
        const user: User = {
            firstName: "test",
            lastName: "testing",
            id: "204658362",
            email: "jest@gmail.com",
            issuedAtTimestamp: Date.now(),
            expiresAtTimestamp: 100000000000000,
            roles: ["Admin"],
            groups: [],
            masteredGroups: [],
            issuer: "admin",
            audience: "",
        };
        const component = render(
            <UserContext.Provider value={user}>
                <BrowserRouter>
                    <DisplayManageableGroups />
                </BrowserRouter>
            </UserContext.Provider>
        );

        const labelNode = component.getByText("Group Name");
        expect(labelNode).toBeDefined;
    });
});
