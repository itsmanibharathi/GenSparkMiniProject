import $ from 'jquery';
import HomePage from './home.html';

const loadHomeCallback = (api, token) => {
    console.log('Home Callback');
}

module.exports = {
    HomePage,
    loadHomeCallback
}