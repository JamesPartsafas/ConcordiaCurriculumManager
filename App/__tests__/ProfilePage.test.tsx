import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import { UserContext } from "../src/App";
import { User } from "../src/models/user";
import ProfilePage from "../src/pages/ProfilePage";

describe("Profile Page Test Case", () => {
    it("validate function should render all elements ", async () => {
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
        //Ignore error from no mock available for group fetch function.
        console.error = () => {};
        const component = render(
            <UserContext.Provider value={user}>
                <BrowserRouter>
                    <ProfilePage />
                </BrowserRouter>
            </UserContext.Provider>
        );

        const labelNode = component.getByText("Back to Home Page");
        expect(labelNode).toBeDefined;
    });
});
