﻿using CommonLib.Entities;
using Grpc.Core;

namespace DataAccessGrpcService.Services
{
    public partial class DataAccessGrpcBase
    {
        //Users

        //Create 
        public override async Task<UserOperationReply> AddUser(UserRequest request, ServerCallContext context)
        {
            string message = $"Пользоватиель с логином {request.User.Login} ";
            UserOperationReply result = new UserOperationReply { Identificator = request.User.Login };
            User newUser = _mapper.Map<User>(request.User);
            var res = await _repository.AddNewUser(newUser);
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
                if (request.UserDto.Login != string.Empty)
                {
                    responce.User = _mapper.Map<UserGrpc>(await _repository.GetUserbyLogin(request.UserDto.Login));
                    return responce;
                }

                if (request.UserDto.Email != string.Empty)
                {
                    responce.User = _mapper.Map<UserGrpc>(await _repository.GetUserbyEmail(request.UserDto.Email));
                    return responce;
                }

                if (request.UserDto.Phone != string.Empty)
                {
                    responce.User = _mapper.Map<UserGrpc>(await _repository.GetUserbyPhone(request.UserDto.Phone));
                    return responce;
                }
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
            UserOperationReply responce = new UserOperationReply { Identificator = request.UserDto.Login };
            string message = $"Пользователь с логином {request.UserDto.Login} ";
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
}
