using ASP_NET_Assignment1.Data;
using ASP_NET_Assignment1.Models;

namespace ASP_NET_Assignment1.Repos
{
    public class BankAccountRepo
    {
        private readonly ApplicationDbContext _context;

        public BankAccountRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adds bank account to database
        /// </summary>
        /// <param name="ba">BankAccount to add to database</param>
        public void AddBankAccount(BankAccount ba)
        {
            _context.Add(ba);
            _context.SaveChanges();
        }
        /// <summary>
        /// Gets bank account from database
        /// </summary>
        /// <param name="accountNum">Account number of bank account to be returned</param>
        /// <returns></returns>
        public BankAccount GetBankAccount(int accountNum)
        {
            BankAccount bankAccount = _context.BankAccount.Where(ba => ba.AccountNum == accountNum).FirstOrDefault();
            return bankAccount;
        }
      
        /// <summary>
        /// Updates bank account in database
        /// </summary>
        /// <param name="ba"> Bank account to be updated</param>
        public void UpdateBankAccount(BankAccount ba)
        {
            _context.Update(ba);
            _context.SaveChanges();
        }
        /// <summary>
        /// Removes bank account from database
        /// </summary>
        /// <param name="ba"> Bank account to removed</param>
        public void RemoveBankAccount(BankAccount ba)
        {
            _context.Remove(ba);
            _context.SaveChanges();
        }

    }
}
