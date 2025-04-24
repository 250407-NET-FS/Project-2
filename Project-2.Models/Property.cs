using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project_2.Models;

[Table("Properties")]
public class Property(string Country, string State, string City, string ZipCode,

 string StreetAddress, decimal StartingPrice, int Bedrooms, decimal Bathrooms)

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
    [StringLength(50)]
    public string? City { get; set; } = City;
    [Required]
    [StringLength(10)]
    public string? ZipCode { get; set; } = ZipCode;
    [Required]
    [StringLength(50)]
    public string? StreetAddress { get; set; } = StreetAddress;
    [Required]
    [Precision(18, 2)]
    public decimal StartingPrice { get; set; } = StartingPrice;

    [Required]
    public int Bedrooms { get; set; } = Bedrooms;

    [Required]
    [Precision(10, 1)]
    //When it was float it caused Column, parameter, or variable #8: Cannot specify a column width on data type real.
    public decimal Bathrooms { get; set; } = Bathrooms;

    public DateTime ListDate { get; set; } = DateTime.Now;

    [ForeignKey("OwnerID")]
    public Guid OwnerID { get; set; }

    public bool ForSale { get; set; } = true;
}
