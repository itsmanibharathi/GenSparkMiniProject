const $ = require('jquery');
import fontawesome from '@fortawesome/fontawesome-free/css/all.css';

import homeComponent from '../views/home/home.html';
import footer from '../partials/footer.html';


$(document).ready(function () {
  $("#root").load(homeComponent, function () {
    $("#footer-placeholder").load(footer);
  });
});