using ASP_NET_Assignment1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP_NET_Assignment1.ViewModels
{
    public class ClientAccountVM
    {
        [Display(Name = "Client ID")]
        public int ClientID { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Account Number")]
        public int AccountNum { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        [RegularExpression("^(?!(?:0|0.0|0.00)$)[+]?\\d+(.\\d|.\\d[0-9])?$", ErrorMessage = "Improper Format: Must only go 2 decimal places")]
        [Display(Name = "Balance")]
        [Required(ErrorMessage = "Balance Required")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Balance { get; set; }

        public string? Message { get; set; }
    }
}
