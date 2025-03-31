namespace Healthcare.Api.Core.Entities.DTO
{
    public class GenerateMedicalReportPdf
    {
        public GenerateMedicalReportPdf(int? doctorId, byte[] studyFileBytes, string pdfFileName, string userName, int studyType)
        {
            DoctorId = doctorId;
            StudyFileBytes = studyFileBytes;
            PdfFileName = pdfFileName;
            UserName = userName;
            StudyType = studyType;
        }

        public int? DoctorId { get; set; }
        public byte[] StudyFileBytes { get; set; }
        public string PdfFileName { get; set; }
        public string UserName { get; set; }
        public int StudyType { get; set; }
    }
}
