using Api.Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    internal class EmailRepository : BaseRepository<EmailEntity>, IEmailRepository
    {
        public EmailRepository(MyContext context) : base(context)
        {
        }
    }
}
