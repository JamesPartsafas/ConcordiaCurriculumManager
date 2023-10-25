// import { render } from "@testing-library/react";
// import DisplayGroups from "../src/pages/RegularGroups";
// import React from "react";
// import { ChakraProvider } from "@chakra-ui/react";
// import theme from "../theme";

// describe("Add User to Group Test Case", () => {
//     it("validate function should render all elements ", () => {
//         const mockUseEffect = jest.spyOn(React, "useEffect").mockImplementation(() => {});
//         const mockUseContext = jest
//             .spyOn(React, "useContext")
//             .mockImplementation(() => ({ firstName: "Daniel", lastName: "Soldi", email: "jest@gmail.com", id: "7" }));
//         const component = render(
//             <ChakraProvider theme={theme}>
//                 <DisplayGroups />
//             </ChakraProvider>
//         );

//         const labelNode = component.getByText("Group Name");
//         expect(labelNode).toBeDefined;
//         mockUseEffect.mockRestore();
//         mockUseContext.mockRestore();
//     });
// });
