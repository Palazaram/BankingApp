using BankingApp.Application.Services;
using BankingApp.Core.Interfaces;
using BankingApp.Core.Models;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountTests
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> _mockRepo;
        private readonly AccountService _service;

        public AccountServiceTests()
        {
            _mockRepo = new Mock<IAccountRepository>();
            _service = new AccountService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllAccounts_ShouldReturnAllAccounts()
        {
            var accounts = new List<Account>
            {
                new Account { AccountNumber = 1, Balance = 100m },
                new Account { AccountNumber = 2, Balance = 200m }
            };
            _mockRepo.Setup(repo => repo.GetAsync()).ReturnsAsync(accounts);

            var result = await _service.GetAllAccounts();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAccountByNumber_ShouldReturnAccount()
        {
            var account = new Account { AccountNumber = 1, Balance = 100m };
            _mockRepo.Setup(repo => repo.GetByAccountNumberAsync(1)).ReturnsAsync(account);

            var result = await _service.GetAccountByNumber(1);

            Assert.Equal(100m, result.Balance);
        }

        [Fact]
        public async Task CreateAccount_ShouldCallCreateAsync()
        {
            var account = new Account { AccountNumber = 1, Balance = 100m };

            await _service.CreateAccount(account);

            _mockRepo.Verify(repo => repo.CreateAsync(account), Times.Once);
        }

        [Fact]
        public async Task Deposit_ShouldCallDeposit()
        {
            await _service.Deposit(1, 50m);

            _mockRepo.Verify(repo => repo.Deposit(1, 50m), Times.Once);
        }

        [Fact]
        public async Task Withdraw_ShouldCallWithdraw()
        {
            await _service.Withdraw(1, 50m);

            _mockRepo.Verify(repo => repo.Withdraw(1, 50m), Times.Once);
        }

        [Fact]
        public async Task Transfer_ShouldCallTransfer()
        {
            await _service.Transfer(1, 2, 50m);

            _mockRepo.Verify(repo => repo.Transfer(1, 2, 50m), Times.Once);
        }
    }
}
