using Api.Domain.Models;
using AutoMapper;
using Data.Entities;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Mappers
{
    public class AddToProfile : Profile
    {
        public AddToProfile()
        {
            CreateMap<UserGroupDto, UserEntity>().ReverseMap();
            CreateMap<Users, UserGroupDto>().ReverseMap();
            CreateMap<UserEntity, Users>().ReverseMap();
        }
    }
}
