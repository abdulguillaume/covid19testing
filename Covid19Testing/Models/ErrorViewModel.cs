using System;

namespace Covid19Testing.Models
{
    public class ErrorViewModel
    {
        public int HttpCode { get; set; }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}