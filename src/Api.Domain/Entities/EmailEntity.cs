using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmailEntity : BaseEntity
    {
        public string mail { get; set; }
        public DateTime createdAt { get; set; }
    }
}
