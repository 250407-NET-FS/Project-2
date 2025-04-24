using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project_2.Models;

[Table("Offers")]
public class Offer(Guid UserID, Guid PropertyID, decimal BidAmount)
{
    [Key]
    public Guid OfferID { get; set; } = Guid.NewGuid();
    [Required]
    [ForeignKey("UserID")]
    public Guid UserID { get; set; } = UserID;
    [Required]
    [ForeignKey("PropertyID")]
    public Guid PropertyID { get; set; } = PropertyID;

    [Required]
    [Precision(18, 2)]
    public decimal BidAmount { get; set; } = BidAmount;

    public DateTime Date { get; set; } = DateTime.Now;
}
