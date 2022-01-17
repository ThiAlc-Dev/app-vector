using System;

namespace Api.Domain.Models
{
    public class Users
    {
        public string id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string mail { get; set; }
        public DateTime createdAt { get; set; }
    }
}
