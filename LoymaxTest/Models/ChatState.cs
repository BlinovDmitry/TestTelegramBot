using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoymaxTest.Models
{
    [Table("ChatStates", Schema = "states")]
    public class ChatState
    {
        public ChatState()
        {
        }

        public ChatState(long chatId, int userId, Guid stateGuid, object additionalData) : this()
        {
            ChatId = chatId;
            UserId = userId;
            StateGuid = stateGuid;
            AdditionalData = additionalData;
        }

        private JsonSerializerSettings AdditionalDataSerializationSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };

        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ChatId { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }               
                
        public Guid StateGuid { get; set; }

        [NotMapped]
        public object AdditionalData
        {
            get { return AdditionalDataInternal == null ? null : JsonConvert.DeserializeObject<object>(AdditionalDataInternal, AdditionalDataSerializationSettings); }
            set { AdditionalDataInternal = JsonConvert.SerializeObject(value, AdditionalDataSerializationSettings); }
        }

        [Column("AdditionalData")]
        public string AdditionalDataInternal { get; set; }
    }
}