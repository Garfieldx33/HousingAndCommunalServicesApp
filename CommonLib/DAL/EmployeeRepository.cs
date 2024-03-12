using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.DAL;

public partial class PostgresRepository
{

    public async Task<List<User>> GetUsersByDepartmentIdAsync(int departmentId, CancellationToken cancellation = default)
    {
        var emplList = _context.EmployeeInfos.AsNoTracking().Where(u => u.DepartmentId == departmentId).Select(i => i.EmployeeUserId).ToList();
        return await _context.Users.Where(u => emplList.Contains(u.Id)).ToListAsync(cancellationToken: cancellation);
    }

    public async Task<string> GetDepartmentByUserIdAsync(int employeeId, CancellationToken cancellation = default)
    {
        if (_context.EmployeeInfos.Any(o => o.EmployeeUserId == employeeId))
        {
            int deptId = _context.EmployeeInfos.AsNoTracking().Where(u => u.EmployeeUserId == employeeId).Select(i => i.DepartmentId).First();
            return await _context.Departaments.AsNoTracking().Where(d => d.Id == deptId).Select(i => i.Name).FirstAsync(cancellationToken: cancellation);
        }
        else
        {
            return $"Работник с ID {employeeId} не найден";
        }
    }

    public async Task<int> AddNewEmployeeAsync(EmployeeInfo newEmployee, CancellationToken cancellation = default)
    {
        await _context.EmployeeInfos.AddAsync(newEmployee, cancellation);
        return _context.SaveChanges();
    }

    public async Task<EmployeeInfo> UpdateEmployeeAsync(EmployeeInfo updatingEmployee, CancellationToken cancellation = default)
    {
        EmployeeInfo updatedEmployee = _context.EmployeeInfos.Single(q => q.EmployeeUserId == updatingEmployee.EmployeeUserId);
        if (updatedEmployee != null)
        {
            updatedEmployee.DepartmentId = updatingEmployee.DepartmentId;
            updatedEmployee.Position = updatingEmployee.Position;

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
