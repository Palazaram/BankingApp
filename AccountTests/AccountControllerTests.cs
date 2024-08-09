using BankingApp.Application.Services;
using BankingApp.Controllers;
using BankingApp.Core.Models;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Core.Interfaces;
using BankingApp.Persistence.Repositories;

namespace AccountTests
{

    public class AccountControllerTests
    {
        private readonly AccountController _controller;
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly AccountService _mockAccountService;

        public AccountControllerTests()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockAccountService = new AccountService(_mockAccountRepository.Object);
            _controller = new AccountController(_mockAccountService);
        }

        [Fact]
        public async Task GetAllAccounts_ReturnsOkObjectResult()
        {
            // Arrange
            _mockAccountRepository.Setup(x => x.GetAsync()).ReturnsAsync(new List<Account>
            {
                new Account { AccountNumber = 1, Balance = 100 },
                new Account { AccountNumber = 2, Balance = 200 }
            });

            // Act
            var result = await _controller.GetAllAccounts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var accounts = Assert.IsAssignableFrom<IEnumerable<Account>>(okResult.Value);
            Assert.Equal(2, accounts.Count());
        }

        [Fact]
        public async Task GetAccountByNumber_WithValidAccountNumber_ReturnsOkObjectResult()
        {
            // Arrange
            var accountNumber = 1;
            var account = new Account { AccountNumber = accountNumber, Balance = 100 };
            _mockAccountRepository.Setup(x => x.GetByAccountNumberAsync(accountNumber)).ReturnsAsync(account);

            // Act
            var result = await _controller.GetAccountByNumber(accountNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultAccount = Assert.IsAssignableFrom<Account>(okResult.Value);
            Assert.Equal(accountNumber, resultAccount.AccountNumber);
        }

        [Fact]
        public async Task GetAccountByNumber_WithInvalidAccountNumber_ReturnsNotFoundResult()
        {
            // Arrange
            var accountNumber = 1;
            _mockAccountRepository.Setup(x => x.GetByAccountNumberAsync(accountNumber)).ReturnsAsync((Account)null);

            // Act
            var result = await _controller.GetAccountByNumber(accountNumber);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateAccount_WithValidAccount_ReturnsOkObjectResult()
        {
            // Arrange
            var account = new Account { AccountNumber = 1, Balance = 100 };

            // Act
            var result = await _controller.CreateAccount(account);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var resultValue = (okResult.Value).GetType().GetProperty("message").GetValue(okResult.Value, null);

            Assert.Contains("successfully created", resultValue.ToString());
        }

        [Fact]
        public async Task Deposit_WithValidAccountNumber_ReturnsOkObjectResult()
        {
            // Arrange
            var accountNumber = 1;
            var amount = 50m;
            var account = new Account { AccountNumber = accountNumber, Balance = 100 };
            _mockAccountRepository.Setup(x => x.GetByAccountNumberAsync(accountNumber)).ReturnsAsync(account);

            // Act
            var result = await _controller.Deposit(accountNumber, amount);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var resultValue = okResult.Value.GetType().GetProperty("message").GetValue(okResult.Value, null);

            Assert.Contains("has been deposited to account with number", resultValue.ToString());
        }

        [Fact]
        public async Task Withdraw_WithValidAccountNumber_ReturnsOkObjectResult()
        {
            // Arrange
            var accountNumber = 1;
            var amount = 50m;
            var account = new Account { AccountNumber = accountNumber, Balance = 100 };
            _mockAccountRepository.Setup(x => x.GetByAccountNumberAsync(accountNumber)).ReturnsAsync(account);

            // Act
            var result = await _controller.Withdraw(accountNumber, amount);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var resultValue = okResult.Value.GetType().GetProperty("message").GetValue(okResult.Value, null);

            Assert.Contains("has been withdrawn from account with number", resultValue.ToString());
        }

        [Fact]
        public async Task Transfer_WithValidAccountNumbers_ReturnsOkObjectResult()
        {
            // Arrange
            var senderAccNumber = 1;
            var receiverAccNumber = 2;
            var amount = 50m;

            var senderAccount = new Account { AccountNumber = senderAccNumber, Balance = 100 };
            var receiverAccount = new Account { AccountNumber = receiverAccNumber, Balance = 200 };

            _mockAccountRepository.Setup(x => x.GetByAccountNumberAsync(senderAccNumber)).ReturnsAsync(senderAccount);
            _mockAccountRepository.Setup(x => x.GetByAccountNumberAsync(receiverAccNumber)).ReturnsAsync(receiverAccount);

            // Act
            var result = await _controller.Transfer(senderAccNumber, receiverAccNumber, amount);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var resultValue = okResult.Value.GetType().GetProperty("message").GetValue(okResult.Value, null);

            Assert.Contains("has been transferred from account", resultValue.ToString());
        }

    }

}
