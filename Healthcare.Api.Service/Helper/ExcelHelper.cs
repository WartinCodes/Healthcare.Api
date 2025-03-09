using Healthcare.Api.Core.Entities;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Healthcare.Api.Service.Helper
{
    public class ExcelHelper : IExcelHelper
    {
        public async Task<List<NutritionData>> ParseNutritionDataExcel(IFormFile file, int userId)
        {
            if (file == null || file.Length == 0)
                throw new Exception("El archivo está vacío.");

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                throw new Exception("El archivo debe tener la extensión .xlsx.");

            var nutritionDataList = new List<NutritionData>();

            var formatosFecha = new[] { "dd/MM/yyyy", "d/M/yyyy" };
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault() ?? throw new Exception("El archivo Excel no contiene hojas.");

            var endRow = worksheet.Dimension.End.Row;

            var cells = new string[9];

            for (int row = 2; row <= endRow; row++)
            {
                for (int col = 0; col < 9; col++)
                    cells[col] = worksheet.Cells[row, col + 1].Text;

                if (!DateTime.TryParseExact(cells[0], formatosFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                    throw new Exception($"Formato de fecha inválido en la fila {row}. Use d/M/yyyy o dd/MM/yyyy.");

                nutritionDataList.Add(new NutritionData
                {
                    UserId = userId,
                    Date = date,
                    Weight = ParseOrNull(cells[1]),
                    Difference = ParseOrNull(cells[2]),
                    FatPercentage = ParseOrNull(cells[3]),
                    MusclePercentage = ParseOrNull(cells[4]),
                    VisceralFat = ParseOrNull(cells[5]),
                    IMC = ParseOrNull(cells[6]),
                    TargetWeight = ParseOrNull(cells[7]),
                    Observations = string.IsNullOrWhiteSpace(cells[8]) ? null : cells[8]
                });
            }

            return nutritionDataList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double? ParseOrNull(string value)
        {
            return double.TryParse(value, out var result) ? result : (double?)null;
        }
    }
}
