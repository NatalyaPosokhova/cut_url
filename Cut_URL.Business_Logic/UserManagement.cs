﻿using Cut_URL.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public class UserManagement : IUserManagement
    {
        private IRepository _repository;

        public UserManagement(IRepository repository)
        {
            _repository = repository;
        }

        public Guid LoginUser(string login, string password)
        {
            User user = _repository.GetUserByLogin(login) as User;
            return user.UserId;
        }

        public Guid RegisterUser(string login, string password)
        {
            if (_repository.GetUserByLogin(login) != null)
            {
                throw new UserManageException("The Login already exists in database.");
            }
            //TODO Можно ещё проверить пароль на корректность.

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
