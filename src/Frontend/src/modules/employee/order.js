import $ from 'jquery';
import OrderPage from './order.html';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import OrderTemplate from '../../components/orderTemplate.js';

var orders = [];

const loadSearchOrderCallback = async (api) => {
    orders = await GetOrders(api, "Search");
    loadOrder(orders);
    document.updateOrder = async (id, status) => updateOrderStatus(id, status, api);

}


const loadOrderCallback = async (api) => {
    orders = await GetOrders(api, '');
    loadOrder(orders);
    document.updateOrder = async (id, status) => updateOrderStatus(id, status, api);
}

const loadAllOrderCallback = async (api) => {
    orders = await GetOrders(api, 'all');
    loadOrder(orders);
    document.updateOrder = async (id, status) => showAlert('You can only view all orders', 'warning');
}

const loadOrder = async (orders) => {
    if (orders == undefined || orders.length == 0) {
        showAlert('Today you didn\'t receive any order', 'warning');
        const orderContainer = $('#order-container');
        orderContainer.empty();
        orderContainer.append('<h1>Today you didn\'t receive any order</h1>');
        return;
    }
    // const orderContainer = $('#order-container');
    // orderContainer.empty();
    // orderContainer.append(orders.map(order => OrderTemplate(order, 'employee')).join(''));
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



const GetOrders = async (api, path) => {
    return await api.get(`employee/order/${path}`).then(res => {
        return res.data;
    }).catch(err => {
        log.error(err);
        if (err.status === 500) {
            showAlert('Internal server error', 'error');
        }
    });
}

const updateOrderStatus = async (id, status, api) => {
    return await api.put(`employee/Order/${status}/${id}`).then(res => res.data)
        .then(data => {
            if (data) {
                $(`#order-${id}`).replaceWith(OrderTemplate(data, 'restaurant'));
            }
            showAlert('Order status updated successfully', 'success');
        }).catch(err => {
            log.error(err);
            showAlert(err.message, 'error');
        });
}

module.exports = {
    OrderPage,
    loadSearchOrderCallback,
    loadOrderCallback,
    loadAllOrderCallback
}