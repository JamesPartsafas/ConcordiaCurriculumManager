import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { Card, Heading, Text } from "@chakra-ui/react";

function useQuery() {
    return new URLSearchParams(useLocation().search);
}

const CourseDetails = () => {
    const query = useQuery(); // Use the useQuery function

    // Access specific query parameters
    const subject: string = query.get("subject");
    const catalogNumber: string = query.get("catalog");
    const [data, setData] = useState([]);

    useEffect(() => {
        fetch("/data.json")
            .then((response) => response.json())
            .then((json) => {
                if (catalogNumber) {
                    const result = json?.Data?.filter((d) => d?.Subject == subject && d?.Catalog == catalogNumber);
                    setData(result);
                } else {
                    const result = json?.Data?.filter((d) => d?.Subject == subject);
                    setData(result);
                }
            });
    }, [subject, catalogNumber]);
    console.log(subject, catalogNumber);
    return (
        <div style={{ maxWidth: "90%", marginLeft: "5%", marginRight: "5%", paddingTop: "30px" }}>
            {data?.map((d) => (
                <Card key={d?.Id} marginBottom={20} padding={15}>
                    <Heading as="h1" size="md" marginBottom={8}>
                        {d?.Subject} {d?.Catalog} {d?.Title} ({parseInt(d?.CreditValue)} credits)
                    </Heading>
                    <Text marginBottom={5}>
                        <Text as="b" marginRight={2}>
                            Prerequisite/Corequisite:
                        </Text>{" "}
                        Permission of the GCS is required.{" "}
                    </Text>
                    <Text marginBottom={5}>
                        <Text as="b" marginRight={2}>
                            Description:
                        </Text>{" "}
                        {d?.Description}{" "}
                    </Text>

                    <Text>
                        <Text as="b" marginRight={2}>
                            {" "}
                            Component(s):
                        </Text>
                        {d?.CourseComponent?.slice(0, -1)?.map((c) => <span key={c?.Id}>{c?.ComponentName},</span>)}
                        {d?.CourseComponent[d?.CourseComponent?.length - 1]?.ComponentName}
                    </Text>
                </Card>
            ))}
        </div>
    );
};

export default CourseDetails;
