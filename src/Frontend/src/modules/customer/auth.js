import $ from 'jquery';
import AuthPage from './auth.html';
import { signInPage, signUpPage } from '../../Services/authLayoutService.js';
import showAlert from '../../Services/alertService.js';
const loadAuthCallback = (api, token) => {
    console.log('Loading Auth Callback');
    $('#signUpPage').on('click', signUpPage);
    $('#signInPage').on('click', signInPage);
    $('#signIn').on('submit', function (e) {
        e.preventDefault();
        var formData = $(this).serializeArray();
        const data = {};
        formData.forEach((item) => {
            data[item.name] = item.value;
        });
        console.log(data);
        api.post('login', data)
            .then((res) => {
                token.set(res.data.token);
                showAlert('Sign In Successful', 'success');
            })
            .catch((err) => {
                console.log(err);
                showAlert('Sign In Failed', 'error');
            });
    });
}


module.exports = {
    AuthPage,
    loadAuthCallback
}
