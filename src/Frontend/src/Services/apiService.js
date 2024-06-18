import log from '../utility/loglevel.js';

const apiService = (function (baseURL, token = null) {
    const baseURL = 'https://localhost:3000';
    const token = null;
    function makeRequest(endpoint, method, data = null) {
        return $.ajax({
            url: baseURL + endpoint,
            type: method,
            dataType: 'json',
            data: data,
            beforeSend: function (xhr) {
                if (token) {
                    xhr.setRequestHeader('Authorization', `Bearer ${token}`);
                }
                log.debug(`Request is about to be sent to ${baseURL + endpoint}`);
            }
        }).then(
            function (response) {
                log.debug('Success:', response);
                return response;
            },
            function (xhr, status, error) {
                log.error('Error:', status, error);
                log.debug('Response:', xhr.responseText);
                throw new Error(`Request failed with status: ${status}, error: ${error}`);
            }
        ).always(function (xhr, status) {
            log.debug(`Request completed with status: ${status}`);
        });
    }

    return {
        get: function (endpoint, data = null) {
            return makeRequest(endpoint, 'GET', data);
        },
        post: function (endpoint, data) {
            return makeRequest(endpoint, 'POST', data);
        },
        put: function (endpoint, data) {
            return makeRequest(endpoint, 'PUT', data);
        },
        delete: function (endpoint, data = null) {
            return makeRequest(endpoint, 'DELETE', data);
        }
    };
})(baseURL, token);

export default apiService;
