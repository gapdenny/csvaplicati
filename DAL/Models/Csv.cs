using DAL.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Csv : IEntity<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public bool IsMaried { get; set; }
    }
}
