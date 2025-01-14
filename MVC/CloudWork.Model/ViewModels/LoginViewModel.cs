﻿using System.ComponentModel.DataAnnotations;

namespace CloudWork.Model.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
