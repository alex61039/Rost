using Rost.WebApi.Models.Account;
using Rost.WebApi.Models.City;
using Rost.WebApi.Models.District;
using Rost.WebApi.Models.Education;
using Rost.WebApi.Models.MunicipalUnion;

namespace Rost.WebApi.Models.Structure
{
    public class InstitutionAdminModel
    {
        public int StructureId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CityModel City { get; set; }

        public DistrictModel District { get; set; }

        public MunicipalUnionModel MunicipalUnion { get; set; }

        public string Address { get; set; }

        public EducationModel Education { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public ApplicationUserModel AdminUser { get; set; }
        
        // TODO: Сотрудники
        // TODO: Сообщества
        // TODO: События
    }
}