﻿namespace SpyDuh.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int HandlerId { get; set; }
        public AssignmentHandler Handler { get; set; }
        public AssigmentSpy Spy { get; set; }
        public int AllotedTime { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime endDate { get; set; }

        public int daysRemaining { get; set; }
        public string Status { get; set; }

    }
    //is it bad practice to do this instead of adding multiple files just to separate from Spy class?
    public class AssigmentSpy
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AssignmentHandler
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AssignmentShort 
    {
        private string _completed = "completed";
        private string _ongoing = "ongoing";
        private string _failed = "failed";
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsCompleted { get; set; }

        public int DaysRemaining { get; set; }
        public string Status { get 
            {
                if (IsCompleted)
                {
                    return _completed;
                } else if 
                    (DaysRemaining > 0)
                {
                    return _ongoing;
                } else
                {
                    return _failed;
                }
                
            } }
    }

}
