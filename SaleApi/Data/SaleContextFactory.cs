
using Microsoft.EntityFrameworkCore;

namespace SaleApi.Data
{
    public class SaleContextFactory
    {
        private const string ConnectionString = ("Server==(localdb)\\MSSQLLocalDB;DataBase=SaleDB;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=True;");
        //private const string ConnectionString = "Server=DESKTOP-1L8084V\\SQLEXPRESS;DataBase=HomePoducts216234070;Integrated Security=SSPI;" +
        //   "Persist Security Info=False;TrustServerCertificate=true"; 
        public static SaleContextDB CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SaleContextDB>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new SaleContextDB(optionsBuilder.Options);
        }
    }
}
