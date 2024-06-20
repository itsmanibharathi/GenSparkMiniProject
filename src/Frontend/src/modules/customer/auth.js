import $ from 'jquery';
import AuthPage from './auth.html';
import { signInPage, signUpPage } from '../../services/authLayoutService.js';


const loadAuthCallback = (api, token) => {
    console.log('Loading Auth Callback');
    $('#signUpPage').on('click', signUpPage);
    $('#signInPage').on('click', signInPage);
    $('#signIn').on('submit', function (e) {
        e.preventDefault();
        alert('Sign In');
        var formData = $(this).serializeArray();
        const data = {};
        formData.forEach((item) => {
            data[item.name] = item.value;
        });
        console.log(data);

        api.post('login', data)
            .then((res) => {
                token.set(res.data.token);

            })
            .catch((err) => {
                console.log(err);
            });
    });
}


module.exports = {
    AuthPage,
    loadAuthCallback
}
