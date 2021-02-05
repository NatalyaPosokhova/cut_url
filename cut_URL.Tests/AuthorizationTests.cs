using System;
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
        private const string login = "login";
        private const string password = "password";

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
            _repository.IsUserExistsInDatabase(login).Returns(true);

            Assert.Throws<UserManageException>(() => bl.RegisterUser(login, password));
        }

        [Test]
        public void TryToCreateUserErrorInDbShouldBeError()
        {
            _repository.When(x => x.AddUser(Arg.Any<Guid>(), login, password)).Do(x => 
            { 
                throw new DataAccessException("Cannot add User to database."); 
            });

            Assert.Throws<UserManageException>(() => bl.RegisterUser(login, password));
        }
    }
}
