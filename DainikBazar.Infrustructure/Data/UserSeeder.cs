using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Infrastructure.Data;

public class UserSeeder
{
    public async Task SeedInitialUserAsync(AppDbContext db)
    {
        if(await db.Users.AnyAsync())
        {
            return;
        }

        var users = new List<User>();
        users.Add(new User { Id = Guid.NewGuid().ToString(), Name = "Alice" });
        users.Add(new User { Id = Guid.NewGuid().ToString(), Name = "Bob" });
        await db.Users.AddRangeAsync(users);
        await db.SaveChangesAsync();
    }
}
