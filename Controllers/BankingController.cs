using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using simpleBankingAppAPI.DataAccess.Interface;
using simpleBankingAppAPI.Model.DTOs;

namespace simpleBankingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly ISimpleBanking _repo;
        public BankingController(ISimpleBanking repo)
        {
            _repo = repo;
        }

        [HttpPost("CreateNewUser")]

        public async Task<IActionResult> CreateUser([FromBody] CreateAccountDTO account)
        {
             try
             {
                var response = await _repo.CreateAccount(account);
                return Ok(response);
        
             }
              catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        [HttpGet("CheckAccountBalance")]
        public async Task<IActionResult> CheckAccountBalance(string accountNumber) 
        {
            try
            {
                var response = await _repo.GetAccountBalance(accountNumber);
                return Ok(response);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("DepositMoney")]

        public async Task<IActionResult> Deposit(string accountNumber, decimal amount)
        {
            try 
            {
             var response = await _repo.Deposit(accountNumber, amount);
                return Ok(response); 

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("TransferMoney")]
        
        public async Task<IActionResult> Transfer(TransferDTO transfer)
        {
            try 
            {
                var response = await _repo.Transfer(transfer);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("WithdrawMoney")]
        public async Task<IActionResult> Withdraw(WithdrawDTO account)
        {
            try
            {
                var response = await _repo.Withdraw(account);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
