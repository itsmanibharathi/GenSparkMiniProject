import $ from 'jquery';
function loadComponent(id, html, callback, ...args) {
    $.ajax({
        url: html,
        type: 'GET',
        dataType: 'html',
        success: function (response) {
            $(id).html(response);
            if (callback) {
                callback(...args);
            }
        },
        error: function () {
            $(id).html('Failed to load content.');
        }
    });
}

export default loadComponent;