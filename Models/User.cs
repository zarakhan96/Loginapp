﻿using System.ComponentModel.DataAnnotations;

namespace Loginapp.Models
{
    public class User
    {
        public int Id { get; set; }


        [Required] 
        public string Username { get; set; }

        [Required] 
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

            [Required]
            public string ConfirmPassword { get; set; }

    }
}
