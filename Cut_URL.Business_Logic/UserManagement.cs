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

        public Guid RegisterUser(string login, string password)
        {
            if(_repository.IsUserExistsInDatabase(login))
            {
                throw new UserManagementException("The Login already exists in database.");
            }
            //TODO Можно ещё проверить пароль на корректность.

            Guid token = Guid.NewGuid();

            _repository.AddUser(token, login, password);

            return token;
        }
    }
}