using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NSubstitute;
using Cut_URL.Business_Logic;
using Cut_URL.DataAccess;

namespace Cut_URL.Tests
{
    [TestFixture]
    public class AuthorizationTests
    {
        [Test] 
        public void TryToCreateUserShouldBeSuccess()
        {
            string login = "login";
            string password = "password";

            IRepository _repository = Substitute.For<IRepository>();
            IUserManagement bl = new UserManagement(_repository);

            Guid token = bl.RegisterUser(login, password);

            _repository.Received().AddUser(token, login, password);
        }
    }
}
