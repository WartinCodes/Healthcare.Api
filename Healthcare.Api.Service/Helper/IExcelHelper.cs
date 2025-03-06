using Healthcare.Api.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Healthcare.Api.Service.Helper
{
    public interface IExcelHelper
    {
        Task<List<NutritionData>> ParseNutritionDataExcel(IFormFile file, int patientId);
    }
}
