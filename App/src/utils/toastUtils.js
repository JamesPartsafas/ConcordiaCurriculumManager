/**
 * Show a toast notification using Chakra UI toast component
 * @param {*} toast Toast component from Chakra UI
 * @param {*} title Toast title
 * @param {*} description Toast description
 * @param {*} status Toast status ("success", "error", "warning", "info")
 * @param {*} position Toast position on screen ("top-right", "top", "top-left", "bottom-right", "bottom", "bottom-left", "left", "right")
 */
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
