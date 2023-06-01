using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_NET_Assignment1.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ClientID")]
        public int ClientID { get; set; }

        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Display(Name ="Email")]
        public string Email { get; set; }

        [Display(Name ="Accounts")]
        public List<ClientAccount> ClientAccounts { get; set; }
    }
}
