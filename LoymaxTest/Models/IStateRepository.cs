using System;
using System.Threading.Tasks;
using Unity.Interception.Utilities;

namespace LoymaxTest.Models
{
    public interface IStateRepository
    {
        State GetState(long chatId, int userId);

        State SetState(long chatId, int userId, State state);

        State ClearState(long chatId, int userId);

        Task<State> GetStateAsync(long chatId, int userId);

        Task<State> SetStateAsync(long chatId, int userId, State state);

        Task<State> ClearStateAsync(long chatId, int userId);
    }
}

