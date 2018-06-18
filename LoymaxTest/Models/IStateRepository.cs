using System;

namespace LoymaxTest.Models
{
    public interface IStateRepository
    {
        Guid this[long chatId, int userId] { get; set; }

        bool IsStateEmpty(long chatId, int userId);

        void ClearState(long chatId, int userId);
    }
}

