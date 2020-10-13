namespace Core.Entities
{
    public class SkillTag : BaseEntity
    {
        public string Name { get; set; }
        public int Popular { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
