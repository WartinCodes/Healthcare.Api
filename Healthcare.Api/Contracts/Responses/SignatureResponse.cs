namespace Healthcare.Api.Contracts.Responses
{
    public class SignatureResponse
    {
        public DoctorSignatureResponse DoctorSignature { get; set; }
        public PatientSignatureResponse PatientSignature { get; set; }
    }
}
