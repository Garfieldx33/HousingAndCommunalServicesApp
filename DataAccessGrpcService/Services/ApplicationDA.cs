using CommonLib.DTO;
using CommonLib.Entities;
using Grpc.Core;

namespace DataAccessGrpcService.Services;

public partial class DataAccessGrpcBase
{
    //Applications

    //Create 
    public override async Task<AddNewAppReply> AddNewApp(AddNewAppRequest request, ServerCallContext context)
    {
        AddNewAppReply reply = new() { ResultOfInsert = 0};
        try
        {
            _logger.Info($"Пришла новая заявка {request.ApplicationDto.Description}");
            Application? res = await _repository.AddNewApplication(_mapper.Map<Application>(request.ApplicationDto));
            if (res != null)
            {
                reply.ResultOfInsert = res.Id;
                List<MessageDTO> messages = await ProcessNewMessagesForEmployers(res);
                _notificationQueueService.SendMultipleMessagesInvoke(messages);
            }
        }
        catch (Exception ex) { _logger.Warn(ex); }
        
        return reply;
    }

    //Read
    public override async Task<AppsListReply> GetAppsByUserId(GetAppsByUserIdRequest request, ServerCallContext context)
    {
        AppsListReply responce = new();
        try
        {
            var result = await _repository.GetApplicationByApplicantId(request.ApplicantId);
            result.ForEach(i => responce.Applications.Add(_mapper.Map<ApplicationGrpc>(i)));
        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
        }
        return responce;
    }

    public override async Task<AppsListReply> GetOpenedAppsByDepartmentId(GetOpenAppsByDepartmentIdRequest request, ServerCallContext context)
    {
        AppsListReply responce = new();
        try
        {
            var result = await _repository.GetOpenedApplicationsByDepartmentId(request.DepartmentId);
            result.ForEach(i => responce.Applications.Add(_mapper.Map<ApplicationGrpc>(i)));
        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
        }
        return responce;
    }

    public override async Task<AppsListReply> GetAppsByExecutorId(GetAppsByExecutorIdRequest request, ServerCallContext context)
    {
        AppsListReply responce = new();
        try
        {
            var result = await _repository.GetApplicationsByExecutorId(request.ExecutorId);
            result.ForEach(i => responce.Applications.Add(_mapper.Map<ApplicationGrpc>(i)));
        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
        }
        return responce;
    }

    public override async Task<ApplicationGrpc> GetAppByAppId(GetAppsByAppIdRequest request, ServerCallContext context)
    {
        ApplicationGrpc responce = new();
        try
        {
            var result = await _repository.GetApplicationById(request.Id);
            responce = _mapper.Map<ApplicationGrpc>(result);
        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
        }
        return responce;
    }
    //Update
    public override async Task<UpdateAppReply> UpdateApp(UpdateAppRequest request, ServerCallContext context)
    {
        UpdateAppReply responce = new();
        try
        {
            var updatingInfo = _mapper.Map<UpdateAppDTO>(request);
            var currentApplicationInfo = await _repository.GetApplicationById(updatingInfo.Id);
            
            var result = await _repository.UpdateApplicationAsync(updatingInfo);
            responce.UpdatedApplication = _mapper.Map<ApplicationGrpc>(result);

            if (updatingInfo.Status > 0 && updatingInfo.Status != (int)currentApplicationInfo.Status)
            {
                MessageDTO message = await ProcessChangeStatusMessageForApplicant(currentApplicationInfo);
                _notificationQueueService.SendNewMessageInvoke(message);
            }
            
            if (updatingInfo.ExecutorId > 0 && currentApplicationInfo.ExecutorId is null)
            {
                MessageDTO message = await ProcessExecutorAppointedForApplicant(currentApplicationInfo, updatingInfo.ExecutorId);
                _notificationQueueService.SendNewMessageInvoke(message);
            }

        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
        }
        return responce;
    }
    //Delete
    public override async Task<DeleteAppReply> DeleteApp(DeleteAppRequest request, ServerCallContext context)
    {
        DeleteAppReply responce = new();
        try
        {
            var result = await _repository.DeleteApplicationAsync(request.DeleteAppId);
            if (result > 0)
            {
                responce.DeletedAppId = request.DeleteAppId;
                responce.Message = "Заявка удалена успешно";
            }
            else
            {
                responce.DeletedAppId = 0;
                responce.Message = "Заявка не удалена";
            }
        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
        }
        return responce;
    }
}
