using CommonLib.DTO;

namespace CommonGrpcService.Services
{
    public class AdministratorGrpcService
    {

        public AdministratorGrpcService()
        {

        }

        /// <summary>
        /// Получение всей существующей информации о жителе по его id
        /// </summary>
        /// <param name="ownerId">id жителя</param>
        /// <returns>Информация по определенному жителю</returns>
        public ResidentDTO GetOwnerById(long ownerId)
        {
            return new ResidentDTO()
            {
                FirstName = "Иванова",
                SecondName = "Карина",
                DateOfBirth = DateTime.Now,
                Email = "karina@gmail.com",
                Phone = "89545642412"
            };
        }

        /// <summary>
        /// Получение всей существующей информации о работнике по его id
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns>Информация по определенному работнику</returns>
        public StaffDTO GetStaffById(long staffId)
        {
            return new StaffDTO()
            {
                FirstName = "Иванов",
                SecondName = "Марио",
                DateOfBirth = DateTime.Now,
                Email = "mario@gmail.com",
                Phone = "89562542412"
            };
        }

        /// <summary>
        /// Получение всей существующей информации о заявлении по его id
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ApplicationDTO GetApplicationById(long appId)
        {
            return new ApplicationDTO()
            {
                Subject = "Потолок течет",
                Description = "Потолок течет по адресу ул.Г, д.Ж, 7 этаж",
                StatusId = 5,
                ApplicationTypeId = 9,
                DepartamentId  = 8,
                ApplicantId = 98,
                DateCreate = DateTime.Now
            };
        }

        /// <summary>
        /// Просмотр архива завершенных заявлений
        /// </summary>
        /// <returns>Список всех завершенных заявлений из архива</returns>
        public List<ApplicationDTO> GetApplicationsFromArchive()
        {
            return new List<ApplicationDTO>()
            {
                new ApplicationDTO() {
                    Subject = "Потолок течет",
                    Description = "Потолок течет по адресу ул.Г, д.Ж, 7 этаж",
                    StatusId = 5,
                    ApplicationTypeId = 9,
                    DepartamentId  = 8,
                    ApplicantId = 98,
                    DateCreate = DateTime.Now
                },
                new ApplicationDTO() {
                    Subject = "Потолок упал",
                    Description = "Потолок упал по адресу ул.Г, д.Ж, 7 этаж",
                    StatusId = 5,
                    ApplicationTypeId = 9,
                    DepartamentId  = 8,
                    ApplicantId = 98,
                    DateCreate = DateTime.Now
                }
            };
        }

        /// <summary>
        /// Просмотр архива не завершенных заявлений
        /// </summary>
        /// <returns>Список всех не завершенных заявлений</returns>
        public List<ApplicationDTO> GetActiveApplications()
        {
            return new List<ApplicationDTO>()
            {
                new ApplicationDTO() {
                    Subject = "Потолок сыпется",
                    Description = "Потолок сыпется по адресу ул.Г, д.Ж, 7 этаж",
                    StatusId = 5,
                    ApplicationTypeId = 9,
                    DepartamentId  = 8,
                    ApplicantId = 98,
                    DateCreate = DateTime.Now
                },
                new ApplicationDTO() {
                    Subject = "Потолок исчез",
                    Description = "Потолок исчез по адресу ул.Г, д.Ж, 7 этаж",
                    StatusId = 5,
                    ApplicationTypeId = 9,
                    DepartamentId  = 8,
                    ApplicantId = 98,
                    DateCreate = DateTime.Now
                }
            };
        }

        /// <summary>
        /// Просмотр архива удаленных собственников
        /// </summary>
        /// <returns>Список всех собственников из архива</returns>
        public List<ResidentDTO> GetOwnersFromArchive()
        {
            return new List<ResidentDTO>()
            {
                new ResidentDTO() {
                    FirstName = "Кукушкин",
                    SecondName ="Иван Андреевич",
                    Address = "набережная Фонтанка, д.24, кв.2",
                    DateOfBirth = DateTime.Now
                },
                new ResidentDTO() {
                    SecondName = "Кулачкова ",
                    FirstName = "Надежда Петровна",
                    Address = "улица Фанфика, д.24, кв.2",
                    DateOfBirth = DateTime.Now
                }
            };
        }

        /// <summary>
        /// Просмотр архива удаленных работников
        /// </summary>
        /// <returns>Список всех работников из архива</returns>
        public List<StaffDTO> GetStaffFromArchive()
        {
            return new List<StaffDTO>()
            {
                new StaffDTO() {
                    SecondName = "Пушкина",
                    FirstName = "Александра Петровна",
                    DateOfBirth = DateTime.Now
                },
                new StaffDTO() {
                    FirstName = "Куклачева",
                    SecondName ="Ивана Андреевич",
                    DateOfBirth = DateTime.Now
                }
            };
        }

        /// <summary>
        /// Перевод работника в архив
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public long DeleteStaffById(long staffId)
        {
            return staffId;
        }

        /// <summary>
        /// Перевод собственника в архив
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public long DeleteOwnerById(long ownerId)
        {
            return ownerId;
        }
    }
}
