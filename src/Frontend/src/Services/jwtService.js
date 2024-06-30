import log from '../utility/loglevel.js';

class tokenService {
    constructor(moduleName) {
        this.moduleName = moduleName;
    }
    get = () => {
        log.debug('Token retrieved');
        return localStorage.getItem(`${this.moduleName}_token`);
    }

    name = () => {
        // decode the token and get the name of the user
        const token = this.get();
        if (!token) {
            return null;
        }
        const payload = token.split('.')[1];
        const decoded = atob(payload);
        const { unique_name } = JSON.parse(decoded);
        return unique_name;
    }

    set = (token) => {
        localStorage.setItem(`${this.moduleName}_token`, token);
        log.debug(`Token set: ${token}`);

    }

    remove = () => {
        localStorage.removeItem(`${this.moduleName}_token`);
        log.debug('Token removed');
    }

    exists = () => {
        return localStorage.getItem(`${this.moduleName}_token`) ? true : false;
    }

}

export default tokenService;