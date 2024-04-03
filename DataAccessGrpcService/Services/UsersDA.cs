using CommonLib.Entities;
using Grpc.Core;

namespace DataAccessGrpcService.Services;

public partial class DataAccessGrpcBase
{
    //Users

    //Create 
    public override async Task<UserOperationReply> AddUser(UserRequest request, ServerCallContext context)
    {
        string message = $"Пользователь с логином {request.User.Login} ";
        User newUser = _mapper.Map<User>(request.User);
        newUser.RegistrationDate = DateTime.Now;
        var res = await _repository.AddNewUser(newUser);
        UserOperationReply result = new UserOperationReply { Identificator = res.ToString() };
        result.Message = res > 0 ? message += "добавлен" : message += "не добавлен";
        return result;
    }

    //Read
    public override async Task<UsersReply> GetUsers(UsersRequest request, ServerCallContext context)
    {
        UsersReply responce = new();
        try
        {
            var result = await _repository.GetUsers();
            result.ForEach(i => responce.Users.Add(_mapper.Map<UserGrpc>(i)));
        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
        }
        return responce;
    }

    public override async Task<UserReply> GetUser(UserDtoRequest request, ServerCallContext context)
    {
        UserReply responce = new();
        try
        {
            responce.User = _mapper.Map<UserGrpc>(await _repository.GetUserbyLogin(request.UserDto.Login, request.UserDto.Password));
        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
        }
        return responce;
    }

    //Update
    public override async Task<UserReply> UpdateUser(UserRequest request, ServerCallContext context)
    {
        UserReply responce = new();
        try
        {
            var result = await _repository.UpdateUserAsync(_mapper.Map<User>(request.User));
            responce.User = _mapper.Map<UserGrpc>(result);
        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
        }
        return responce;
    }

    //Delete
    public override async Task<UserOperationReply> DeleteUser(UserDtoRequest request, ServerCallContext context)
    {
        UserOperationReply responce = new UserOperationReply { Identificator = request.UserDto.Id.ToString() };
        string message = $"Пользователь с ID {request.UserDto.Id} ";
        try
        {
            var result = await _repository.DeleteUserAsync(request.UserDto.Id);
            responce.Message = result > 0 ? message += "удален успешно" : message += "не удален";
        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
        }
        return responce;
    }
}
