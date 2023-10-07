import Login from "../src/pages/Login";
import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
//import userEvent from "@testing-library/user-event";

describe("Login Render Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = render(
            <BrowserRouter>
                <Login
                    setUser={() => {
                        throw new Error("Function not implemented.");
                    }}
                />
            </BrowserRouter>
        );

        const labelNode = component.getByText("Email");
        expect(labelNode).toBeDefined;
    });

    // it("test login input", () => {
    //     const username = "admin@ccm.ca";
    //     const password = "";
    //     const onSubmit = jest.fn();

    //     render(
    //         <BrowserRouter>
    //             <Login
    //                 setUser={() => {
    //                     onSubmit;
    //                 }}
    //             />
    //         </BrowserRouter>
    //     );

    //     userEvent.type(screen.getByLabelText("Email"), username);
    //     userEvent.type(screen.getByLabelText("Password"), password);
    //     userEvent.click(screen.getByText("Sign in"));

    //     expect(onSubmit).toHaveBeenCalledTimes(1);
    // });
});
