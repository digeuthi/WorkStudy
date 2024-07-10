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

        //private Stack<string> messageStack;
        private List<string> messageList;

        public MessageStack()
        {

            /*messageStack = new Stack<string>();*/
            messageList = new List<string>();

        }

        public void PushMessage(string message)
        {
            // messageStack.Push(message);
            messageList.Add(message);
        }

        public string PopMessage()
        {
            //return messageList.Pop();

            if (messageList.Count == 0) {
                throw new InvalidOperationException("스택이 비어 있습니다.");
            }

            int lastInedx = messageList.Count - 1;
            string message = messageList[lastInedx]; //마지막 인덱스의 값을 message에 반환
            messageList.RemoveAt(lastInedx); //List에서 마지막 인덱스의 값을 삭제
            return message;
        }

        public int Count
        {
            get { return messageList.Count; }
        }
    
}
}
