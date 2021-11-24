using AutoMapper;
using GestionContact.DAL;
using GestionContactApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactApi.Mapper
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Contact, ContactApi>();
            CreateMap<User, UserApi>();
            CreateMap<LoginDto, ViewLoginApi>();
            // Resource to Domain
            CreateMap<ContactApi, Contact>();
            CreateMap<UserApi, User>();
            CreateMap<ViewLoginApi, LoginDto>();
        }
    }
}

