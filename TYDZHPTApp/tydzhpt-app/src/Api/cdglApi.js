import { post } from "../httpaxios/request";

export const cdgl = data => {
    return new Promise((resolve, reject) => {
        post("/api/menu/page", data).then(resp => resolve(resp)).catch(er => reject(er));
    });
}