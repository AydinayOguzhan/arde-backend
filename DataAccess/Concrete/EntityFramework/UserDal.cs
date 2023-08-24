using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserDal : EfEntityRepositoryBase<User, EfContext>, IUserDal
    {
        public List<InvoiceUserDto> GetAllUsers()
        {
            using (var context = new EfContext())
            {
                var result = from user in context.Users
                             select new InvoiceUserDto {FirstName = user.FirstName, LastName = user.LastName, UserId = user.Id,
                             UserEmail = user.Email};
                return result.ToList();
            }
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new EfContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }
    }
}
