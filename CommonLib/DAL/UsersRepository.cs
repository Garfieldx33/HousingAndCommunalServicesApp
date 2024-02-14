using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DAL
{
    public partial class PostgresRepository
    {
        public async Task<List<User>> GetUsers(CancellationToken cancellation = default)
        {
            return await context.Users.AsNoTracking().ToListAsync(cancellation);
        }

        public User GetUserbyId(int userId, CancellationToken cancellation = default)
        {
            return (User)context.Users.AsNoTracking().Where(u => u.Id == userId);
        }

        public Task<User> GetUserbyLogin(string login, CancellationToken cancellation = default)
        {
            return Task.FromResult((User)context.Users.AsNoTracking().Where(u => u.Login == login));
        }

        public Task<User> GetUserbyEmail(string email, CancellationToken cancellation = default)
        {
            return Task.FromResult((User)context.Users.AsNoTracking().Where(u => u.Email == email));
        }

        public Task<User> GetUserbyPhone(string phone, CancellationToken cancellation = default)
        {
            return Task.FromResult((User)context.Users.AsNoTracking().Where(u => u.Phone == phone));
        }

        public async Task<int> AddNewUser(User newUser, CancellationToken cancellation = default)
        {
            await context.Users.AddAsync(newUser, cancellation);
            return context.SaveChanges();
        }

        public async Task<User> UpdateUserAsync(User userUpdate, CancellationToken cancellation = default)
        {
            User? updatedApp;
            if (userUpdate.Id > 0)
            {
                updatedApp = context.Users.Single(q => q.Id == userUpdate.Id);
                if (updatedApp != null)
                {
                    context.Users.Update(userUpdate);
                    await context.SaveChangesAsync(cancellation);
                }
            }
            return context.Users.Single(a => a.Id == userUpdate.Id);
        }

        public async Task<int> DeleteUserAsync(int userId, CancellationToken cancellation = default)
        {
            if (userId > 0)
            {
                var delUser = context.Users.Single(q => q.Id == userId);
                if (delUser != null)
                {
                    context.Users.Remove(delUser);
                    return await context.SaveChangesAsync(cancellation);
                }
            }
            return await Task.FromResult(0);
        }

    }
}
