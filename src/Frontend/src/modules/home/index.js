import $ from 'jquery';
import HomeHeader from '../../components/home-header.html';
import HomeBody from './index.html'
import Footer from '../../components/footer.html';
import loadComponent from '../../services/loadComponent.js';

const loadHome = () => {
    console.log('Loading Home');
    loadComponent('#header-placeholder', HomeHeader);
    loadComponent('#body-placeholder', HomeBody);
    loadComponent('#footer-placeholder', Footer);
}

export default loadHome;