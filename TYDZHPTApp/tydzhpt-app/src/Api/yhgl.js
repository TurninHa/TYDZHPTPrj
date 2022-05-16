import { get, post } from "../httpaxios/request";

export const getUserList = (search = {}) => {
    return new Promise((resolve, reject) => {

        post("/api/User/list", search).then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}

export const getModel = (id) => {
    return new Promise((resolve, reject) => {

        get("/api/User/model", { id }).then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}

export const saveUser = (formData = {}) => {
    return new Promise((resolve, reject) => {

        post("/api/User/save", formData).then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}

export const deleteUser = (id) => {
    return new Promise((resolve, reject) => {

        post("/api/User/delete", { id }).then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}

export const disEnUser = (disenData={}) => {
    return new Promise((resolve, reject) => {

        post("/api/User/disablen", disenData).then(resp => {
            resolve(resp);
        }).catch(er => {
            reject(er);
        });
    });
}