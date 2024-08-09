using BankingApp.Core.Interfaces;
using BankingApp.Core.Models;

namespace BankingApp.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly List<Account> _accounts = new List<Account>();

        public async Task<IEnumerable<Account>> GetAsync()
        {
            return await Task.FromResult(_accounts);
        }

        public async Task<Account> GetByAccountNumberAsync(int accountNumber)
        {
            if (accountNumber <= 0) 
            {
                throw new InvalidOperationException("The account number must be greater than 0.");
            }

            var account = _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                throw new KeyNotFoundException($"The account with number {accountNumber} not found.");
            }

            return await Task.FromResult(account);
        }

        public async Task CreateAsync(Account account)
        {
            var existingAccount = _accounts.FirstOrDefault(a => a.AccountNumber == account.AccountNumber);

            if (existingAccount != null)
            {
                throw new InvalidOperationException($"The account with number {account.AccountNumber} already exists.");
            }

            _accounts.Add(account);
            await Task.CompletedTask;
        }

        public async Task Deposit(int accountNumber, decimal amount)
        {
            var account = await GetByAccountNumberAsync(accountNumber);

            if (amount <= 0)
            {
                throw new InvalidOperationException("The deposit amount must be greater than 0.");
            }

            account.Balance += amount;
            await Task.CompletedTask;
        }

        public async Task Withdraw(int accountNumber, decimal amount)
        {
            var account = await GetByAccountNumberAsync(accountNumber);

            if (amount <= 0)
            {
                throw new InvalidOperationException("Withdrawal amount must be greater than 0.");
            }

            if (account.Balance < amount) 
            {
                throw new InvalidOperationException($"Insufficient funds in the account with number {account.AccountNumber}. Balance: {account.Balance}.");
            }

            account.Balance -= amount;
            await Task.CompletedTask;
        }

        public async Task Transfer(int senderAccNumber, int receiverAccNumber, decimal amount)
        {
            var senderAccount = await GetByAccountNumberAsync(senderAccNumber);
            var receiverAccount = await GetByAccountNumberAsync(receiverAccNumber);

            if (amount <= 0)
            {
                throw new InvalidOperationException("The transfer amount must be greater than 0.");
            }

            if (senderAccount.Balance < amount) 
            {
                throw new InvalidOperationException($"Insufficient funds in the sender account with number {senderAccNumber}. Balance: {senderAccount.Balance}.");
            }

            senderAccount.Balance -= amount;
            receiverAccount.Balance += amount;
            await Task.CompletedTask;
        }
    }
}
