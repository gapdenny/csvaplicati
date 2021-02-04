using CsvHelper;
using CsvHelper.Configuration;
using Services.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Services
{
    public class FileService
    {
       public IEnumerable<string> ParseFile(string Path)
        {
            return File.ReadAllLines(Path);
        }

        public IEnumerable<string> SplitString(string input)
        {
            return input.Split(',');
        }

        public ViewCsv GetCsv(IEnumerable<string> inputString)
        {
            ViewCsv result = new ViewCsv();
            

            result.FullName = inputString.ElementAt(0);
            result.DateOfBirth = DateTime.Parse(inputString.ElementAt(1));
            result.IsMaried = Boolean.Parse(inputString.ElementAt(2));
            result.Phone = inputString.ElementAt(3);
            result.Salary = Decimal.Parse(inputString.ElementAt(4), CultureInfo.InvariantCulture);
                return result;
        }

    }
}
