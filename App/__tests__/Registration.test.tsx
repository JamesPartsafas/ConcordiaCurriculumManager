import Register from "../src/pages/Register";
import { fireEvent, render, waitFor } from "@testing-library/react";
import * as authService from "../src/services/auth";
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

    it("handles rejected promise from RegisterUser", async () => {
        const mockConsoleLog = jest.spyOn(console, "log").mockImplementation(() => {});
        const mockRegisterUser = jest.spyOn(authService, "RegisterUser");
        mockRegisterUser.mockRejectedValue(new Error("Invalid credentials"));

        const { getByText, getByLabelText } = render(
            <BrowserRouter>
                <Register setUser={jest.fn()} />
            </BrowserRouter>
        );

        fireEvent.change(getByLabelText("First Name"), { target: { value: "John" } });
        fireEvent.change(getByLabelText("Last Name"), { target: { value: "Doe" } });
        fireEvent.change(getByLabelText("Email"), { target: { value: "john.doe@example.com" } });
        fireEvent.change(getByLabelText("Password"), { target: { value: "password123" } });

        fireEvent.click(getByText("Create Account"));

        await waitFor(() => {
            expect(getByText("Invalid Credentials")).toBeDefined();
        });

        expect(mockRegisterUser).toHaveBeenCalledWith({
            firstName: "John",
            lastName: "Doe",
            email: "john.doe@example.com",
            password: "password123",
        });

        mockRegisterUser.mockRestore();
        mockConsoleLog.mockRestore();
    });
});
