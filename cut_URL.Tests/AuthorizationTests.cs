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
            Guid token = bl.RegisterNewUser(login, password);

            _repository.Received().AddUser(token, login, password);
        }

        [Test]
        public void TryToCreateAlreadyExistedUserShouldBeError()
        {
            Guid id = Guid.NewGuid();
            _repository.GetUserByLogin(login).Returns(new User { Login = login, Password = password, UserId = id});

            Assert.Throws<UserManageException>(() => bl.RegisterNewUser(login, password));
        }

        [Test]
        public void TryToCreateUserErrorInDbShouldBeError()
        {
            _repository.When(x => x.AddUser(Arg.Any<Guid>(), login, password)).Do(x => 
            { 
                throw new DataAccessException("Cannot add User to database."); 
            });

            Assert.Throws<UserManageException>(() => bl.RegisterNewUser(login, password));
        }

        [Test]
        public void TryToLoginUserShouldBeSuccess()
        {
            Guid id = Guid.NewGuid();
            _repository.GetUserByLogin(login).Returns(new User { Login = login, Password = password, UserId = id });
          
            Guid actToken = bl.LoginUser(login, password);

            Assert.AreEqual(id, actToken);
        }

        [Test]
        public void TryToLoginUnexistedUserShouldBeError()
        {
            _repository.GetUserByLogin(login).Returns(null);

            Assert.Throws<UnableToLoginUserException>(() => bl.LoginUser(login, password));
        }

        [Test]
        public void TryToLoginWithIncorrectPasswordShouldBeError()
        {
            Guid id = Guid.NewGuid();
            string incorrectPassw = "incorrectPassword";
            _repository.GetUserByLogin(login).Returns(new User { Login = login, Password = password, UserId = id });

            Assert.Throws<IncorrectPasswordException>(() => bl.LoginUser(login, incorrectPassw));
        }
    }
}
