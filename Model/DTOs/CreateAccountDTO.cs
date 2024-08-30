namespace simpleBankingAppAPI.Model.DTOs
{
    public class CreateAccountDTO
    {
        public String AccountName { get; set; } = "";
        public String Email { get; set; } = "";
        public String AccountNumber { get; set; } = "";
    }
}
