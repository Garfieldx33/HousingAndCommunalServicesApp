﻿using CommonLib.Entities;
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

        public Task<User> GetUserbyLogin(string login, string password, CancellationToken cancellation = default)
        {
            return context.Users.AsNoTracking().Where(u => u.Login == login && u.Password == password).SingleAsync(cancellation);
        }

        public List<User> GetUserbyDepartmentId(int departmentId)
        {
            List<int> userIds = context.EmployeeInfos.AsNoTracking().Where(u => u.DepartmentId == departmentId).Select(i => i.EmployeeUserId).ToList();
            return context.Users.AsNoTracking().Where(u => userIds.Contains(u.Id)).ToList();
        }

        /*public Task<User> GetUserbyPhone(string phone, CancellationToken cancellation = default)
        {
            return context.Users.AsNoTracking().Where(u => u.Phone == phone).SingleAsync(cancellation);
        }*/

        public async Task<int> AddNewUser(User newUser, CancellationToken cancellation = default)
        {
            await context.Users.AddAsync(newUser, cancellation);
            return context.SaveChanges();
        }

        public async Task<User> UpdateUserAsync(User userUpdate, CancellationToken cancellation = default)
        {
            if (userUpdate.Login != string.Empty)
            {
                User updatedUser = context.Users.Single(q => q.Login == userUpdate.Login);
                if (updatedUser != null)
                {
                    if (userUpdate.FirstName != string.Empty) updatedUser.FirstName = userUpdate.FirstName;
                    if (userUpdate.SecondName != string.Empty) updatedUser.SecondName = userUpdate.SecondName;
                    if (userUpdate.Password != string.Empty) updatedUser.Password = userUpdate.Password;
                    if (userUpdate.Email != string.Empty)  updatedUser.Email = userUpdate.Email; 
                    if (userUpdate.Address != string.Empty) updatedUser.Address = userUpdate.Address;
                    if (userUpdate.Phone != string.Empty) updatedUser.Phone = userUpdate.Phone;
                    if (!float.IsNaN(userUpdate.Balance)) updatedUser.Balance = userUpdate.Balance;

                    context.Users.Update(updatedUser);
                    await context.SaveChangesAsync(cancellation);
                }
            }
            return context.Users.Single(q => q.Login == userUpdate.Login);
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
