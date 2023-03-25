namespace SpyDuh.Models
{
    public class Agency
    {
        int Id { get; set; }
        public string AgencyName { get; set; }
        public List<HandlerSpy> Spies { get; set; }
    }
}
