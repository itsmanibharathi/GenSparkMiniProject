import Cart from './cart.html';
import $ from 'jquery';
import log from '../../utility/loglevel.js';
import showAlert from '../../Services/alertService.js';

class cartCallback {
    constructor(api, db, document) {
        this.api = api;
        this.db = db;
        this.cartData = this.db.get('cart') || [];
        this.document = document;
        this.address = [];
    }

    async init() {
        this.loadCart();
        await this.loadAddress();

        this.document.updateQuantity = this.updateQuantity.bind(this);
        this.document.deleteItem = this.deleteItem.bind(this);
        this.document.checkout = this.checkout.bind(this);
        this.document.clearCart = this.clearCart.bind(this);
    }
    async loadAddress() {
        this.address = await this.api.get('Customer/Address').then((res) => res.data);
        log.info('Customer Address :', this.address);

        const addressContainer = $('#customer-address');
        addressContainer.empty();
        // create dropdown select box
        this.address.forEach((address) => {
            const addressRow = $(`
                <option value="${address.addressId}">${address.type}</option>
            `);
            addressContainer.append(addressRow);
        });
    }
    loadCart() {
        console.log('Loading Cart');
        const cartItemsContainer = $('#cart-items');
        cartItemsContainer.empty();
        const cartData = this.db.get('cart') || [];
        cartData.forEach((item, index) => {
            const itemRow = $(`
                <div class="flex flex-row justify-between items-center border-b py-2">
                    <div class="flex flex-col">
                        <h2 class="mt-2 mb-2 font-bold">${item.productName} <span class="font-light">${item.restaurantName}</span></h2>
                        <span class="text-gray-500">${item.productPrice}</span>
                    </div>
                    <div class="flex flex-row items-center">
                        <button class="px-2 hover:text-button-hover" onclick="document.updateQuantity(${index}, -1)"><i class="fa-solid fa-minus"></i></button>
                        <span class="px-2">${item.quantity} </span>
                        <button class="px-2 hover:text-button-hover" onclick="document.updateQuantity(${index}, 1)"><i class="fa-solid fa-plus"></i></button>
                    </div>
                    <button class="px-2 text-button hover:text-button-hover" onclick="document.deleteItem(${index})"><i class="fa-solid fa-trash-can"></i></button>
                </div>
            `);
            cartItemsContainer.append(itemRow);
        });
        this.updateTotal();
    }

    addToCart(product) {
        const cartData = this.db.get('cart') || [];
        const existingProductIndex = cartData.findIndex(item => item.productId === product.productId);

        if (existingProductIndex !== -1) {
            cartData[existingProductIndex].quantity += 1;
        } else {
            product.quantity = 1;
            cartData.push(product);
        }

        this.db.set('cart', cartData);
        this.loadCart();
    }

    updateQuantity(index, change) {
        const cartData = this.db.get('cart') || [];
        if (cartData[index]) {
            cartData[index].quantity += change;
            if (cartData[index].quantity > 10) {
                alert('You can only add 10 items at a time');
                cartData[index].quantity--;
            }
            if (cartData[index].quantity <= 0) {
                cartData.splice(index, 1);
            }
            this.db.set('cart', cartData);
            this.loadCart();
        }
    }

    deleteItem(index) {
        const cartData = this.db.get('cart') || [];
        cartData.splice(index, 1);
        this.db.set('cart', cartData);
        this.loadCart();
    }

    updateTotal() {
        const cartData = this.db.get('cart') || [];
        const total = cartData.reduce((sum, item) => sum + (item.productPrice * item.quantity), 0);
        $('#total').text(total);
    }

    clearCart() {
        this.db.remove('cart');
        this.loadCart();
    }

    async checkout(e) {
        const cartData = this.db.get('cart') || [];
        if (cartData.length === 0) {
            showAlert('Cart is empty', 'error');
            return;
        }

        const order = {
            shippingAddressId: $('#customer-address').val(),
            orderItemIds: cartData.map(item => ({
                productId: item.productId,
                quantity: item.quantity
            }))
        };

        try {
            const res = await this.api.post('Customer/Order/create', order);
            showAlert('Order placed successfully', 'success');
            this.db.remove('cart');
            this.db.set('order', res.data);
            this.loadCart();
        } catch (err) {
            console.log("Error: ", err);
            showAlert(err.message, 'error');
        }
    }
}

export { Cart, cartCallback };
