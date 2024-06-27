import $ from 'jquery';
import AddressPage from './address.html'
import showAlert from '../../Services/alertService';
import log from '../../utility/loglevel';
const AddressCallback = async (api, localStorage) => {
    $('#addAddressButton').click(() => {
        $('#addAddressForm').toggle();
        $('#addAddressButton').addClass('hidden');
    });

    $('#formClose').click(() => {
        $('#addAddressForm').toggle();
        $('#addAddressButton').removeClass('hidden');
    });

    $('#addressForm').submit(async (e) => {
        e.preventDefault();
        const formData = $('#addressForm').serializeArray();
        const data = {};
        formData.forEach((item) => {
            data[item.name] = item.value;
        });
        await api.post('customer/address', data)
            .then((res) => {
                showAlert('Address added successfully', 'success');
                log.debug('Address added:', res.data);
                $('#addAddressForm').toggle();
                $('#addAddressButton').removeClass('hidden');
                $('#addressForm')[0].reset();
                const addressContainer = $('#addressList');
                addressContainer.append(addressTemplate(res.data));
                const address = localStorage.get('addresses');
                address.push(res.data);
                localStorage.set('addresses', address);
            })
            .catch((err) => {
                log.error(err);
                showAlert('Failed to add address', 'error');
            });
    });

    await api.get('customer/address')
        .then((res) => {
            loadAddress(res.data);
        })
        .catch((err) => {
            log.error(err);
            if (err.response.status === 404) {
                showAlert('No address found', 'info');
                const addressContainer = $('#addressList');
                addressContainer.empty();
                addressContainer.append('<p class="text-center text-gray-600">No address found</p>');
                return;
            }
            else {
                showAlert('Failed to load address', 'error');
            }
        });
}

const loadAddress = (address) => {
    const addressContainer = $('#addressList');
    addressContainer.empty();
    addressContainer.append(address.map((address) => addressTemplate(address)).join(''));
}

const addressTemplate = (address) => {
    return (`
                <div class="mt-4 bg-white p-2 rounded-lg shadow-md hover:shadow-lg md:1/3 lg:w-1/4  ">
                        <p class="text-sm text-gray-600"><strong>Address ID:</strong> ${address.addressId}</p>
                        <p class="text-sm text-gray-600"><strong>City:</strong> ${address.city} </p>
                        <p class="text-sm text-gray-600"><strong>Code:</strong> ${address.code}</p>
                        <p class="text-sm text-gray-600"><strong>Country:</strong> ${address.country} </p>
                        <p class="text-sm text-gray-600"><strong>State:</strong> ${address.state}</p>
                        <p class="text-sm text-gray-600"><strong>Street:</strong> ${address.state}</p>
                        <p class="text-sm text-gray-600"><strong>Type:</strong> ${address.type}</p>
                        <p class="text-sm text-gray-600"><strong>Zip Code:</strong>${address.zipCode} </p>
                        <p class="text-sm text-gray-600"><strong>Updated At:</strong> ${address.updateAt} <i class="fa-solid fa-clock-rotate-left"></i> </p>
                    </div>

                `);
}

module.exports = {
    AddressPage,
    AddressCallback
}