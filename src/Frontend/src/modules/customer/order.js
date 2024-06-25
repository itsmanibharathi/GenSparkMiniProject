import $ from 'jquery';
import OrderPage from './order.html';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import OrderTemplate from '../../components/orderTemplate.js';

var orders = [];
const OrderCallback = async (api) => {
    orders = await GetOrders(api);
    log.debug('Orders :', orders);
    $('#order-container').append(orders.map(order => OrderTemplate(order)).join('')
    );
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