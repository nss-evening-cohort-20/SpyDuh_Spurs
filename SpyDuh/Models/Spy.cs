using System.ComponentModel.DataAnnotations;

namespace SpyDuh.Models
{
    public class Spy
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsMember { get; set; }
        public DateTime DateCreated { get; set; }
        //public List<Skill> Skills { get; set; }
        //public List<Service> Services { get; set; }
    }
}
