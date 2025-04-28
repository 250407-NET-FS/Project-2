using System.ComponentModel.DataAnnotations;

namespace Project_2.Models.DTOs;

public class OfferNewDTO
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int PropertyId { get; set; }
    [Required]
    public int BidAmount { get; set; }

}