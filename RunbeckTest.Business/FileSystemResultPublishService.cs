using RunbeckTest.Business.Interfaces;
using RunbeckTest.Dto;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace RunbeckTest.Business
{
    public class FileSystemResultPublishService : IResultPublishService
    {
        private readonly IFileSystem FileSystem;
        private readonly string ResultLocation;
        private readonly string ErrorLocation;

        public FileSystemResultPublishService(IFileSystem fileSystem, string resultLocation, string errorLocation)
        {
            FileSystem = fileSystem;
            ResultLocation = resultLocation;
            ErrorLocation = errorLocation;
        }

        public void PublishResult(ResultDto result)
        {
            var resultText = string.Join(Environment.NewLine, result.ValidRows);
            var errorText = string.Join(Environment.NewLine, result.InvalidRows);

            //only write the file if there are results to go in it.
            if (result.ValidRows.Count() != 0)
            {
                FileSystem.File.WriteAllText(ResultLocation, resultText);
            }

            if (result.InvalidRows.Count() != 0)
            {
                FileSystem.File.WriteAllText(ErrorLocation, errorText);
            }
        }
    }
}
