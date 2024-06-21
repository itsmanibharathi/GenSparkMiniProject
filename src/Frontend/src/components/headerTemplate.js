import logo from '../../public/assets/Image/logo.svg'
const headerTemplate = (data, btn) => {
    return (
        `<header class="bg-tertiary">
            <nav class="flex justify-between items-center w-[92%] h-[8%]  mx-auto">
                <div>
                    <img class="w-16 cursor-pointer" src=${logo} alt="...">
                </div>
                <div
                    class=" text-white text-lg nav-links duration-500 md:static absolute md:min-h-fit min-h-[60vh] left-0 top-[-100%] md:w-auto  w-full flex items-center px-5">
                    <ul class="flex md:flex-row flex-col md:items-center md:gap-[4vw] gap-8">
                    ${data.map
            (item => `<li>
                        <a href='${item.path}' class="hover:text-button">${item.name}</a>
                    </li>`).join('')
        }

                    </ul>
                </div>
                <div class="flex items-center gap-6 my-2">
                    <button onclick="${btn.function}" class="bg-button text-white text-lg px-5 py-2 rounded-full hover:bg-button-hover">${btn.name}</button>
                    <button onclick="onToggleMenu(this)" class="md:hidden"><i name="menu" class="fa-solid fa-bars"></i></button>
                </div>
            </nav>
        </header>
        <script>
            const navLinks = document.querySelector('.nav-links')
            function onToggleMenu(e) {
                e.name = e.name === 'menu' ? 'close' : 'menu'
                navLinks.classList.toggle('top-[8%]')
            }
        </script>`
    )
}

export default headerTemplate;