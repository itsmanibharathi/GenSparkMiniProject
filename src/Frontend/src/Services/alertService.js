import $ from 'jquery';
function showAlert(message, type = 'error') {
    const alertContainer = $('#alert-container');
    console.log("alertContainer");
    const alertId = 'alert-' + new Date().getTime(); // Unique ID for each alert

    const alertHtml = `
                <div id="${alertId}" class=" transform border px-4 py-3 rounded ${type}" role="alert">
                <strong class="font-bold"> <i class="fa-solid fa-triangle-exclamation"></i> ${type}!</strong>
                <span class="block sm:inline" id="alert-message">${message}</span>
                <span onclick="closeAlert('${alertId}')" id="close-alert" class="absolute top-0 bottom-0 right-0 px-4 py-3">
                    <i class="fas fa-times"></i>
                </span>
            </div>
    `;
    alertContainer.append(alertHtml);
    setTimeout(() => {
        $(`#${alertId}`).fadeOut(500, function () {
            $(this).remove();
        });
    }, 5000);
}

function closeAlert(alertId) {
    $(`#${alertId}`).fadeOut(500, function () {
        $(this).remove();
    });
}

window.closeAlert = closeAlert;


export default showAlert;