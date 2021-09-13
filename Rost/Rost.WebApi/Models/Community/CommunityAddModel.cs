using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Rost.Common.Enums;
using Rost.Repository.Domain;

namespace Rost.WebApi.Models.Community
{
    public class CommunityAddModel
    {
        public string Name { get; set; }
        
        public int? CommunityTypeId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? MunicipalUnionId { get; set; }
        public int? EducationId { get; set; }
        public int? CareerId { get; set; }
        
        public string Image { get; set; }
        
        public bool IsActive { get; set; }

        public AccessType AccessType { get; set; }
        
        public string UserId { get; set; }
    }
}