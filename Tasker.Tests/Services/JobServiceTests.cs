using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Tasker.Common.Services;

namespace Tasker.Tests.Services
{
    [TestFixture]
    public class JobServiceTests
    {
        [Test]
        public void CreationTest()
        {
            var service = new JobService();
            Assert.IsNotNull(service);
        }
    }
}
