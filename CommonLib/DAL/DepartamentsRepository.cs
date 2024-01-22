using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CommonLib.DAL
{
    public partial class PostgresRepository
    {
        public async Task<List<Department>> GetAllDepartamentsAsync(CancellationToken cancellation = default)
        {
            return await context.Departaments.AsNoTracking().ToListAsync(cancellation);
        }

        public async Task<Department> AddDepartamentAsync(string newName, CancellationToken cancellation = default)
        {
            var query = context.Departaments.AsNoTracking();
            context.Departaments.Add(new Department { Name = newName });
            await context.SaveChangesAsync(cancellation);
            query = query.Where(d => d.Name == newName);
            return await Task.FromResult(query.First());
        }

        public async Task<Department> GetDepartamentByIdAsync(int departamentId,  CancellationToken cancellation = default)
        {
            var query = context.Departaments.AsNoTracking();
            if (departamentId > 0)
            {
                query = query.Where(q => q.Id == departamentId);
            }
            return await Task.FromResult(query.First());
        }

        public async Task<int> UpdateDepartamentAsync(Department department,  CancellationToken cancellation = default)
        {
            Department? updatedDepartment;
            if (department.Id > 0)
            {
                updatedDepartment = context.Departaments.Single(q => q.Id == department.Id);
                if (updatedDepartment != null)
                {
                    updatedDepartment.Name = department.Name;
                    context.Departaments.Update(updatedDepartment);
                }
            }
            return await context.SaveChangesAsync(cancellation); 
        }
        
        public async Task<int> DeleteDepartamentAsync(Department department, CancellationToken cancellation = default)
        {
            var query = context.Departaments.AsNoTracking();
            if (department.Id > 0)
            {
                context.Departaments.Remove(department);
            }
            return await context.SaveChangesAsync(cancellation);
        }

    }
}
