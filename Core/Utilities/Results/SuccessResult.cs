using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // If operations is succeeded, SuccessResult will send true and message to base (Result)
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {

        }

        public SuccessResult() : base(true)
        {

        }
    }
}
