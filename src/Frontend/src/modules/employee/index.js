import $ from 'jquery';
import log from '../../utility/loglevel.js';
import loadComponent from '../../Services/loadComponent.js';
import headerTemplate from '../../components/headerTemplate.js';
import Footer from '../../components/footer.html';

import { AuthPage, loadAuthCallback } from './auth.js';
import { HomePage, loadHomeCallback } from './home.js';
import { OrderPage, loadOrderCallback, loadAllOrderCallback } from './order.js';

import apiService from '../../Services/apiService.js';
import jwtService from '../../Services/jwtService.js';
import localStorageService from '../../Services/localStorageService.js';
import showAlert from '../../Services/alertService.js';
import { basePath } from '../../Services/routerService.js';

const token = new jwtService('employee');
const localStorage = new localStorageService('employee');
const api = new apiService(process.env.API_URL, token.get());

const localRoutes = {
    name: 'employee',
    routes: [
        { path: '/employee', name: 'Home', component: HomePage, callback: loadHomeCallback },
        { path: '/employee/orders', name: 'Orders', component: OrderPage, callback: loadOrderCallback },
        { path: '/employee/orders/all', name: 'AllOrders', component: OrderPage, callback: loadAllOrderCallback },
        { path: '/employee/auth', name: 'Auth', hide: true, component: AuthPage, callback: loadAuthCallback },
    ]
};

const loadEmployee = (path) => {
    log.debug('Loading employee');
    $('#header-placeholder').html(headerTemplate(localRoutes, token.exists(), false));
    loadComponent('#footer-placeholder', Footer);

    if (path == '/employee/logout') {
        console.log('Logging out');
        token.remove();
        localStorage.clear();
        window.location.href = basePath + '/employee/auth';
        return;
    }

    const route = localRoutes.routes.find(route => route.path === path);

    if (route) {
        if (!token.exists() && path !== '/employee/auth') {
            window.location.href = basePath + '/employee/auth';
        }
        else if (token.exists() && path === '/employee/auth') {
            window.location.href = basePath + '/employee';
        }
        loadComponent('#body-placeholder', route.component, route.callback, api, token);
    } else {
        showAlert('Page not found', 'error');
    }
};

export default loadEmployee;
