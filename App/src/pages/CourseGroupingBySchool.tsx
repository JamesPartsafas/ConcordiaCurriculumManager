import { Select, Text } from "@chakra-ui/react";
import { useEffect, useRef, useState } from "react";
import { SchoolEnum } from "../models/courseGrouping";
export default function groupingBySchool() {
    const selectedSchoolRef = useRef<HTMLSelectElement>(null);
    const [selectedSchool, setSelectedSchool] = useState<string>("GinaCody");
    const [changer, setChanger] = useState<SchoolEnum>(SchoolEnum.GinaCody);
    useEffect(() => {
        if (selectedSchool == "GinaCody") {
            setChanger(SchoolEnum.GinaCody);
        }
        if (selectedSchool == "InReview") {
            setChanger(SchoolEnum.ArtsAndScience);
        }
        if (selectedSchool == "Rejected") {
            setChanger(SchoolEnum.FineArts);
        }
        if (selectedSchool == "Approved") {
            setChanger(SchoolEnum.JMSB);
        }
    }, [selectedSchool]);
    return (
        <div>
            <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                Curriculum by School
            </Text>
            <Select ref={selectedSchoolRef} value={selectedSchool} onChange={(e) => setSelectedSchool(e.target.value)}>
                <option
                    key={0}
                    value={"GinaCody"} // Store the entire component object as a string
                >
                    GinaCody
                </option>
                <option
                    key={1}
                    value={"ArtsAndScience"} // Store the entire component object as a string
                >
                    ArtsAndScience
                </option>
                <option
                    key={2}
                    value={"FineArts"} // Store the entire component object as a string
                >
                    FineArts
                </option>
                <option
                    key={3}
                    value={"JMSB"} // Store the entire component object as a string
                >
                    JMSB
                </option>
            </Select>
        </div>
    );
}
