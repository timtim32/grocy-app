import { useState, useEffect } from "react";
import api from "../api/api";

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const [show, setShow] = useState(false);

    useEffect(() => {
        const timer = setTimeout(() => setShow(true), 50);
        return () => clearTimeout(timer);
    }, []);

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const res = await api.post("/Auth/login", { email, password });
            localStorage.setItem("token", res.data.token);
            window.location.href = "/ShoppingLists";
        } catch {
            setError("Ongeldig e-mailadres of wachtwoord");
        }
    };

    return (
        <div className="flex h-screen w-screen bg-amber-400 overflow-hidden">
            <div
                className={`m-auto flex flex-col items-center bg-white rounded-2xl shadow-lg px-8 py-8 w-full max-w-2xl transform transition-all duration-700 ease-out ${
                    show ? "opacity-100 translate-y-0" : "opacity-0 translate-y-10"
                }`}
            >
                <h2 className="text-2xl font-bold mb-6 text-gray-800">Inloggen</h2>
                <form onSubmit={handleLogin} className="flex flex-col w-full space-y-4">
                    <input
                        type="email"
                        placeholder="E-mailadres"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        className="px-4 py-2 border border-gray-300 rounded-lg text-black placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500"
                    />
                    <input
                        type="password"
                        placeholder="Wachtwoord"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        className="px-4 py-2 border border-gray-300 rounded-lg text-black placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500"
                    />
                    <button
                        type="submit"
                        className="w-full py-2 bg-blue-600 text-white font-semibold rounded-lg hover:bg-blue-700 transition"
                    >
                        Inloggen
                    </button>
                </form>
                {error && <p className="mt-4 text-red-500 text-sm">{error}</p>}
            </div>
        </div>
    );
}
