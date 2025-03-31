namespace Healthcare.Api.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Gender(this string? gender)
        {
            if (gender == "Masculino")
            {
                return "Dr. ";
            }
            else if (gender == "Femenino")
            {
                return "Dra. ";
            }
            else 
            {
                return string.Empty;
            }
        }
    }
}