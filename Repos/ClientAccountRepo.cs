using ASP_NET_Assignment1.Data;
using ASP_NET_Assignment1.ViewModels;
using ASP_NET_Assignment1.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_Assignment1.Repos
{
    public class ClientAccountRepo
    {
        private readonly ApplicationDbContext _context;

        public ClientAccountRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Returns IEnumerable of ClientAccountVm of given user
        ///  - To be used in Index.cstml to show all accounts
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ClientAccountVM> ReturnCLientAccountVMList(string id)
        {
            var vmList = from c in _context.Client
                         join ca in _context.ClientAccount
                                 on c.ClientID equals ca.ClientID
                         where c.Email == id
                         join ba in _context.BankAccount
                                 on ca.AccountNum equals ba.AccountNum
                                 orderby ca.AccountNum descending
                         select new ClientAccountVM
                         {
                             ClientID = c.ClientID
                           ,
                             FirstName = c.FirstName
                           ,
                             LastName = c.LastName
                           ,
                             Email = c.Email
                           ,
                             AccountNum = ba.AccountNum
                           ,
                             AccountType = ba.AccountType
                           ,
                             Balance = ba.Balance
                         };

            return vmList;
        }
        /// <summary>
        /// Gets single client Account VM based on given account Num
        /// </summary>
        /// <param name="accountNum"></param>
        /// <returns></returns>
        public ClientAccountVM GetClientAccountVM(int accountNum)
        {
            var vm = from c in _context.Client
                         join ca in _context.ClientAccount
                                 on c.ClientID equals ca.ClientID
                         join ba in _context.BankAccount
                                 on ca.AccountNum equals ba.AccountNum
                                 where ca.AccountNum == accountNum

                     select new ClientAccountVM
                         {
                             ClientID = c.ClientID
                           ,
                             FirstName = c.FirstName
                           ,
                             LastName = c.LastName
                           ,
                             Email = c.Email
                           ,
                             AccountNum = ba.AccountNum
                           ,
                             AccountType = ba.AccountType
                           ,
                             Balance = ba.Balance
                         };

            return vm.FirstOrDefault();
        }
        /// <summary>
        /// Gets Client Account based on account number
        /// </summary>
        /// <param name="accountNum"></param>
        /// <returns></returns>
        public ClientAccount GetClientAccount(int accountNum)
        {
            ClientAccount ca = _context.ClientAccount.Where(ca => ca.AccountNum == accountNum).FirstOrDefault();
            return ca;
        }
        /// <summary>
        /// Adds client account to database
        /// </summary>
        /// <param name="ca"> Client account to be added</param>
        public void AddClientAccount(ClientAccount ca)
        {
            _context.Add(ca);
            _context.SaveChanges();
        }
        /// <summary>
        /// Removes client account to database
        /// </summary>
        /// <param name="ca"></param>
        public void RemoveClientAccount(ClientAccount ca)
        {
            _context.Remove(ca);
            _context.SaveChanges();
        }


    }
}
