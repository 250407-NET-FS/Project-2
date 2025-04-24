using System.ComponentModel.DataAnnotations;

namespace Project_2.Models.DTOs;

public class OfferSearchIdDTO
{
    [Required]
    public Guid OfferId { get; set; }
}