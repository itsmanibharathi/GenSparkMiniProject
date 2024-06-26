import log from '../utility/loglevel.js';
class apiService {
    constructor(url, token) {
        this.url = url;
        this.token = token;
    }

    get = async (endpoint) => {
        return await this.request('GET', endpoint);
    }

    post = async (endpoint, data) => {
        return await this.request('POST', endpoint, data);
    }

    put = async (endpoint, data) => {
        return await this.request('PUT', endpoint, data);
    }

    delete = async (endpoint) => {
        return await this.request('DELETE', endpoint);
    }

    request = async (method, endpoint, data) => {
        const options = {
            method: method,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.token}`
            }
        };

        if (data != null) {
            options.body = JSON.stringify(data);
        }
        console.debug(`${this.url}/${endpoint}`);
        const response = await fetch(`${this.url}/${endpoint}`, options);
        const json = await response.json();
        if (response.status >= 200 && response.status < 300) {
            log.debug('API Success:', json);
            return json;
        } else {
            log.debug('API Error:', json);
            return Promise.reject(json);
        }
    }
}

export default apiService;