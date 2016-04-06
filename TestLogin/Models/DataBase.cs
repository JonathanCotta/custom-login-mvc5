using System.Data.Entity;

namespace TestLogin.Models
{
    public class DataBase : DbContext
    {
        public DataBase() : base("name=DataBaseConnection")
        {

        }
        
        public DbSet<User> Users { get; set; } // Tabela usuario

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");                      
                          
        }
    }
}