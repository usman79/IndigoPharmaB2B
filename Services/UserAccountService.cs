 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository modelRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ILicenseInformationRepository licenseInformationRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserAccountService(IUserAccountRepository modelRepository, IOrderRepository orderRepository, ILicenseInformationRepository licenseInformationRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.orderRepository = orderRepository;
            this.licenseInformationRepository = licenseInformationRepository;
            this.unitOfWork = unitOfWork;
        }

        public UserAccount GetById(long id)
        {
            var obj = modelRepository.Get(x => x.UserId == id);

            return obj;
        }

        public int GetCount()
        {
            var objs = modelRepository.Get().Count();
            return objs;
        }
        
        public IEnumerable<UserAccount> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }
        public IEnumerable<UserAccount> GetAllByRole(int RoleId)
        {
            var query = from user in modelRepository.Get()
                        join assignedUser in modelRepository.Get() on user.AssignedDeliveryBoyId equals (int)assignedUser.UserId into result
                        from assignedUser in result.DefaultIfEmpty()
                        where (user.UserRoleId == RoleId)
                        select new UserAccount
                        {
                            ActiveStatus = user.ActiveStatus,
                            UserFirstName = user.UserFirstName,
                            UserLastName = user.UserLastName,
                            Address = user.Address,
                            UserPassword = user.UserPassword,
                            UserId = user.UserId,
                            UserPhone = user.UserPhone,
                            UserEmailAddress = user.UserEmailAddress,
                            AuthToken = user.AuthToken,
                            BillingAddress = user.BillingAddress,
                            IsPasswordReset = user.IsPasswordReset,
                            Latitude = user.Latitude,
                            Logitude = user.Logitude,
                            UserProfilePicture = user.UserProfilePicture,
                            UserRoleId = user.UserRoleId,
                            UserUuid = user.UserUuid,
                            CreatedAt = user.CreatedAt,
                            IsVerified = user.IsVerified,
                            UserSore = user.UserSore,
                            AssignedOrderTakerId=user.AssignedOrderTakerId,
                            AssignedDeliveryBoyId=user.AssignedDeliveryBoyId,
                            Orders = (from order in orderRepository.Get() where order.OrderTakerId == user.UserId select order).Count(),
                            LicenseInformation = RoleId == 3 ? (from license in licenseInformationRepository.Get() where license.UserId == user.UserId select license).FirstOrDefault() : null,
                            AssignedUserName = RoleId == 3 ? (assignedUser.UserFirstName+" "+assignedUser.UserLastName) : ""
                        };
            if (query.Any())
            {
                return query.ToList();
            }
            return null;
        }
        public IEnumerable<UserAccount> GetAllByOt(int RoleId,int OtId)
        {
            var query = from user in modelRepository.Get()
                        where (user.UserRoleId == RoleId&&user.AssignedOrderTakerId==OtId)
                        select new UserAccount
                        {
                            ActiveStatus = user.ActiveStatus,
                            UserFirstName = user.UserFirstName,
                            UserLastName = user.UserLastName,
                            Address = user.Address,
                            UserPassword = user.UserPassword,
                            UserId = user.UserId,
                            UserPhone = user.UserPhone,
                            UserEmailAddress = user.UserEmailAddress,
                            AuthToken = user.AuthToken,
                            BillingAddress = user.BillingAddress,
                            IsPasswordReset = user.IsPasswordReset,
                            Latitude = user.Latitude,
                            Logitude = user.Logitude,
                            UserProfilePicture = user.UserProfilePicture,
                            UserRoleId = user.UserRoleId,
                            UserUuid = user.UserUuid,
                            CreatedAt = user.CreatedAt,
                            IsVerified = user.IsVerified,
                            UserSore = user.UserSore,
                            Orders = (from order in orderRepository.Get() where order.OrderTakerId == user.UserId select order).Count(),
                            LicenseInformation = RoleId == 3 ? (from license in licenseInformationRepository.Get() where license.UserId == user.UserId select license).FirstOrDefault() : null
                        };
            if (query.Any())
            {
                return query.ToList();
            }
            return null;
        }

        public UserAccount GetByEmailAndPassword(string email, string password)
        {
            var obj = modelRepository.Get(x => x.UserEmailAddress == email && x.UserPassword == password);
            if (obj != null)
            {
                return obj;
            }
            return null;
        }
        public long Create(UserAccount objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.UserId;
        }

        public void Update(UserAccount objModel)
        {
            modelRepository.Update(objModel);
            Commit();
        }

        public void Delete(long id)
        {
            modelRepository.Delete(id);
            Commit();
        }

        public void Commit()
        {
            unitOfWork.Commit();
        }

    }
    public interface IUserAccountService : IService
    {
        UserAccount GetById(long id);
        IEnumerable<UserAccount> GetAll();
        IEnumerable<UserAccount> GetAllByOt(int RoleId,int OtId);

        IEnumerable<UserAccount> GetAllByRole(int RoleId);
        int GetCount();
        UserAccount GetByEmailAndPassword(string email, string password);
        long Create(UserAccount objModel);
        void Update(UserAccount objModel);
        void Delete(long id);
    }
}
