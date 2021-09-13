namespace Rost.WebApi.Models.Institution
{
    /// <summary>
    /// Модель создания и редактирования учреждения
    /// </summary>
    public class InstitutionEditModel
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
        public int CityId { get; set; }

        /// <summary>
        /// Район
        /// </summary>
        public int DistrictId { get; set; }

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
        public int? EducationId { get; set; }

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
        public string AdminUserId { get; set; }
    }
}