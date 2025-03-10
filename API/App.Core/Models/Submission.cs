﻿namespace App.Core.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public ProgramLanguage Language { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Question Question { get; set; } = null!;
        public virtual SubmissionResult? Evaluation { get; set; }
    }

    public enum ProgramLanguage
    {
        CSharp,
        // 其他支持的语言
    }
}