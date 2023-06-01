using ASP_NET_Assignment1.Models;
using ASP_NET_Assignment1.Data;


namespace ASP_NET_Assignment1.Repos
{
    public class ClientRepo
    {
        ApplicationDbContext _context;
        
        public ClientRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adds Client to database
        /// </summary>
        /// <param name="client"> Client to be added </param>
        /// <returns></returns>
        public string CreateClient(Client client)
        {
            string message = "";
            _context.Add(client);
            _context.SaveChanges();

            return message = "Success";
        }
        /// <summary>
        /// Updates Client in database
        /// </summary>
        /// <param name="client"> Client to be updated</param>
        public void UpdateClient(Client client)
        {
            _context.Update(client);
            _context.SaveChanges();
        }
        /// <summary>
        /// Gets client based on given id (email)
        /// </summary>
        /// <param name="id"> Email of user </param>
        /// <returns></returns>
        public Client GetClient(string id)
        {
            var user = _context.Client.Where(c => c.Email == id).FirstOrDefault();
            return user;
        }
    }
}
