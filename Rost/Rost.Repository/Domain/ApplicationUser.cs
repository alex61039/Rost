using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Rost.Common;
using Rost.Common.Enums;

namespace Rost.Repository.Domain
{
    public class ApplicationUser : IdentityUser 
    {
        public int PersonalNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? MunicipalUnionId { get; set; }
        public bool IsFirstLogin { get; set; }
        public bool IsDisplayHint { get; set; }
        
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
        
        [ForeignKey("MunicipalUnionId")]
        public virtual MunicipalUnion MunicipalUnion { get; set; }
        
        public EmployeeRole? EmployeeRole { get; set; }
        public int? InstitutionId { get; set; }
        
        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        
        public virtual ICollection<UserPhoto> Photos { get; set; } = new List<UserPhoto>();
        public virtual ICollection<Child> Children { get; set; } = new List<Child>();
        public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

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
    }
}