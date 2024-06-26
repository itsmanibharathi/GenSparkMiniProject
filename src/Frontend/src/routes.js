import $ from 'jquery';
import loadCustomer from './modules/customer/index.js';
import loadMain from './modules/main/index.js';
import loadComponent from './Services/loadComponent.js';
import Page404 from './components/404.html';
const routes = {
    '/': loadMain,
    '/customer': loadCustomer,
    '/customer/orders': loadCustomer,
    '/customer/auth': loadCustomer,
    '/customer/logout': loadCustomer,
    '/customer/address': loadCustomer,
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
        loadComponent("#root", Page404);
    }
};

export default loadRoutes;
