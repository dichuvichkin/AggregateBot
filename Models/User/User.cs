using System;

namespace AggregateBot.Models.User
{
    public class User
    {
        public long UserId { get; set; }
        public DateTime PayExpireTime { get; set; }

        public Group[] Groups { get; set; }
    }
}