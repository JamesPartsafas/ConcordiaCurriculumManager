import {
    Modal,
    ModalBody,
    ModalCloseButton,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalOverlay,
} from "@chakra-ui/react";
import { GroupDTO } from "../services/group";
import Tree from "react-d3-tree";
import Button from "./Button";
import PureSvgNodeElement from "./GroupNodeElement";
import { UserDTO } from "../models/user";

interface ManageableGroupsTreeStructureProps {
    isOpen: boolean;
    onClose: () => void;
    ManageableGroups: GroupDTO[];
}

export default function ManageableGroupsTreeStructure(props: ManageableGroupsTreeStructureProps) {
    const transformDataToTree = (groupsOrUser: GroupDTO[] | UserDTO) => {
        // Helper function to transform a single group or user
        const transformNode = (node) => ({
            name: node.name || node.firstName + " " + node.lastName,
            attributes: {
                size: node.members ? node.members.length : undefined,
                id: node.id,
                email: node.email,
            },
            children: node.members?.map(transformNode),
        });

        // If it's an array of GroupDTO objects
        if (Array.isArray(groupsOrUser)) {
            const root = {
                name: "My Groups",
                children: groupsOrUser.map(transformNode), // Map over each group to transform them
            };
            return root;
        } else {
            // If it's a single UserDTO object
            return transformNode(groupsOrUser); // Transform the single user
        }
    };

    const renderNode = ({ nodeDatum, toggleNode }) => (
        <PureSvgNodeElement
            nodeDatum={nodeDatum}
            toggleNode={toggleNode}
            onNodeClick={() => console.log("Node clicked", nodeDatum)}
        />
    );
    return (
        <Modal isOpen={props.isOpen} onClose={props.onClose}>
            <ModalOverlay />
            <ModalContent maxWidth="95%">
                <ModalHeader>{"Manageable Groups"}</ModalHeader>
                <ModalCloseButton />
                <ModalBody>
                    <div id="treeWrapper" style={{ width: "100%", height: "100vh" }}>
                        <Tree
                            data={transformDataToTree(props.ManageableGroups)}
                            orientation="vertical"
                            pathFunc="step"
                            collapsible={true}
                            translate={{ x: window.innerWidth / 2, y: window.innerHeight / 5 }}
                            nodeSize={{ x: 120, y: 120 }}
                            separation={{ siblings: 1.5, nonSiblings: 1.5 }}
                            renderCustomNodeElement={renderNode}
                        />
                    </div>
                </ModalBody>

                <ModalFooter>
                    <Button colorScheme="blue" mr={3} onClick={props.onClose}>
                        Close
                    </Button>
                </ModalFooter>
            </ModalContent>
        </Modal>
    );
}
