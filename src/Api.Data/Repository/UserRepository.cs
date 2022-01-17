using Api.Data.Context;
using Data.Entities;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        //private static DbSet<UserEntity> _dataset;

        //public UserRepository(MyContext context) : base(context)
        //{
        //    _dataset = context.Set<UserEntity>();
        //}
        public UserRepository(MyContext context) : base(context)
        {
        }
    }
}
