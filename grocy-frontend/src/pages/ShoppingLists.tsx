import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/api";

interface ShoppingList {
    id: number;
    name: string;
    userId: number;
    _removing?: boolean;
}

export default function ShoppingLists() {
    const [lists, setLists] = useState<ShoppingList[]>([]);
    const [newListName, setNewListName] = useState("");
    const [error, setError] = useState("");
    const [show, setShow] = useState(false);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const timer = setTimeout(() => setShow(true), 50);
        return () => clearTimeout(timer);
    }, []);

    useEffect(() => {
        const fetchLists = async () => {
            try {
                const token = localStorage.getItem("token");
                const res = await api.get("/ShoppingLists", {
                    headers: { Authorization: `Bearer ${token}` },
                });
                setLists(res.data);
            } catch (err: any) {
                console.error(err.response?.data || err.message || err);
                setError("Kon lijsten niet ophalen.");
            }
        };
        fetchLists();
    }, []);

    const handleAddList = async () => {
        if (!newListName.trim()) return;
        setError("");
        setLoading(true);
        try {
            const token = localStorage.getItem("token");
            const res = await api.post(
                "/ShoppingLists",
                { name: newListName },
                { headers: { Authorization: `Bearer ${token}` } }
            );
            setLists([...lists, res.data]);
            setNewListName("");
            const inputEl = document.getElementById("new-list-input") as HTMLInputElement;
            inputEl?.focus();
        } catch (err) {
            console.error(err);
            setError("Kon lijst niet toevoegen.");
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async (list: ShoppingList) => {
        setError("");
        try {
            const token = localStorage.getItem("token");

            setLists((prev) =>
                prev.map((l) => (l.id === list.id ? { ...l, _removing: true } : l))
            );

            setTimeout(async () => {
                await api.delete(`/ShoppingLists/${list.id}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });
                setLists((prev) => prev.filter((l) => l.id !== list.id));
            }, 200);
        } catch (err) {
            console.error(err);
            setError("Kon lijst niet verwijderen.");
        }
    };

    return (
        <div className="flex h-screen w-screen bg-amber-400 overflow-hidden">
            <div
                className={`m-auto flex flex-col items-center bg-white rounded-2xl shadow-lg px-12 py-8 w-full max-w-2xl h-[80vh] transform transition-all duration-700 ease-out ${
                    show ? "opacity-100 translate-y-0" : "opacity-0 translate-y-10"
                }`}
            >
                <h1 className="text-2xl font-bold mb-6 text-gray-800">Jouw boodschappenlijsten</h1>
                {error && <p className="text-red-500 mb-4">{error}</p>}

                <div className="flex w-full mb-6 space-x-2">
                    <input
                        id="new-list-input"
                        type="text"
                        placeholder="Nieuwe lijst"
                        value={newListName}
                        onChange={(e) => setNewListName(e.target.value)}
                        className="flex-1 px-4 py-2 border border-gray-300 rounded-lg text-black placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500"
                    />
                    <button
                        onClick={handleAddList}
                        disabled={loading}
                        className={`px-4 py-2 rounded-lg text-white ${
                            loading ? "bg-gray-400 cursor-not-allowed" : "bg-blue-600 hover:bg-blue-700"
                        }`}
                    >
                        {loading ? "Toevoegen..." : "Toevoegen"}
                    </button>
                </div>

                {/* Scrollbare container */}
                <div className="w-full flex-1 overflow-y-auto overflow-x-hidden relative">
                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 pb-4">
                        {lists.map((list) => (
                            <div
                                key={list.id}
                                className={`relative flex items-center p-4 bg-white rounded-2xl shadow-md cursor-pointer transform transition-all duration-200 hover:scale-105 hover:shadow-xl
                                    ${list._removing ? "opacity-0 scale-75" : "opacity-100 scale-100"}
                                `}
                            >
                                <div
                                    className="flex items-center flex-1"
                                    onClick={() => navigate(`/lists/${list.id}`)}
                                >
                                    <div className="flex-shrink-0 w-10 h-10 flex items-center justify-center bg-blue-500 text-white font-bold rounded-full mr-4">
                                        {list.name.charAt(0).toUpperCase()}
                                    </div>
                                    <div className="text-gray-800 font-semibold text-lg">{list.name}</div>
                                </div>

                                <button
                                    onClick={(e) => {
                                        e.stopPropagation();
                                        handleDelete(list);
                                    }}
                                    className="absolute top-2 right-2 w-5 h-5 flex items-center justify-center bg-red-500 text-white text-xs font-bold rounded-full hover:bg-red-600"
                                >
                                    ✕
                                </button>
                            </div>
                        ))}
                    </div>

                    {/* Subtiele scrollbar alleen voor de verticale container */}
                    <style jsx>{`
                        .overflow-y-auto::-webkit-scrollbar {
                            width: 6px;
                        }
                        .overflow-y-auto::-webkit-scrollbar-track {
                            background: transparent;
                        }
                        .overflow-y-auto::-webkit-scrollbar-thumb {
                            background-color: rgba(107, 114, 128, 0.4);
                            border-radius: 3px;
                        }
                        .overflow-y-auto:hover::-webkit-scrollbar-thumb {
                            background-color: rgba(107, 114, 128, 0.7);
                        }
                    `}</style>
                </div>
            </div>
        </div>
    );
}
