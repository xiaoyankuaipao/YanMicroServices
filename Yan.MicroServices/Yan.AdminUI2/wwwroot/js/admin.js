
const httpRequestService = axios.create({
    baseURL: '',
    timeout: 5000
});
httpRequestService.interceptors.request.use(
    config => {
        config.headers.axios = "true";
        return config;
    },
    err => {
        return Promise.reject(err);
    });
httpRequestService.interceptors.response.use(
    response => {
        return response.data;
    },
    error => {
        if (error.response) {
            switch (error.response.status) {
                case 401:
                {
                    var tt = new Vue();
                    tt.$confirm('登录已超时，请重新登录',
                        '提示',
                        {
                            confirmButtonText: '重新登录',
                            cancelButtonText: '取消',
                            type: 'warning'
                        }).then(() => {
                        window.location.href = "/Login";
                    });
                }
            }
        }
        return Promise.reject(error.response.data);
    });

/* GET 方法 */
httpRequestService.GET = async (url, params) => {
    let result = await httpRequestService({
        url: url,
        method: 'GET',
        params: params
    });
    return result;
}

/* POST 方法 */
httpRequestService.POST = async (url, data) => {
    let result = await httpRequestService({
        url: url,
        method: 'POST',
        data
    });
    return result;
}

/** PUT 方法 */
httpRequestService.PUT = async (url, data) => {
    let result = await httpRequestService({
        url: url,
        method: 'PUT',
        data
    });
    return result;
}

/**  DELETE 方法 */
httpRequestService.DELETE = async (url, data) => {
    let result = await httpRequestService({
        url: url,
        method: 'DELETE',
        data
    });
    return result;
}