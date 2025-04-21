using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.Models;

public class Credential(Guid UserID, string UserToken)
{
    public Guid UserID { get; set; } = UserID;

    public string UserToken { get; set; } = UserToken;
}