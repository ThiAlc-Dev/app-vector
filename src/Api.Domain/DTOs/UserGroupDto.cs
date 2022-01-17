using Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UserGroupDto
    {
        public string data { get; set; }
        public List<string> nomes { get; set; }
    }
}
