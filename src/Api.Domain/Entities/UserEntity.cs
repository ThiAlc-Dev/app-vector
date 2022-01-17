using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UserEntity : BaseEntity
    {

        public int id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string mail { get; set; }
        public DateTime createdAt { get; set; }
    }
}
