using System;
using System.Collections.Generic;

namespace WebCoreProject.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool? UserState { get; set; }
    }
}
