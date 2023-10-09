import Register from "../src/pages/Register";
import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";

describe("Registration Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = render(
            <BrowserRouter>
                <Register
                    setUser={() => {
                        throw new Error("Function not implemented.");
                    }}
                />
            </BrowserRouter>
        );

        const labelNode = component.getByText("Email");
        expect(labelNode).toBeDefined;
    });
});
