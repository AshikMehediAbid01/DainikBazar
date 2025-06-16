using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Managers.Interfaces;

public interface IAuth0Manager
{
    Task<bool> ChangePassword(string userId, string newPassword);
    Task<User> GetUserInfo(string userId);
}

