using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using MessagerLibrary;

namespace MessagerLibrary
{
    public class MessageStack
    {

        private Stack<string> messageStack;
        //private List<string> messages;

        public MessageStack()
        {
            messageStack = new Stack<string>();
        }

        public void PushMessage(string message)
        {
            //
            messageStack.Push(message);
        }

        public string PopMessage()
        { 
            //
            return messageStack.Pop();
        }

        public int Count
        {
            get { return messageStack.Count; }
        }
    
}
}
