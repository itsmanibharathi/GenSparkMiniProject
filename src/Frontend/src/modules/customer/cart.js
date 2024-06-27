import Cart from './cart.html';
import $ from 'jquery';
import log from '../../utility/loglevel.js';
import showAlert from '../../Services/alertService.js';

class cartCallback {
    constructor(api, db, window, islogin) {
        this.api = api;
        this.db = db;
        this.window = window;
        this.document = window.document;
        this.islogin = islogin;
    }

    async init() {
        this.document.onCartClick = async () => {
            const cart = document.querySelector('#cart');
            cart.classList.toggle('hidden');
            if (!cart.classList.contains('hidden')) {
                if (!this.islogin) {
                    this.loadCart();
                    return;
                }
                const lastorder = await this.GetLastCreateOrder();
                console.log("Last Order: ", lastorder);
                if (lastorder.length > 0) {
                    this.db.set('order', lastorder);
                    this.loadPayment();
                }
                else {
                    const orderData = this.db.get('order') || [];
                    if (orderData.length > 0) {
                        this.loadPayment();
                    }
                    else {
                        this.loadCart();
                    }
                }
            }
        }
    }

    async GetAddress() {
        return await this.api.get('Customer/Address')
            .then((res) => res.data)
            .catch((err) => {
                if (err.statusCode == 404) {
                    showAlert("Pls add address",)
                }
            });
    }

    async GetLastCreateOrder() {
        return await this.api.get('Customer/order/lastCreate')
            .then((res) => res.data)
            .catch((err) => {
                if (err.statusCode == 404) {
                    return [];
                }
                else {
                    showAlert(err.message, 'error');
                }
            });
    }


    async loadAddress() {
        var addresses = this.db.get("addresses") || [];
        console.log("load address: ", addresses);
        if (!addresses.length > 0) {
            addresses = await this.GetAddress();
            this.db.set('addresses', addresses);
        }
        const addressContainer = $('#customer-address');
        addressContainer.empty();
        addresses.forEach((address) => {
            const addressRow = $(`
                <option value="${address.addressId}">${address.type}</option>
            `);
            addressContainer.append(addressRow);
        });
    }
    loadCart() {
        this.document.querySelector('#payment-container').classList.add('hidden');
        this.document.querySelector('#cart-container').classList.remove('hidden');
        log.debug("Load card")
        if (this.islogin) {
            this.loadAddress();
        }
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


    loadPayment() {
        this.document.querySelector('#cart-container').classList.add('hidden');
        this.document.querySelector('#payment-container').classList.remove('hidden');
        console.log('Loading Payment');
        const orderData = this.db.get('order') || [];
        const orderItemsContainer = $('#order-details');
        orderItemsContainer.empty();
        orderData.forEach((order) => {
            const orderRow = $(`
                <div class="flex flex-col border-b py-2">
                    <div class="flex flex-row justify-between items-center">
                        <div class="flex flex-col">
                            <h2 class="mt-2 font-bold">Order Id: #${order.orderId}</h2>
                            <span class="text-gray-500 -mt-1">${order.restaurantName}</span>
                        </div>
                    </div>
                    <div class="flex flex-col">
                        <p class="text-gray-500">Order Details: </p>
                        ${order.orderItems.map(item => `
                            <div class="flex flex-row justify-between items-center">
                                <h2 class="mt-2 mb-2 font-bold">${item.productName}</h2>
                                <span class="text-gray-500">${item.quantity} x ${item.productPrice}</span>
                                <span class="text-gray-500 rupee ">${item.quantity * item.productPrice}</span>
                            </div>                            
                        `).join('')}
                    </div>
                    <div class="flex flex-col ">
                        <p class="text-gray-500">Bill Details: </p>
                        <div class="flex flex-row justify-between items-center -mt-3">
                            <h2 class="mt-2 mb-2 font-bold">Total Order Price: </h2>
                            <span class="text-gray-500 rupee">${order.totalOrderPrice}</span>
                        </div>
                        <div class="flex flex-row justify-between items-center -mt-3">
                            <h2 class="mt-2 mb-2 font-bold">Shipping Price: </h2>
                            <span class="text-gray-500 rupee">${order.shippingPrice}</span>
                        </div>
                        <div class="flex flex-row justify-between items-center -mt-3">
                        <h2 class="mt-2 mb-2 font-bold">TaxRat: </h2>
                        <span class="text-gray-500 percent">${order.taxRat * 100}</span>
                        </div>
                        <div class="flex flex-row justify-between items-center -mt-3">
                        <h2 class="mt-2 mb-2 font-bold">Discount Rat: </h2>
                        <span class="text-gray-500 percent">${order.discountRat * 100}</span>
                        </div>
                        <div class="flex flex-row justify-between items-center -mt-3">
                            <h2 class="mt-2 mb-2 font-bold">Total Amount: </h2>
                            <span class="text-gray-500 rupee">${order.totalAmount}</span>
                        </div>
                    </div>
                </div>
            `);
            orderItemsContainer.append(orderRow);
        });


    }
    addToCart(product) {
        const orderData = this.db.get('order') || [];
        if (orderData.length > 0) {
            showAlert('You have already placed an order', 'error');
            return;
        }
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
        this.document.querySelector('#cart').classList.remove('hidden');
        showAlert('Product added to cart', 'success');
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
        e.preventDefault();
        if (!this.islogin) {
            showAlert('Please login to place order', 'error');
            return;
        }
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
            this.loadPayment();
        } catch (err) {
            console.log("Error: ", err);
            showAlert(err.message, 'error');
        }
    }

    async payment(e) {
        e.preventDefault();
        const orderData = this.db.get('order') || [];
        if (orderData.length === 0) {
            showAlert('No order to pay', 'error');
            return;
        }
        const paymentMethod = $('#payment-method').val();
        if (!paymentMethod) {
            showAlert('Please select a payment method', 'error');
            return;
        }

        var payload = {
            "orders": orderData.map(order => order.orderId),
        };

        if (paymentMethod === 'cod') {
            var res = await this.api.post('Customer/Order/payment/cod', payload)
                .then((res) => res.data)
                .catch((err) => showAlert(err.message, 'error'));
            console.log(res);
            if (res) {
                showAlert('Order placed successfully', 'success');
                this.db.remove('order');
                this.loadCart();
                this.document.querySelector('#cart').classList.add('hidden');
                this.window.href = '/customer/orders';
            }
        }
        else if (paymentMethod === 'online') {
            var res = await this.api.post('Customer/Order/payment/online', payload)
                .then((res) => res.data)
                .catch((err) => showAlert(err.message, 'error'));
            console.log(res);
            if (res) {
                showAlert('Order placed successfully', 'success');
                this.db.remove('order');
                this.loadCart();
                this.document.querySelector('#cart').classList.add('hidden');
                this.window.href = '/customer/orders';
            }
        }
    }
}

export { Cart, cartCallback };
