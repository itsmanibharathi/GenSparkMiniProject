import $ from 'jquery';

// load html
import home from './modules/home/index.html';
import customer from './modules/customer/index.html';
import Employee from './modules/employee/index.html';
import Restaurant from './modules/restaurant/index.html';

// load js

const routePaths = [
    { path: '/', html: home },
    { path: '/customer', html: customer },
    { path: '/employee', html: Employee },
    { path: '/restaurant', html: Restaurant }

];

function loadRoutes() {
    $(document).ready(() => {
        const path = window.location.pathname.toLowerCase();
        console.log(path);
        const route = routePaths.find(r => r.path === path);
        if (route) {
            loadComponent('#root', route.html);
        }
        else {
            $('#root').html('404 - Not Found');
        }
    });
}

function loadComponent(id, html) {
    $.ajax({
        url: html,
        type: 'GET',
        dataType: 'html',
        success: function (response) {
            $(id).html(response);
        },
        error: function () {
            $(id).html('Failed to load content.');
        }
    });
}

module.exports = {
    loadRoutes,
    loadComponent
};

