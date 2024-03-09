using CommonLib.DTO;
using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.DAL;

public partial class PostgresRepository
{
    public async Task<List<Application>> GetApplicationByApplicantId(int UserId, CancellationToken cancellation = default)
    {
        var query = _context.Apps.AsNoTracking();
        if (UserId > 0)
        {
            query = query.Where(q => q.ApplicantId == UserId);
        }
        return await query.ToListAsync(cancellation);
    }

    public async Task<Application> GetApplicationById(int applicationId, CancellationToken cancellation = default)
    {
        var query = _context.Apps.AsNoTracking();
        if (applicationId > 0)
        {
            query = query.Where(q => q.Id == applicationId);
        }
        return await query.SingleAsync(cancellation);
    }

    public async Task<Application> AddNewApplication(Application newApplication, CancellationToken cancellation = default)
    {
        Application newApp = newApplication;
        await _context.Apps.AddAsync(newApplication, cancellation);
        await _context.SaveChangesAsync(cancellation);
        return newApp;
    }

    public async Task<Application> UpdateApplicationAsync(UpdateAppDTO request, CancellationToken cancellation = default)
    {
        if (request.Id > 0)
        {
            Application updatedApp = _context.Apps.Single(q => q.Id == request.Id);
            if (updatedApp != null)
            {
                if (request.ApplicationTypeId > 0)
                { updatedApp.ApplicationTypeId = (Enums.AppTypeEnum)request.ApplicationTypeId; }
                if (request.Status > 0)
                { updatedApp.Status = (Enums.AppStatusEnum)request.Status; }
                if (request.DepartmentId > 0)
                { updatedApp.DepartmentId = request.DepartmentId; }
                if (request.ExecutorId > 0)
                { updatedApp.ExecutorId = request.ExecutorId; }
                if (request.DateClose != DateTime.MinValue)
                { updatedApp.DateClose = request.DateClose; }
                if (request.DateConfirm != DateTime.MinValue)
                { updatedApp.DateConfirm = request.DateConfirm; }

                _context.Apps.Update(updatedApp);
                await _context.SaveChangesAsync(cancellation);
            }
        }
        return _context.Apps.Single(q => q.Id == request.Id);
    }

    public async Task<int> DeleteApplicationAsync(int appId, CancellationToken cancellation = default)
    {
        if (appId > 0)
        {
            var delApp = _context.Apps.Single(q => q.Id == appId);
            if (delApp != null)
            {
                _context.Apps.Remove(delApp);
                return await _context.SaveChangesAsync(cancellation);
            }
        }
        return await Task.FromResult(0);
    }
}
