import { render } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import CourseGroupingByName from "../src/pages/courseGroupings/CourseGroupingByName";

describe("Course Grouping by Name Test Case", () => {
    it("validate function should render all elements ", () => {
        const component = render(
            <BrowserRouter>
                <CourseGroupingByName />
            </BrowserRouter>
        );

        const labelNode = component.getByText("Curriculum by Name");
        expect(labelNode).toBeDefined;
    });
});
