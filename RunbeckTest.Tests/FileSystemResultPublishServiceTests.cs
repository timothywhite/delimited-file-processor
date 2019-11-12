using RunbeckTest.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.IO.Abstractions;
using RunbeckTest.Business;

namespace RunbeckTest.Tests
{
    public class FileSystemResultPublishServiceTests
    {
        [Fact]
        public void ShouldPublishResults()
        {
            var result = new ResultDto()
            {
                ValidRows = new List<string>()
                {
                    "Jane,Taylor,Doe",
                    "Jose,,Morro"
                },
                InvalidRows = new List<string>()
                {
                    "Chris,Lee"
                }
            };
            var resultLocation = "resultLocation";
            var expectedResultText = string.Join(Environment.NewLine, result.ValidRows);
            var errorLocation = "errorLocation";
            var expectedErrorText = string.Join(Environment.NewLine, result.InvalidRows);

            var fileMock = new Mock<IFile>();
            var fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(mock => mock.File).Returns(fileMock.Object);

            var service = new FileSystemResultPublishService(fileSystemMock.Object, resultLocation, errorLocation);
            service.PublishResult(result);

            fileMock.Verify(mock => mock.WriteAllText(resultLocation, expectedResultText));
            fileMock.Verify(mock => mock.WriteAllText(errorLocation, expectedErrorText));
        }

        [Fact]
        public void ShouldSkipPublishingEmptyResults()
        {
            var result = new ResultDto()
            {
                ValidRows = new List<string>()
                {
                    "Jane,Taylor,Doe",
                    "Jose,,Morro"
                },
                InvalidRows = new List<string>()
            };
            var resultLocation = "resultLocation";
            var expectedResultText = string.Join(Environment.NewLine, result.ValidRows);
            var errorLocation = "errorLocation";

            var fileMock = new Mock<IFile>();
            var fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(mock => mock.File).Returns(fileMock.Object);

            var service = new FileSystemResultPublishService(fileSystemMock.Object, resultLocation, errorLocation);
            service.PublishResult(result);

            fileMock.Verify(mock => mock.WriteAllText(resultLocation, expectedResultText));
            fileMock.Verify(mock => mock.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}
