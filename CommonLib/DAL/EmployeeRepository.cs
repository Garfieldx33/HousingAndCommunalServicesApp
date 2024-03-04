using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DAL
{
    //todo
    public partial class PostgresRepository
    {

        public List<int> GetUserIdsByDepartmentId(int departmentId, CancellationToken cancellation = default)
        {
            return context.EmployeeInfos.AsNoTracking().Where(u => u.DepartmentId == departmentId).Select(i => i.EmployeeUserId).ToList();
        }

        public int GetByDepartmentIdByUserId(int employeeId, CancellationToken cancellation = default)
        {
            return context.EmployeeInfos.AsNoTracking().Where(u => u.EmployeeUserId == employeeId).Select(i => i.DepartmentId).First();
        }

        public async Task<int> AddNewEmployee(int employeeId, int departmentId, CancellationToken cancellation = default)
        {
            await context.EmployeeInfos.AddAsync(new EmployeeInfo { DepartmentId = departmentId, EmployeeUserId = employeeId }, cancellation);
            return context.SaveChanges();
        }

        public async Task<EmployeeInfo> UpdateEmployeeDepartmentAsync(int employeeId, int departmentId, CancellationToken cancellation = default)
        {
            EmployeeInfo updatedEmployee = context.EmployeeInfos.Single(q => q.EmployeeUserId == employeeId);
            if (updatedEmployee != null)
            {
                updatedEmployee.DepartmentId = departmentId;
                context.EmployeeInfos.Update(updatedEmployee);
                await context.SaveChangesAsync(cancellation);
            }

            return context.EmployeeInfos.Single(q => q.EmployeeUserId == updatedEmployee.EmployeeUserId);
        }

        public async Task<int> DeleteEmployeeAsync(int employeeId, CancellationToken cancellation = default)
        {
            if (employeeId > 0)
            {
                var delEmployee = context.EmployeeInfos.Single(q => q.EmployeeUserId == employeeId);
                if (delEmployee != null)
                {
                    context.EmployeeInfos.Remove(delEmployee);
                    return await context.SaveChangesAsync(cancellation);
                }
            }
            return await Task.FromResult(0);
        }
    }
}
