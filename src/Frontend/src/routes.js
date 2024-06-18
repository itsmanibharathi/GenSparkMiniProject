import $ from 'jquery';

import home from './Components/Home/index.html';
import customer from './Components/Customer/index.html';
import Employee from './Components/Employee/index.html';
import Restaurant from './Components/Restaurant/index.html';

const routePaths = [
    { path: '/', component: home },
    { path: '/customer', component: customer },
    { path: '/employee', component: Employee },
    { path: '/restaurant', component: Restaurant }

];

function loadRoutes() {
    $(document).ready(() => {
        const path = window.location.pathname.toLowerCase();
        console.log(path);
        const route = routePaths.find(r => r.path === path);
        if (route) {
            loadComponent('#root', route.component);
        }
        else {
            $('#root').html('404 - Not Found');
        }
    });
}

function loadComponent(id, component) {
    $.ajax({
        url: component,
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

