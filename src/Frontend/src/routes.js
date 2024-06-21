import $ from 'jquery';
import loadCustomer from './modules/Customer/index.js';
import loadHome from './modules/home/inde.js';
const routes = {
    '/': loadHome,
    '/customer': loadCustomer
};
const loadRoutes = () => {
    var path = window.location.pathname.toLocaleLowerCase();
    path = path === '/' ? '/' : path.replace(/\/$/, '');
    console.log(path);
    if (routes[path]) {
        routes[path]();
    }
    else {
        console.log('404');
    }
};

export default loadRoutes;
