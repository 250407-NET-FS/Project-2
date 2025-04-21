using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.Models;

[Table("Purchases")]
public class Purchase(Guid OwnerID, Guid PropertyID, string FinalPrice)
{
    [Key]
    public Guid PurchaseID { get; set; } = Guid.NewGuid();
    [Required]
    [ForeignKey("OwnerID")]
    public Guid OwnerID { get; set; } = OwnerID;    
    [Required]
    [ForeignKey("PropertyID")]
    public Guid PropertyID { get; set; } = PropertyID;
    [Required]
    [StringLength(50)]
    public string FinalPrice { get; set; } = FinalPrice;
    public DateTime Date { get; set; } = DateTime.Now;
}