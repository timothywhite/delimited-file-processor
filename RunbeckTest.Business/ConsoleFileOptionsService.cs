using RunbeckTest.Business.Interfaces;
using RunbeckTest.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RunbeckTest.Business
{
    public class ConsoleFileOptionsService : IFileOptionsService
    {
        public FileOptionsDto GetFileOptions(string location, string delimeter, int fieldCount)
        {
            var result = new FileOptionsDto
            {
                Location = location,
                Delimiter = delimeter,
                FieldCount = fieldCount
            };

            //only get the value if there wasn't one passed in
            if (location == null)
            {
                Console.WriteLine("Enter file location: ");
                result.Location = Console.ReadLine();
            }

            if (delimeter == null)
            {
                Console.WriteLine("Enter C for CSV of T for TSV (C): ");
                var choice = Console.ReadLine();
                if (choice.ToUpper() == "T")
                {
                    result.Delimiter = "\t";
                }
                else
                {
                    result.Delimiter = ",";
                }
            }

            if (fieldCount == default(int))
            {
                Console.WriteLine("Enter number of fields (3): ");

                if (Int32.TryParse(Console.ReadLine(), out int fieldCountInput))
                {
                    result.FieldCount = fieldCountInput;
                }
                else
                {
                    result.FieldCount = 3;
                }
            }

            return result;
        }
    }
}
