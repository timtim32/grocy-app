// src/api/api.ts
import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:5076/api", // jouw backend URL
    headers: {
        "Content-Type": "application/json",
    },
});

// Voeg token automatisch toe als die aanwezig is
api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;
