using System.ComponentModel.DataAnnotations;

namespace DainikBazar.UI.Models;

public class ChangePasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 8)]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}
