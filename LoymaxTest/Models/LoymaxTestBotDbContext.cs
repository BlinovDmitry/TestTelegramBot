using System.Data.Entity;

namespace LoymaxTest.Models
{
    public class LoymaxTestBotDbContext : DbContext
    {        
        public LoymaxTestBotDbContext() : base("name=LoymaxTestBotDbContext")
        {
        }

        public DbSet<UserData> UserDatas { get; set; }

        public DbSet<ChatState> ChatStates { get; set; }
    }
}
