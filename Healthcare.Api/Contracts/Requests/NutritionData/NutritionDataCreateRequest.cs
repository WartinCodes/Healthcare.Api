namespace Healthcare.Api.Contracts.Requests.NutritionData
{
    public class NutritionDataCreateRequest
    {
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public double Weight { get; set; }
        public double Difference { get; set; }
        public double FatPercentage { get; set; }
        public double MusclePercentage { get; set; }
        public double VisceralFat { get; set; }
        public double IMC { get; set; }
        public double TargetWeight { get; set; }
        public string? Observations { get; set; }
    }
}