using BankingApp.Core.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AccountTests
{
    public class AccountTests
    {
        private void ValidateModel(Account account)
        {
            var validationContext = new ValidationContext(account);
            Validator.ValidateObject(account, validationContext, validateAllProperties: true);
        }

        [Fact]
        public void Account_ShouldThrowError_WhenAccountNumberIsNegative()
        {
            var account = new Account { AccountNumber = -1 };

            var exception = Assert.Throws<ValidationException>(() => ValidateModel(account));
            Assert.Equal("Account number must be greater than 0.", exception.ValidationResult.ErrorMessage);
        }

        [Fact]
        public void Account_ShouldThrowError_WhenBalanceIsNegative()
        {
            var account = new Account { AccountNumber = 1, Balance = -1m };

            var exception = Assert.Throws<ValidationException>(() => ValidateModel(account));
            Assert.Equal("Balance must be non-negative.", exception.ValidationResult.ErrorMessage);
        }
    }
}
