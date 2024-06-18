const $ = require('jquery');
import fontawesome from '@fortawesome/fontawesome-free/css/all.css';

const routes = require('./routes.js');


$(document).ready(() => {
  routes.loadRoutes();
  $(document).on('click', 'nav a', function (e) {
    e.preventDefault();
    const url = $(this).attr('href');
    history.pushState(null, '', url);
    routes.loadRoutes();
  })

  $(window).on('popstate', function () {
    routes.loadRoutes();
  });
});