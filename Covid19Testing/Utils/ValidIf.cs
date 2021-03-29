using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Utils
{
    public class ValidIf  : ValidationAttribute
    {
        private readonly string condition;

        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }

        public ValidIf(string condition)
        {
            this.condition = condition;
        }


    }
}
