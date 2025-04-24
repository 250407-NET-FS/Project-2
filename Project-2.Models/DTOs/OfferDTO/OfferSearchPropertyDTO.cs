using System.ComponentModel.DataAnnotations;

namespace Project_2.Models.DTOs;

public class OfferSearchPropertyDTO
{
    [Required]
    public Guid PropertyId { get; set; }
}