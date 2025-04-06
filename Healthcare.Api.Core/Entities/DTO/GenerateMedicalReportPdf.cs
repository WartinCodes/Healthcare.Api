namespace Healthcare.Api.Core.Entities.DTO
{
    public class GenerateMedicalReportPdf
    {
        public GenerateMedicalReportPdf(string? doctorUserId, byte[] studyFileBytes, string pdfFileName, string userName, string studyType)
        {
            DoctorUserId = doctorUserId;
            StudyFileBytes = studyFileBytes;
            PdfFileName = pdfFileName;
            UserName = userName;
            StudyType = studyType;
        }

        public string? DoctorUserId { get; set; }
        public byte[] StudyFileBytes { get; set; }
        public string PdfFileName { get; set; }
        public string UserName { get; set; }
        public string StudyType { get; set; }
    }
}
