namespace Healthcare.Api.Service.Helper
{
    public static class BuildPdfSignature
    {
        public static string Matricula(string matricula)
        {
            return $"Mt. {int.Parse(matricula).ToString("N0").Replace(",", ".")}";
        }

        public static string FullName(string gender, string firstName, string lastName)
        {
            if (gender == "Masculino")
            {
                return "Dr. " + firstName + " " + lastName;
            }
            else if (gender == "Femenino")
            {
                return "Dra. " + firstName + " " + lastName;
            }
            else
            {
                return firstName + " " + lastName;
            }
        }

        public static string DoctorSpeciality(string gender, List<string> specialities)
        {
            var specialityMapping = new Dictionary<string, (string male, string female)>(StringComparer.OrdinalIgnoreCase)
            {
                { "Cardiología", ("Cardiólogo", "Cardióloga") },
                { "Cirugía Plástica", ("Cirujano", "Cirujana") },
                { "Clínica Médica", ("Clínico", "Clínica") },
                { "Ecografías", ("Ecografista", "Ecografista") },
                { "Flebología", ("Flebólogo", "Flebóloga") },
                { "Ginecología", ("Ginecólogo", "Ginecóloga") },
            };

            string prefix;
            if (gender.Equals("Masculino", StringComparison.OrdinalIgnoreCase))
            {
                prefix = "Médico";
            }
            else if (gender.Equals("Femenino", StringComparison.OrdinalIgnoreCase))
            {
                prefix = "Médica";
            }
            else
            {
                return string.Empty;
            }

            List<string> mappedSpecialities = new List<string>();
            foreach (var speciality in specialities)
            {
                if (specialityMapping.TryGetValue(speciality, out var mapped))
                {
                    string mappedSpeciality = gender.Equals("Masculino", StringComparison.OrdinalIgnoreCase)
                                                ? mapped.male
                                                : mapped.female;
                    mappedSpecialities.Add(mappedSpeciality);
                }
                else
                {
                    mappedSpecialities.Add(speciality.ToUpper());
                }
            }

            return $"{prefix} {string.Join(" ", mappedSpecialities)}";
        }
    }
}
