using CsvHelper;
using RunbeckTest.Business.Interfaces;
using RunbeckTest.Dto;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace RunbeckTest.Business
{
    public class DelimitedFileService : IDelimitedFileService
    {
        private readonly IFileOptionsService FileOptionsService;
        private readonly IFileSystem FileSystem;
        private readonly IFactory CSVReaderFactory;
        public DelimitedFileService(IFileOptionsService fileOptionsService, IFileSystem fileSystem, IFactory factory)
        {
            FileOptionsService = fileOptionsService;
            FileSystem = fileSystem;
            CSVReaderFactory = factory;
        }

        public ResultDto ProcessDelimitedFile(string location = null, string delimiter = null, int fieldCount = default(int))
        {
            var validRows = new List<string>();
            var invalidRows = new List<string>();

            //get the options from the options service passing in the provided parameters 
            var options = FileOptionsService.GetFileOptions(location, delimiter, fieldCount);
            
            using (var file = FileSystem.File.OpenText(options.Location))
            using (var reader = CSVReaderFactory.CreateReader(file))
            {
                reader.Configuration.Delimiter = options.Delimiter;
                //if it expects a header row, it will truncate any rows with
                //more values than the header. we'll just toss the header manually.
                reader.Configuration.HasHeaderRecord = false;

                //get all records except the first one and cast them to a dictionary
                var records = reader.GetRecords<dynamic>().Skip(1).Select(r => r as IDictionary<string, object>);
                
                foreach (var record in records)
                {
                    var rowString = string.Join(options.Delimiter, record.Values.Where(v => v != null));
                    //if we have the right number of fields and none of them is null, it is a valid row
                    if (record.Count == options.FieldCount && record.Values.All(v => v != null))
                    {
                        //this is a valid row
                        validRows.Add(rowString);
                    }
                    else
                    {
                        //this is an invalid row
                        invalidRows.Add(rowString);
                    }
                } 
            }

            return new ResultDto
            {
                ValidRows = validRows,
                InvalidRows = invalidRows
            };
        }
    }
}
