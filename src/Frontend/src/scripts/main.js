const $ = require('jquery');

import homeComponent from '../views/default/home.html';

$(document).ready(function() {
  $("#root").load(homeComponent);
});