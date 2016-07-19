using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup
{
    class GetFeedExceptopn : Exception
    {
        public GetFeedExceptopn(string message) : base(message) { }
        public GetFeedExceptopn(string message, Exception inner_exception) : base(message, inner_exception) { }
    }
}
