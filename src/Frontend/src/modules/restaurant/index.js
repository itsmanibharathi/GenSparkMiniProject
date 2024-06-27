import $ from 'jquery';
import log from '../../utility/loglevel.js';
import loadComponent from '../../Services/loadComponent.js';
import headerTemplate from '../../components/headerTemplate.js';
import Footer from '../../components/footer.html';

import { AuthPage, loadAuthCallback } from './auth.js';
import { HomePage, loadHomeCallback } from './home.js';
import { ProductPage, loadProductCallback } from './product.js';
import { OrderPage, loadOrderCallback, loadAllOrderCallback } from './order.js';

import apiService from '../../Services/apiService.js';
import jwtService from '../../Services/jwtService.js';
import localStorageService from '../../Services/localStorageService.js';
import showAlert from '../../Services/alertService.js';
import { basePath } from '../../Services/routerService.js';

const token = new jwtService('restaurant');
const localStorage = new localStorageService('restaurant');
const api = new apiService(process.env.API_URL, token.get());

const localRoutes = {
    name: 'restaurant',
    routes: [
        { path: '/restaurant', name: 'Home', component: HomePage, callback: loadHomeCallback },
        { path: '/restaurant/product', name: 'Product', component: ProductPage, callback: loadProductCallback },
        { path: '/restaurant/orders', name: 'Orders', component: OrderPage, callback: loadOrderCallback },
        { path: '/restaurant/orders/all', name: 'AllOrders', component: OrderPage, callback: loadAllOrderCallback },
        { path: '/restaurant/auth', name: 'Auth', hide: true, component: AuthPage, callback: loadAuthCallback },
    ]
};

const loadRestaurant = (path) => {
    log.debug('Loading Restaurant');
    $('#header-placeholder').html(headerTemplate(localRoutes, token.exists(), false));
    loadComponent('#footer-placeholder', Footer);

    if (path == '/restaurant/logout') {
        console.log('Logging out');
        token.remove();
        localStorage.clear();
        window.location.href = basePath + '/restaurant/auth';
        return;
    }

    const route = localRoutes.routes.find(route => route.path === path);

    if (route) {
        if (!token.exists() && path !== '/restaurant/auth') {
            window.location.href = basePath + '/restaurant/auth';
        }
        else if (token.exists() && path === '/restaurant/auth') {
            window.location.href = basePath + '/restaurant';
        }
        loadComponent('#body-placeholder', route.component, route.callback, api, token);
    } else {
        showAlert('Page not found', 'error');
    }
};

export default loadRestaurant;
