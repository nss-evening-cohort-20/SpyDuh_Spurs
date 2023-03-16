namespace SpyDuh.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
        public int SkillLevel { get; set; }
        public int SpyId { get; set; }
        public Spy spy { get; set; }
        //public List<Spy> Spies { get; set; }

    }
}
