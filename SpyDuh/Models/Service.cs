﻿namespace SpyDuh.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public Spy Spy { get; set; }
        
        public ServiceJoin ServiceJoin { get; set; }
    }
}
