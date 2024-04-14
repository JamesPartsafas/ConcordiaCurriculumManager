import { useEffect } from "react";
import { getCourseHistory } from "../../services/course";
import { getCourseGroupingHistory } from "../../services/courseGrouping";

export default function CourseHistory() {
    useEffect(() => {
        fetchCourseHistory("COMP", 335);
    }, []);
    function fetchCourseHistory(subject: string, catalog: number) {
        getCourseHistory(subject, catalog).then((res) => {
            console.log(res.data);
        });
        getCourseGroupingHistory("4a8a12cd-1555-4d69-931b-40bae1951c18").then((res) => {
            console.log(res);
        });
    }
    return (
        <>
            <h1>Hellow course history</h1>
        </>
    );
}
