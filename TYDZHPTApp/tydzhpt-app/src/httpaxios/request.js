import axios from "./AxiosInterceptor"

export function post(uri = "", data, isFormData = false) {

    let config = {};
    if (isFormData)
        config = {
            headers: { "Content-Type": "multipart/form-data" }
        };
    return new Promise((resolve, reject) => {
        axios.post(uri, data, config).then(response => {
            resolve(response);
        }).catch(er => {
            reject(er);
        });
    });
}

export function get(uri = "", params) {
    
    return new Promise((resolve, reject) => {
        axios.get(uri, {
            params: params
        }).then(response => {
            resolve(response);
        }).catch(er => {
            reject(er);
        });
    });
}