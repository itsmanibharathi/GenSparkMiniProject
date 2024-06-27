import $ from 'jquery';
import AuthPage from './auth.html';
import { signInPage, signUpPage } from '../../Services/authLayoutService.js';
import showAlert from '../../Services/alertService.js';
import { basePath } from '../../Services/routerService.js';
const loadAuthCallback = (api, token) => {
    console.log('basepath  :', basePath)
    console.log('Loading Auth Callback');
    $('#signUpPage').on('click', signUpPage);
    $('#signInPage').on('click', signInPage);

    $('#signIn').on('blur', 'input', function (e) {
        const input = e.target;
        const value = input.value;
        if(value===''&& input.required ){
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'This field is required';
        }
        else if (input.name === 'customerEmail' && !/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid email address';
        }
        else {
            input.classList.remove('border-red-500');
            input.nextElementSibling.innerText = '';
            if (value !== '') {
                input.classList.add('border-green-500');
            }
        }
    });

    $('#signUp').on('blur', 'input', function (e) {
        const input = e.target;
        const value = input.value;
        if(value===''&& input.required ){
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'This field is required';
        }
        else if (input.name === 'customerEmail' && !/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid email address';
        }
        else {
            input.classList.remove('border-red-500');
            input.nextElementSibling.innerText = '';
            if (value !== '') {
                input.classList.add('border-green-500');
            }
        }
    });

    $('#signIn').on('submit', function (e) {
        e.preventDefault();
        var formData = $(this).serializeArray();
        const data = {};
        formData.forEach((item) => {
            data[item.name] = item.value;
        });
        console.log(data);
        api.post('customer/login', data)
            .then((res) => {
                token.set(res.data.token);
                showAlert('Sign In Successful', 'success');
                window.location.href = basePath + '/customer';
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
        api.post('customer/register', data)
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
