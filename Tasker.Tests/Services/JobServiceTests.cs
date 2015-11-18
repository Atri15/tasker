using NUnit.Framework;
using Tasker.Data.DAL;
using Tasker.Data.Services;

namespace Tasker.Tests.Services
{
    [TestFixture]
    public class JobServiceTests
    {
        [Test]
        public void CreationTest()
        {
            var dbContext = new TaskerDbContext();
            var service = new TaskService(dbContext);

            Assert.IsNotNull(service);
        }
    }
}
