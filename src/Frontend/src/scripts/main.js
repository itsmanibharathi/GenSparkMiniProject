import '../scripts/auth.js';
import '../scripts/auth.js';
const $ = require('jquery');
import fontawesome from '@fortawesome/fontawesome-free/css/all.css';
import fontawesome from '@fortawesome/fontawesome-free/css/all.css';

import homeComponent from '../views/home/home.html';
import footer from '../partials/footer.html';

import Component from '../partials/order.html';

$(document).ready(function () {
  // $("#root").load(homeComponent, function () {
  //   $("#footer-placeholder").load(footer);
  // });
  $('#root').load(Component);
});