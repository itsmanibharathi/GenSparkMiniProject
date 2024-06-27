import $, { type } from 'jquery';
import log from '../../utility/loglevel.js';
import loadComponent from '../../Services/loadComponent.js';
import headerTemplate from '../../components/headerTemplate.js';

import Footer from '../../components/footer.html';

import { Cart, cartCallback } from './cart.js';
import { HomePage, HomeCallback } from './home.js';
import { OrderPage, OrderCallback } from './order.js';
import { AuthPage, loadAuthCallback } from './auth.js';
import { AddressPage, AddressCallback } from './address.js';

import apiService from '../../Services/apiService.js';
import jwtService from '../../Services/jwtService.js';
import localStorageService from '../../Services/localStorageService.js';
import showAlert from '../../Services/alertService.js';

const token = new jwtService('customer');
const localStorage = new localStorageService('customer');
const api = new apiService(process.env.API_URL, token.get());

const localRoutes = {
    name: 'customer',
    routes: [
        { path: '/customer/', name: 'Home' },
        { path: '/customer/orders', name: 'Orders' },
        { path: '/customer/address', name: 'Address' },
    ]
};

const loadCustomer = (path) => {
    log.debug('Loading Customer');
    log.debug('Token:', token.get());

    const _cartCallback = new cartCallback(api, localStorage, window, token.exists());
    loadComponent('body', Cart, () => _cartCallback.init());
    document.updateQuantity = (index, change) => _cartCallback.updateQuantity(index, change);
    document.deleteItem = (index) => _cartCallback.deleteItem(index);
    document.checkout = (e) => _cartCallback.checkout(e);
    document.payment = (e) => _cartCallback.payment(e);
    $('#header-placeholder').html(headerTemplate(localRoutes, token.exists(), true));
    loadComponent('#footer-placeholder', Footer);

    switch (path) {
        case '/':
        case '/customer':
            loadComponent('#body-placeholder', HomePage, HomeCallback, api, _cartCallback);
            break;
        case '/customer/orders':
            if (!token.exists()) {
                window.location.href = '/customer/login';
                showAlert('Please login to view orders', 'error');
            }
            loadComponent('#body-placeholder', OrderPage, OrderCallback, api);
            break;
        case '/customer/address':
            if (!token.exists()) {
                window.location.href = '/customer/login';
                showAlert('Please login to view address', 'error');
            }
            loadComponent('#body-placeholder', AddressPage, AddressCallback, api, localStorage);
            break;
        case '/customer/auth':
            if (token.exists()) {
                window.location.href = '/customer';
            }
            loadComponent('#body-placeholder', AuthPage, loadAuthCallback, api, token);
            break;
        case '/customer/logout':
            token.remove();
            localStorage.clear();
            window.location.href = '/customer';
            break;
        default:
            loadHome();
            break;
    }
};

export default loadCustomer;