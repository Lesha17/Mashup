using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup.Twitter
{
    class TwitterGetFeedException : GetFeedExceptopn
    {
        public TwitterGetFeedException(string message) : base(message) { }
        public TwitterGetFeedException(string message, Exception inner_exception) : base(message, inner_exception) { }
    }
}
