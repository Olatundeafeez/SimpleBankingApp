using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using simpleBankingAppAPI.DataAccess.Interface;
using simpleBankingAppAPI.DataAccess.Utilities;
using simpleBankingAppAPI.Model;
using simpleBankingAppAPI.Model.DTOs;

namespace simpleBankingAppAPI.DataAccess.Repository
{
    
    public class SimpleBankingRepository : ISimpleBanking
    {
        private readonly ApplicationContext context;
        public SimpleBankingRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<User> CreateAccount(CreateAccountDTO account)
        {
            try
            {
                var checkAccount = await context.Users.AnyAsync( x => x.AccountNumber == account.AccountNumber );
                if ( checkAccount ) 
                {
                    throw new Exception("account number already exist");
                }
                var newAccount = new User()
                {
                    AccountName = account.AccountName,
                    Email = account.Email,
                    AccountNumber = GenerateRandomAccount.GetAcctNumber(),
                };
                await context.Users.AddAsync(newAccount);
                await context.SaveChangesAsync();
                return newAccount;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> Deposit(string accountNumber, decimal amount)
        {
            try
            {
                var acct = await context.Users.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
                if (acct == null)
                {
                    throw new Exception("Account number does not exist");
                }
                if(amount <= 0)
                {
                    throw new Exception("cannot deposit zero or negative amount ");
                }
                // aact.AccountBalance += amount;
                //acct.AccountBalance = acct.AccountBalance + amount;
                //context.Users.Update(acct);
                //await context.SaveChangesAsync();
                return acct;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<GetAccountBalanceDTO> GetAccountBalance(string accountNumber)
        {
            try
            {
                var  checkAcct = await context.Users.FirstOrDefaultAsync( x => x.AccountNumber == accountNumber);
                if(checkAcct == null)
                {
                    throw new Exception("account number is wrong");
                }
                var acctBalance = new GetAccountBalanceDTO()
                {
                    AccountBalance = checkAcct.AccountBalance
                };
                return acctBalance;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Transfer(TransferDTO transfer)
        {
            try 
            {
                var sender = await context.Users.FirstOrDefaultAsync(x => x.AccountNumber == transfer.SenderAccount);
                var reciever = await context.Users.FirstOrDefaultAsync(x => x.AccountNumber == transfer.RecieverAccount);
                if (sender == null || reciever == null) 
                {
                    throw new Exception("invalid account number for sender or reciever");
                }
                if(sender.AccountBalance < 100)
                {
                    throw new Exception("your account balance is too low for this transaction");

                }
                if(transfer.Amount <= 0)
                {
                    throw new Exception("you cannot transfer zero or negative number");

                }
                sender.AccountBalance -=  transfer.Amount;
                reciever.AccountBalance += transfer.Amount;
                context.Users.Update(sender);
                context.Users.Update(reciever);
                await context.SaveChangesAsync();
                return $"dear{sender.AccountName} you have successfully transfered" +
                    $"#{transfer.Amount} from {reciever.AccountName}" +
                    $"your account balance is {sender.AccountBalance}";
            }
            catch( Exception ex ) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Withdraw(WithdrawDTO account)
        {
            try
            {
                var acct = await context.Users.FirstOrDefaultAsync(x => x.AccountNumber == account.AccountNumber);
                if(acct == null)
                {
                    throw new Exception("Account does not exist");

                }
                if(account.Amount <= 0)
                {
                    throw new Exception("cant have negative digit");
                }
                acct.AccountBalance = acct.AccountBalance - account.Amount;
                context.Users.Update(acct);
                await context.SaveChangesAsync();
                return $"total sum of {acct.AccountName}" +
                    $" withdrawn successfully from {account.Amount} " +
                    $"from your account number {acct.AccountNumber}," +
                    $" your new account balance is {acct.AccountBalance} ";
                
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
                    
            }
        }
    }
}
