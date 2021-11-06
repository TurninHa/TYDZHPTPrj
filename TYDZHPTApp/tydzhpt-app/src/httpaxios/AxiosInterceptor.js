import axios from "axios"

axios.defaults.baseURL = "http://localhost:5612";
axios.defaults.timeout = 10000;

axios.interceptors.request.use(requestConfig => {
    console.log("requestConfig",requestConfig.data);
    requestConfig.headers["Content-Type"]="application/json";
    let user = JSON.parse( sessionStorage.getItem("user"));
    if(user && user.token && user.token !=="")
        requestConfig.headers["Authorization"] = "bearer "+ user.token;
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