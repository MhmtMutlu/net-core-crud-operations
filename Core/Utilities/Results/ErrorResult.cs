using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // If operations is not succeeded, SuccessResult will send false and message to base (Result)
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {

        }

        public ErrorResult() : base(false)
        {

        }
    }
}
