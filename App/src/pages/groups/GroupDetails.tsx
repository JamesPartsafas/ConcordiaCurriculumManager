import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { GetGroupByID, GroupDTO } from "../../services/group";
import { Box, Heading, Text, UnorderedList, ListItem } from "@chakra-ui/react";

function GroupDetails() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const groupId = queryParams.get("groupId");
    const [group, setGroup] = useState<GroupDTO | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchGroupDetails = async () => {
            try {
                const response = await GetGroupByID(groupId);
                setGroup(response.data);
                setLoading(false);
            } catch (error) {
                console.error("Error fetching group details:", error);
                setLoading(false);
            }
        };

        if (groupId) {
            fetchGroupDetails();
        }
    }, [groupId]);

    if (!groupId) {
        return <div>Group ID not provided!</div>;
    }

    if (loading) {
        return <div>Loading...</div>;
    }

    if (!group) {
        return <div>Group not found!</div>;
    }

    return (
        <Box p={4}>
            <Heading as="h1" mb={4}>
                Group Details
            </Heading>
            <Text mb={2}>Group Name: {group.name}</Text>
            <Text mb={2}>Number of Members: {group.members.length}</Text>
            <Text mb={2}>Members:</Text>
            <UnorderedList>
                {group.members.map((member) => (
                    <ListItem key={member.id}>{member.firstName}</ListItem>
                ))}
            </UnorderedList>
            <Text mt={4} mb={2}>
                Number of Masters: {group.groupMasters.length}
            </Text>
            <Text mb={2}>Masters:</Text>
            <UnorderedList>
                {group.groupMasters.map((master) => (
                    <ListItem key={master.id}>{master.firstName}</ListItem>
                ))}
            </UnorderedList>
            {/* Add more details as needed */}
        </Box>
    );
}

export default GroupDetails;
