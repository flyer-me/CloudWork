﻿namespace App.Core.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<TestCase>? TestCases { get; set; }
        public virtual ICollection<Submission>? Submissions { get; set; }
    }
}