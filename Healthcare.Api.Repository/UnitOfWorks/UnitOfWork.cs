using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HealthcareDbContext _context;

        public UnitOfWork(
            HealthcareDbContext context,
            IPatientRepository patientRepository,
            IBloodTestRepository bloodTestRepository,
            IBloodTestDataRepository bloodTestDataRepository,
            IDoctorRepository doctorRepository,
            ISpecialityRepository specialityRepository,
            IHealthInsuranceRepository healthInsuranceRepository,
            IHealthPlanRepository healthPlanRepository,
            IDoctorSpecialityRepository doctorSpecialityRepository,
            IAddressRepository addressRepository,
            ICountryRepository countryRepository,
            IStateRepository stateRepository,
            ICityRepository cityRepository,
            IDoctorHealthInsuranceRepository doctorHealthPlanRepository,
            IPatientHealthPlanRepository patientHealthPlanRepository,
            IStudyTypeRepository studyTypeRepository,
            IStudyRepository studyRepository,
            ISupportRepository supportRepository,
            IPatientHistoryRepository patientHistoryRepository,
            IUltrasoundImageRepository ultrasoundImageRepository,
            IUnitRepository unitRepository,
            INutritionDataRepository nutritionDataRepository)
        {
            _context = context;
            PatientRepository = patientRepository;
            BloodTestRepository = bloodTestRepository;
            BloodTestDataRepository = bloodTestDataRepository;
            DoctorRepository = doctorRepository;
            SpecialityRepository = specialityRepository;
            HealthInsuranceRepository = healthInsuranceRepository;
            HealthPlanRepository = healthPlanRepository;
            DoctorSpecialityRepository = doctorSpecialityRepository;
            AddressRepository = addressRepository;
            CountryRepository = countryRepository;
            StateRepository = stateRepository;
            CityRepository = cityRepository;
            DoctorHealthInsuranceRepository = doctorHealthPlanRepository;
            PatientHealthPlanRepository = patientHealthPlanRepository;
            StudyTypeRepository = studyTypeRepository;
            StudyRepository = studyRepository;
            SupportRepository = supportRepository;
            PatientHistoryRepository = patientHistoryRepository;
            UltrasoundImageRepository = ultrasoundImageRepository;
            UnitRepository = unitRepository;
            NutritionDataRepository = nutritionDataRepository;
        }

        public IBloodTestRepository BloodTestRepository { get; }
        public IBloodTestDataRepository BloodTestDataRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IHealthInsuranceRepository HealthInsuranceRepository { get; }
        public IHealthPlanRepository HealthPlanRepository { get; }
        public IDoctorSpecialityRepository DoctorSpecialityRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public ICityRepository CityRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IStateRepository StateRepository { get; }
        public IDoctorHealthInsuranceRepository DoctorHealthInsuranceRepository { get; }
        public IPatientHealthPlanRepository PatientHealthPlanRepository { get; }
        public ISpecialityRepository SpecialityRepository { get; }
        public IStudyRepository StudyRepository { get; }
        public IStudyTypeRepository StudyTypeRepository { get; }
        public ISupportRepository SupportRepository { get; }
        public IPatientHistoryRepository PatientHistoryRepository { get; }
        public IUltrasoundImageRepository UltrasoundImageRepository { get; }
        public IUnitRepository UnitRepository { get; }
        public INutritionDataRepository NutritionDataRepository { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}