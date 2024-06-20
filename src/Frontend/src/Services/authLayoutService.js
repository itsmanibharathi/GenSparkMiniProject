const signUpPage = () => {

    const authContainer = document.getElementById('auth-container');
    authContainer.classList.add('right-panel-active');
}

const signInPage = () => {
    const authContainer = document.getElementById('auth-container');
    authContainer.classList.remove('right-panel-active');
}

export { signUpPage, signInPage };