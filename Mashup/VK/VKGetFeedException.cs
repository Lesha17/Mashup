using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup.VK
{
    class VKGetFeedException : GetFeedExceptopn
    {
        public VKGetFeedException(string message) : base(message) { }
        public VKGetFeedException(string message, VKError error) : this(message)
        {
            this.Error = error;
        }
        public VKGetFeedException(string message, Exception inner_exception) : base(message, inner_exception) { }

        public VKError Error { get; }
    }
}
