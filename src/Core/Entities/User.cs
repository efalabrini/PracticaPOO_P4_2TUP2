using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class User
{
    [Column(Order = 0)]
    public int Id { get; set; }

    [Column(Order = 1)]
    public Guid? ExternalId { get; set; }

    [Column(Order = 2)]
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Column(Order = 3)]
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [Column(Order = 4)]
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Column(Order = 5)]
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Column(Order = 6)]
    [Required]
    public bool IsActive { get; set; }

    [MaxLength(25)]
    public string Phone { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(100)")]
    public string Password { get; set; }

}