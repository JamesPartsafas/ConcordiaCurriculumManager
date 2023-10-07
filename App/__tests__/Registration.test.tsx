import Register from "../src/pages/Register";
import { render } from "@testing-library/react";
//import userEvent from "@testing-library/user-event";
import { BrowserRouter } from "react-router-dom";

describe("Registration Render Test Case", () => {
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

    // it("test registration input", () => {
    //     const firstName = "JestTest";
    //     const lastName = "User";
    //     const username = "jest@ccm.ca";
    //     const password = "Passcode";
    //     const onSubmit = jest.fn();

    //     render(
    //         <BrowserRouter>
    //             <Register
    //                 setUser={() => {
    //                     onSubmit;
    //                 }}
    //             />
    //         </BrowserRouter>
    //     );

    //     userEvent.type(screen.getByLabelText("First Name"), firstName);
    //     userEvent.type(screen.getByLabelText("Last Name"), lastName);
    //     userEvent.type(screen.getByLabelText("Email"), username);
    //     userEvent.type(screen.getByLabelText("Password"), password);
    //     userEvent.click(screen.getByText("Create Account"));

    //     expect(onSubmit).toHaveBeenCalledTimes(1);
    // });
});
