import $, { type } from 'jquery';
import log from '../../utility/loglevel.js';
import loadComponent from '../../Services/loadComponent.js';
import headerTemplate from '../../components/headerTemplate.js';

import Footer from '../../components/footer.html';

import { Cart, cartCallback } from './cart.js';
import { HomePage, HomeCallback } from './home.js';
import { OrderPage, OrderCallback } from './order.js';
import { AuthPage, loadAuthCallback } from './auth.js';


import apiService from '../../Services/apiService.js';
import jwtService from '../../Services/jwtService.js';
import localStorageService from '../../Services/localStorageService.js';

const token = new jwtService('customer');
const localStorage = new localStorageService('customer');
const api = new apiService(process.env.API_URL, token.get());

const localRoutes = {
    name: 'customer',
    routes: [
        { path: '/customer/', name: 'Home' },
        { path: '/customer/orders', name: 'Orders' },
    ]
};

const loadCustomer = (path) => {
    log.debug('Loading Customer');
    log.debug('Token:', token.get());

    const _cartCallback = new cartCallback(api, localStorage, document);
    $('#header-placeholder').html(headerTemplate(localRoutes, token.exists(), true));
    loadComponent('body', Cart, () => _cartCallback.init());
    loadComponent('#footer-placeholder', Footer);

    switch (path) {
        case '/':
        case '/customer':
            loadComponent('#body-placeholder', HomePage, HomeCallback, api, _cartCallback);
            break;
        case '/customer/orders':
            loadComponent('#body-placeholder', OrderPage, OrderCallback, api);
            break;
        case '/customer/login':
            if (token.exists()) {
                window.location.href = '/customer';
            }
            loadComponent('#body-placeholder', AuthPage, loadAuthCallback, api, token);
            break;
        case '/customer/logout':
            token.remove();
            localStorage.remove('cart');
            localStorage.remove('order');
            window.location.href = '/customer';
            break;
        default:
            loadHome();
            break;
    }
};

export default loadCustomer;
