using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Utils
{
    public class ActionResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string JSONData { get; set; }
    }
}
