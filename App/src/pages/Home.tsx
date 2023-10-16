import { useContext } from "react";
import { UserContext } from "../App";
import Button from "../components/Button";
import { BaseRoutes } from "../constants";
import { useNavigate } from "react-router-dom";
import { Container } from "@chakra-ui/react";

export default function Home() {
    const navigate = useNavigate();
    const user = useContext(UserContext);
    return (
        <div>
            <Container maxW="80%" py={{ base: "12", md: "24" }} px={{ base: "0", sm: "8" }}>
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
                        margin: "5%",
                        fontWeight: "bold",
                        fontSize: "24px",
                        color: "#FF8888",
                        textAlign: "center",
                    }}
                >
                    Dossier List:{" "}
                    <Button
                        type="primary"
                        variant="outline"
                        width="20%"
                        height="40px"
                        margin="5%"
                        onClick={() => navigate(BaseRoutes.Dossiers)}
                    >
                        View Dossier List
                    </Button>
                </h2>

                <h2
                    style={{
                        margin: "20px",
                        fontWeight: "bold",
                        fontSize: "24px",
                        color: "#FF8888",
                        textAlign: "center",
                    }}
                >
                    View your Groups{" "}
                    <Button
                        type="primary"
                        variant="outline"
                        width="20%"
                        height="40px"
                        margin="5%"
                        onClick={() => navigate(BaseRoutes.Dossiers)}
                    >
                        View Groups List
                    </Button>
                </h2>
            </Container>
        </div>
    );
}
