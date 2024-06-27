const ProductTemplate = (product, isrestaurant = false) => {
    return (
        `
    <div id="p-${product.productId}" class="w-full sm:w-1/2 md:w-1/2 lg:w-1/3 xl:w-1/4 p-4">
        <div class="c-card block bg-white text-tertiary shadow-md hover:shadow-xl rounded-lg overflow-hidden">
            <div class="relative pb-48 overflow-hidden">
                <img class="absolute inset-0 h-full w-full object-cover"
                    src="https://www.foodiesfeed.com/wp-content/uploads/2023/06/burger-with-melted-cheese-768x768.jpg"
                    alt="">
            </div>
            <div class="p-4">
                <span class="inline-block px-2 py-1 leading-none  rounded-full font-semibold uppercase tracking-wide text-xs ${product.productAvailable} ">${product.productCategories} <span class=" font-bold" > ${product.productAvailable ? 'Available' : 'Not Available'} </span> </span>
                <h2 class="mt-2 mb-2  font-bold">${product.productName} <span class="font-light">${product.restaurantName}</span></h2> 
                <p class="text-sm">${product.productDescription}</p>
                <div class="mt-3 flex items-center">
                    <span class="font-bold text-xl rupee">${product.productPrice}</span>
                </div>
                ${isrestaurant
            ? `<button onclick="editProduct(${product.productId})" class="bg-button text-white text-lg px-5 py-2 rounded-lg hover:bg-button-hover w-full"><i class="fa-solid fa-edit"></i> Edit</button>`
            : `<button onclick="addToCart(${product.productId})" class="bg-button text-white text-lg px-5 py-2 rounded-lg hover:bg-button-hover w-full"><i class="fa-solid fa-cart-shopping"></i> Add to Cart</button>`
        }
            </div>
        </div>
    </div>`

    );
}

export default ProductTemplate;