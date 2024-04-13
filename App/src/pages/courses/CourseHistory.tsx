import { useEffect } from "react";
import { getCourseHistory } from "../../services/course";

export default function CourseHistory() {
    useEffect(() => {
        fetchCourseHistory("COMP", 335);
    }, []);
    function fetchCourseHistory(subject: string, catalog: number) {
        getCourseHistory(subject, catalog).then((res) => {
            console.log(res.data);
        });
    }
    return (
        <>
            <h1>Hellow course history</h1>
        </>
    );
}
