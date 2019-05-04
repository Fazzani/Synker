using System;
using System.Collections.Generic;
using System.Text;

namespace Synker.Application.Notifications
{
    public class Message
    {
        public string Content { get; set; }
        public string Origin { get; set; }

        public override string ToString() => $"{Origin}: {Content}";
    }
}
