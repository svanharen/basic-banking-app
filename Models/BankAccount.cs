using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_NET_Assignment1.Models
{
    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Account Number")]
        public int AccountNum { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        [Display(Name = "Balance")]
        [RegularExpression("^(?!(?:0|0.0|0.00)$)[+]?\\d+(.\\d|.\\d[0-9])?$", ErrorMessage ="Improper Format: Must only go 2 decimal places")]
        [Required(ErrorMessage ="Balance Required")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Balance { get; set; }

        [Display(Name="Accounts")]
        public  List<ClientAccount> ClientAccounts { get; set; }
    }
}
