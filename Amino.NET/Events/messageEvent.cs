using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amino.Events
{

    public delegate void messageEventHandler(object source, Objects.Message message);

    public class messageEvent : EventArgs
    {
        private Amino.Objects.Message msg;
        public messageEvent(Objects.Message message)
        {
            msg = message;
        }
        public Amino.Objects.Message message()
        {
            return msg;
        }
    }
}
