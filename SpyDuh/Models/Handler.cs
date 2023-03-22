namespace SpyDuh.Models
{
    public class Handler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Spy> Spies { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}
