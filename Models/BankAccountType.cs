using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_NET_Assignment1.Models
{
    public class BankAccountType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AccountType { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
