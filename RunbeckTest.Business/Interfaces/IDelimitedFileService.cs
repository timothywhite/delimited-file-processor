using RunbeckTest.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RunbeckTest.Business.Interfaces
{
    public interface IDelimitedFileService
    {
        ResultDto ProcessDelimitedFile(string location = null, string delimiter = null, int fieldCount = default(int));
    }
}
