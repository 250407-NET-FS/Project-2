using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project_2.Models;

[Table("Properties")]
public class Property(string Country, string State, string ZipCode,
 string StreetAddress, decimal StartingPrice)
{
    [Key]
    public Guid PropertyID { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(50)]
    public string? Country { get; set; } = Country;
    [Required]
    [StringLength(50)]
    public string? State { get; set; } = State;
    [Required]
    [StringLength(10)]
    public string? ZipCode { get; set; } = ZipCode;
    [Required]
    [StringLength(50)]
    public string? StreetAddress { get; set; } = StreetAddress;
    [Required]
    [Precision(18, 2)]
    public decimal StartingPrice { get; set; } = StartingPrice;

    public DateTime ListDate { get; set; } = DateTime.Now;

    [ForeignKey("OwnerID")]
    public Guid OwnerID { get; set; }
}
