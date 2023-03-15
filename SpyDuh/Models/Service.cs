namespace SpyDuh.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public int Cost { get; set; }
        public int SpyId { get; set; }
        public List<Spy> Spies { get; set; }
    }
}
