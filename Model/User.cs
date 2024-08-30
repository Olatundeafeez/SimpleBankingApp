using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simpleBankingAppAPI.Model
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        public String AccountName { get; set; } = "";
        public String Email { get; set; } = "";
        [Required, Length(10, 10)]
        public String AccountNumber { get; set; } = "";
        [Column(TypeName = "decimal(18,2)")]
        public Decimal AccountBalance { get; set; } = 0 ;
    }
}
