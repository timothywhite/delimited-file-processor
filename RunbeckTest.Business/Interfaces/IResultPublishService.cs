using RunbeckTest.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RunbeckTest.Business.Interfaces
{
    public interface IResultPublishService
    {
        void PublishResult(ResultDto result);
    }
}
