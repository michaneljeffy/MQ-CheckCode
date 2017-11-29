using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace ConsoleApplication2.MQ
{
    public class MSMQ
    {
        public void Connect()
        {
            MessageQueue queue = new MessageQueue();
            queue.QueueName = "123";
            queue.Send("Hello World!");
        }
    }
}
