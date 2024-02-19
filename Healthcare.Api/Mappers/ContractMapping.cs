﻿using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;

namespace Helthcare.Api.Mappers
{
    public class ContractMapping : Profile
    {
        public ContractMapping()
        {
            #region Responses
            CreateMap<Role, RoleResponse>().ReverseMap();
            #endregion

            #region Requests
            CreateMap<User, PatientRequest>().ReverseMap();     
            CreateMap<User, DoctorRequest>().ReverseMap();
            CreateMap<Speciality, SpecialityRequest>().ReverseMap();
            CreateMap<HealthInsurance, HealthInsuranceRequest>().ReverseMap();

            #endregion
        }
    }
}
