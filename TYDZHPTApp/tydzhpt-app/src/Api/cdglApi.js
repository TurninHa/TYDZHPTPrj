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

export const delOne = (id = 0) => {
    if (id <= 0)
        return;
    return new Promise((resolve, reject) => {
        post("/api/menu/delete", { id }).then(response => {
            resolve(response);
        }).catch(er => {
            reject(er);
        });
    });

};

export const getOperateFuncList = (cdId = 0) => {
    return new Promise((resolve, reject) => {
        get("/api/Operation/list", { cdId }).then(response => {
            resolve(response);
        }).catch(er => {
            reject(er);
        });
    });

};

export const saveCzGn = (data = {}) => {
    return new Promise((resolve, reject) => {
        post("/api/Operation/save", data).then(resp => resolve(resp))
            .catch(er => {
                reject(er);
            });
    });
};

export const deleteCzgn = (id = 0) => {
    return new Promise((resolve, reject) => {
        post("/api/Operation/delete", { id }).then(resp => resolve(resp))
            .catch(er => {
                reject(er);
            });
    });
};