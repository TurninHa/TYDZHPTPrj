import { get, post } from "../httpaxios/request";

export const getRoleList = (search = {}) => {
    return new Promise((resolve, reject) => {

        post("/api/Role/list", search).then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}

export const getModel = (id) => {
    return new Promise((resolve, reject) => {

        get("/api/Role/model", { id }).then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}

export const saveRole = (formData = {}) => {
    return new Promise((resolve, reject) => {

        post("/api/Role/save", formData).then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}

export const deleteRole = (id) => {
    return new Promise((resolve, reject) => {

        post("/api/Role/delete", { id }).then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}