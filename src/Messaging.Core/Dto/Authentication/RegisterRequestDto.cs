﻿using System;
namespace Messaging.Core.Dto.Authentication
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
