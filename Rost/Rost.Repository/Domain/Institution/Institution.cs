using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rost.Repository.Domain
{
    /// <summary>
    /// Модель представляет сущность "Учреждения" в БД.
    /// </summary>
    public class Institution : BaseEntity
    {
        public int StructureId { get; set; }

        public Structure Structure { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public int DistrictId { get; set; }

        public District District { get; set; }

        public int MunicipalUnionId { get; set; }

        public MunicipalUnion MunicipalUnion { get; set; }

        [StringLength(4000)]
        public string Address { get; set; }

        public int? EducationId { get; set; }
        public Education Education { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public virtual ICollection<ApplicationUser> Employees { get; set; } = new List<ApplicationUser>();
    }
}