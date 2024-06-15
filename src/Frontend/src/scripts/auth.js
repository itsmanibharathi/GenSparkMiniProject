import '../styles/auth.css';

const signUpPage = () => {

    const authContainer = document.getElementById('auth-container');
    authContainer.classList.add('right-panel-active');
}

const signInPage = () => {
    const authContainer = document.getElementById('auth-container');
    authContainer.classList.remove('right-panel-active');
}

window.signUpPage = signUpPage;
window.signInPage = signInPage;