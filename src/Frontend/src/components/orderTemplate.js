const OrderTemplate = (item, module) => {
    const setBtn = (item, module, employeeName) => {
        const { orderId, orderStatus } = item;
        if (module === 'customer') {
            if (orderStatus == 'Delivered') {
                return `<button class="mt-2 bg-gray-600 text-white px-4 py-2 rounded" onClick="buyAgain(${orderId})" >Buy Again</button>`;
            }
        }

        else if (module === 'restaurant') {
            if (orderStatus == 'Place') {
                return `<button class="mt-2 bg-green-700 text-white px-4 py-2 rounded" onClick="updateOrder(${orderId},'Preparing')" >Start Preparing</button>`;
            }
            else if (orderStatus === 'Preparing') {
                return `<button class="mt-2 bg-green-700 text-white px-4 py-2 rounded" onClick="updateOrder(${orderId},'Prepared')" >Prepared Order</button>`;
            }
        }
        else if (module === 'employee') {
            console.log(orderStatus);
            if (employeeName == null) {
                return `<button class="mt-2 bg-green-700 text-white px-4 py-2 rounded" onClick="updateOrder(${orderId},'accept')" >Accept Order</button>`;
            }
            else if (orderStatus == 'Prepared') {
                return `<button class="mt-2 bg-green-700 text-white px-4 py-2 rounded" onClick="updateOrder(${orderId},'pickup')" >PickedUp Order</button>`;
            }
            else if (orderStatus == 'PickedUp') {
                return `<button class="mt-2 bg-green-700 text-white px-4 py-2 rounded" onClick="updateOrder(${orderId},'deliver')" >Delivered Order</button>`;
            }
        }
        return '';
    }

    const orderItemsHtml = item.orderItems.map(orderItem => `
        <div class="flex flex-col md:flex-row">
            <img src="https://www.foodiesfeed.com/wp-content/uploads/2023/06/burger-with-melted-cheese-768x768.jpg" alt="Product Image"
                class="w-full md:w-1/4 rounded-lg mb-4 md:mb-0">
            <div class="md:ml-4 flex-1">
                <h2 class="text-xl font-bold">${orderItem.productName} <span class="font-light">${orderItem.restaurantName}</span></h2>
                <p class="mt-2">${orderItem.productDescription}</p>
                <div class="mt-4">
                    <p class="text-xl font-bold text-green-400 rupee">${orderItem.productPrice.toFixed(2)}</p>
                    <p class="text-gray-400">Quantity: ${orderItem.quantity}</p>
                </div>
            </div>
        </div>
    `).join('');

    return `
        <div id="order-${item.orderId}" class="relative max-w-4xl mx-auto bg-white rounded-lg shadow-md p-4 mb-4 hover:shadow-lg">
            <div class="absolute top-1 right-1">
            <span class="${item.orderStatus.toLowerCase()} rounded-lg px-2">${item.orderStatus}</span>
            </div>

            <div class="flex flex-wrap justify-between items-center">
                <div class="w-full sm:w-auto mb-2 sm:mb-0">
                    <p class="text-gray-600">ORDER PLACED</p>
                    <p class="font-bold">${new Date(item.orderDate).toLocaleDateString()}</p>
                </div>
                <div class="w-full sm:w-auto mb-2 sm:mb-0">
                    <p class="text-gray-600">TOTAL</p>
                    <p class="font-bold rupee">${item.totalOrderPrice.toFixed(2)}</p>
                </div>
                <div class="w-full sm:w-auto mb-2 sm:mb-0">
                    <p class="text-gray-600">SHIP TO</p>
                    <p class="font-bold">${item.customerName}</p>
                </div>
                <div class="w-full sm:w-auto mb-2 sm:mb-0">
                    <p class="text-gray-600">PICKUP BY</p>
                    <p class="font-bold">${item.employeeName ?? '---'}</p>
                </div>
                <div class="w-full sm:w-auto mb-2 sm:mb-0">
                    <p class="text-gray-600">ORDER #</p>
                    <p class="font-bold">${item.orderId}</p>
                </div>
                <div class="w-full sm:w-auto bottom-1 ">
                    <button class="text-blue-600" onclick="toggleDetails('order-details-${item.orderId}')">View order details</button>
                </div>
            </div>
            <div id="order-details-${item.orderId}" class="hidden mt-4">
                <div class=" bg-tertiary text-white rounded-lg p-4">
                    <div class="grid grid-cols-1 md:grid-cols-2 gap-y-2">
                        ${orderItemsHtml}
                    </div>
                    ${module === 'customer' || module === 'employee' ? `
                        <div class="mt-4">
                            <p class="text-xl font-bold">Delivery Address</p>
                            <p>${item.deliveryAddress}</p>
                        </div>
                    ` : ''}
                    <div class="mt-4 md:mt-0 md:ml-auto text-right">
                        ${setBtn(item, module, item.employeeName)}
                    </div>
                </div>
            </div>
        </div>
    `;
};

window.toggleDetails = (id) => {
    var orderDetails = document.getElementById(id);
    orderDetails.classList.toggle('hidden');
}
export default OrderTemplate;