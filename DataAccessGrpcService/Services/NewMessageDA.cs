using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Helpers;
using Application = CommonLib.Entities.Application;

namespace DataAccessGrpcService.Services;

public partial class DataAccessGrpcBase
{
    private async Task<List<MessageDTO>> ProcessNewMessagesForEmployers(Application newApplication)
    {
        List<MessageDTO> messages = new();
        User applicantInfo = _repository.GetUserbyId(newApplication.ApplicantId);
        List<User> employers = _repository.GetUsersByDepartmentId(newApplication.DepartmentId);
        foreach (User employee in employers)
        {
            NotificationType messagingMethod = await _repository.GetNotificationTypeByIdAsync(employee.MessagingMethodId);
            MessageDTO messageDTO = new()
            {
                ApplicantAddress = applicantInfo.Address,
                ApplicantFullName = $"{applicantInfo.SecondName} {applicantInfo.FirstName}",
                Destination = messagingMethod.Name == "email" ? employee.Email : employee.MessagingDestination,
                ApplicationId = newApplication.Id,
                Body = newApplication.Description,
                MessagingMethod = messagingMethod.Name,
                Subject = $"Новая заявка #{newApplication.Id}. {newApplication.Subject}",
                TelephoneNumber = applicantInfo.Phone
            };

            Message message = CreateNewMessage(messageDTO, messagingMethod.Id);
            await _repository.AddNewMessage(message);

            messages.Add(messageDTO);
        }


        return messages;
    }

    private async Task<MessageDTO> ProcessChangeStatusMessageForApplicant(Application applicationInfo)
    {
        User applicantInfo = _repository.GetUserbyId(applicationInfo.ApplicantId);
        NotificationType messagingMethod = await _repository.GetNotificationTypeByIdAsync(applicantInfo.MessagingMethodId);

        string fullName = $"{applicantInfo.SecondName} {applicantInfo.FirstName}";
        string messageText = @$"Здравствуйте, {fullName}.
Информируем об изменении статуса Вашей заявки. 
Текущий статус {EnumConverter.GetEnumDescription(applicationInfo.Status)}";
        
        var messageDto = new MessageDTO
        {
            ApplicantAddress = applicantInfo.Address,
            ApplicantFullName = fullName,
            Destination = messagingMethod.Name == "email" ? applicantInfo.Email : applicantInfo.MessagingDestination,
            ApplicationId = applicationInfo.Id,
            Body = messageText,
            MessagingMethod = messagingMethod.Name,
            Subject = $"Заявка #{applicationInfo.Id}",
            TelephoneNumber = applicantInfo.Phone
        };

        Message message = CreateNewMessage(messageDto, messagingMethod.Id);
        await _repository.AddNewMessage(message);

        return messageDto;
    }

    private async Task<MessageDTO> ProcessExecutorAppointedForApplicant(Application applicationInfo)
    {
        User applicantInfo = _repository.GetUserbyId(applicationInfo.ApplicantId);
        User executorInfo = _repository.GetUserbyId((int)applicationInfo.ExecutorId);
        NotificationType messagingMethod = await _repository.GetNotificationTypeByIdAsync(applicantInfo.MessagingMethodId);
        
        string fullName = $"{applicantInfo.SecondName} {applicantInfo.FirstName}";
        string messageText = @$"Здравствуйте, {fullName}.
Для выполнения Вашей заявки назначен исполнитель {executorInfo.FirstName} {executorInfo.SecondName}
Телефон для связи {executorInfo.Phone}";
       
        var messageDto = new MessageDTO
        {
            ApplicantAddress = applicantInfo.Address,
            ApplicantFullName = fullName,
            Destination = messagingMethod.Name == "email" ? applicantInfo.Email : applicantInfo.MessagingDestination,
            ApplicationId = applicationInfo.Id,
            Body = messageText,
            MessagingMethod = messagingMethod.Name,
            Subject = $"Заявка #{applicationInfo.Id}",
            TelephoneNumber = applicantInfo.Phone
        };

        Message message = CreateNewMessage(messageDto, messagingMethod.Id);
        await _repository.AddNewMessage(message);

        return messageDto;
    }

    private Message CreateNewMessage(MessageDTO messageDTO, int messagingMethodId)
    {
        return new Message
        {
            ApplicationId = messageDTO.ApplicationId,
            Subject = messageDTO.Subject,
            Body = messageDTO.Body,
            MessagingMethodId = messagingMethodId,
            Destination = messageDTO.Destination
        };
    }
}
