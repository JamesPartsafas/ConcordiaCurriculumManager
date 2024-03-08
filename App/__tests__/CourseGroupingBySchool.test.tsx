import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import GroupingBySchool from "../src/pages/courseGroupings/CourseGroupingBySchool";

describe("Course Grouping by School Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = render(
            <BrowserRouter>
                <GroupingBySchool />
            </BrowserRouter>
        );

        const labelNode = component.getByText("Curriculum by School");
        expect(labelNode).toBeDefined;
    });
});
