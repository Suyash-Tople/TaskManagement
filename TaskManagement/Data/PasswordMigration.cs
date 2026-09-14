using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Data
{
    public class PasswordMigration
    {
        public static async Task MigratePasswordAsync(AppDbContext context)
        {
            var passwordHasher = new PasswordHasher<User>();
            var users = await context.Users.ToListAsync();
            foreach(var user in users)
            {
                if (string.IsNullOrWhiteSpace(user.Password))
                    continue;

                user.Password = passwordHasher.HashPassword(user, user.Password);
            }
            await context.SaveChangesAsync();
        }
    }
}
