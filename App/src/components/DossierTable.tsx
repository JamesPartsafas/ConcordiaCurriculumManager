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
    Tooltip,
} from "@chakra-ui/react";
import { Button as ChakraButton } from "@chakra-ui/react";
import { DeleteIcon, EditIcon, InfoIcon, ViewIcon, ArrowDownIcon } from "@chakra-ui/icons";
import { dossierStateToString } from "../models/dossier";
import PropTypes from "prop-types";

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
    myDossiers,
    startIndex,
    endIndex,
    onOpen,
    setDossierModalTitle,
    setSelectedDossier,
    displayDossierModal,
    handleNavigateToDossierDetails,
    handleNavigateToDossierReview,
    handleNavigateToDossierReport,
    setCurrentPage,
    currentPage,
    totalResults,
    useIcons,
    reviewIcons,
}) {
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
                    {myDossiers.slice(startIndex - 1, endIndex).map((dossier) => (
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
                                    <Tooltip label="Delete This Dossier">
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
                                    </Tooltip>
                                )}
                                {useIcons && (
                                    <Tooltip label="Edit This Dossier">
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
                                    </Tooltip>
                                )}
                                {(useIcons || reviewIcons) && (
                                    <Tooltip label="Dossier Details">
                                        <IconButton
                                            ml={2}
                                            aria-label="Details"
                                            icon={<InfoIcon />}
                                            onClick={() => {
                                                setSelectedDossier(dossier);
                                                handleNavigateToDossierDetails(dossier.id);
                                            }}
                                        />
                                    </Tooltip>
                                )}
                                {reviewIcons && (
                                    <Tooltip label="Reviews of This Dossier">
                                        <IconButton
                                            ml={2}
                                            aria-label="Review"
                                            icon={<ArrowDownIcon />}
                                            onClick={() => {
                                                setSelectedDossier(dossier);
                                                handleNavigateToDossierReview(dossier.id);
                                            }}
                                        />
                                    </Tooltip>
                                )}
                                <Tooltip label="Dossier Report">
                                    <IconButton
                                        ml={2}
                                        aria-label="Report"
                                        icon={<ViewIcon />}
                                        onClick={() => {
                                            setSelectedDossier(dossier);
                                            handleNavigateToDossierReport(dossier.id);
                                        }}
                                    />
                                </Tooltip>
                            </Td>
                        </Tr>
                    ))}
                </Tbody>
                <Tfoot>
                    <Tr>
                        <Td height={20}>
                            <Flex>
                                <Text alignSelf="center">
                                    Showing {startIndex} to {endIndex} of {myDossiers.length} results
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
