import log from '../utility/loglevel.js';

class tokenService {
    constructor(moduleName) {
        this.moduleName = moduleName;
    }

    get = () => {
        log.debug('Token retrieved');
        return localStorage.getItem(`${this.moduleName}_token`);
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