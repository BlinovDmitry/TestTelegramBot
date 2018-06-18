using LoymaxTest.Models;
using Telegram.Bot;

namespace LoymaxTest.Controllers
{
    public interface IMessageContext
    {      
        ITelegramBotClient TelegramBotApiClient { get; }

        IUserDataRepository UserDataRepository { get; }

        IStateRepository StateRepository { get; }
    }
}