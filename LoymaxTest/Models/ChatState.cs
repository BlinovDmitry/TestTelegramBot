using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoymaxTest.Models
{
    public class ChatState 
    {
        public ChatState()
        {
        }

        public ChatState(long chatId, int userId, Guid stateGuid) : this()
        {
            ChatId = chatId;
            UserId = userId;
            StateGuid = stateGuid;
        }

        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ChatId { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }               
                
        public Guid StateGuid { get; set; }    
    }
}