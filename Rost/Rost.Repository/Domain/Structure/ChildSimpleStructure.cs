namespace Rost.Repository.Domain
{
    public class ChildSimpleStructure
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Флаг, указывает, есть ли у данной структуры подчиненные/дочерние структуры.
        /// </summary>
        public bool HasChildren { get; set; }
    }
}