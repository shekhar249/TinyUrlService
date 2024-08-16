using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adroit.Services.TinyUrl.Validation.Model
{
    public class ValidationResult
    {                    
        public string? Message { get; set; }
        public ValidationResultType Type { get; set; }
    }
}
