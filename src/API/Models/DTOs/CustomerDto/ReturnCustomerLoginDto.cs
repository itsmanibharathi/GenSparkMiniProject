﻿namespace API.Models.DTOs.CustomerDto
{
    public class ReturnCustomerLoginDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Token { get; set; }
    }
}
