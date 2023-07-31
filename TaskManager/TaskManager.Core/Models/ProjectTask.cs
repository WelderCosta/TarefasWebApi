﻿namespace TaskManager.Core.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
    }
}