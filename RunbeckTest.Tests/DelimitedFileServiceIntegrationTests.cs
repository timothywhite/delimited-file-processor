using CsvHelper;
using RunbeckTest.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using Xunit;

namespace RunbeckTest.Tests
{
    public class DelimitedFileServiceIntegrationTests
    {

        [Fact]
        public void GivenExample()
        {
            var expectedValidRows = new List<string>()
            {
                "Jane,Taylor,Doe",
                "Jose,,Morro"
            };
            var expectedInvalidRows = new List<string>()
            {
                "Chris,Lee"
            };
            var service = new DelimitedFileService(new ConsoleFileOptionsService(), new FileSystem(), new Factory());
            var result = service.ProcessDelimitedFile(AppContext.BaseDirectory + "TestFiles\\example.csv", ",", 3);

            Assert.Equal(expectedValidRows, result.ValidRows);
            Assert.Equal(expectedInvalidRows, result.InvalidRows);
        }

        [Fact]
        public void GivenExampleTabbed()
        {
            var expectedValidRows = new List<string>()
            {
                "Jane\tTaylor\tDoe",
                "Jose\t\tMorro"
            };
            var expectedInvalidRows = new List<string>()
            {
                "Chris\tLee"
            };
            var service = new DelimitedFileService(new ConsoleFileOptionsService(), new FileSystem(), new Factory());
            var result = service.ProcessDelimitedFile(AppContext.BaseDirectory + "TestFiles\\example.tsv", "\t", 3);

            Assert.Equal(expectedValidRows, result.ValidRows);
            Assert.Equal(expectedInvalidRows, result.InvalidRows);
        }

        [Fact]
        public void ErrorOnFileNotFound()
        {
            var service = new DelimitedFileService(new ConsoleFileOptionsService(), new FileSystem(), new Factory());
            Assert.Throws<FileNotFoundException>(() => service.ProcessDelimitedFile(AppContext.BaseDirectory + "TestFiles\\notthere.csv", ",", 3));
        }
    }
}
