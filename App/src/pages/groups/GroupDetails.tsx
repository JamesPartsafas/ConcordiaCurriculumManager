import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { GetGroupByID, GroupDTO } from "../../services/group";
import { Box, Heading, Text, Card } from "@chakra-ui/react";
import { BaseRoutes } from "../../constants";
import Button from "../../components/Button";
import { useNavigate } from "react-router-dom";

function GroupDetails() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const groupId = queryParams.get("groupId");
    const [group, setGroup] = useState<GroupDTO | null>(null);
    const navigate = useNavigate();
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
        <Box style={{ maxWidth: "50%", marginLeft: "25%", marginRight: "25%", paddingTop: "100px" }}>
            <Card marginBottom={20} padding={15} style={{ backgroundColor: "#e9e3d3" }}>
                <Heading as="h1" size="md" marginBottom={8}>
                    Group Details
                </Heading>
                <Text marginBottom={5}>
                    <Text as="b" marginRight={2}>
                        Group Name:
                    </Text>{" "}
                    {group.name}
                </Text>
                <Text marginBottom={5}>
                    <Text as="b" marginRight={2}>
                        Number of Members:
                    </Text>{" "}
                    {group.members.length}
                </Text>
                <Text marginBottom={5}>
                    <Text as="b" marginRight={2}>
                        Members:
                    </Text>{" "}
                    {group.members.map((member, index) => (
                        <React.Fragment key={member.id}>
                            {index > 0 && ", "}
                            {member.firstName} {""} {member.lastName}
                        </React.Fragment>
                    ))}
                </Text>
                <Text marginBottom={5}>
                    <Text as="b" marginRight={2}>
                        emails:
                    </Text>{" "}
                    {group.members.map((member, index) => (
                        <React.Fragment key={member.id}>
                            {index > 0 && ", "}
                            {member.email}
                        </React.Fragment>
                    ))}
                </Text>
                <Text marginBottom={5}>
                    <Text as="b" marginRight={2}>
                        Number of Masters:
                    </Text>{" "}
                    {group.groupMasters.length}
                </Text>
                <Text marginBottom={5}>
                    <Text as="b" marginRight={2}>
                        Masters:
                    </Text>{" "}
                    {group.groupMasters.map((master, index) => (
                        <React.Fragment key={master.id}>
                            {index > 0 && ", "}
                            {master.firstName}
                        </React.Fragment>
                    ))}
                </Text>
                <Button
                    style="secondary"
                    variant="solid"
                    width="240px"
                    height="40px"
                    margin="auto"
                    onClick={() => navigate(BaseRoutes.allGroups)}
                >
                    Go back to all Groups
                </Button>
            </Card>
        </Box>
    );
}

export default GroupDetails;
