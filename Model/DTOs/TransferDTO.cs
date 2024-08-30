namespace simpleBankingAppAPI.Model.DTOs
{
    public class TransferDTO
    {
        public string SenderAccount { get; set; }
        public string RecieverAccount { get; set; }
        public decimal Amount { get; set; }



    }
}
