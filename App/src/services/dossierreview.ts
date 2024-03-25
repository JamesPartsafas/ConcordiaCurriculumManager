import axios from "axios";
import { DiscussionMessageVoteEnum } from "../models/dossier";

interface SubmitDossierForReviewDTO {
    dossierId: string;
    groupIds: string[];
}

interface ReviewDossierDTO {
    message: string;
    groupId: string;
    parentDiscussionMessageId?: string;
}

interface EditReviewMessageDTO {
    discussionMessageId: string;
    newMessage: string;
}

interface VoteReviewMessageDTO {
    discussionMessageId: string;
    value: DiscussionMessageVoteEnum;
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

export function editReviewMessage(dossierId: string, editReviewMessageDTO: EditReviewMessageDTO) {
    return axios.put(`/DossierReview/EditReviewMessage/${dossierId}`, editReviewMessageDTO);
}

export function voteDossierMessage(dossierId: string, dto: VoteReviewMessageDTO) {
    return axios.post(`/DossierReview/VoteReviewMessage/${dossierId}`, dto);
}

export function deleteReviewMessage(dossierId: string, messageId: string) {
    return axios.delete(`/DossierReview/DeleteReviewMessage/${dossierId}/${messageId}`);
}
