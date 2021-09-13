using Microsoft.AspNetCore.Http;

namespace Rost.WebApi.Models
{
    /// <summary>
    /// Модель загрузки изображения
    /// </summary>
    public class ImageModel
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// Изображение
        /// </summary>
        public IFormFile Image { get; set; }
    }
}