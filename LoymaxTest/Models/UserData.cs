using Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoymaxTest.Models
{
    public class UserData
    {
        public UserData()
        {
        }

        public UserData(int userId, string name = "", string midName = "", string lastName = "", DateTime? birthDate = null) : this()
        {
            UserId = userId;
            Name = name;
            MidName = midName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required(AllowEmptyStrings=false, ErrorMessageResourceName="UserDataValidationNameRequired", ErrorMessageResourceType=typeof(LoymaxTestBotResources))]
        public string Name { get; set; }

        public string MidName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "UserDataValidationLastNameRequired", ErrorMessageResourceType = typeof(LoymaxTestBotResources))]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}