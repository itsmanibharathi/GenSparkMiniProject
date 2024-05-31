using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.CustomerDto
{
    /// <summary>
    /// Child Dto For CustomerOrderDto
    /// <para>To Collect the Order item</para>
    /// </summary>
    public class CustomerOrderItemDto
    {
        [Required(ErrorMessage = "Product id is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
