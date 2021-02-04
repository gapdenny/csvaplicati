using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dto
{
    public class CsvDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
