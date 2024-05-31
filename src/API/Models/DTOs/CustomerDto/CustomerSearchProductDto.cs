using API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.CustomerDto
{
    /// <summary>
    /// To search for products by the customer base on the query 
    /// <para> ProductName, ProductPrice, ProductAvailable, ProductCategories, RestaurantName, RestaurantBranch</para>
    /// </summary>
    public class CustomerSearchProductDto
    {
        public string? ProductName { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? ProductPrice { get; set; }
        public bool ProductAvailable { get; set; } = true;
        [EnumDataType(typeof(ProductCategory), ErrorMessage = "Invalid product category")]
        public ICollection<ProductCategory>? ProductCategories { get; set; }
        public string? RestaurantName { get; set; }
        public string? RestaurantBranch { get; set; }

    }

}
