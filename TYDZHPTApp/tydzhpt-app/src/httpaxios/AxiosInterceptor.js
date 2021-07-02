import axios from "axios"

axios.defaults.baseURL = "http://localhost:5000";
axios.defaults.timeout = 10000;

axios.interceptors.request.use(requestConfig => {
    console.log("requestConfig",requestConfig.data);
    requestConfig.headers["Content-Type"]="application/json";
    requestConfig.headers["Authorization"] = "bearer "+ sessionStorage.getItem("token");
    return requestConfig;
}, er => {
    return Promise.reject(er);
});

axios.interceptors.response.use(responseConfig=>{
    console.log("responseConfig",responseConfig.data);
    return responseConfig;
},er=>{
    if(er.toString().indexOf("401")>=0){
        window.location.href="/Login";
        return Promise.reject(er);
    }
    else 
        return Promise.reject(er);
});

export default axios;