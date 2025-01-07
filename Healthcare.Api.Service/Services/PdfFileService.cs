using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using System.Text.RegularExpressions;

namespace Healthcare.Api.Service.Services
{
    public class PdfFileService : IPdfFileService
    {
        /// <summary>
        /// Parses PDF text to extract blood test data based on provided properties.
        /// </summary>
        /// <param name="text">The raw text extracted from the PDF.</param>
        /// <param name="idStudy">The identifier of the study associated with the data.</param>
        /// <param name="properties">A collection of blood test properties to match against the text.</param>
        /// <returns>A list of extracted blood test data.</returns>
        public List<BloodTestData> ParsePdfText(string text, int idStudy, IEnumerable<BloodTest> properties)
        {
            var datas = new List<BloodTestData>();
            string[] lines = text.Split('\n').Skip(1).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                string cleanLine = lines[i].Trim();

                foreach (var property in properties)
                {
                    if (datas.Any(d => d.IdBloodTest == property.Id))
                        continue;

                    if (!cleanLine.Contains(property.ParsedName, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    var bloodTestData = new BloodTestData
                    {
                        IdStudy = idStudy,
                        IdBloodTest = property.Id,
                        Value = ExtractBloodTestValue(cleanLine, lines, i)
                    };

                    datas.Add(bloodTestData);
                    break;
                }
            }

            return datas;
        }

        /// <summary>
        /// Extracts the value of a blood test from a specific line and nearby lines if needed.
        /// </summary>
        /// <param name="line">The current line being processed.</param>
        /// <param name="lines">All lines from the PDF text.</param>
        /// <param name="currentIndex">The index of the current line.</param>
        /// <returns>The extracted value as a string.</returns>
        private string ExtractBloodTestValue(string line, string[] lines, int currentIndex)
        {
            if (TryExtractTime(line, out var timeValue))
                return timeValue;

            string cleanedLine = CleanLine(line);
            var matches = Regex.Matches(cleanedLine, @"(?<![a-zA-Z])\d{1,3}(?:\.\d{3})*(?:,\d+)?(?![a-zA-Z])");

            if (matches.Count > 0)
                return FormatMatches(matches, cleanedLine);

            return SearchNextLinesForValue(lines, currentIndex);
        }

        /// <summary>
        /// Attempts to extract a time value (e.g., HH:MM) from a line.
        /// </summary>
        /// <param name="line">The line to search for a time value.</param>
        /// <param name="timeValue">The extracted time value if found.</param>
        /// <returns>True if a time value is found; otherwise, false.</returns>
        private bool TryExtractTime(string line, out string timeValue)
        {
            var timeMatch = Regex.Match(line, @"\b\d{1,2}:\d{2}\b");
            if (timeMatch.Success)
            {
                timeValue = timeMatch.Value;
                return true;
            }

            timeValue = null;
            return false;
        }

        /// <summary>
        /// Cleans a line by removing unwanted prefixes and patterns.
        /// </summary>
        /// <param name="line">The line to clean.</param>
        /// <returns>The cleaned line.</returns>
        private string CleanLine(string line)
        {
            line = Regex.Replace(line, @"^\d+\s*-\s*", "").Trim();
            line = Regex.Replace(line, @"\s*[-(]\s*\d+.*$", "").Trim();
            return line;
        }

        /// <summary>
        /// Formats matched numeric values from a regex collection.
        /// </summary>
        /// <param name="matches">A collection of regex matches.</param>
        /// <param name="line">The original line for context.</param>
        /// <returns>A formatted string representing the numeric value.</returns>
        private string FormatMatches(MatchCollection matches, string line)
        {
            string formattedNumber = string.Join(".", matches.Cast<Match>().Select(m => m.Value));

            if (line.Contains("eritrosedimentacion", StringComparison.InvariantCultureIgnoreCase))
                formattedNumber = matches.Last().Value.Split(',').Last();

            return formattedNumber;
        }

        /// <summary>
        /// Searches subsequent lines for numeric values if none are found in the current line.
        /// </summary>
        /// <param name="lines">All lines from the PDF text.</param>
        /// <param name="currentIndex">The current line index.</param>
        /// <returns>The extracted value if found; otherwise, an empty string.</returns>
        private string SearchNextLinesForValue(string[] lines, int currentIndex)
        {
            for (int offset = 2; offset <= 3 && currentIndex + offset < lines.Length; offset++)
            {
                var nextLine = lines[currentIndex + offset].Trim();
                var matches = Regex.Matches(nextLine, @"(?<![a-zA-Z])\d{1,3}(?:\.\d{3})*(?:,\d+)?(?![a-zA-Z])");

                if (matches.Count > 0)
                    return string.Join(",", matches.Cast<Match>().Select(m => m.Value));
            }

            return string.Empty;
        }
    }
}
