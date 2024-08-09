using BankingApp.Application.Services;
using BankingApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAllAccounts();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAccountByNumber/{accountNumber}")]
        public async Task<IActionResult> GetAccountByNumber(int accountNumber)
        {
            try
            {
                var account = await _accountService.GetAccountByNumber(accountNumber);
                if (account == null)
                {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAccount(Account account)
        {
            try
            {
                await _accountService.CreateAccount(account);
                return Ok(new { message = $"The account with number {account.AccountNumber} successfully created." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Deposit/{accountNumber}")]
        public async Task<IActionResult> Deposit(int accountNumber, [FromBody] decimal amount)
        {
            try
            {
                await _accountService.Deposit(accountNumber, amount);
                var acc = await _accountService.GetAccountByNumber(accountNumber);
                return Ok(new
                {
                    message = $"{amount} has been deposited to account with number {accountNumber}. Balance: {acc.Balance}."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Withdraw/{accountNumber}")]
        public async Task<IActionResult> Withdraw(int accountNumber, [FromBody] decimal amount)
        {
            try
            {
                await _accountService.Withdraw(accountNumber, amount);
                var acc = await _accountService.GetAccountByNumber(accountNumber);
                return Ok(new
                {
                    message = $"{amount} has been withdrawn from account with number {accountNumber}. Balance: {acc.Balance}."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer(int senderAccNumber, int receiverAccNumber, [FromBody] decimal amount)
        {
            try
            {
                await _accountService.Transfer(senderAccNumber, receiverAccNumber, amount);

                var senderAccount = await _accountService.GetAccountByNumber(senderAccNumber);
                var receiverAccount = await _accountService.GetAccountByNumber(receiverAccNumber);

                return Ok(new
                {
                    message = $"{amount} has been transferred from account {senderAccNumber} to account {receiverAccNumber}. " +
                              $"Sender's new balance: {senderAccount.Balance}. Receiver's new balance: {receiverAccount.Balance}."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
