import * as signalR from "@microsoft/signalr";
import { decodeTokenToUser } from "../services/auth";
import { DossierDiscussionMessage } from "../models/dossier";

export interface SignalRManagerProps {
    handleMessageReceived: (dossierId: string, message: DossierDiscussionMessage) => void;
    handleError: (error: string) => void;
}

export class SignalRManager {
    private connection: signalR.HubConnection | null = null;
    private accessToken: string | null = null;
    private baseUrl: string = import.meta.env.VITE_API_URL as string;

    constructor(
        private dossierId: string,
        private props: SignalRManagerProps
    ) {
        const token = localStorage.getItem("token");

        if (token === null) {
            this.props.handleError("Access token not found");
        } else {
            const user: User = decodeTokenToUser(token);

            if (user.expiresAtTimestamp * 1000 < Date.now()) {
                this.props.handleError("Access token expired");
            } else {
                this.accessToken = token;
            }
        }
    }

    startConnection() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${this.baseUrl}/ws/DossierReview?dossierId=${this.dossierId}`, {
                accessTokenFactory: () => this.accessToken,
                transport: signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.LongPolling
            })
            .configureLogging(signalR.LogLevel.Critical)
            .build();

        this.connection.on("ReviewDossier", (dossierId: string, message: DossierDiscussionMessage) => {
            this.props.handleMessageReceived(dossierId, message);
        });

        this.connection.onclose((err) => {
            console.log("SignalR closed");

            if (err) {
                console.log(err);
            }
        });

        this.connection.on("Error", (message: string) => {
            this.props.handleError(message);
        });

        return this.connection
            .start()
            .then(() => {
                console.log("SignalR connected");
            })
            .catch((err) => {
                this.props.handleError(err.toString());
            });
    }

    endConnection() {
        if (this.connection) {
            this.connection.stop();
        }
    }

    sendMessage(message: DossierDiscussionMessage) {
        if (this.connection) {
            this.connection.invoke("ReviewDossier", this.dossierId, message).catch((err) => {
                this.props.handleError(err.toString());
            });
        }
    }
}
