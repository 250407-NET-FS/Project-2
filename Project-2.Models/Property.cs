using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Project_2.Models;

[Table("Properties")]
public class Property {
    [Key]
    public Guid PropertyID { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(50)]
    public string? Country { get; set; }
    [Required]
    [StringLength(50)]
    public string? State { get; set; }
    [Required]
    [StringLength(50)]
    public string? City { get; set; }
    [Required]
    [StringLength(10)]
    public string? ZipCode { get; set; }
    [Required]
    [StringLength(50)]
    public string? StreetAddress { get; set; }
    [Required]
    [Precision(18, 2)]
    public decimal StartingPrice { get; set; }

    [Required]
    public int Bedrooms { get; set; }

    [Required]
    [Precision(10, 1)]
    //When it was float it caused Column, parameter, or variable #8: Cannot specify a column width on data type real.
    public decimal Bathrooms { get; set; }

    public DateTime ListDate { get; set; } = DateTime.Now;

    [ForeignKey(nameof(Owner))]
    public Guid? OwnerID { get; set; }

    public bool ForSale { get; set; } = true;

    // Navigations
    #nullable disable
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public User Owner { get; init; }
    #nullable restore

    public Property() {}

    public Property(string country, string state, string city, string zipCode,
                    string streetAddress, decimal startingPrice, int bedrooms, decimal bathrooms, Guid ownerId) {
        Country = country;
        State = state;
        City = city;
        ZipCode = zipCode;
        StreetAddress = streetAddress;
        StartingPrice = startingPrice;
        Bedrooms = bedrooms;
        Bathrooms = bathrooms;
        OwnerID = ownerId;
    }
}
