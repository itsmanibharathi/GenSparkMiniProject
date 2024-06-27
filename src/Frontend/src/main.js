import './styles/auth.css';
import $ from 'jquery';
import { loadRoutes } from './Services/routerService';
import fontawesome from '@fortawesome/fontawesome-free/css/all.css';
import dotenv from 'dotenv';
dotenv.config();

$(document).ready(() => {
    loadRoutes();
});