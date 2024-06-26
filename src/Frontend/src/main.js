// const $ = require('jquery');
import './styles/auth.css';
import fontawesome from '@fortawesome/fontawesome-free/css/all.css';
// import loadRoutes from './routes.js';
import dotenv from 'dotenv';
dotenv.config();


// $(document).ready(() => {
//   loadRoutes();
//   $(document).on('click', 'nav a', function (e) {
//     e.preventDefault();
//     const url = $(this).attr('href');
//     history.pushState(null, '', url);
//     loadRoutes();
//   })
//   $(window).on('popstate', function () {
//     loadRoutes();
//   });
// });


import $ from 'jquery';
import { loadRoutes } from './Services/routerService';



$(document).ready(() => {
    loadRoutes();
});
