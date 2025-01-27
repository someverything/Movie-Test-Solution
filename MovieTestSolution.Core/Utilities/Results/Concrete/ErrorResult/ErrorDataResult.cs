﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Core.Utilities.Results.Concrete.ErrorResult
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message, bool success, HttpStatusCode statusCode) : base(data, message, success, statusCode)
        {
        }

        public ErrorDataResult(T data, HttpStatusCode statusCode) : base(data, false, statusCode)
        {
        }

        public ErrorDataResult(string message, HttpStatusCode statusCode) : base(default, message, false, statusCode)
        {
        }

        public ErrorDataResult(HttpStatusCode statusCode) : base(default, false, statusCode)
        {
        }
    }
}
