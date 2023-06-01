using ASP_NET_Assignment1.Data;
using ASP_NET_Assignment1.Models;
using ASP_NET_Assignment1.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP_NET_Assignment1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;

        }
        /// <summary>
        /// Index View Method
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            if(User.Identity.IsAuthenticated)
            {
                ClientRepo cr = new ClientRepo(_context);
                var client = _context.Client.Where(c => c.Email == User.Identity.Name).FirstOrDefault();
                HttpContext.Session.SetString("fName", client.FirstName);
                HttpContext.Session.SetInt32("ClientID", client.ClientID);
            }
            else
            {
                HttpContext.Session.SetString("fname", " ");
            }
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}