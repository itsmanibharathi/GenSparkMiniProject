const OrderTemplate = (item) => {

    return (`
                <div class="flex flex-col md:flex-row justify-between items-center w-[92%] mx-auto my-4 p-4 border rounded-lg shadow-lg cursor-pointer order-card" onclick="toggleDetails(event)">
            <div class="w-full md:w-1/3">
                <h2 class="text-2xl font-bold">Order ID: ${item.orderId}</h2>
            </div>
            <div class="w-full md:w-1/3">
                <h2 class="text-xl">${new Date(item.orderDate).toLocaleDateString()}</h2>

            </div>
            <div class="w-full md:w-1/3">
                <h2 class="text-xl ${item.orderStatus.toLowerCase()} text-center m-2">${item.orderStatus}</h2>
            </div>
            <div class="hidden details w-full mt-4 md:mt-0 md:flex md:flex-col">
                <div class="flex justify-between">
                    <span class="font-semibold">Total Price:</span> <span>${item.totalOrderPrice}</span>
                </div>
                <div class="flex justify-between">
                    <span class="font-semibold">Shipping Price:</span> <span>${item.shippingPrice}</span>
                </div>
                <div class="flex justify-between">
                    <span class="font-semibold">Total Amount:</span> <span>${item.totalAmount}</span>
                </div>
                <div class="flex justify-between">
                    <span class="font-semibold">Payment Status:</span> <span>${item.paymentStatus}</span>
                </div>
                <div class="flex justify-between">
                    <span class="font-semibold">Delivery Date:</span> 
                    <span>  ${new Date(item.deliveryDate).getFullYear() < 2024 ? '--:--:--' : new Date(item.deliveryDate).toLocaleDateString()}</span>
                </div>
            </div>
        </div>

        <script>
            function toggleDetails(event) {
                const card = event.currentTarget;
                const details = card.querySelector('.details');
                details.classList.toggle('hidden');
            }
        </script>
    `);
}
export default OrderTemplate;