using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_NET_Assignment1.Models
{
    public class ClientAccount
    {
        [Display(Name="Client ID")]
        public int ClientID { get; set; }

        [Display(Name = "Account Number")]
        public int AccountNum { get; set; }

        public Client Client { get; set; }

        public BankAccount BankAccount { get; set; }
    }
}
