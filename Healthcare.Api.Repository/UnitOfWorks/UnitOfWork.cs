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
            IDoctorRepository doctorRepository,
            ISpecialityRepository specialityRepository,
            IHealthInsuranceRepository healthInsuranceRepository,
            IHealthPlanRepository healthPlanRepository,
            IDoctorSpecialityRepository doctorSpecialityRepository,
            IAddressRepository addressRepository,
            ILaboratoryDetailsRepository laboratoryDetailRepository,
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
            IUnitRepository unitRepository)
        {
            _context = context;
            PatientRepository = patientRepository;
            DoctorRepository = doctorRepository;
            SpecialityRepository = specialityRepository;
            HealthInsuranceRepository = healthInsuranceRepository;
            HealthPlanRepository = healthPlanRepository;
            DoctorSpecialityRepository = doctorSpecialityRepository;
            AddressRepository = addressRepository;
            LaboratoryDetailsRepository = laboratoryDetailRepository;
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
        }

        public ISpecialityRepository SpecialityRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IHealthInsuranceRepository HealthInsuranceRepository { get; }
        public IHealthPlanRepository HealthPlanRepository { get; }
        public IDoctorSpecialityRepository DoctorSpecialityRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public ILaboratoryDetailsRepository LaboratoryDetailsRepository { get; }
        public ICityRepository CityRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IStateRepository StateRepository { get; }
        public IDoctorHealthInsuranceRepository DoctorHealthInsuranceRepository { get; }
        public IPatientHealthPlanRepository PatientHealthPlanRepository { get; }
        public IStudyRepository StudyRepository { get; }
        public IStudyTypeRepository StudyTypeRepository { get; }
        public ISupportRepository SupportRepository { get; }
        public IPatientHistoryRepository PatientHistoryRepository { get; }
        public IUltrasoundImageRepository UltrasoundImageRepository { get; }
        public IUnitRepository UnitRepository { get; }

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