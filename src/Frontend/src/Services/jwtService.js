const jwtService = {
    get: (moduleName) => {
        return localStorage.getItem(`${moduleName}_jwt_token`);
    },
    set: (moduleName, token) => {
        localStorage.setItem(`${moduleName}_jwt_token`, token);
    },
    remove: (moduleName) => {
        localStorage.removeItem(`${moduleName}_jwt_token`);
    },
    clear: () => {
        localStorage.clear();
    }
}