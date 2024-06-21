import $ from 'jquery';
import log from '../../utility/loglevel.js';
import loadComponent from '../../services/loadComponent.js';
import headerTemplate from '../../components/headerTemplate.js';
import Footer from '../../components/footer.html';
import { AuthPage, loadAuthCallback } from './auth.js';
import apiService from '../../services/apiService.js';
import jwtService from '../../services/jwtService.js';

const token = new jwtService('customer');
const api = new apiService('http://localhost:5170/api/customer', token.get());

const btn = {
    login: { name: 'Login', function: 'loadLogin(this)' },
    logout: { name: 'Logout', function: 'loadLogout(this)' }
}
const localRoutes = [
    {
        name: 'Home', path: '/'
    },
];
const loadCustomer = () => {
    log.debug('Loading Customer');
    log.debug('Token:', token.get());

    $('#header-placeholder').html(headerTemplate(localRoutes, btn.login));
    loadComponent('#body-placeholder', AuthPage, loadAuthCallback, api, token);
    loadComponent('#footer-placeholder', Footer);
}


export default loadCustomer;