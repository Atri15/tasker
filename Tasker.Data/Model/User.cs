using System;
using Microsoft.AspNet.Identity;

namespace Tasker.Data.Model
{
    public class User : IUser<Guid>
    {
        public static readonly User TestUser = new User
        {
            Id = Guid.Parse("A2D7E8AF-97E6-4A05-BB71-325DE202DE4B"),
            UserName = "Test"
        };

        public Guid Id { get; set; }

        public string UserName { get; set; }
    }
}