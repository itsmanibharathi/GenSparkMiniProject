import $ from 'jquery';
import loadCustomer from './modules/customer/index.js';
import loadHome from './modules/home/index.js';

const routes = {
    '/': loadHome,
    '/customer': loadCustomer
};
const loadRoutes = () => {
    var path = window.location.pathname.toLocaleLowerCase();
    const base = process.env.BASE_PATH.toLocaleLowerCase();
    if (path.indexOf(base) === 0) {
        path = path.replace(base, '');
        path = path === '' ? '/' : path;
    }

    if (routes[path]) {
        routes[path]();
    }
    else {
        console.log('404');
    }
};

export default loadRoutes;
