import { useContext } from "react";
import { UserContext } from "../App";
import Button from "../components/Button";

export default function Home() {
    const user = useContext(UserContext);
    return (
        <div>
            <h1
                style={{
                    margin: "5%",
                    fontWeight: "bold",
                    fontSize: "24px",
                    color: "#FF8888",
                    textAlign: "center",
                }}
            >
                Hello {user?.firstName}, Welcome to the Concordia Curriculum Manager!
            </h1>

            <h2
                style={{
                    marginBottom: "20px",
                    fontWeight: "bold",
                    fontSize: "24px",
                    color: "#FF8888",
                    textAlign: "center",
                }}
            >
                Dossier List: <Button type="primary"></Button>
            </h2>
        </div>
    );
}
