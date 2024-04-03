using AutoMapper;
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
            CreateMap<HealthPlan, HealthPlanResponse>();
            CreateMap<HealthInsurance, HealthInsuranceResponse>().ReverseMap();
            CreateMap<Speciality, SpecialityResponse>().ReverseMap();
            CreateMap<Doctor, DoctorResponse>().ReverseMap();
            CreateMap<Address, AddressResponse>().ReverseMap();
            CreateMap<DoctorSpeciality, DoctorSpecialityResponse>().ReverseMap();
            CreateMap<Country, CountryResponse>();
            CreateMap<State, StateResponse>();
            CreateMap<City, CityResponse>();
            CreateMap<Study, StudyResponse>().ReverseMap();
            CreateMap<StudyType, StudyTypeResponse>().ReverseMap();
            CreateMap<HealthInsurance, HealthInsuranceByHealthPlanResponse>().ReverseMap();
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
            #endregion

            #region Requests
            CreateMap<User, PatientRequest>().ReverseMap();     
            CreateMap<User, DoctorRequest>().ReverseMap();
            CreateMap<Speciality, SpecialityRequest>().ReverseMap();
            CreateMap<HealthInsurance, HealthInsuranceRequest>().ReverseMap();
            CreateMap<HealthPlan, HealthInsuranceRequest>().ReverseMap();
            CreateMap<Address, AddressRequest>().ReverseMap();
            CreateMap<City, CityRequest>().ReverseMap();
            CreateMap<State, StateRequest>().ReverseMap();
            CreateMap<Country, CountryRequest>().ReverseMap();
            CreateMap<StudyType, StudyTypeRequest>().ReverseMap();
            CreateMap<LaboratoryDetail, LaboratoryDetailRequest>().ReverseMap();

            //CreateMap<HealthPlanRequest, HealthPlan>()
            //    .ForMember(dest => dest.HealthInsurance, opt => opt.MapFrom(src => src.HealthInsurance));
            #endregion
        }
    }
}
