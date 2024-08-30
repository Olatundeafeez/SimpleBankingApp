using simpleBankingAppAPI.Model;
using simpleBankingAppAPI.Model.DTOs;

namespace simpleBankingAppAPI.DataAccess.Interface
{
    public interface ISimpleBanking
    {
        Task<User> CreateAccount(CreateAccountDTO account);
        
        Task<User> Deposit(string accountNumber, decimal amount);
        Task<GetAccountBalanceDTO>  GetAccountBalance(string accountNumber);
        Task<string> Transfer(TransferDTO transfer);
        Task<string> Withdraw(WithdrawDTO account);



    }
}
