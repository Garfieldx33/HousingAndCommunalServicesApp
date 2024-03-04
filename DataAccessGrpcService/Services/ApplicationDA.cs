using CommonLib.DTO;
using CommonLib.Entities;
using Grpc.Core;

namespace DataAccessGrpcService.Services
{
    public partial class DataAccessGrpcBase
    {
        //Applications

        //Create 
        public override async Task<AddNewAppReply> AddNewApp(AddNewAppRequest request, ServerCallContext context)
        {
            _logger.Info($"Пришла новая заявка {request.ApplicationDto.Description}");
            var newApplication = _mapper.Map<Application>(request.ApplicationDto);
            var res = await _repository.AddNewApplication(newApplication);
            if (res > 0 )
            {
                var newMessage = PrepareNewMessage(newApplication);
                _notificationQueueService.SendNewMessageInvoke(newMessage);
            }
            return new AddNewAppReply { ResultOfInsert = res };
        }

        //Read
        public override async Task<GetAppsByUserIdReply> GetAppsByUserId(GetAppsByUserIdRequest request, ServerCallContext context)
        {
            GetAppsByUserIdReply responce = new();
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

        //Update
        public override async Task<UpdateAppReply> UpdateApp(UpdateAppRequest request, ServerCallContext context)
        {
            UpdateAppReply responce = new();
            try
            {
                _logger.Info($"UpdateApp Id = {request.Id}  Status {request.Status } AppTypeId = {request.ApplicationTypeId} Dept Id = {request.DepartmentId} ExecId = {request.ExecutorId} {request.DateConfirm} {request.DateClose}");
                var req = _mapper.Map<UpdateAppDto>(request);
                _logger.Info($"UpdateApp Id = {req.Id} AppTypeId = {req.ApplicationTypeId} Status {req.Status} Dept Id = {req.DepartmentId} ExecId = {req.ExecutorId} {req.DateConfirm} {req.DateClose}");
                var result = await _repository.UpdateApplicationAsync(_mapper.Map<UpdateAppDto>(request));
                responce.UpdatedApplication = _mapper.Map<ApplicationGrpc>(result);
                
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
}
