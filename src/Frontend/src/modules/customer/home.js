import $ from 'jquery';
import HomePage from './home.html';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import ProductTemplate from '../../components/productTemplate.js';
import { addToCart } from './cart.js';
var products = [];
const HomeCallback = async (api) => {
    products = await GetProdcts(api);
    $('#product-container').append(products.map(product => ProductTemplate(product)).join(''));

    document.addtoCart = (productId) => {
        log.debug('Adding to cart:', productId);
        var product = products.find(p => p.productId == productId);
        addToCart(product);
        showAlert('Product added to cart', 'success');
    }
}

const GetProdcts = async (api) => {
    return await api.get('Product/all').then(res => {
        return res.data;
    }).catch(err => {
        log.error(err);
        showAlert('Failed to load products', 'error');
    });
}



module.exports = {
    HomePage,
    HomeCallback
}