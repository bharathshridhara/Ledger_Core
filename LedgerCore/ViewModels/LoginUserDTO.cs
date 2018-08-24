﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LedgerCore.ViewModels
{
    public class LoginUserDTO : IValidatableObject
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var loginUser = validationContext.ObjectInstance as LoginUserDTO;
            if(loginUser != null && (string.IsNullOrEmpty(loginUser.Username) || string.IsNullOrEmpty(loginUser.Password)))
                return new List<ValidationResult>
                {
                    new ValidationResult(
                    
                        "Username or password cannot be empty.",
                        new List<string>{"Username", "Password"}
                    )
                };
            return new List<ValidationResult>();
        }
    }
}
