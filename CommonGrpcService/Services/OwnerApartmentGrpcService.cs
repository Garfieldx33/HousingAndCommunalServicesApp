using CommonLib.Services.Models;
using Grpc.Core;
using Grpc.Net.Client;
using System.Collections.Generic;

namespace CommonGrpcService.Services
{
    public class OwnerApartmentGrpcService //: OwnerApartmentService.OwnerApartmentServiceClient
    {
        public OwnerApartmentGrpcService()
        {

        }

        public InvoiceDataResponce GetInvoce(int apartmentId, DateTime period)
        {
            //Метод получения счета на оплату
            return new InvoiceDataResponce()
            {
                Fio = "Бражникова Мария Ивановна",
                Address = "улица Фонвизина, д.24, кв.2",
                InvoiceSum = 4500,
            };
        }

        public long NewOwner(NewOwnerRequest request)
        {
            long newOwnerId = 1;
            //Метод добавления нового жителя
            return newOwnerId;
        }

        public OwnerDataResponce GetOwnerDataByInn(long inn)
        {
            //Получение информации о жителе по ИНН
            return new OwnerDataResponce()
            {
                Fio = "Бражникова Мария Ивановна",
                Address = "улица Фонвизина, д.24, кв.2",
            };
        }

        public OwnerDataResponce GetDebtOwnerByInn(long inn)
        {
            //Получение информации о долге собственника
            return new OwnerDataResponce()
            {
                Fio = "Бражникова Мария Ивановна",
                Address = "улица Фонвизина, д.24, кв.2",
                Debt = 0,
            };
        }

        public long UpdateOwnerDataByInn(long inn)
        {
            long ownerId = 1;
            //Обновление данных жителя по ИНН
            return ownerId;
        }

        public long DeleteOwner(long inn)
        {
            long ownerId = 1;
            //Перевод собственника в архив данных
            return ownerId;
        }
    }
}
