using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Core.Utilities.Results.Concrete.ErrorResult
{
    public class ErrorResult : Result
    {
        private string message;
        private HttpStatusCode notFound;
        private HttpStatusCode badRequest;

        public ErrorResult(string message, bool success, HttpStatusCode statusCode) : base(message, success, statusCode)
        {
        }

        public ErrorResult(HttpStatusCode statusCode) : base(false, statusCode)
        {
        }
        public ErrorResult(string message, HttpStatusCode statusCode) : base(message, false, statusCode)
        {
        }
    }
}
