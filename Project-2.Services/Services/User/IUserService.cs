using Project_2.Models;

namespace Project_2.Services;
public interface IUserService{
    Task<string> GenerateToken(User user);
}