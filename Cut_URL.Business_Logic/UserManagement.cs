using Cut_URL.DataAccess;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: InternalsVisibleTo("Cut_URL.Tests")]
namespace Cut_URL.Business_Logic
{
    public class UserManagement : IUserManagement
    {
        private IRepository _repository;
        private DateTime _currentTime;

        public UserManagement(IRepository repository)
        {
            _repository = repository;            
            _currentTime = DateTime.Now;
        }
        internal UserManagement(IRepository repository, DateTime testTime) //TODO: сделать internal
        {
            _repository = repository;
            _currentTime = testTime;
        }

        public Guid LoginUser(string login, string password)
        {
            User user;
            try
            {
                user = _repository.GetUserByLogin(login) as User;
                if(user == null)
                {
                    throw new UnableToLoginUserException($"The User with {login} doesn't exist.");
                }
                if(user.Password != password)
                {
                    throw new IncorrectPasswordException("The password is wrong.");
                }
            }
            catch(DataAccessException ex)
            {
                throw new UserManageException(ex.Message);
            }
             
            return user.UserId;
        }

        public bool IsSessionActive(Guid guid)
        {
            try
            {
                Session session = _repository.GetSessionByGuid(guid);
                if(session.Id == guid && session.LastAccessTime > _currentTime.AddMinutes(-30) && session.LastAccessTime < _currentTime)
                {
                    return true;
                }
            }
            catch(SessionIsNotExistedException ex)
            {
                throw new UserManageException(ex.Message, ex);
            }
            return false;
        }

        public Guid RegisterNewUser(string login, string password)
        {
            if (_repository.GetUserByLogin(login) != null)
            {
                throw new UnableRegisterUserException("The Login already exists in database.");
            }

            Guid token = Guid.NewGuid();

            try
            {
                _repository.AddUser(token, login, password);
            }
            catch(DataAccessException ex)
            {
                throw new UserManageException(ex.Message, ex);
            }

            return token;
        }
    }
}
