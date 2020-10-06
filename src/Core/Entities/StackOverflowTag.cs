namespace Core.Entities
{
    public class StackOverflowTag : BaseEntity
    {
        public string Name { get; set; }
        public int Popular { get; set; }
        public string Description { get; set; }
    }
}
