import axios from "axios";

interface SubmitDossierForReviewDTO {
    dossierId: string;
    groupIds: string[];
}

interface ReviewDossierDTO {
    message: string;
    groupId: string;
    parentDiscussionMessageId?: string;
}

export function submitDossierForReview(dossierId: string, dossierForReviewDTO: SubmitDossierForReviewDTO) {
    return axios.post(`/DossierReview/SubmitDossierForReview/${dossierId}`, dossierForReviewDTO);
}

export function rejectDossier(dossierId: string): Promise<void> {
    return axios.put(`/DossierReview/RejectDossier/${dossierId}`);
}

export function returnDossier(dossierId: string): Promise<void> {
    return axios.put(`/DossierReview/ReturnDossier/${dossierId}`);
}

export function forwardDossier(dossierId: string): Promise<void> {
    return axios.put(`/DossierReview/ForwardDossier/${dossierId}`);
}

export function reviewDossier(dossierId: string, reviewDossierDTO: ReviewDossierDTO) {
    return axios.post(`/DossierReview/ReviewDossier/${dossierId}`, reviewDossierDTO);
}