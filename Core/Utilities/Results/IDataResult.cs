using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // IDataResult is implemented from IResult and it takes T as an entity
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
