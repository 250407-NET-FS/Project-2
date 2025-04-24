using System.ComponentModel.DataAnnotations;

namespace Project_2.Models.DTOs;

public class OfferRemoveIdDTO
{
    [Required]
    public Guid OfferId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid PropertyId { get; set; }

}