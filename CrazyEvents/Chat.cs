using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyEvents
{
    class Chat
    {
        readonly Database dataBase = new Database();
        public void ShowChat(string name, int eventID)
        {
            var currentUser = name;

            //Load all messages from database into chat
            GetAllMessages(eventID);

            //While user is in the chat
            while (true)
            {
                Console.WriteLine("Write 'exit' to exit chat");
                Console.WriteLine("Write 'reload' to load new messages");
                Console.Write("Message: ");
                var message = Console.ReadLine();
                if (message != null && message.ToLower() == "exit")
                {
                    Console.Clear();
                    break;
                }
                else if (message != null && message.ToLower() == "reload")
                {
                    GetAllMessages(eventID);
                }
                else
                {
                    var time = DateTime.Now;
                    AddMessage(time, currentUser, message, eventID);
                    GetAllMessages(eventID);
                }
            }

        }

        private void AddMessage(DateTime time, string currentUser, string message, int eventID)
        {
            dataBase.AddMessage(time, currentUser, message, eventID);
        }

        //Load all messages/chat 
        private void GetAllMessages(int eventID)
        {
            Console.Clear();

            //Create list with all messages
            List<string> messages = dataBase.GetAllMessages(eventID);

            //Write every message in console
            Console.WriteLine("---------------------------");
            foreach (var x in messages)
            {
                var message = x;
                var messageToSend = ""; 
                var messageSplited = message.Split(" ");
                messageToSend += "| ";
                for (var y = 0; y < messageSplited.Length; y++)
                {
                    messageToSend += messageSplited[y];
                    messageToSend += " ";
                    if (y == 5)
                        messageToSend += " |\n|";
                }
                if(messageToSend[messageToSend.Length - 1].Equals("|") == true)
                {

                }
                else
                {
                    messageToSend += "|";
                }
                Console.WriteLine($"{messageToSend}");
            }
            Console.WriteLine("---------------------------");
        }
        
    }
}
