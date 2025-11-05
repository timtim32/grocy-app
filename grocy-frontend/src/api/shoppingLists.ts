import api from "./api";

export async function getShoppingLists() {
    const res = await api.get("/ShoppingLists");
    return res.data;
}

export async function createShoppingList(list: any) {
    const res = await api.post("/ShoppingLists", list);
    return res.data;
}
