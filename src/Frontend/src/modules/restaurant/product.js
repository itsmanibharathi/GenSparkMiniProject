import $ from 'jquery';
import log from '../../utility/loglevel.js';
import showAlert from '../../Services/alertService.js';

import ProductPage from './product.html';
import ProductTemplate from '../../components/productTemplate.js';



const loadProductCallback = (api, token) => {
    var products = [];
    var isFormDirty = false;
    $('#productFormOpen').on('click', () => {
        $('#productFormContainer').toggle();
        $('#productFormOpen').toggleClass('hidden');
    });

    $('#productFormClose').on('click', () => {
        if (isFormDirty) {
            const result = confirm('You have unsaved changes. Are you sure you want to leave?');
            if (!result) {
                return;
            }
        }
        isFormDirty = false;
        $('#productForm').trigger('reset');
        $('#productFormContainer').toggle();
        $('#productFormOpen').toggleClass('hidden');
    });

    document.editProduct = (productId) => {
        log.debug('Edit Product', productId);
        const product = products.find(p => p.productId === productId);
        $('#productFormContainer').toggle();
        $('#productFormOpen').toggleClass('hidden');
        $('#productForm').trigger('reset');
        $('#productForm').find('input[name="productId"]').val(product.productId);
        $('#productForm').find('input[name="productName"]').val(product.productName);
        $('#productForm').find('input[name="productPrice"]').val(product.productPrice);
        $('#productForm').find('input[name="productAvailable"]').val(product.productAvailable);
        $('#productForm').find('input[name="productDescription"]').val(product.productDescription);
    }

    $('#productForm').on('input', (e) => {
        isFormDirty = true;
    });

    $(window).on('beforeunload', function (event) {
        if (isFormDirty) {
            const message = 'You have unsaved changes. Are you sure you want to leave?';
            event.returnValue = message;
            return message;
        }
    });



    $('#productForm').on('submit', (e) => {
        e.preventDefault();
        const formdata = $('#productForm').serializeArray();
        const data = {};
        formdata.forEach(item => {
            if (item.name === 'productAvailable')
                data[item.name] = item.value === 'True' ? true : false;
            else
                data[item.name] = item.value;

        });

        log.debug('Product Data:', data);

        api.post('restaurant/Product/add', data).then(res => {
            showAlert('Product added successfully', 'success');
            $('#productForm').trigger('reset');
            $('#productFormContainer').toggle();
            $('#productFormOpen').toggleClass('hidden');
            $('#product-container').append(ProductTemplate(res.data, true));

        }).catch(err => {
            log.error(err);
            showAlert('Failed to add product', 'error');
        });
    });


    GetProdcts(api).then(data => {
        products = data;
        $('#product-container').append(products.map(product => ProductTemplate(product, true)).join(''));
    });

}

const GetProdcts = async (api) => {
    return await api.get('restaurant/product/all').then(res => {
        return res.data;
    }).catch(err => {
        log.error(err);
        showAlert('Failed to load products', 'error');
    });
}

module.exports = {
    ProductPage,
    loadProductCallback
}