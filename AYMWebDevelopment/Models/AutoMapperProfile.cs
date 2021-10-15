using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AYMWebDevelopment.Models;
using AYMWebDevelopment.Models.ModelDTO;

namespace AYMWebDevelopment.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeTBL, EmployeeDTO>();
            CreateMap<EmployeeDTO, EmployeeTBL>();
        }
    }
}