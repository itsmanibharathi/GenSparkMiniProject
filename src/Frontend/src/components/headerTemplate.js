import logo from '../../public/assets/Image/logo.svg'
const headerTemplate = (localRoutes, islogin, addCart) => {
    return (
        `<header class="bg-tertiary">
            <nav class="flex justify-between items-center w-[92%] h-[8%] mx-auto">
                <div>
                    <a href="/">
                    <img class="w-16 cursor-pointer" src="${logo}" alt="...">
                    </a>
                </div>
                <div
                    class="bg-tertiary text-white  z-10 text-lg nav-links duration-500 md:static absolute md:min-h-fit min-h-[60vh] left-0 top-[-100%] md:w-auto w-full flex items-center px-5">
                    <ul class="m-auto flex md:flex-row flex-col md:items-center md:gap-[4vw] gap-8">
                    <li><a href="/" class="nav" >Main</a></li>
                    ${islogin ? `
                          ${localRoutes.routes.map((item) => item.hide? '' : `<li><a href="${item.path}" class="text-white hover:text-button-hover">${item.name}</a></li>`).join('')}
                            </ul>
                            </div>
                            <div class="flex items-center gap-6 my-2">
                                <a href="/${localRoutes.name}/logout" class="bg-button text-white text-lg px-5 py-2 rounded-xl ">logout</a>
                                ` : `
                                </ul>
                                </div>
                                <div class="flex items-center gap-6 my-2">
                                <a href="/${localRoutes.name}/auth" class="bg-button text-white text-lg px-5 py-1 rounded-xl">LogIn</a>`
        } 
                            ${addCart ? ` <button onclick="onCartClick(this)" ><i class="fa-solid fa-cart-arrow-down text-white hover:text-secondary"></i></button>` : ''}
                            <button onclick="onToggleMenu(this)" class="md:hidden "><i name="menu" class="fa-solid fa-bars"></i></button>
                </div>
            </nav>
        </header>
        <script>
            function onToggleMenu(e) {
                const navLinks = document.querySelector('.nav-links')
                e.name = e.name === 'menu' ? 'close' : 'menu'
                navLinks.classList.toggle('top-[6%]');

            }
            function onCartClick() {
                const cart = document.querySelector('#cart');
                cart.classList.toggle('hidden');
            }
        </script>`
    )
}

export default headerTemplate;