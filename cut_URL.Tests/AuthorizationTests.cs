using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NSubstitute;
using Cut_URL.Business_Logic;
using Cut_URL.DataAccess;

namespace Cut_URL.Tests
{
    public class AuthorizationTests
    {
        private IRepository _repository;
        private IUserManagement bl;
        private string login = "login";
        private string password = "password";

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            bl = new UserManagement(_repository);
        }

        [Test] 
        public void TryToCreateUserShouldBeSuccess()
        {
            Guid token = bl.RegisterUser(login, password);

            _repository.Received().AddUser(token, login, password);
        }

        [Test]
        public void TryToCreateAlreadyExistedUserShouldBeError()
        {
            Guid token = bl.RegisterUser(login, password);

            _repository.IsLoginExistsInDatabase(login).Returns(true);

            Assert.Throws<UserManageException>(() => bl.RegisterUser(login, password));
        }

        [Test]
        public void TryToCreateUserErrorInDbShouldBeError()
        {
            Guid token = bl.RegisterUser(login, password);

            _repository.When(x => x.AddUser(token, login, password)).Do(x => { throw new DataAccessException("Cannot add User to database."); });

            Assert.Throws<UserManageException>(() => bl.RegisterUser(login, password));
        }
    }
}
