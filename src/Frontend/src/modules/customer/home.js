import $ from 'jquery';
import HomePage from './home.html';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import ProductTemplate from '../../components/productTemplate.js';

const HomeCallback = async (api) => {
    const products = await GetProdcts(api);
    console.log("products:", products); 
    console.log("ProductTemplate:", products.map(product => ProductTemplate(product)).join(''));
    $('#product-container').append(products.map(product => ProductTemplate(product)).join(''));
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