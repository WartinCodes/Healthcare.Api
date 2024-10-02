using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.Utilities
{
    public class FileNameParameters
    {
        public FileNameParameters(User user, StudyType studyType, string date, int? number = null, string extension = null)
        {
            User = user;
            StudyType = studyType;
            Date = date;
            Number = number;
            Extension = extension;
        }

        public User User { get; set; }
        public StudyType StudyType { get; set; }
        public string Date { get; set; }
        public int? Number { get; set; }
        public string Extension { get; set; }
    }
}
