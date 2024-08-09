using BankingApp.Core.Interfaces;
using BankingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Application.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _accountRepository.GetAsync();
        }

        public async Task<Account> GetAccountByNumber(int accountNumber)
        {
            return await _accountRepository.GetByAccountNumberAsync(accountNumber);
        }

        public async Task CreateAccount(Account account)
        {
            await _accountRepository.CreateAsync(account);
        }

        public async Task Deposit(int accountNumber, decimal amount)
        {
            await _accountRepository.Deposit(accountNumber, amount);
        }

        public async Task Withdraw(int accountNumber, decimal amount)
        {
            await _accountRepository.Withdraw(accountNumber, amount);
        }

        public async Task Transfer(int senderAccNumber, int receiverAccNumber, decimal amount)
        {
            await _accountRepository.Transfer(senderAccNumber, receiverAccNumber, amount);
        }
    }
}
