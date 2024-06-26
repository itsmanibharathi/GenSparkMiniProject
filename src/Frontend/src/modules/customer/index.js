import $ from 'jquery';
import log from '../../utility/loglevel.js';
import loadComponent from '../../Services/loadComponent.js';
import headerTemplate from '../../components/headerTemplate.js';
import Footer from '../../components/footer.html';

import { AuthPage, loadAuthCallback } from './auth.js';
import { HomePage, HomeCallback } from './home.js';
import { OrderPage, OrderCallback } from './order.js';
import { AddressPage, AddressCallback } from './address.js';
import { Cart, cartCallback } from './cart.js';

import apiService from '../../Services/apiService.js';
import jwtService from '../../Services/jwtService.js';
import localStorageService from '../../Services/localStorageService.js';
import showAlert from '../../Services/alertService.js';
import { basePath } from '../../Services/routerService.js';

const token = new jwtService('customer');
const localStorage = new localStorageService('customer');
const api = new apiService(process.env.API_URL, token.get());

const localRoutes = {
    name: 'customer',
    routes: [
        { path: '/customer', name: 'Home', component: HomePage, callback: HomeCallback },
        { path: '/customer/orders', name: 'Orders', component: OrderPage, callback: OrderCallback },
        { path: '/customer/address', name: 'Address', component: AddressPage, callback: AddressCallback },
        { path: '/customer/auth', name: 'Auth', hide: true, component: AuthPage, callback: loadAuthCallback },
    ]
};

const loadCustomer = (path) => {
    log.debug('Loading Customer');
    
    $('#header-placeholder').html(headerTemplate(localRoutes, token.exists(), true));
    loadComponent('#footer-placeholder', Footer);

    if (path === '/customer/logout') {
        console.log('Logging out');
        token.remove();
        localStorage.remove('cart');
        localStorage.remove('order');
        window.location.href = basePath + '/customer/auth';
        return;
    }

    const route = localRoutes.routes.find(route => route.path === path);

    if (route) {
        if (!token.exists() && path !== '/customer/auth') {
            window.location.href = basePath + '/customer/auth';
            showAlert('Please login to continue', 'error');
        } else if (token.exists() && path === '/customer/auth') {
            window.location.href = basePath + '/customer';
        } else {
            if (path === '/customer') {
                const _cartCallback = new cartCallback(api, localStorage, window, token.exists());
                loadComponent('body', Cart, () => _cartCallback.init());
                document.updateQuantity = (index, change) => _cartCallback.updateQuantity(index, change);
                document.deleteItem = (index) => _cartCallback.deleteItem(index);
                document.checkout = (e) => _cartCallback.checkout(e);
                document.payment = (e) => _cartCallback.payment(e);
            }
            loadComponent('#body-placeholder', route.component, route.callback, api, token);
        }
    } else {
        showAlert('Page not found', 'error');
    }
};

export default loadCustomer;
