namespace Rost.Repository.Domain
{
    public class ParentSimpleStructure
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Флаг, указывает, является ли эта структура структурой текущего уровня иерархии.
        /// </summary>
        public bool IsCurrentLevel { get; set; }
    }
}