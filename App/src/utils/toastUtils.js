export const showToast = (toast, title, description, status = "info", position = "top-right") => {
    toast({
        title,
        description,
        status, // "success", "error", "warning", "info"
        duration: 3000,
        isClosable: true,
        position, // "top-right", "top", "top-left", "bottom-right", "bottom", "bottom-left", "left", "right"
    });
};