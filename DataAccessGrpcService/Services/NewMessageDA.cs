using CommonLib.DTO;
using CommonLib.Entities;

namespace DataAccessGrpcService.Services
{
    public partial class DataAccessGrpcBase
    {
        //todo сделать методы для сообщения для персонала и для пользователей
        private async Task<List<MessageDTO>> ProcessNewApplicationMessages(Application newApplication)
        {
            //todo 
            /*User applicantInfo = _repository.GetUserbyId(newApplication.Id);
            NotificationType messagingMethod = await _repository.GetNotificationTypeByIdAsync(applicantInfo.MessagingMethodId);
            
            Message message = _repository.CreateNewMessage(applicantInfo, messagingMethod, newApplication);
            await _repository.AddNewMessage(message);
            
            return new MessageDTO
            {
                ApplicantAddress = applicantInfo.Address,
                ApplicantFullName = $"{applicantInfo.SecondName} {applicantInfo.FirstName}",
                Destination = applicantInfo.Email,
                ApplicationId = newApplication.Id,
                Body = newApplication.Description,
                MessagingMethod = messagingMethod.Name,
                Subject = newApplication.Subject,
                TelephoneNumber = applicantInfo.Phone
            };*/
        }

        private async Task<MessageDTO> ProcessNewMessagesForApplicant(Application newApplication)
        {
            //todo 
            User applicantInfo = _repository.GetUserbyId(newApplication.Id);
            NotificationType messagingMethod = await _repository.GetNotificationTypeByIdAsync(applicantInfo.MessagingMethodId);

            Message message = _repository.CreateNewMessage(applicantInfo, messagingMethod, newApplication);
            await _repository.AddNewMessage(message);

            return new MessageDTO
            {
                ApplicantAddress = applicantInfo.Address,
                ApplicantFullName = $"{applicantInfo.SecondName} {applicantInfo.FirstName}",
                Destination = applicantInfo.Email,
                ApplicationId = newApplication.Id,
                Body = newApplication.Description,
                MessagingMethod = messagingMethod.Name,
                Subject = newApplication.Subject,
                TelephoneNumber = applicantInfo.Phone
            };
        }
    }
}
