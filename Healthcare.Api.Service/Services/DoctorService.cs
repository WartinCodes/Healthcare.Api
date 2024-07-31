using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using System.Numerics;

namespace Healthcare.Api.Service.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialityService _specialityService;
        private readonly IDoctorSpecialityService _doctorSpecialityService;
        private readonly IHealthPlanService _healthPlanService;
        private readonly IDoctorHealthInsuranceService _doctorHealthInsuranceService;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(
            IDoctorRepository doctorRepository,
            ISpecialityService specialityService,
            IHealthPlanService healthPlanService,
            IDoctorSpecialityService doctorSpecialityService,
            IDoctorHealthInsuranceService doctorHealthInsuranceService,
            IUnitOfWork unitOfWork)
        {
            _doctorSpecialityService = doctorSpecialityService;
            _doctorHealthInsuranceService = doctorHealthInsuranceService;
            _healthPlanService = healthPlanService;
            _specialityService = specialityService;
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Doctor> Add(Doctor entity)
        {
            var record = await _unitOfWork.DoctorRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public async Task Edit(Doctor entity)
        {
            _unitOfWork.DoctorRepository.Edit(entity);

            var doctorHealthInsurances = await _doctorHealthInsuranceService.GetHealthPlansByDoctor(entity.UserId);
            foreach (var php in doctorHealthInsurances)
            {
                _doctorHealthInsuranceService.Remove(php);
            }

            foreach (var healthInsurance in entity.HealthInsurances)
            {
                var healthInsuranceEntity = await _healthPlanService.GetHealthPlanByIdAsync(healthInsurance.Id);
                if (healthInsuranceEntity == null)
                {
                    await Task.CompletedTask;
                }

                var doctorHealthInsurance = new DoctorHealthInsurance { DoctorId = entity.Id, HealthInsuranceId = healthInsuranceEntity.Id };
                await _doctorHealthInsuranceService.Add(doctorHealthInsurance);
            }

            var doctorSpecialities = await _doctorSpecialityService.GetSpecialitiesByDoctor(entity.UserId);
            foreach (var ds in doctorSpecialities)
            {
                _doctorSpecialityService.Remove(ds);
            }

            foreach (var speciality in entity.Specialities)
            {
                var specialityEntity = await _specialityService.GetSpecialityByIdAsync(speciality.Id);
                if (specialityEntity == null)
                {
                    await Task.CompletedTask;
                }

                var doctorSpeciality = new DoctorSpeciality { DoctorId = entity.Id, SpecialityId = specialityEntity.Id };
                await _doctorSpecialityService.Add(doctorSpeciality);
            }

            _unitOfWork.Save();
        }

        public IQueryable<Doctor> GetAsQueryable()
        {
            return _doctorRepository.GetAsQueryable();
        }

        public Task<IEnumerable<Doctor>> GetAsync()
        {
            return _doctorRepository.GetAsync();
        }

        public Task<Doctor> GetDoctorByUserIdAsync(int userId)
        {
            return _doctorRepository.GetDoctorByUserIdAsync(userId);
        }

        public Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return _doctorRepository.GetDoctorByIdAsync(id);
        }

        public void Remove(Doctor entity)
        {
            _unitOfWork.DoctorRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
