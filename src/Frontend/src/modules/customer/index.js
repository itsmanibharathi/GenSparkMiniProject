import $ from 'jquery';
import header from '../../components/moduleHeader.js';
console.log("header");
var data = [
    { name: 'Home', link: '/' }
]
$(document).ready(function () {
    $('#customer-nav').html(header(data));
});