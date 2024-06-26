import $ from 'jquery';
import AuthPage from './auth.html';
import { signInPage, signUpPage } from '../../Services/authLayoutService.js';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
const loadAuthCallback = (api, token) => {
    $('#signUpPage').on('click', signUpPage);
    $('#signInPage').on('click', signInPage);
    $('#signIn').on('submit', function (e) {
        e.preventDefault();
        var formData = $(this).serializeArray();
        const data = {};
        formData.forEach((item) => {
            data[item.name] = item.value;
        });
        log.debug(data);
        api.post('restaurant/login', data)
            .then((res) => {
                token.set(res.data.token);
                showAlert('Sign In Successful', 'success');
                window.location.href = basePath+'/restaurant';
            })
            .catch((err) => {
                console.log(err);
                showAlert('Sign In Failed', 'error');
            });
    });

    $('#signUp').on('submit', function (e) {
        e.preventDefault();
        var formData = $(this).serializeArray();
        const data = {};
        formData.forEach((item) => {
            data[item.name] = item.value;
        });
        console.log(data);
        api.post('Restaurant/register', data)
            .then((res) => {
                showAlert('Sign Up Successful', 'success');
                signInPage();
            })
            .catch((err) => {
                console.log(err);
                showAlert('Sign Up Failed', 'error');
            });
    });
}


module.exports = {
    AuthPage,
    loadAuthCallback
}
