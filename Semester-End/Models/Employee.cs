using System;
using System.Collections.Generic;

#nullable disable

namespace Semester_End.Models
{
    public partial class Employee
    {
        public byte Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Dob { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
    }
}
