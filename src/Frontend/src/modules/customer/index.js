import $ from 'jquery';
import log from '../../utility/loglevel.js';
import loadComponent from '../../Services/loadComponent.js';
import headerTemplate from '../../components/headerTemplate.js';

import Footer from '../../components/footer.html';

import { AuthPage, loadAuthCallback } from './auth.js';

import { HomePage, HomeCallback } from './home.js';
import { Cart, cartCallback } from './cart.js';

import apiService from '../../Services/apiService.js';
import jwtService from '../../Services/jwtService.js';
import localStorageService from '../../Services/localStorageService.js';

const token = new jwtService('customer');
const localStorage = new localStorageService('customer');
const api = new apiService(process.env.API_URL, token.get());

const btn = {
    login: { name: 'Login', function: 'loadLogin(this)' },
    logout: { name: 'Logout', function: 'loadLogout(this)' }
}
const localRoutes = [{ "path": "/", "name": "Home" }, { "path": "customer/orders", "name": "Orders" }]
const loadCustomer = () => {
    log.debug('Loading Customer');
    log.debug('Token:', token.get());

    const _cartCallback = new cartCallback(api, localStorage, document);
    $('#header-placeholder').html(headerTemplate(localRoutes, btn.login, 'customer'));
    loadComponent('body', Cart, () => _cartCallback.init());
    // loadComponent('#body-placeholder', AuthPage, loadAuthCallback, api, token);
    loadComponent('#body-placeholder', HomePage, HomeCallback, api, _cartCallback);
    loadComponent('#footer-placeholder', Footer);
}

export default loadCustomer;