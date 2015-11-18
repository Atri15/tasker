using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Data.Interfaces;

namespace Tasker.Data.Services
{
    public class DateService : IDateService
    {
        public DateTime GetCurrentDateUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
