using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Rost.Common;
using Rost.Common.Enums;

namespace Rost.Repository.Domain
{
    public class Child : BaseEntity
    {
        public int PersonalNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDay { get; set; }
        public Sex Sex { get; set; }
        public ICollection<Career> Careers { get; set; } = new List<Career>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        
        [NotMapped]
        public string PersonalNumberDisplay
        {
            get
            {
                var numberLength = PersonalNumber.ToString().Length;
                var result = "";
                for (var i = 0; i < Constants.PersonalNumberLength - numberLength; i++)
                {
                    result += "0";
                }

                return $"{result}{PersonalNumber}";
            }
        }

        [NotMapped]
        public string Age
        {
            get
            {
                var ageInDays = (DateTime.Today - BirthDay).Days;
                if (ageInDays >= 365)
                {
                    return $"{ageInDays / 365} лет";
                }
                
                if (ageInDays > 30)
                {
                    return $"{ageInDays / 12} месяцев";
                }

                return $"{ageInDays} дней";
            }
        }
    }
}