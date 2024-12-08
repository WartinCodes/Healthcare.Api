using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Requests.LaboratoryDetail;
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

            CreateMap<Address, AddressResponse>().ReverseMap();
            CreateMap<DoctorSpeciality, DoctorSpecialityResponse>().ReverseMap();
            CreateMap<Country, CountryResponse>();
            CreateMap<State, StateResponse>();
            CreateMap<City, CityResponse>();
            CreateMap<Unit, UnitResponse>();
            CreateMap<PatientHistory, PatientHistoryResponse>()
                .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => string.Concat(src.Doctor.User.FirstName, " ", src.Doctor.User.LastName)));

            CreateMap<Study, StudyResponse>().ReverseMap();
            CreateMap<StudyType, StudyTypeResponse>().ReverseMap();
            CreateMap<HealthInsurance, HealthInsuranceByHealthPlanResponse>().ReverseMap();
            CreateMap<LaboratoryDetail, LaboratoryDetailResponse>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Study.Date))
                .ReverseMap();
            CreateMap<UltrasoundImage, UltrasoundImageResponse>().ReverseMap();
            CreateMap<BloodTest, BloodTestResponse>();


            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();

            CreateMap<Doctor, DoctorAllResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.User.Photo))
                .ForMember(dest => dest.Matricula, opt => opt.MapFrom(src => src.Matricula))
                .ReverseMap();

            CreateMap<Patient, PatientAllResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.User.Photo))
                .ReverseMap();

            CreateMap<Patient, PatientIdResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.User.BirthDate))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.PhoneNumber2, opt => opt.MapFrom(src => src.User.PhoneNumber2))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.User.Photo))
                .ForMember(dest => dest.RegisteredById, opt => opt.MapFrom(src => src.User.RegisteredById))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.User.RegistrationDate))
                .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
                .ForMember(dest => dest.RhFactor, opt => opt.MapFrom(src => src.User.RhFactor))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.CUIL, opt => opt.MapFrom(src => src.User.CUIL))
                .ForMember(dest => dest.CUIT, opt => opt.MapFrom(src => src.User.CUIT))
                .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.User.MaritalStatus))
                .ReverseMap();

            CreateMap<Doctor, DoctorIdResponse>()
              .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
              .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
              .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.User.UserName))
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
              .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.User.BirthDate))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
              .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
              .ForMember(dest => dest.PhoneNumber2, opt => opt.MapFrom(src => src.User.PhoneNumber2))
              .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.User.Photo))
              .ForMember(dest => dest.RegisteredById, opt => opt.MapFrom(src => src.User.RegisteredById))
              .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.User.RegistrationDate))
              .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.User.BloodType))
              .ForMember(dest => dest.RhFactor, opt => opt.MapFrom(src => src.User.RhFactor))
              .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
              .ForMember(dest => dest.CUIL, opt => opt.MapFrom(src => src.User.CUIL))
              .ForMember(dest => dest.CUIT, opt => opt.MapFrom(src => src.User.CUIT))
              .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.User.MaritalStatus))
              .ReverseMap();
            #endregion

            #region Requests
            CreateMap<User, UserRequest>().ReverseMap();
            CreateMap<User, PatientRequest>().ReverseMap();
            CreateMap<User, DoctorRequest>().ReverseMap();
            CreateMap<Speciality, SpecialityRequest>().ReverseMap();

            CreateMap<Doctor, DoctorRequest>().ReverseMap();
            CreateMap<Patient, PatientRequest>().ReverseMap();

            CreateMap<HealthInsurance, HealthInsuranceRequest>().ReverseMap();
            CreateMap<HealthPlan, HealthInsuranceRequest>().ReverseMap();
            CreateMap<HealthPlan, HealthPlanRequest>().ReverseMap();

            CreateMap<Address, AddressRequest>().ReverseMap();
            CreateMap<City, CityRequest>().ReverseMap();
            CreateMap<State, StateRequest>().ReverseMap();
            CreateMap<Country, CountryRequest>().ReverseMap();
            CreateMap<StudyType, StudyTypeRequest>().ReverseMap();
            CreateMap<LaboratoryDetail, LaboratoryDetailRequest>().ReverseMap();

            
            CreateMap<Support, SupportRequest>().ReverseMap();
            CreateMap<PatientHistory, PatientHistoryRequest>().ReverseMap();
            CreateMap<UnitRequest, Unit>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion
        }
    }
}
