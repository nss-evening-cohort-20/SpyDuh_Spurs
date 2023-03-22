namespace SpyDuh.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HandlerId { get; set; }
        public Handler Handler { get; set; }
        public Spy Spy { get; set; }
        public int AllotedTime { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }

    }
}
