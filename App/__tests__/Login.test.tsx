import Login from "../src/pages/Login";
import { fireEvent, render, waitFor } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import * as authService from "../src/services/auth";

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

    it("handles rejected promise from RegisterUser", async () => {
        const mockConsoleLog = jest.spyOn(console, "log").mockImplementation(() => {});
        const mockLoginUser = jest.spyOn(authService, "login");
        mockLoginUser.mockRejectedValue(new Error("Invalid credentials"));

        const { getByText, getByLabelText } = render(
            <BrowserRouter>
                <Login setUser={jest.fn()} />
            </BrowserRouter>
        );

        fireEvent.change(getByLabelText("Email"), { target: { value: "john.doe@example.com" } });
        fireEvent.change(getByLabelText("Password"), { target: { value: "password123" } });

        fireEvent.click(getByText("Sign in"));

        await waitFor(() => {
            expect(getByText("Incorrect Credentials")).toBeDefined();
        });

        expect(mockLoginUser).toHaveBeenCalledWith({
            email: "john.doe@example.com",
            password: "password123",
        });

        mockLoginUser.mockRestore();
        mockConsoleLog.mockRestore();
    });
});
