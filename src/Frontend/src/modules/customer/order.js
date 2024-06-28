import $ from 'jquery';
import OrderPage from './order.html';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import OrderTemplate from '../../components/orderTemplate.js';

var orders = [];
const OrderCallback = async (api) => {
    orders = await GetOrders(api);
    log.debug('Orders :', orders);
    // $('#order-container').append(orders.map(order => OrderTemplate(order)).join('')
    // );
    loadOrders(orders);


}

const loadOrders = (orders) => {
    const OrderContainer = $('#order-container');
    OrderContainer.empty();

    const orderList = orders.map(order => OrderTemplate(order));


    const orderPerPage = 4;
    let currentPage = 1;
    const numPages = Math.ceil(orders.length / orderPerPage);
    const num = $('#num');
    OrderContainer.append(orderList.slice((currentPage - 1) * orderPerPage, currentPage * orderPerPage).join(''));
    num.html(`${currentPage} of ${numPages}`);
    $('#Previous').on('click', () => {
        if (currentPage > 1) {
            currentPage--;
            OrderContainer.html(orderList.slice((currentPage - 1) * orderPerPage, currentPage * orderPerPage).join(''));
            num.html(`${currentPage} of ${numPages}`);
        }
    });
    $('#next').on('click', () => {
        if (currentPage < numPages) {
            currentPage++;
            OrderContainer.html(orderList.slice((currentPage - 1) * orderPerPage, currentPage * orderPerPage).join(''));
            num.html(`${currentPage} of ${numPages}`);
        }
    });
}

const GetOrders = async (api) => {
    return await api.get('Customer/Order').then(res => {
        return res.data;
    }).catch(err => {
        log.error(err);
        showAlert('Failed to load orders', 'error');
    });
}

module.exports = {
    OrderPage,
    OrderCallback
}