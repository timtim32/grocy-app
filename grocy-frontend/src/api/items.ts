import api from "./api";

export async function getItems(listId: string) {
    const res = await api.get(`/Items/${listId}`);
    return res.data;
}

export async function createItem(listId: string, item: any) {
    const res = await api.post(`/Items/${listId}`, item);
    return res.data;
}

export async function updateItem(id: string, item: any) {
    const res = await api.put(`/Items/${id}`, item);
    return res.data;
}

export async function deleteItem(id: string) {
    const res = await api.delete(`/Items/${id}`);
    return res.data;
}
