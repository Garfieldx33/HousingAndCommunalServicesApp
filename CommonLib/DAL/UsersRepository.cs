using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.DAL;

public partial class PostgresRepository
{
    public async Task<List<User>> GetUsers(CancellationToken cancellation = default)
    {
        return await _context.Users.AsNoTracking().ToListAsync(cancellation);
    }

    public User GetUserbyId(int userId, CancellationToken cancellation = default)
    {
        return (User)_context.Users.AsNoTracking().Where(u => u.Id == userId);
    }

    public Task<User> GetUserbyLogin(string login, string password, CancellationToken cancellation = default)
    {
        return _context.Users.AsNoTracking().Where(u => u.Login == login && u.Password == password).SingleAsync(cancellation);
    }

    public List<User> GetUsersByDepartmentId(int departmentId)
    {
        List<int> userIds = _context.EmployeeInfos.AsNoTracking().Where(u => u.DepartmentId == departmentId).Select(i => i.EmployeeUserId).ToList();
        return _context.Users.AsNoTracking().Where(u => userIds.Contains(u.Id)).ToList();

    }

    public async Task<int> AddNewUser(User newUser, CancellationToken cancellation = default)
    {
        await _context.Users.AddAsync(newUser, cancellation);
        return _context.SaveChanges();
    }

    public async Task<User> UpdateUserAsync(User userUpdate, CancellationToken cancellation = default)
    {
        if (userUpdate.Login != string.Empty)
        {
            User updatedUser = _context.Users.Single(q => q.Login == userUpdate.Login);
            if (updatedUser != null)
            {
                if (userUpdate.FirstName != string.Empty) updatedUser.FirstName = userUpdate.FirstName;
                if (userUpdate.SecondName != string.Empty) updatedUser.SecondName = userUpdate.SecondName;
                if (userUpdate.Password != string.Empty) updatedUser.Password = userUpdate.Password;
                if (userUpdate.Email != string.Empty) updatedUser.Email = userUpdate.Email;
                if (userUpdate.Address != string.Empty) updatedUser.Address = userUpdate.Address;
                if (userUpdate.Phone != string.Empty) updatedUser.Phone = userUpdate.Phone;
                if (!float.IsNaN(userUpdate.Balance)) updatedUser.Balance = userUpdate.Balance;

                _context.Users.Update(updatedUser);
                await _context.SaveChangesAsync(cancellation);
            }
        }
        return _context.Users.Single(q => q.Login == userUpdate.Login);
    }

    public async Task<int> DeleteUserAsync(int userId, CancellationToken cancellation = default)
    {
        if (userId > 0)
        {
            var delUser = _context.Users.Single(q => q.Id == userId);
            if (delUser != null)
            {
                _context.Users.Remove(delUser);
                return await _context.SaveChangesAsync(cancellation);
            }
        }
        return await Task.FromResult(0);
    }

}
