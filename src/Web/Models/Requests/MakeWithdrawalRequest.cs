using System.ComponentModel.DataAnnotations;

namespace Web.Models.Requests;

public record MakeWithdrawalRequest(
    [Required]
    decimal Amount,

    [Required]
    string Note,

    [Required]
    string Number
);