﻿using API.Models.Enums;

namespace API.Models.DTOs
{
    public class AddCustomerAddressDto
    {
        public int CustomerId { get; set; }
        public AddressType Type { get; set; }
        public AddressCode Code { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }

    }
}