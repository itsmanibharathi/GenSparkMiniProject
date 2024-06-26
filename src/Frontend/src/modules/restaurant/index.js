import $ from 'jquery';
import log from '../../utility/loglevel.js';
import loadComponent from '../../Services/loadComponent.js';
import headerTemplate from '../../components/headerTemplate.js';
import Footer from '../../components/footer.html';

import { AuthPage, loadAuthCallback } from './auth.js';

import apiService from '../../Services/apiService.js';
import jwtService from '../../Services/jwtService.js';
import localStorageService from '../../Services/localStorageService.js';
import showAlert from '../../Services/alertService.js';

const token = new jwtService('restaurant');
const localStorage = new localStorageService('restaurant');
const api = new apiService(process.env.API_URL, token.get());

const localRoutes = {
    name: 'restaurant',
    routes: [
        { path: '/restaurant', name: 'Home', component: AuthPage, callback: loadAuthCallback },
        { path: '/restaurant/orders', name: 'Orders', component: AuthPage, callback: loadAuthCallback },
        { path: '/restaurant/auth', name: 'Auth', component: AuthPage, callback: loadAuthCallback },
    ]
};

const loadRestaurant = (path) => {
    log.debug('Loading Restaurant');
    $('#header-placeholder').html(headerTemplate(localRoutes, token.exists(), false));
    loadComponent('#footer-placeholder', Footer);

    const route = localRoutes.routes.find(route => route.path === path);

    if (route) {
        if (!token.exists() && path !== '/restaurant/auth') {
            window.location.href = '/restaurant/auth';
        }
        loadComponent('#body-placeholder', route.component, () => {
            if (route.callback) {
                route.callback(api, token);
            }
        });
    } else {
        showAlert('Page not found', 'error');
    }
};

export default loadRestaurant;
