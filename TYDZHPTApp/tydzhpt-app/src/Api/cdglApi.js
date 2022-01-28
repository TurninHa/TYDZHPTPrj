import { post, get } from "../httpaxios/request";

export const cdgl = data => {
    return new Promise((resolve, reject) => {
        post("/api/menu/page", data).then(resp => resolve(resp)).catch(er => reject(er));
    });
}

export const getModel = (id = 0) => {
    return new Promise((resolve, reject) => {
        get("/api/menu/model", { id }).then(data => {
            resolve(data);
        }).catch(er => {
            reject(er);
        });
    });
}

export const save = (sqCdb = {}) => {
    return new Promise((resolve, reject) => {
        post("/api/menu/save", sqCdb).then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}

export const menuTree = () => {
    return new Promise((resolve, reject) => {
        get("/api/menu/menutree").then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}