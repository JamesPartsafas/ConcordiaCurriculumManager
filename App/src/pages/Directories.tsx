import { useEffect, useState } from "react";
import { UserDTO } from "../models/user";
import { searchUsersByEmail, searchUsersByFirstname, searchUsersByLastname } from "../services/user";

export default function Directories() {
    const [users, setUsers] = useState<UserDTO[]>([]);

    useEffect(() => {}, []);

    function getUsersByFirstName(firstName: string) {
        searchUsersByFirstname(firstName)
            .then(
                (response) => {
                    console.log(response.data);
                    setUsers((prev) => [...prev, ...response.data]);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    function getUsersByLastName(lastName: string) {
        searchUsersByLastname(lastName)
            .then(
                (response) => {
                    console.log(response.data);
                    setUsers((prev) => [...prev, ...response.data]);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    function getUsersByEmail(email: string) {
        searchUsersByEmail(email)
            .then(
                (response) => {
                    console.log(response.data);
                    setUsers((prev) => [...prev, ...response.data]);
                },
                (rej) => {
                    console.log(rej);
                }
            )
            .catch((err) => {
                console.log(err);
            });
    }

    return (
        <div>
            <h1>Directories</h1>
        </div>
    );
}
