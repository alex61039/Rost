namespace Rost.WebApi.Models.Institution
{
    /// <summary>
    /// Модель деталей учреждения
    /// </summary>
    public class InstitutionDetailsModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Ижентификатор структуры
        /// </summary>
        public int StructureId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Район
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Район
        /// </summary>
        public int DistrictId { get; set; }
        
        /// <summary>
        /// Муниципальное образование
        /// </summary>
        public string MunicipalUnion { get; set; }

        /// <summary>
        /// Муниципальное образование
        /// </summary>
        public int MunicipalUnionId { get; set; }
        
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Образование
        /// </summary>
        public string Education { get; set; }
        
        /// <summary>
        /// Образование
        /// </summary>
        public string EducationId { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Администратор
        /// </summary>
        public string AdminUser { get; set; }
        
        /// <summary>
        /// Фото
        /// </summary>
        public string MainPhoto { get; set; }
    }
}