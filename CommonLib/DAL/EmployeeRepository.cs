﻿using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.DAL;

public partial class PostgresRepository
{

    public List<int> GetUserIdsByDepartmentId(int departmentId, CancellationToken cancellation = default)
    {
        return _context.EmployeeInfos.AsNoTracking().Where(u => u.DepartmentId == departmentId).Select(i => i.EmployeeUserId).ToList();
    }

    public int GetDepartmentIdByUserId(int employeeId, CancellationToken cancellation = default)
    {
        return _context.EmployeeInfos.AsNoTracking().Where(u => u.EmployeeUserId == employeeId).Select(i => i.DepartmentId).First();
    }

    public async Task<int> AddNewEmployee(int employeeId, int departmentId, CancellationToken cancellation = default)
    {
        await _context.EmployeeInfos.AddAsync(new EmployeeInfo { DepartmentId = departmentId, EmployeeUserId = employeeId }, cancellation);
        return _context.SaveChanges();
    }

    public async Task<EmployeeInfo> UpdateEmployeeDepartmentAsync(int employeeId, int departmentId, CancellationToken cancellation = default)
    {
        EmployeeInfo updatedEmployee = _context.EmployeeInfos.Single(q => q.EmployeeUserId == employeeId);
        if (updatedEmployee != null)
        {
            updatedEmployee.DepartmentId = departmentId;
            _context.EmployeeInfos.Update(updatedEmployee);
            await _context.SaveChangesAsync(cancellation);
        }

        return _context.EmployeeInfos.Single(q => q.EmployeeUserId == updatedEmployee.EmployeeUserId);
    }

    public async Task<int> DeleteEmployeeAsync(int employeeId, CancellationToken cancellation = default)
    {
        if (employeeId > 0)
        {
            var delEmployee = _context.EmployeeInfos.Single(q => q.EmployeeUserId == employeeId);
            if (delEmployee != null)
            {
                _context.EmployeeInfos.Remove(delEmployee);
                return await _context.SaveChangesAsync(cancellation);
            }
        }
        return await Task.FromResult(0);
    }
}