using Microsoft.EntityFrameworkCore.Update.Internal;
using Project_2.Models;

namespace Project_2.Data;

public interface IUserRepository : IBaseRepository<User> {
    public Task Update(User user);
}