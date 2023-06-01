using ASP_NET_Assignment1.Data;
using ASP_NET_Assignment1.Repos;
using ASP_NET_Assignment1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASP_NET_Assignment1.Models;

namespace ASP_NET_Assignment1.Controllers
{

    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientRepo ClientRepo;
        public ClientAccountRepo ClientAccountRepo;
        public BankAccountRepo BankAccountRepo;



        public AccountController(ApplicationDbContext context)
        {
            _context = context;

            ClientRepo = new ClientRepo(_context);
            ClientAccountRepo = new ClientAccountRepo(_context);
            BankAccountRepo = new BankAccountRepo(_context);
        }
        /// <summary>
        /// Index View Method
        /// - sends IEnumberable of client account view mdoels to the Index view
        /// </summary>
        [Authorize]
        public IActionResult Index()
        {
            string userName = User.Identity.Name;

            IEnumerable<ClientAccountVM> carList = ClientAccountRepo.ReturnCLientAccountVMList(userName);
            
            return View(carList);
        }
        /// <summary>
        /// Details Function
        /// - sends specific client account view model to the Detail view
        /// </summary>
        /// <param name="accountNum">Passed from Index.cshtml</param>
        /// <param name="message"> Message to display on Detail view on Create / Edit actions </param>
        public IActionResult Details(int accountNum, string message)
        {
            string userName = User.Identity.Name;
            
            var user = ClientRepo.GetClient(userName);
            
            IEnumerable<ClientAccountVM> accounts =  ClientAccountRepo.ReturnCLientAccountVMList(userName);

            var account = ClientAccountRepo.GetClientAccountVM(accountNum);

            account.Message = message;

            return View(account);
        }
        /// <summary>
        /// Create View Method
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["AccountType"] = new SelectList(_context.BankAccountType, "AccountType", "AccountType");
            return View();
        }

        /// <summary>
        /// Post Create Method
        /// - Binds a client account view model from Create View then adds newly created BankAccount and ClientAccount to database
        /// </summary>
        /// <param name="account">Binded ClientAccountVM</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([Bind("FirstName,LastName,AccountNum,AccountType,Balance")] ClientAccountVM account)
        {
            string userName = User.Identity.Name;

            var regUser = ClientRepo.GetClient(userName);
            var clientId = regUser.ClientID;

            if(!ModelState.IsValid)
            {
         
                BankAccount bankAccount = new BankAccount()
                {
                    AccountType = account.AccountType,
                    Balance = account.Balance
                };

                BankAccountRepo.AddBankAccount(bankAccount);

                ClientAccount clientAccount = new ClientAccount()
                {
                    ClientID = clientId,
                    BankAccount = bankAccount,
                };

                ClientAccountRepo.AddClientAccount(clientAccount);

                string createMessage = $"{bankAccount.AccountType} Account created.";
    
                return RedirectToAction("Details", new {AccountNum = bankAccount.AccountNum, Message = createMessage});
            }

            ViewData["AccountType"] = new SelectList(_context.BankAccountType, "AccountType", "AccountType");
            return View();
        }
        /// <summary>
        /// Edit View Method
        /// </summary>
        /// <param name="accountNum">Passed from Index.cshtml</param>
        /// <returns> View with details of account </returns>
        public IActionResult Edit(int accountNum)
        {
            string userName = User.Identity.Name;
            var user = ClientRepo.GetClient(userName);

            
            IEnumerable<ClientAccountVM> accounts = ClientAccountRepo.ReturnCLientAccountVMList(userName);
            
            var account = ClientAccountRepo.GetClientAccountVM(accountNum);
            account.Message = "Account Edited!";
            ViewData["AccountType"] = new SelectList(_context.BankAccountType, "AccountType", "AccountType");

            return View(account);

        }
        /// <summary>
        /// Post Edit Method
        /// - Binds client account Vm and updates BankAccount with new values from binded client account vm
        /// </summary>
        /// <param name="clientAccountVM"></param>
        /// <param name="accountNum"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(
            [Bind("ClientID, FirstName,LastName,Email,AccountNum,AccountType,Balance")] ClientAccountVM clientAccountVM, int accountNum)
        {
            
            ViewData["Message"] = "";

            string userName = User.Identity.Name;
            var user = ClientRepo.GetClient(userName);

            if (ModelState.IsValid)
            {
                try
                {
                    user.FirstName = clientAccountVM.FirstName;
                    user.LastName = clientAccountVM.LastName;

                    ClientRepo.UpdateClient(user);

                    BankAccount bankAccount = BankAccountRepo.GetBankAccount(accountNum);
                    bankAccount.AccountType = clientAccountVM.AccountType;
                    bankAccount.Balance = clientAccountVM.Balance;

                    BankAccountRepo.UpdateBankAccount(bankAccount);

                    string createMessage = $"Account #{bankAccount.AccountNum} edited.";

                    return RedirectToAction("Details", new { AccountNum = accountNum, Message = createMessage });

                }
                catch (Exception ex)
                {
                    ViewData["Message"] = ex.Message;
                }
            }
            ViewData["AccountType"] = new SelectList(_context.BankAccountType, "AccountType",
                                                "AccountType", clientAccountVM.AccountType);
            return View();
            
        }
        /// <summary>
        /// Delete View Method
        /// </summary>
        /// <param name="accountNum"></param>
        /// <returns></returns>
        public IActionResult Delete(int accountNum)
        {
            var bankAccount = BankAccountRepo.GetBankAccount(accountNum);

            return View(bankAccount);
        }
        /// <summary>
        /// Post Delete Method
        /// - Binds BankAccount to be removed from database
        /// - Removes ClientAccount and Bank Account from database (respectively)
        /// </summary>
        /// <param name="account"></param>
        /// <param name="accountNum">Passed from Index.cshtml </param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete([Bind("AccountNum,AccountType,Balance")] BankAccount account, int accountNum)
        {
            string userName = User.Identity.Name;
            var user = ClientRepo.GetClient(userName);

            if (!ModelState.IsValid)
            {
                try
                {
                    ClientAccount clientAccount = ClientAccountRepo.GetClientAccount(accountNum);

                    ClientAccountRepo.RemoveClientAccount(clientAccount);
                    BankAccountRepo.RemoveBankAccount(account);

                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ViewData["Message"] = ex.Message;
                }
            }
            return RedirectToAction("Index", "Account");
        }

        
    }
}
