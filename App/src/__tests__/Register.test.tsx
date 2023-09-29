import { render } from "@testing-library/react";
import Register from "../pages/Register";
import { User } from "../services/user";

describe("login", () => {
    test("validate function should render all elements ", () => {
        const component = render(
            <Register
                setUser={function (user: User | null): void {
                    throw new Error("Function not implemented.");
                }}
            />
        );
        const labelNode = component.getByText("email");
        expect(labelNode).toBeDefined;
    });
});
