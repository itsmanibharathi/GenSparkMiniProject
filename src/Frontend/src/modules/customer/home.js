import $ from 'jquery';
import HomePage from './home.html';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import ProductTemplate from '../../components/productTemplate.js';
var products = [];
const HomeCallback = async (api, cart) => {
    products = await GetProdcts(api);
    const productContainer = $('#product-container');
    productContainer.empty();
    productContainer.append(products.map(p => ProductTemplate(p)));
    document.addToCart = (productId) => {
        log.debug('Adding to cart:', productId);
        var product = products.find(p => p.productId == productId);
        cart.addToCart(product);
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