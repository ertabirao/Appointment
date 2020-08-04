using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oa.models
{
    public class ActionResponseModel
    {

        public bool Success { get; set; }

        public List<string> Error { get; set; }

        public object Result { get; set; }
    }
}
