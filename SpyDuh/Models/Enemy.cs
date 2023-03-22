namespace SpyDuh.Models
{
    public class Enemy
    {
        public int Id { get; set; }
        public int SpyId { get; set; }
        public Spy Spy { get; set; }
        public int EnemySpyId { get; set; }
        public Spy EnemySpy { get; set; }
    }
}
