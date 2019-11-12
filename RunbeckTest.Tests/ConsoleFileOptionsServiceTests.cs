using RunbeckTest.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace RunbeckTest.Tests
{
    public class ConsoleFileOptionsServiceTests
    {
        [Fact]
        public void ShouldRetrieveOptionsFromTheConsole()
        {
            var expectedPrompts = string.Format("Enter file location: {0}Enter C for CSV of T for TSV (C): {0}Enter number of fields (3): {0}", Environment.NewLine);
            var expectedLocation = "path/to/file.csv";
            var expectedDelimeter = "\t";
            var expectedFieldCount = 6;
            var inputs = string.Format("{1}{0}T{0}{2}{0}", Environment.NewLine, expectedLocation, expectedFieldCount);
            var fileOptionsService = new ConsoleFileOptionsService();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(inputs))
                {
                    Console.SetIn(sr);

                    var result = fileOptionsService.GetFileOptions(null, null, 0);
                    var prompts = sw.ToString();
                    Assert.Equal(expectedPrompts, prompts);
                    Assert.Equal(expectedLocation, result.Location);
                    Assert.Equal(expectedDelimeter, result.Delimiter);
                    Assert.Equal (expectedFieldCount, result.FieldCount);
                }
            }
        }

        [Fact]
        public void ShouldRetrieveMissingOptionsFromTheConsole()
        {
            var expectedPrompts = string.Format("Enter file location: {0}", Environment.NewLine);
            var expectedLocation = "path/to/file.csv";
            var expectedDelimeter = "\t";
            var expectedFieldCount = 6;
            var inputs = string.Format("{1}{0}", Environment.NewLine, expectedLocation);
            var fileOptionsService = new ConsoleFileOptionsService();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(inputs))
                {
                    Console.SetIn(sr);

                    var result = fileOptionsService.GetFileOptions(null, expectedDelimeter, expectedFieldCount);
                    var prompts = sw.ToString();
                    Assert.Equal(expectedPrompts, prompts);
                    Assert.Equal(expectedLocation, result.Location);
                    Assert.Equal(expectedDelimeter, result.Delimiter);
                    Assert.Equal(expectedFieldCount, result.FieldCount);
                }
            }
        }

        [Fact]
        public void ShouldPromptForNothingIfAllOptionsAreProvided()
        {
            var expectedPrompts = "";
            var expectedLocation = "path/to/file.csv";
            var expectedDelimeter = "\t";
            var expectedFieldCount = 6;
            var fileOptionsService = new ConsoleFileOptionsService();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                var result = fileOptionsService.GetFileOptions(expectedLocation, expectedDelimeter, expectedFieldCount);
                var prompts = sw.ToString();
                Assert.Equal(expectedPrompts, prompts);
                Assert.Equal(expectedLocation, result.Location);
                Assert.Equal(expectedDelimeter, result.Delimiter);
                Assert.Equal(expectedFieldCount, result.FieldCount);
            }
        }
    }
}
