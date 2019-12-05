using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyEvents
{
    class Chat
    {
        /*
        public void ShowChat()
        {
            string message;
            string currentUser = loggedInUser.Name;
            DateTime time;

            //Load all messages from database into chat
            GetAllMessages();

            //While user is in the chat
            while (true)
            {
                Console.WriteLine("Write 'exit' to exit chat");
                Console.WriteLine("Write 'reload' to load new messages");
                Console.Write("Message: ");
                message = Console.ReadLine();
                if (message.ToLower() == "exit")
                {
                    Console.Clear();
                    break;
                }
                else if (message.ToLower() == "reload")
                {
                    GetAllMessages();
                }
                else
                {
                    time = DateTime.Now;
                    AddMessage(time, currentUser, message);
                    GetAllMessages();
                }
            }

        }

        private void AddMessage(DateTime time, string currentUser, string message)
        {
            dataBase.AddMessage(time, currentUser, message);
        }

        //Load all messages/chat 
        private void GetAllMessages()
        {
            Console.Clear();

            //Create list with all messages
            List<string> messages = dataBase.GetAllMessages();

            //Write every message in console
            foreach (string x in messages)
            {
                Console.WriteLine(x);
            }
        }
        */
    }
}
