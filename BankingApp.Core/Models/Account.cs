using System.ComponentModel.DataAnnotations;

namespace BankingApp.Core.Models
{
    public class Account
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Account number must be greater than 0.")]
        public int AccountNumber { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be non-negative.")]
        public decimal Balance { get; set; }
    }
}
