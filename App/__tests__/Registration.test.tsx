import Register from "../src/pages/Register";
import { fireEvent, render, waitFor } from "@testing-library/react";
import * as authService from "../src/services/auth";
import { BrowserRouter } from "react-router-dom";
import theme from "../theme"; // Import your custom theme
import { ChakraProvider } from "@chakra-ui/react";
import { LoadingProvider } from "../src/utils/loadingContext"; // Import the provider

function browserRouterComponent(component) {
    return render(
        <BrowserRouter>
            <ChakraProvider theme={theme}>
                <LoadingProvider>{component}</LoadingProvider>
            </ChakraProvider>
        </BrowserRouter>
    );
}

describe("Registration Render Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = browserRouterComponent(
            <Register
                setUser={() => {
                    throw new Error("Function not implemented.");
                }}
            />
        );

        const labelNode = component.getByText("Email");
        expect(labelNode).toBeDefined;
    });

    it("handles rejected promise from RegisterUser", async () => {
        const mockConsoleLog = jest.spyOn(console, "log").mockImplementation(() => {});
        const mockRegisterUser = jest.spyOn(authService, "RegisterUser");
        mockRegisterUser.mockRejectedValue(new Error("Invalid credentials"));

        const { getByText, getByLabelText } = browserRouterComponent(<Register setUser={jest.fn()} />);

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
