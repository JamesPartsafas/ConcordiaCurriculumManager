import { Box, Flex, FormControl, FormLabel, Input, Select, useDisclosure, Text } from "@chakra-ui/react";
import Button from "../../components/Button";
import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { searchDossiersByGuid, searchDossiersByState, searchDossiersByTitle } from "../../services/dossier";
import { DossierDTO, DossierStateEnum, GetMyDossiersResponse } from "../../models/dossier";
import DossierTable from "../../components/DossierTable";
import { BaseRoutes } from "../../constants";
import { GetAllGroups, GroupDTO, MultiGroupResponseDTO } from "../../services/group";

export default function DossierBrowser() {
    const [searchInput, setSearchInput] = useState<string>();
    const [mySearchedDossiers, setMySearchedDossiers] = useState<DossierDTO[]>([]);
    const navigate = useNavigate();
    const handleChange = (event) => setSearchInput(event.target.value);
    const { onOpen } = useDisclosure();
    const selectedStateRef = useRef<HTMLSelectElement>(null);
    const selectedGroupRef = useRef<HTMLSelectElement>(null);
    const [selectedState, setSelectedState] = useState<string>("Created");
    const [selectedGroup, setSelectedGroup] = useState<string>();
    const [selectedDossier, setSelectedDossier] = useState<DossierDTO | null>(null);
    const [myGroups, setMyGroups] = useState<GroupDTO[]>([]);
    const [changer, setChanger] = useState<DossierStateEnum>(DossierStateEnum.Created);

    function searchDossiersFromTitle(input: string) {
        console.log("Searching for " + input);
        searchDossiersByTitle(input)
            .then((res: GetMyDossiersResponse) => {
                console.log(JSON.stringify(res.data));
                setMySearchedDossiers(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    }

    useEffect(() => {
        if (selectedState == "Created") {
            setChanger(DossierStateEnum.Created);
        }
        if (selectedState == "InReview") {
            setChanger(DossierStateEnum.InReview);
        }
        if (selectedState == "Rejected") {
            setChanger(DossierStateEnum.Rejected);
        }
        if (selectedState == "Approved") {
            setChanger(DossierStateEnum.Approved);
        }
    }, [selectedState]);

    useEffect(() => {
        GetAllGroups()
            .then((res: MultiGroupResponseDTO) => {
                setMyGroups(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    }, []);

    function searchDossiersFromState() {
        console.log("Searching for State " + changer);
        searchDossiersByState(changer)
            .then((res: GetMyDossiersResponse) => {
                console.log(JSON.stringify(res.data));
                setMySearchedDossiers(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    }

    function searchDossiersFromGroup(guid: string) {
        console.log("Searching for Group with id " + guid);
        searchDossiersByGuid(guid)
            .then((res: GetMyDossiersResponse) => {
                console.log(JSON.stringify(res.data));
                setMySearchedDossiers(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    }

    function handleNavigateToDossierDetails(dossierId: string) {
        navigate(BaseRoutes.DossierDetails.replace(":dossierId", dossierId));
    }

    function handleNavigateToDossierReport(dossierId: string) {
        navigate(BaseRoutes.DossierReport.replace(":dossierId", dossierId));
    }

    function handleNavigateToDossierReview(dossierId: string) {
        navigate(BaseRoutes.DossierReview.replace(":dossierId", dossierId));
    }

    return (
        <div>
            <Box maxW="5xl" m="auto">
                <Flex flexDirection="column">
                    <Text textAlign="center" fontSize="3xl" fontWeight="bold" marginTop="7%" marginBottom="5">
                        Dossier Browser
                    </Text>
                    <FormControl>
                        <FormLabel htmlFor="search-text">Search By Title:</FormLabel>
                        <Input id="searcher" type="text" value={searchInput} onChange={handleChange} />
                        <Button
                            mt={4}
                            mb={2}
                            style="secondary"
                            variant="outline"
                            width="22%"
                            height="40px"
                            onClick={() => {
                                searchDossiersFromTitle(searchInput);
                                console.log(JSON.stringify(selectedDossier));
                            }}
                        >
                            Search for Title
                        </Button>
                        <FormLabel htmlFor="search-state">Search By State:</FormLabel>
                        <Select
                            ref={selectedStateRef}
                            value={selectedState}
                            onChange={(e) => setSelectedState(e.target.value)}
                        >
                            <option
                                key={0}
                                value={"Created"} // Store the entire component object as a string
                            >
                                Created
                            </option>
                            <option
                                key={1}
                                value={"InReview"} // Store the entire component object as a string
                            >
                                InReview
                            </option>
                            <option
                                key={2}
                                value={"Rejected"} // Store the entire component object as a string
                            >
                                Rejected
                            </option>
                            <option
                                key={3}
                                value={"Approved"} // Store the entire component object as a string
                            >
                                Approved
                            </option>
                        </Select>
                        <Button
                            mt={4}
                            mb={2}
                            style="secondary"
                            variant="outline"
                            width="22%"
                            height="40px"
                            onClick={() => {
                                searchDossiersFromState();
                                console.log(JSON.stringify(selectedDossier));
                            }}
                        >
                            Search for State
                        </Button>
                        <FormLabel htmlFor="search-state">Search By Group:</FormLabel>
                        <Select
                            ref={selectedGroupRef}
                            value={selectedGroup}
                            onChange={(e) => setSelectedGroup(e.target.value)}
                        >
                            {myGroups.map((group, index) => (
                                <option key={index} value={group.id}>
                                    {group.name}
                                </option>
                            ))}
                        </Select>
                        <Button
                            mt={4}
                            mb={2}
                            style="secondary"
                            variant="outline"
                            width="22%"
                            height="40px"
                            onClick={() => {
                                searchDossiersFromGroup(selectedGroup);
                                console.log(JSON.stringify(selectedDossier));
                            }}
                        >
                            Search for Group
                        </Button>
                    </FormControl>
                    <div style={{ margin: "5px" }}>
                        <DossierTable
                            myDossiers={mySearchedDossiers}
                            onOpen={onOpen}
                            handleNavigateToDossierDetails={handleNavigateToDossierDetails}
                            setSelectedDossier={setSelectedDossier}
                            handleNavigateToDossierReport={handleNavigateToDossierReport}
                            handleNavigateToDossierReview={handleNavigateToDossierReview}
                            useIcons={false}
                            reviewIcons={true}
                        />
                    </div>
                </Flex>
                <Button
                    style="primary"
                    variant="outline"
                    height="40px"
                    width="fit-content"
                    onClick={() => navigate(BaseRoutes.Home)}
                >
                    Return to Home
                </Button>
            </Box>
        </div>
    );
}
