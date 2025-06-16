namespace DainikBazar.Service.Models;

public class ChangePasswordRequest
{
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
