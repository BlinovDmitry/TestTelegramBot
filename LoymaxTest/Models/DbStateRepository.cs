using System;

namespace LoymaxTest.Models
{
    public class DbStateRepository : IStateRepository
    {
        public DbStateRepository(LoymaxTestBotDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected LoymaxTestBotDbContext DbContext { get; }

        public Guid this[long chatId, int userId]
        {
            get => DbContext.ChatStates.Find(chatId, userId)?.StateGuid ?? Guid.Empty;

            set
            {
                var chatState = DbContext.ChatStates.Find(chatId, userId)
                    ?? DbContext.ChatStates.Add(new ChatState(chatId, userId, value));
                chatState.StateGuid = value;
                DbContext.SaveChanges();
            }
        }

        public bool IsStateEmpty(long chatId, int userId)
        {
            return this[chatId, userId] == Guid.Empty;
        }

        public void ClearState(long chatId, int userId)
        {
            this[chatId, userId] = Guid.Empty;
        }
    }

}
