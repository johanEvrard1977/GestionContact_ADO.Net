using AutoMapper;
using GestionContact.DAL;
using GestionContact.DAL.ViewModels;
using GestionContactASP.Models;
using GestionContactASP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactASP.Mapper
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Contact, ContactASP>();
            CreateMap<User, UserAsp>();
            CreateMap<LoginDto, Login>();
            CreateMap<LoginDto, LoginSuccessDto>();
            CreateMap<ContactASP, ContactASP>();
            // Resource to Domain
            CreateMap<ContactASP, Contact>();
            CreateMap<UserAsp, User>();
            CreateMap<Login, LoginDto>();
            CreateMap<LoginSuccessDto, LoginDto>();
            CreateMap<ContactASP, ContactDto>();
        }
    }
}
