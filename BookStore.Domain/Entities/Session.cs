using System;

namespace BookStore.Domain.Entities
{
    public class Session
    {
        public string AccessToken { get; set; }
        public DateTime ExpireIn { get; set; }
    }
}
