import $ from 'jquery';
import OrderPage from './order.html';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import OrderTemplate from '../../components/orderTemplate.js';

var orders = [];
const loadOrderCallback = async (api) => {
    orders = await GetOrders(api, 'all');
    loadOrder(orders);
    document.updateOrder = async (id, status) => updateOrderStatus(id, status, api);
}

const loadOrder = async (orders) => {
    const orderContainer = $('#order-container');
    orderContainer.empty();
    orderContainer.append(orders.map(order => OrderTemplate(order, 'restaurant')).join(''));
}

const GetOrders = async (api, path) => {
    return await api.get(`restaurant/Order/${path}`).then(res => {
        return res.data;
    }).catch(err => {
        log.error(err);
        showAlert('Failed to load orders', 'error');
    });
}

const updateOrderStatus = async (id, status, api) => {
    return await api.put(`restaurant/Order/${status}/${id}`).then(res => res.data)
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
    loadOrderCallback
}