using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Entities
{
    public class ResponseEntity
    {
        public string? ContentType { set; get; }
        public int StatusCode { set; get; }
        public string? message { get; set; }
        public object? data { get; set; }

    }
}
