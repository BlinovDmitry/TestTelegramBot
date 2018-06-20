using System.Threading.Tasks;

namespace LoymaxTest.Models
{
    public class DbStateRepository : IStateRepository
    {
        public DbStateRepository(LoymaxTestBotDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected LoymaxTestBotDbContext DbContext { get; }

        public State ClearState(long chatId, int userId)
        {
            return SetState(chatId, userId, State.Empty);
        }

        public async Task<State> ClearStateAsync(long chatId, int userId)
        {            
            return await SetStateAsync(chatId, userId, State.Empty);
        }

        public State GetState(long chatId, int userId)
        {
            var chatState = DbContext.ChatStates.Find(chatId, userId);
            if (chatState == null)
                return State.Empty;
            else
                return new State(chatState.StateGuid, chatState.AdditionalData);
        }

        public async Task<State> GetStateAsync(long chatId, int userId)
        {
            var chatState = await DbContext.ChatStates.FindAsync(chatId, userId);

            if (chatState == null)
                return State.Empty;
            else
                return new State(chatState.StateGuid, chatState.AdditionalData);
        }

        public State SetState(long chatId, int userId, State state)
        {
            var chatState = DbContext.ChatStates.Find(chatId, userId)
                ?? DbContext.ChatStates.Add(new ChatState(chatId, userId, state.StateGuid, state.AdditionalData));
            chatState.StateGuid = state.StateGuid;
            chatState.AdditionalData = state.AdditionalData;
            DbContext.SaveChanges();
            return state;
        }

        public async Task<State> SetStateAsync(long chatId, int userId, State state)
        {
            var chatState = await DbContext.ChatStates.FindAsync(chatId, userId)
                ?? DbContext.ChatStates.Add(new ChatState(chatId, userId, state.StateGuid, state.AdditionalData));
            chatState.StateGuid = state.StateGuid;
            chatState.AdditionalData = state.AdditionalData;
            await DbContext.SaveChangesAsync();
            return state;
        }
    }

}
