using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amino.Events
{

    public class messageEvent : EventArgs
    {
        public Objects.Message message { get; private set; }
        public messageEvent(Objects.Message _message)
        {
            message = _message;
        }
    }
}
