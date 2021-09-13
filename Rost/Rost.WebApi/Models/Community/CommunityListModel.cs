namespace Rost.WebApi.Models.Community
{
    /// <summary>
    /// Модель списка сообществ
    /// </summary>
    public class CommunityListModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// профориентация
        /// </summary>
        public string Career { get; set; }
        
        /// <summary>
        /// Cсылка на фото
        /// </summary>
        public string Image { get; set; }
    }
}