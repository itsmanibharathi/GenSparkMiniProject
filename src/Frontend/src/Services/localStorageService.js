import log from '../utility/loglevel.js';

class localStorageService {
    constructor(moduleName) {
        this.moduleName = moduleName;
    }

    get = (key) => {
        log.debug('Local storage retrieved');
        return localStorage.getItem(`${this.moduleName}_${key}`);
    }

    set = (key, value) => {
        localStorage.setItem(`${this.moduleName}_${key}`, value);
        log.debug(`Local storage set: ${key}`);
    }

    remove = (key) => {
        localStorage.removeItem(`${this.moduleName}_${key}`);
        log.debug('Local storage removed');
    }

    exists = (key) => {
        return localStorage.getItem(`${this.moduleName}_${key}`) ? true : false;
    }
}
export default localStorageService;