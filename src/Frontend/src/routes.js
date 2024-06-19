import $ from 'jquery';
import homeHeader from './components/home-header.html';
import homeBody from './modules/home/index.html';
import customerHeader from './components/customer-header.html';
import customerBody from './modules/customer/index.html';
import employeeHeader from './components/employee-header.html';
import employeeBody from './modules/employee/index.html';
import restaurantHeader from './components/restaurant-header.html';
import restaurantBody from './modules/restaurant/index.html';
import footer from './components/footer.html';

const routePaths = [
    { path: '/', header: homeHeader, body: homeBody, footer: footer },
    { path: '/customer', header: customerHeader, body: customerBody, footer: footer },
    { path: '/employee', header: employeeHeader, body: employeeBody, footer: footer },
    { path: '/restaurant', header: restaurantHeader, body: restaurantBody, footer: footer }
];

function loadRoutes() {
    const path = window.location.pathname.toLowerCase();
    console.log(path);
    const route = routePaths.find(r => r.path === path);
    if (route) {

        loadComponent('#header-placeholder', route.header);
        loadComponent('#body-placeholder', route.body);
        loadComponent('#footer-placeholder', route.footer);
    } else {
        $('#root').html('404 - Not Found');
    }
}

function loadFooter() {
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
