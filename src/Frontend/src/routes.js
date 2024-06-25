import $ from 'jquery';
import loadCustomer from './modules/customer/index.js';
import loadHome from './modules/home/index.js';

const routes = {
    '/': loadHome,
    '/customer': loadCustomer,
    '/customer/orders': loadCustomer,
    '/customer/login': loadCustomer,
    '/customer/logout': loadCustomer,
};

const loadRoutes = () => {
    let path = window.location.pathname.toLocaleLowerCase();
    const base = process.env.BASE_PATH.toLocaleLowerCase() || '';

    if (base && path.startsWith(base)) {
        path = path.replace(base, '');
    }
    path = path === '/' ? '/' : path.replace(/\/$/, '');
    if (routes[path]) {
        routes[path](path);
    } else {
        console.log('404');
    }
};

export default loadRoutes;
