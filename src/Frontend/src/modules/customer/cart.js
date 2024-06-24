import Cart from './cart.html';
import $ from 'jquery';
import log from '../../utility/loglevel.js';
import showAlert from '../../Services/alertService.js';

const cartCallback = async (api) => {
    loadCart();
    document.updateQuantity = updateQuantity;
    document.deleteItem = deleteItem;
    document.clearCart = clearCart;
    document.checkout = (e) => checkout(e, api);
};

const loadCart = () => {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    const cartItemsContainer = $('#cart-items');
    cartItemsContainer.empty();
    cart.forEach((item, index) => {
        const itemRow = $(`
            <div class="flex flex-row justify-between items-center border-b py-2">
                <div class="flex flex-col">
                    <h2 class="mt-2 mb-2  font-bold">${item.productName} <span class="font-light">${item.restaurantName}</span></h2> 
                    <span class="text-gray-500">${item.productPrice}</span>
                </div>
                <div class="flex flex-row items-center">
                    <button class="px-2 hover:text-button-hover" onclick="updateQuantity(${index}, -1)"><i class="fa-solid fa-minus"></i></button>
                    <span class="px-2">${item.quantity} </span>
                    <button class="px-2 hover:text-button-hover" onclick="updateQuantity(${index}, 1)"><i class="fa-solid fa-plus"></i></button>
                </div>
                <button class="px-2 text-button hover:text-button-hover" onclick="deleteItem(${index})"><i class="fa-solid fa-trash-can"></i></button>
            </div>
        `);
        cartItemsContainer.append(itemRow);
    });

    updateTotal();
};

const addToCart = (product) => {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    const existingProductIndex = cart.findIndex(item => item.productId === product.productId);

    if (existingProductIndex !== -1) {
        cart[existingProductIndex].quantity += 1;
    } else {
        product.quantity = 1;
        cart.push(product);
    }

    localStorage.setItem('cart', JSON.stringify(cart));
    loadCart();
};

const updateQuantity = (index, change) => {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];

    if (cart[index]) {
        cart[index].quantity += change;
        if (cart[index].quantity > 10) {
            alert('You can only add 10 items at a time');
            cart[index].quantity--;
        }
        if (cart[index].quantity <= 0) {
            cart.splice(index, 1);
        }
        localStorage.setItem('cart', JSON.stringify(cart));
        loadCart();
    }
};

const deleteItem = (index) => {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    cart.splice(index, 1);
    localStorage.setItem('cart', JSON.stringify(cart));
    loadCart();
};

const updateTotal = () => {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    const total = cart.reduce((sum, item) => sum + (item.productPrice * item.quantity), 0);
    $('#total').text(total);
};

const clearCart = () => {
    localStorage.removeItem('cart');
    loadCart();
}

const checkout = async (e, api) => {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    if (cart.length === 0) {
        showAlert('Cart is empty', 'error');
        return;
    }

    const order = {
        orderItemIds: cart.map(item => ({
            productId: item.productId,
            quantity: item.quantity
        }))
    };

    api.post('Order', order).then(res => {
        showAlert('Order placed successfully', 'success');
        localStorage.removeItem('cart');
        loadCart();
    }).catch(err => {
        log.error(err);
        showAlert('Failed to place order', 'error');
    });
}

module.exports = {
    Cart,
    cartCallback,
    addToCart
};
