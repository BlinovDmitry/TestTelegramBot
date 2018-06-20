using LoymaxTest.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LoymaxTest.Controllers.Actions
{
    public class DefaultActionListFactory : IActionListFactory
    {       
    
        protected delegate void SetFieldsHandler(UserData userData);

        public IList<AbstractAction> CreateInstance()
        {
            var list = DoCreateInstance();
            list.Add(new UnknownAction());
            return list;
        }

        protected virtual IList<AbstractAction> DoCreateInstance()
        {
            var list = new List<AbstractAction>();
            list.Add(new GetInfoAction());
            list.Add(new DeleteAction());
            list.Add(CreateRegisterAction());
            return list;
        }

        protected static async Task<bool> RememberTemporaryUserData(Message message, IMessageContext context, SetFieldsHandler setFieldsHandler)
        {
            var state = await context.StateRepository.GetStateAsync(message.Chat.Id, message.From.Id);
            var userData = (state.AdditionalData as UserData)
                    ?? new UserData(message.From.Id);
                 
            setFieldsHandler(userData);
            state.AdditionalData = userData;
            await context.StateRepository.SetStateAsync(message.Chat.Id, message.From.Id, state);
            return true;
        }

        protected AbstractAction CreateRegisterAction()
        {            
            var startWizardAction = new StartWizardAction("register",
                // Имя, начало регистрации
                new ChainWizardAction($"{LoymaxTestBotResources.RegisterActionExecuteTitle}. {LoymaxTestBotResources.RegisterActionExecuteNameQuestion}", 
                    new Guid("464F9A1E-8DAF-47B8-B620-6895053F178D"),
                    beforeActionHandler: async(m, c, a) =>
                    {
                        if (await c.UserDataRepository.FindAsync(m.From.Id) != null)
                        {
                            await a.ReplyToMessageAsync(m, c, LoymaxTestBotResources.RegisterActionExecuteErrorContactExists);
                            return false;
                        }
                        return true;
                    },
                    afterActionHandler: async (m, c, a) => await RememberTemporaryUserData(m, c,
                        (userData) => userData.Name = m.Text)));

            startWizardAction.ChainStart
                // Отчество
                .AddChainAction(new ChainWizardAction(LoymaxTestBotResources.RegisterActionExecuteMidNameQuestion, 
                new Guid("56728ABE-5776-46F1-BCF2-B1D011F86C97"),
                    afterActionHandler: async (m, c, a) => await RememberTemporaryUserData(m, c,
                        (userData) => userData.MidName = m.Text)))

                // Фамилия
                .AddChainAction(new ChainWizardAction(LoymaxTestBotResources.RegisterActionExecuteLastNameQuestion, 
                new Guid("61AF5049-7AEC-4DAF-8532-F13EA867C011"),
                    afterActionHandler: async (m, c, a) => await RememberTemporaryUserData(m, c,
                        (userData) => userData.LastName = m.Text)))

                // Дата рождения, окончание регистрации
                .AddChainAction(new ChainWizardAction(LoymaxTestBotResources.RegisterActionExecuteBirthDateQuestion, 
                new Guid("49DB8E63-B638-41E4-B0D3-61C4603AE1EC"),
                    afterActionHandler: async (m, c, a) =>
                    {
                        if (DateTime.TryParse(m.Text, out var birthDate))
                        {
                            await RememberTemporaryUserData(m, c,
                                (userData) => userData.BirthDate = birthDate);

                            var completedUserData = (await c.StateRepository.GetStateAsync(m.Chat.Id, m.From.Id)).AdditionalData as UserData;
                            if (completedUserData != null)
                            {
                                c.UserDataRepository.Add(completedUserData);
                                await c.UserDataRepository.SaveChangesAsync();
                            }
                            await a.ReplyToMessageAsync(m, c, LoymaxTestBotResources.RegisterActionExecuteOk);
                            return true;
                        }
                        else
                        {
                            await a.ReplyToMessageAsync(m, c, LoymaxTestBotResources.RegisterActionExecuteErrorWrongDateFormat);
                            return false;
                        }
                    }));

            return startWizardAction;
        }
    }
}