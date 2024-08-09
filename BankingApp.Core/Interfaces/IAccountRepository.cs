using BankingApp.Core.Models;

namespace BankingApp.Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAsync();
        Task CreateAsync(Account account);
        Task<Account> GetByAccountNumberAsync(int accountNumber);
        Task Deposit(int accountNumber, decimal amount);
        Task Withdraw(int accountNumber, decimal amount);
        Task Transfer(int senderAccNumber, int receiverAccNumber, decimal amount);
    }
}
