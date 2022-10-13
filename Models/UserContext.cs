namespace reg_log.Models
{

    using System.Data.Entity;
    public class UserContext : DbContext
    {
        public UserContext() : base(@"Server={host};Database={database name};User={user name};Password={password};")
        {
        }

        public DbSet<User> Users { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
