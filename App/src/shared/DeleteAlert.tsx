import {
    AlertDialog,
    AlertDialogBody,
    AlertDialogContent,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogOverlay,
} from "@chakra-ui/react";
import Button from "../components/Button";
import React from "react";

interface DeleteAlertProps {
    isOpen: boolean;
    onClose: () => void;
    loading: boolean;
    headerTitle: string;
    title: string;
    item: unknown;
    onDelete: (item) => void;
}

export default function DeleteAlert(props: DeleteAlertProps) {
    const cancelRef = React.useRef();

    return (
        <AlertDialog isOpen={props?.isOpen} leastDestructiveRef={cancelRef} onClose={props?.onClose}>
            <AlertDialogOverlay>
                <AlertDialogContent>
                    <AlertDialogHeader fontSize="lg" fontWeight="bold">
                        {props?.headerTitle}
                    </AlertDialogHeader>

                    <AlertDialogBody>
                        Are you sure you want to delete <b>{props?.title}</b>? You can&apos;t undo this action
                        afterwards.
                    </AlertDialogBody>

                    <AlertDialogFooter>
                        <Button
                            style="secondary"
                            variant="outline"
                            width="fit-content"
                            height="40px"
                            ref={cancelRef}
                            onClick={props?.onClose}
                        >
                            Cancel
                        </Button>
                        <Button
                            style="primary"
                            variant="solid"
                            width="fit-content"
                            height="40px"
                            isLoading={props?.loading}
                            loadingText="Deleting"
                            onClick={() => {
                                props.onDelete(props?.item);
                            }}
                            ml={3}
                        >
                            Delete
                        </Button>
                    </AlertDialogFooter>
                </AlertDialogContent>
            </AlertDialogOverlay>
        </AlertDialog>
    );
}
