using System.ComponentModel.DataAnnotations;

namespace Project_2.Models.DTOs;

public class CreatePurchaseDTO
{
    [Required]
    public Guid PropertyId { get; set; }
    [Required]
    public Guid BuyerId { get; set; }
    public DateTime PurchaseDate { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public decimal PurchaseAmount { get; set; }
}