using CsvHelper;
using Moq;
using RunbeckTest.Business;
using RunbeckTest.Business.Interfaces;
using RunbeckTest.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using Xunit;

namespace RunbeckTest.Tests
{
    public class DelimitedFileServiceTests
    {
        [Fact]
        public void ShouldProcessExampleInput()
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
            var testFile = string.Format("First Name,Middle Name,Last Name{0}Jane,Taylor,Doe{0}Chris,Lee{0}Jose,,Morro", Environment.NewLine);
            byte[] bytes = Encoding.UTF8.GetBytes(testFile);
            var testStream = new StreamReader(new MemoryStream(bytes));

            var fileOptionsServiceMock = new Mock<IFileOptionsService>();
            fileOptionsServiceMock.Setup(mock => mock.GetFileOptions(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(new FileOptionsDto
            {
                Location = "test",
                Delimiter = ",",
                FieldCount = 3
            });

            var fileMock = new Mock<IFile>();
            fileMock.Setup(mock => mock.OpenText(It.IsAny<string>())).Returns(testStream);

            var fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(mock => mock.File).Returns(fileMock.Object);

            var service = new DelimitedFileService(fileOptionsServiceMock.Object, fileSystemMock.Object, new Factory());
            var result = service.ProcessDelimitedFile("test", ",", 3);

            Assert.Equal(expectedValidRows, result.ValidRows);
            Assert.Equal(expectedInvalidRows, result.InvalidRows);
        }
    }
}
