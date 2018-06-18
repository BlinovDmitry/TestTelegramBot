using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoymaxTest.Models
{
    public class DbUserDataRepository : IUserDataRepository
    {
        public DbUserDataRepository(LoymaxTestBotDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected LoymaxTestBotDbContext DbContext { get; }

        public int SaveChanges()
        {            
            return DbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public UserData Add(UserData userData)
        {            
            return DbContext.UserDatas.Add(userData);
        }

        public UserData Remove(UserData userData)
        {
            return DbContext.UserDatas.Remove(userData);
        }

        public UserData Find(int userDataId)
        {
            return DbContext.UserDatas.Find(userDataId);
        }

        public async Task<UserData> FindAsync(int userDataId)
        {
            return await DbContext.UserDatas.FindAsync(userDataId);
        }
    }
}