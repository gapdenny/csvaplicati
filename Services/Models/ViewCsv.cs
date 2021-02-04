using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Models
{
    public class ViewCsv
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Full name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name ="Birth date")]
        public DateTime DateOfBirth { get; set; }
        public bool IsMaried { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [Range(typeof(decimal), "5,0", "10000,0", ErrorMessage = "min/max - 5,0/10000,0")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Salary { get; set; }
        


        public DAL.Models.Csv CreateDataModel()
        {
            DAL.Models.Csv result = new DAL.Models.Csv();
            var names = this.FullName.Split(' ');

            result.FirstName = names[0];
            result.LastName = names[1];
            result.Phone = this.Phone;
            result.IsMaried = this.IsMaried;
            result.Salary = this.Salary;
            return result;
        }
    }
}
