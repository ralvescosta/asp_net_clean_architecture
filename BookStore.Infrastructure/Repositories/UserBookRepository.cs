using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class UserBookRepository : IUserBookRepository
    {
        public Task<Either<NotificationBase, UserBook>> SaveUserBook(UserBook author)
        {
            throw new NotImplementedException();
        }
        public Task<Either<NotificationBase, UserBook>> FindById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Either<NotificationBase, UserBook>> FindByBookId(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Either<NotificationBase, IEnumerable<UserBook>>> FindAll()
        {
            throw new NotImplementedException();
        }
        public Task<Either<NotificationBase, bool>> Update(UserBook author)
        {
            throw new NotImplementedException();
        }
        public Task<Either<NotificationBase, bool>> DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
