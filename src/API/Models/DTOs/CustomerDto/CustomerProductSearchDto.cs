using API.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.CustomerDto
{
    /// <summary>
    /// To search for products by the customer based on the query 
    /// <para> ProductName, ProductPrice, ProductAvailable, ProductCategories, RestaurantName, RestaurantBranch</para>
    /// </summary>
    public class CustomerProductSearchDto
    {
        [MaxLength(100, ErrorMessage = "Product name cannot be longer than 100 characters.")]
        public string? ProductName { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than 0.")]
        public decimal? ProductPrice { get; set; }

        public bool? ProductAvailable { get; set; } 

        [EnumDataType(typeof(ProductCategory), ErrorMessage = "Invalid product category.")]
        public ICollection<ProductCategory>? ProductCategories { get; set; }


        [MaxLength(100, ErrorMessage = "Restaurant name cannot be longer than 100 characters.")]
        public string? RestaurantName { get; set; }

        [MaxLength(100, ErrorMessage = "Restaurant branch cannot be longer than 100 characters.")]
        public string? RestaurantBranch { get; set; }

    }
}
