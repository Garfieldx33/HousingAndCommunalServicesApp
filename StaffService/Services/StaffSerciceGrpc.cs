using CommonLib.Config;
using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace StaffService.Services
{
    public class StaffSerciceGrpc
    {
        public Application GetApplicationById(int id)
        {
            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7062");
            // создаем клиент
            var client = new DataAccessGrpcClient.DataAccessGrpcClientClient(channel);

            // обмениваемся сообщениями с сервером
            
            var reply = client.GetAppById(new GetAppByIdRequest { Id = id });

            var app = new Application
            {
                Id = reply.Application.Id,
                Status = (AppStatusEnum)reply.Application.Status,
                ApplicantId = reply.Application.ApplicantId,
                ApplicationTypeId = (AppTypeEnum)reply.Application.ApplicationTypeId,
                DepartamentId = reply.Application.DepartmentId,
                Description = reply.Application.Description,
                ExecutorId = reply.Application.ExecutorId,
                //DateClose = reply.Application.DateClose,
                //DateConfirm = reply.Application.DateConfirm,
                //DateCreate = reply.Application.DateCreate,
                Subject = reply.Application.Subject,
            };

            return app;
        }

        public List<Application> GetAllApplications()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7062");
            var client = new DataAccessGrpcClient.DataAccessGrpcClientClient(channel);

            var reply = client.GetAllApps(new GetAllAppsRequest());

            var list = new List<Application>();
            return list;
        }

        public void UpdateApplication(Application application)
        {
            //int32 status = 1;
            //int32 application_type_id = 2;
            //int32 department_id = 3;
            //google.protobuf.Int32Value executor_id = 4;
            //google.protobuf.Timestamp date_confirm = 5;
            //google.protobuf.Timestamp date_close = 6;
            //int32 id = 7;

            using var channel = GrpcChannel.ForAddress("https://localhost:7062");
            var client = new DataAccessGrpcClient.DataAccessGrpcClientClient(channel);

            var request = new UpdateAppRequest
            {
                Id = application.Id,
                Status = (int)application.Status,
                ApplicationTypeId = (int)application.ApplicationTypeId,
                DepartmentId = (int)application.DepartamentId,
                ExecutorId = application.ExecutorId,
                //DateConfirm = application.DateConfirm,
                //DateClose = application.DateClose
            };

            var reply = client.UpdateAppAsync(request);
        }

    }
}
