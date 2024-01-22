using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Infrastructure
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            IsSuccess = true;
            StatusCode = 200;
            Message = "Ok";
            InnerMessage = null;
            Path = null;
            Payload = null;
            Method = null;
        }

        /// <summary>
        /// Default value is true.
        /// </summary>
        /// <value></value>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Default value is 200OK.
        /// </summary>
        /// <value></value>
        public int StatusCode { get; set; }

        /// <summary>
        /// Default value is "Ok".
        /// </summary>
        /// <value></value>
        public string Message { get; set; }

        public string? InnerMessage { get; set; }

        public string? Path { get; set; }

        public string? Method { get; set; }

        public object? Payload { get; set; }
    }
}
