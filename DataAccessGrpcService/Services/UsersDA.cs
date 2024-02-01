using Grpc.Core;

namespace DataAccessGrpcService.Services
{
    public partial class DataAccessGrpcBase
    {
        //Users

        //Create 
        /*public override async Task<AddNewUserReply> AddNewUser(AddNewUserRequest request, ServerCallContext context)
        {
            User newUser= _mapper.Map<User>(request.UserDto);
            var res = await _repository.AddNewUser(newUser);
            return new AddNewUserReply { ResultOfInsert = res };
        }*/

        //Read
        /*public override async Task<GetAppsByUserIdReply> GetAppsByUserId(GetAppsByUserIdRequest request, ServerCallContext context)
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
        }*/

        //Update
        /*public override async Task<UpdateAppReply> UpdateApp(UpdateAppRequest request, ServerCallContext context)
        {
            UpdateAppReply responce = new();
            try
            {
                var result = await _repository.UpdateApplicationAsync(_mapper.Map<Application>(request.UpdatedApplication));
                responce.UpdatedApplication = _mapper.Map<ApplicationGrpc>(result);
            }
            catch (Exception ex)
            {
                _logger.Warn(ex);
            }
            return responce;
        }*/

        //Delete
        /*public override async Task<DeleteAppReply> DeleteApp(DeleteAppRequest request, ServerCallContext context)
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
        }*/

    }
}
