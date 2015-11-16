using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Tasker.Common.Services;
using Tasker.Data.DAL;

namespace Tasker.Tests.Services
{
    [TestFixture]
    public class JobServiceTests
    {
        [Test]
        public void CreationTest()
        {
            var dbContext = new TaskerDbContext();
            var service = new JobService(dbContext);
            Assert.IsNotNull(service);
        }
    }
}
