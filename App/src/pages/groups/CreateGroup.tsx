import { Box, Container, FormControl, FormLabel, Heading, HStack, Input, Stack } from "@chakra-ui/react";
import logo from "../../assets/logo.png";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { GroupCreateDTO, GroupResponseDTO } from "../../services/group";
import { CreateGroupCall } from "../../services/group";
import Button from "../../components/Button";
import { BaseRoutes } from "../../constants";
import { useState } from "react";

export default function CreateGroup() {
    const { handleSubmit } = useForm<GroupCreateDTO>();
    const navigate = useNavigate();
    const [groupName, setGroupName] = useState("");

    function onSubmit(data: GroupCreateDTO) {
        data.name = groupName;
        CreateGroupCall(data)
            .then((res: GroupResponseDTO) => {
                if (res.data != null) {
                    navigate(BaseRoutes.ManageableGroup);
                }
            })
            .catch((err) => {
                console.log(err);
            });
    }
    return (
        <>
            <form onSubmit={handleSubmit(onSubmit)}>
                <Container maxW="lg" py={{ base: "12", md: "24" }} px={{ base: "0", sm: "8" }}>
                    <Stack spacing="8">
                        <Stack spacing="6">
                            <img src={logo} alt="Logo" width="50px" height="50px" style={{ margin: "auto" }} />
                            <Heading textAlign="center" size="lg">
                                Create New Group
                            </Heading>
                        </Stack>
                        <Box
                            py={{ base: "0", sm: "8" }}
                            px={{ base: "4", sm: "10" }}
                            bg={{ base: "transparent", sm: "bg.surface" }}
                            boxShadow={{ base: "none", sm: "2xl" }}
                            borderRadius={{ base: "none", sm: "xl" }}
                        >
                            <Stack spacing="6">
                                <FormControl>
                                    <FormLabel>Name</FormLabel>
                                    <Input
                                        id="groupName"
                                        type="text"
                                        value={groupName}
                                        onChange={(e) => {
                                            setGroupName(e.target.value);
                                        }}
                                    />
                                </FormControl>
                            </Stack>
                        </Box>
                        <HStack justify="space-between">
                            <Button style="primary" type="submit" variant="outline" width="50%" height="40px">
                                Create Group
                            </Button>
                            <Button
                                style="secondary"
                                variant="outline"
                                width="50%"
                                height="40px"
                                onClick={() => {
                                    navigate(BaseRoutes.ManageableGroup);
                                }}
                            >
                                Back
                            </Button>
                        </HStack>
                    </Stack>
                </Container>
            </form>
        </>
    );
}
