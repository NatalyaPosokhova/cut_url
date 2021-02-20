using System;
using NUnit.Framework;
using NSubstitute;
using Cut_URL.Business_Logic;
using Cut_URL.DataAccess;

namespace Cut_URL.Tests
{
    public class SessionTests
    {
        private IRepository repository = Substitute.For<IRepository>();

        [Test]
        public void SessionAliveShouldBeTrue()
        {
            //Arrange
            UserManagement userManager = new UserManagement(repository);
            Guid guid = Guid.NewGuid();
            Session session = new Session { Id = guid, LastAccessTime = DateTime.Today };
            repository.GetSessionByGuid(guid).Returns(session);

            //Act
            var actual = userManager.IsSessionActive(guid);

            //Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void TimeIsOutSessionShouldBeFalse()
        {
            //Arrange
            UserManagement userManager = new UserManagement(repository);
            Guid guid = Guid.NewGuid();
            Session session = new Session { Id = guid, LastAccessTime = DateTime.Today.AddDays(-1) };
            repository.GetSessionByGuid(guid).Returns(session);

            //Act
            var actual = userManager.IsSessionActive(guid);

            //Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void SessionIsAbsentShouldBeFalse()
        {
            //Arrange
            UserManagement userManager = new UserManagement(repository);
            Guid guid = Guid.NewGuid();
            repository.When(x => x.GetSessionByGuid(guid)).Do(x =>
            {
                throw new SessionIsNotExistedException("Session doesn't exist.");
            }
            );

            //Act
            //Assert
            Assert.Throws<SessionIsNotExistedException>(() => userManager.IsSessionActive(guid));
        }
    }
}
