using CommonLib.DTO;
using CommonLib.Entities;

namespace DataAccessGrpcService.Services
{
    public partial class DataAccessGrpcBase
    {
        private async Task<List<MessageDTO>> ProcessNewMessagesForEmployers(Application newApplication)
        {
            List<MessageDTO> messages = new();
            User applicantInfo = _repository.GetUserbyId(newApplication.ApplicantId);
            List<User> employers = _repository.GetUserbyDepartmentId(newApplication.DepartmentId);
            foreach (User employee in employers) 
            {
                NotificationType messagingMethod = await _repository.GetNotificationTypeByIdAsync(employee.MessagingMethodId);
                Message message = _repository.CreateNewMessage(applicantInfo, messagingMethod, newApplication);
                await _repository.AddNewMessage(message);

                MessageDTO messageDTO = new()
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
                messages.Add(messageDTO);
            }
            return messages;
        }

        private async Task<MessageDTO> ProcessNewMessagesForApplicant(Application newApplication)
        {
            //todo 
            User applicantInfo = _repository.GetUserbyId(newApplication.ApplicantId);
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
