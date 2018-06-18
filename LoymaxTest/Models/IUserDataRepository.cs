using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoymaxTest.Models
{
    public interface IUserDataRepository 
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();        

        UserData Add(UserData userData);

        UserData Remove(UserData userData);

        UserData Find(int userDataId);

        Task<UserData> FindAsync(int userDataId);
    }
}
