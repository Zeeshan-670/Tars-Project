using System;
using System.Collections.Generic;

#nullable disable

namespace Semester_End.Models
{
    public partial class Admin
    {
        public byte Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
