import React from "react";
import PropTypes from "prop-types";
import { Tree } from "react-d3-tree";

import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    ModalCloseButton,
    Button,
} from "@chakra-ui/react";
import { CourseGroupingDTO } from "../../models/courseGrouping";
import PureSvgNodeElement from "./NodeElement";

const CourseGroupingTreeStructure = ({ isOpen, onClose, courseGrouping }) => {
    const transformDataToTree = (courseGrouping: CourseGroupingDTO) => {
        const transform = (node: CourseGroupingDTO) => ({
            name: node.name,
            attributes: {
                credits: node.requiredCredits,
                courseGroupingId: node.id,
            },
            children: node.subGroupings.map(transform),
        });

        return transform(courseGrouping);
    };
    const renderNode = ({ nodeDatum, toggleNode }) => (
        <PureSvgNodeElement
            nodeDatum={nodeDatum}
            toggleNode={toggleNode}
            onNodeClick={() => console.log("Node clicked", nodeDatum)}
        />
    );
    return (
        <Modal isOpen={isOpen} onClose={onClose}>
            <ModalOverlay />
            <ModalContent maxWidth="95%">
                <ModalHeader>{courseGrouping?.name || "Course Grouping Details"}</ModalHeader>
                <ModalCloseButton />
                <ModalBody>
                    <div id="treeWrapper" style={{ width: "100%", height: "100vh" }}>
                        <Tree
                            data={transformDataToTree(courseGrouping)}
                            orientation="vertical"
                            pathFunc="step"
                            collapsible={true}
                            translate={{ x: window.innerWidth / 2, y: window.innerHeight / 5 }}
                            nodeSize={{ x: 200, y: 200 }}
                            separation={{ siblings: 1.5, nonSiblings: 1.5 }}
                            renderCustomNodeElement={renderNode}
                        />
                    </div>
                </ModalBody>

                <ModalFooter>
                    <Button colorScheme="blue" mr={3} onClick={onClose}>
                        Close
                    </Button>
                </ModalFooter>
            </ModalContent>
        </Modal>
    );
};

export default CourseGroupingTreeStructure;

CourseGroupingTreeStructure.propTypes = {
    courseGrouping: PropTypes.object.isRequired,
    isOpen: PropTypes.bool.isRequired,
    onClose: PropTypes.func.isRequired,
};
