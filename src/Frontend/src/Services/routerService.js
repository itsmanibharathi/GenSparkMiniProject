import $ from 'jquery';
import Page404 from '../components/404.html';
import loadMain from '../modules/main/index.js';
import loadCustomer from '../modules/customer/index.js';
import loadRestaurant from '../modules/restaurant/index.js';
import loadEmployee from '../modules/employee/index.js';

import loadComponent from '../Services/loadComponent.js';

const routes = {
    '/': loadMain,
    '/customer': loadCustomer,
    '/customer/orders': loadCustomer,
    '/customer/auth': loadCustomer,
    '/customer/logout': loadCustomer,
    '/customer/address': loadCustomer,
    '/restaurant': loadRestaurant,
    '/restaurant/orders': loadRestaurant,
    '/restaurant/orders/all': loadRestaurant,
    '/restaurant/product': loadRestaurant,
    '/restaurant/auth': loadRestaurant,
    '/restaurant/logout': loadRestaurant,
    '/employee': loadEmployee,
    '/employee/orders/search': loadEmployee,
    '/employee/orders': loadEmployee,
    '/employee/orders/all': loadEmployee,
    '/employee/auth': loadEmployee,
    '/employee/logout': loadEmployee,
};

const loadRoutes = () => {
    const base = (process.env.BASE_PATH || '').toLowerCase();
    let path = window.location.pathname.toLowerCase();

    // Remove base path if present
    if (base && path.startsWith(base)) {
        path = path.slice(base.length);
    }

    path = path === '/' ? '/' : path.replace(/\/$/, '');

    const hash = window.location.hash.toLowerCase();
    if (hash && hash !== '#/') {
        path = hash.replace('#', '');
    }

    if (routes[path]) {
        routes[path](path);
    } else {
        loadComponent("#root", Page404);
    }
};

$(document).ready(() => {
    loadRoutes();

    $(window).on('popstate', loadRoutes);
});

$(document).on('click', 'a', function (e) {
    const href = $(this).attr('href');
    if (href && !href.startsWith('http')) {
        e.preventDefault();

        history.pushState(null, '', `${basePath}${href}`);
        loadRoutes();
    }
    else {
        window.href = href;
    }
});

module.exports = {
    loadRoutes,
};

export const basePath = process.env.isProduction ? `/#` : '';

console.log(process.env.BASE_PATH);