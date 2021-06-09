using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // Defining an IResult interface with properties to use crud operations to show results (succes, message)
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
