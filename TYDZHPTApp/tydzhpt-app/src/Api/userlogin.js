import { post } from "../httpaxios/request";

export const userLogin = data => {
    return new Promise((resolve, reject) => {
        post("/api/user/login", data).then(resp => resolve(resp.data)).catch(er => reject(er));
    });
}