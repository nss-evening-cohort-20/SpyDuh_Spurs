namespace SpyDuh.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public int Cost { get; set; }

        public Spy Spy { get; set; }
    }

    public class ServiceWithoutCost
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
    }
}
