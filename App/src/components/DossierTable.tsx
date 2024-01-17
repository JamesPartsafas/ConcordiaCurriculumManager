import {
    Table,
    Thead,
    Tbody,
    Text,
    Tr,
    Th,
    Td,
    TableContainer,
    IconButton,
    Tfoot,
    Spacer,
    Flex,
} from "@chakra-ui/react";
import { Button as ChakraButton } from "@chakra-ui/react";
import { DeleteIcon, EditIcon, InfoIcon, ViewIcon, ArrowDownIcon } from "@chakra-ui/icons";
import { dossierStateToString } from "../models/dossier";
import PropTypes from "prop-types";
import { useState } from "react";

DossierTable.propTypes = {
    myDossiers: PropTypes.array,
    startIndex: PropTypes.number,
    endIndex: PropTypes.number,
    onOpen: PropTypes.func,
    setDossierModalTitle: PropTypes.func,
    setSelectedDossier: PropTypes.func,
    displayDossierModal: PropTypes.func,
    handleNavigateToDossierDetails: PropTypes.func,
    handleNavigateToDossierReview: PropTypes.func,
    handleNavigateToDossierReport: PropTypes.func,
    setCurrentPage: PropTypes.func,
    currentPage: PropTypes.number,
    totalResults: PropTypes.number,
    useIcons: PropTypes.bool,
    reviewIcons: PropTypes.bool,
};
function DossierTable({
    myDossiers: dossiers,
    onOpen,
    setDossierModalTitle,
    setSelectedDossier,
    displayDossierModal,
    handleNavigateToDossierDetails,
    handleNavigateToDossierReview,
    handleNavigateToDossierReport,
    useIcons,
    reviewIcons,
}) {


    const [currentPage, setCurrentPage] = useState<number>(1);
    const totalResults = dossiers.length;
    const resultsPerPage = 5;
    const startIndex = dossiers.length === 0 ? 0 : (currentPage - 1) * resultsPerPage + 1;
    const endIndex = Math.min(currentPage * resultsPerPage, totalResults);


    return (
        <TableContainer borderRadius="xl" boxShadow="xl" border="2px">
            <Table variant="simple" style={{ backgroundColor: "white", tableLayout: "auto" }}>
                <Thead backgroundColor={"#e2e8f0"}>
                    <Tr display={"flex"}>
                        <Th minW={"200px"} maxW={"200px"}>
                            Title
                        </Th>
                        <Th minW={"450px"} maxW={"450px"}>
                            Description
                        </Th>
                        <Th minW={"120px"} maxW={"120px"}>
                            State
                        </Th>
                        <Th width={"25%"}></Th>
                    </Tr>
                </Thead>
                <Tbody>
                    {dossiers.slice(startIndex - 1, endIndex).map((dossier) => (
                        <Tr key={dossier.id} display={"flex"}>
                            <Td minW={"200px"} maxW={"200px"}>
                                {dossier.title}
                            </Td>
                            <Td minW={"450px"} maxW={"450px"}>
                                <Text overflow="hidden" textOverflow="ellipsis" maxW={"500px"}>
                                    {dossier.description}
                                </Text>
                            </Td>
                            <Td minW={"120px"} maxW={"120px"}>
                                {dossierStateToString(dossier)}
                            </Td>

                            <Td width={"25%"}>
                                {useIcons && (
                                    <IconButton
                                        aria-label="Delete"
                                        icon={<DeleteIcon />}
                                        backgroundColor={"#932439"}
                                        color={"white"}
                                        onClick={() => {
                                            setSelectedDossier(dossier);
                                            onOpen();
                                        }}
                                    />
                                )}
                                {useIcons && (
                                    <IconButton
                                        ml={2}
                                        aria-label="Edit"
                                        icon={<EditIcon />}
                                        backgroundColor={"#0072a8"}
                                        color={"white"}
                                        onClick={() => {
                                            setSelectedDossier(dossier);
                                            setDossierModalTitle("edit");
                                            displayDossierModal();
                                        }}
                                    />
                                )}
                                {(useIcons || reviewIcons) && (
                                    <IconButton
                                        ml={2}
                                        aria-label="Details"
                                        icon={<InfoIcon />}
                                        onClick={() => {
                                            setSelectedDossier(dossier);
                                            handleNavigateToDossierDetails(dossier.id);
                                        }}
                                    />
                                )}
                                {reviewIcons && (
                                    <IconButton
                                        ml={2}
                                        aria-label="Review"
                                        icon={<ArrowDownIcon />}
                                        onClick={() => {
                                            setSelectedDossier(dossier);
                                            handleNavigateToDossierReview(dossier.id);
                                        }}
                                    />
                                )}
                                <IconButton
                                    ml={2}
                                    aria-label="Report"
                                    icon={<ViewIcon />}
                                    onClick={() => {
                                        setSelectedDossier(dossier);
                                        handleNavigateToDossierReport(dossier.id);
                                    }}
                                />
                            </Td>
                        </Tr>
                    ))}
                </Tbody>
                <Tfoot>
                    <Tr>
                        <Td height={20}>
                            <Flex>
                                <Text alignSelf="center">
                                    Showing {startIndex} to {endIndex} of {dossiers.length} results
                                </Text>
                                <Spacer />
                                <ChakraButton
                                    mr={4}
                                    p={4}
                                    variant="outline"
                                    onClick={() => setCurrentPage(currentPage - 1)}
                                    isDisabled={startIndex == 1}
                                >
                                    Previous
                                </ChakraButton>
                                <ChakraButton
                                    p={4}
                                    variant="outline"
                                    onClick={() => setCurrentPage(currentPage + 1)}
                                    isDisabled={endIndex == totalResults}
                                >
                                    Next
                                </ChakraButton>
                            </Flex>
                        </Td>
                    </Tr>
                </Tfoot>
            </Table>
        </TableContainer>
    );
}
export default DossierTable;
