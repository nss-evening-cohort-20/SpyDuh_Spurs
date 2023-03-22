namespace SpyDuh.Models
{
    public class ServiceJoin
    {
        public int Id { get; set; }
        public int serviceId { get; set; }
        public int cost { get; set; }
        public int spyId { get; set; }
        //public Spy Spy { get; set; }
        //public List<Service> Service { get; set; }
    }
}
