export default function Home() {
    const user = useContext(UserContext);
    return <h1>Hello {user?.firstName}</h1>;
}
