import { CourseGroupingDTO } from "../models/courseGrouping";

interface CourseGroupingComponentProps {
    courseGrouping: CourseGroupingDTO;
}

export default function CourseGroupingComponent(prop: CourseGroupingComponentProps) {
    return (
        <div>
            <h3>{prop.courseGrouping?.name}</h3>
            {prop.courseGrouping?.courses && prop.courseGrouping.courses.length > 0 && (
                <ul>
                    {prop.courseGrouping.courses.map((course, index) => (
                        <li key={index}>
                            {course.title} - {course.creditValue} credits
                        </li>
                    ))}
                </ul>
            )}
            {prop.courseGrouping?.subGroupings && prop.courseGrouping.subGroupings.length > 0 && (
                <div>
                    {prop.courseGrouping.subGroupings.map((subGroup, index) => (
                        <CourseGroupingComponent key={index} courseGrouping={subGroup} />
                    ))}
                </div>
            )}
        </div>
    );
}
