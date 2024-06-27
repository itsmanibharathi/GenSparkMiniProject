import $, { type } from 'jquery';
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

    $('#addressForm').on('change', 'select', (e) => {
        e.preventDefault();
        let select = e.target;
        if (select.value == '') {
            select.classList.add('border-red-500');
            select.nextElementSibling.innerText = 'This field is required';
        } else {
            select.classList.remove('border-red-500');
            select.classList.add('border-green-500');
            select.nextElementSibling.innerText = '';
        }
    });

    $('#addressForm').on('blur', 'input', (e) => {
        e.preventDefault();
        let input = e.target;
        if (input.value == '' && input.required) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'This field is required';
        }
        // code 
        else if (input.name == 'code' && input.value.length != 1) {
            if (!/^[a-zA-Z]$/.test(input.value)) {
                input.classList.add('border-red-500');
                input.nextElementSibling.innerText = 'Code must be a single character a-z';
            }
        }
        else if (input.name == 'zipCode' && input.value.length != 6) {
            if (!/^[0-9]{6}$/.test(input.value)) {
                input.classList.add('border-red-500');
                input.nextElementSibling.innerText = 'Zip code must be 6 digits';
            }
        }
        else {
            input.classList.remove('border-red-500');
            input.nextElementSibling.innerText = '';
            if (input.value !== '') {
                input.classList.add('border-green-500');
            }
        }



        // if (e.target.value === '') {
        //     e.target.classList.add('border-red-500'); 
        //     e.target.nextElementSibling.innerText = 'This field is required';
        // }
        // else {
        //     e.target.classList.remove('border-red-500');
        // }
    }
    );


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