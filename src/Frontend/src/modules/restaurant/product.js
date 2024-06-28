import $ from 'jquery';
import log from '../../utility/loglevel.js';
import showAlert from '../../Services/alertService.js';

import ProductPage from './product.html';
import ProductTemplate from '../../components/productTemplate.js';



const loadProductCallback = async (api, token) => {
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
        $('#productForm').trigger('reset');
        $('#productForm').find('input').removeClass('border-red-500');
        $('#productForm').find('input').removeClass('border-green-500');
        $('#productForm').find('input').next().text('');
        $('#productForm').attr('action', 'restaurant/product/add');
        $('#productForm').find('select').removeClass('border-red-500');
        $('#productForm').find('select').removeClass('border-green-500');
        $('#productForm').find('select').next().text('');
        $('#productFormContainer').toggle();
        $('#productFormOpen').toggleClass('hidden');
        isFormDirty = false;
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
        $('#productForm').attr('action', 'restaurant/product/update');
        $('#productForm').attr('productId', productId);

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

    $('#productForm').on('blur', 'input', (e) => {
        e.preventDefault();
        let input = e.target;
        if (input.value == '' && input.required) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'This field is required';
        }
        else if (input.name == 'productPrice' && input.value < 0) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Price must be greater than 0';
        }
        else {
            input.classList.remove('border-red-500');
            input.nextElementSibling.innerText = '';
            if (input.value !== '') {
                input.classList.add('border-green-500');
            }
        }

    });


    $('#addressForm').on('change', 'select', (e) => {
        e.preventDefault();
        let select = e.target;
        if (select.value == '') {
            select.classList.add('border-red-500');
            select.nextElementSibling.innerText = 'This field is required';
        } else {
            select.classList.remove('border-red-500');
            select.classList.add('border-green-500');
            select.nextElementSibling.innerText = '';
        }
    });


    $('#productForm').on('submit', (e) => {
        e.preventDefault();
        const action = $('#productForm').attr('action');
        const formdata = $('#productForm').serializeArray();
        const data = {};
        formdata.forEach(item => {
            if (item.name === 'productAvailable')
                data[item.name] = item.value === 'True' ? true : false;
            else
                data[item.name] = item.value;

        });
        $('#productForm').attr('productId') ? data.productId = $('#productForm').attr('productId') : '';
        log.debug('Product Data:', data);

        api.post(action, data).then(res => {
            showAlert('Product added successfully', 'success');
            $('#productForm').trigger('reset');
            $('#productFormContainer').toggle();
            $('#productFormOpen').toggleClass('hidden');
            if (action === 'restaurant/product/update')
                $('#p-' + res.data.productId).replaceWith(ProductTemplate(res.data, true));
            else
                $('#product-container').append(ProductTemplate(res.data, true));

        }).catch(err => {
            log.error(err);
            showAlert('Failed to add product', 'error');
        });
    });
    await GetProdcts(api).then(data => {
        products = data;
        loadProduct(products);
    });

}


const loadProduct = (products) => {
    const productContainer = $('#product-container');
    productContainer.empty();
    productContainer.append(products.map(product => ProductTemplate(product, true)).join(''));

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