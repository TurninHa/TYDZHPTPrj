import { post } from "../httpaxios/request";

export const userLogin = data => {
    return new Promise((resolve, reject) => {
        post("/api/user/login", data).then(resp => resolve(resp)).catch(er => reject(er));
    });
}