using RunbeckTest.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RunbeckTest.Business.Interfaces
{
    public interface IFileOptionsService
    {
        FileOptionsDto GetFileOptions(string location, string delimeter, int fieldCount);
    }
}
