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
        public bool IsMemeber { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Skill>? Skills { get; set; } = null;
        public List<Service>? Services { get; set; } = null;
        public List<EnemySpy> Enemies { get; set; }
    }

    public class NewSpy
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public int HandlerId { get; set; }

    }

    public class EnemySpy
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsMemeber { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Skill>? Skills { get; set; } = null;
        public List<Service>? Services { get; set; } = null;
    }

    public class HandlerSpy
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Skill>? Skills { get; set; } = null;
        public List<Service>? Services { get; set; } = null;
        public List<AssignmentShort> Assignments { get; set; }
    }

}