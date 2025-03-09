namespace Healthcare.Api.Core.Entities
{
    public class NutritionData
    {
        public NutritionData()
        {
        }

        public NutritionData(int userId, DateTime date, double weight, double fatPercentage, double musclePercentage, double visceralFat, double imc, double targetWeight, string observations)
        {
            UserId = userId;
            Date = date;
            Weight = weight;
            FatPercentage = fatPercentage;
            MusclePercentage = musclePercentage;
            VisceralFat = visceralFat;
            IMC = imc;
            TargetWeight = targetWeight;
            Observations = observations;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime Date { get; set; }
        public double? Weight { get; set; }
        public double? Difference { get; set; }
        public double? FatPercentage { get; set; }
        public double? MusclePercentage { get; set; }
        public double? VisceralFat { get; set; }
        public double? IMC { get; set; }
        public double? TargetWeight { get; set; }
        public string? Observations { get; set; }
    }
}