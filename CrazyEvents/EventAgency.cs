using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyEvents
{
    class EventAgency
    {
        public User loggedInUser = null;
        public Database dataBase = new Database();
        public bool running = true;

        public void Start()
        {
            while (running == true)
            {
                int choice = StartMenu();
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Login();
                        break;
                    case 2:
                        Console.Clear();
                        RegisterMenu();
                        break;
                    case 3:
                        Console.WriteLine("Logging out");
                        running = false;
                        break;

                }
            }

        }
        private int StartMenu()
        {
            int choice = 0;
            Console.WriteLine("------------");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.WriteLine("------------");

            Console.Write("> ");
            choice = int.Parse(Console.ReadLine());

            return choice;

        }
        private void RegisterMenu()
        {
            int choice = 0;
            Console.WriteLine("Are you an (1) indiviual or an (2) organisation?");
            Console.Write("> ");

            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    RegisterPrivate();
                    break;
                case 2:
                    RegisterOrg();
                    break;
            }

        }
        private void RegisterOrg()
        {
            User org = new User();

            Console.Write("Username: ");
            org.Username = Console.ReadLine();
            Console.Write("Password: ");
            org.setPassword(Console.ReadLine());
            Console.Write("Email: ");
            org.Email = Console.ReadLine();
            Console.Write("Org number: ");
            string orgNumber = Console.ReadLine();
            org.setSSN(orgNumber);

            if (dataBase.checkOrgNumber(orgNumber) == true)
            {
                Console.WriteLine("Org number alreday registerd");
                RegisterOrg();
            }
            else
            {
                if (dataBase.checkEmail(org.Email) == true)
                {
                    Console.WriteLine("Email alreday registerd");
                    RegisterOrg();
                }
                else
                {
                    if (dataBase.checkUsername(org.Username) == true)
                    {
                        Console.WriteLine("Username alreday registerd");
                        RegisterOrg();
                    }
                    else
                    {
                        org.role.Id = 1;
                        dataBase.addUser(org);
                        loggedInUser = org;
                        ShowAdminMenu();
                    }
                }
            }

        }
        private void RegisterPrivate()
        {
            User user = new User();

            Console.Write("Name: ");
            user.Name = Console.ReadLine();
            Console.Write("Username: ");
            user.Username = Console.ReadLine();
            Console.Write("Password: ");
            user.setPassword(Console.ReadLine());
            Console.Write("Email: ");
            user.setEmail(Console.ReadLine());


        }
        private void Login()
        {
            Console.WriteLine("-----Login-----");
            Console.Write("Username: ");
            string inputUsername = Console.ReadLine();
            Console.Write("Password: ");
            string inputPassword = Console.ReadLine();

            User temp =  dataBase.GetUserByUsername(inputUsername);
            if(temp.checkPassword(inputPassword) == true)
            {
                Console.WriteLine("Password Correct");
                loggedInUser = temp;
                if(loggedInUser.role.Id == 1)
                {
                    ShowAdminMenu();
                }
                else
                {
                    ShowVisitorMenu();
                }
            }
            else
            {
                Console.WriteLine("Wrong Password");
            }
            
        }
        private void ShowLoginError()
        {

        }
        private void ShowAdminMenu()
        {
            while (loggedInUser != null)
            {
                Console.WriteLine($"Logged in as: {loggedInUser.Username}");
                Console.WriteLine("----Admin-Menu----");
                Console.WriteLine("1. Show all events");
                Console.WriteLine("2. Handle events");
                Console.WriteLine("3. Handle users");
                Console.WriteLine("4. Log out");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowEvents();
                        break;
                    case "2":
                        HandleEvents();
                        break;
                    case "3":
                        HandleUsers();
                        break;
                    case "4":
                        loggedInUser = null;
                        break;
                }
            }
        }
        private void ShowVisitorMenu()
        {
            while (loggedInUser != null)
            {
                Console.WriteLine($"Logged in as: {loggedInUser.Username}");
                Console.WriteLine("----Visitor-Menu----");
                Console.WriteLine("1. Show all events");
                Console.WriteLine("2. Show Tickets");
                Console.WriteLine("3. Show chat");
                Console.WriteLine("4. Logout");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowEvents();
                        break;
                    case "2":
                        ShowTickets();
                        break;
                    case "3":
                        ShowChat();
                        break;
                    case "4":
                        loggedInUser = null;
                        break;
                }

            }
        }
        private void ShowEvents()
        {
       
            Console.Clear();
            Console.WriteLine("---Here are all the upcoming Events---");
            List<Event> events = dataBase.GetAllEvents(); //Gets all the events from the database and stores them in to a list called events.
            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine($"\nEvent {i + 1}");
                Console.Write($"Name: {events[i].Name}");
                Console.Write($"\nDescription: {events[i].Description}");
                Console.Write($"\nDate: {events[i].Date}");
                Console.Write($"\nPrice: {events[i].EventPrice}\n");
            }

            Console.WriteLine("\nDo you want to buy a ticket to one of these events? y/n");
            while (true)
            {
                string answer = Console.ReadLine();
                if (answer.ToLower() == "y")
                {
                        Console.WriteLine("Enter the number of the event:");
                        string input = Console.ReadLine();
                        int eventNumber = int.Parse(input);

                    Ticket ticket = new Ticket();
                    ticket.Price = 0; // måste vi senare hitta en lösning för - for now gave it a default, get rid of this property in database and class for example
                    ticket.UserID = loggedInUser.Id;
                    ticket.EventID = events[eventNumber - 1].ID;

                    dataBase.addTicket(ticket);
                }
                else
                {
                    break;
                }
                Console.WriteLine("Do you want to purchase another one? y/n");
            }
            Console.WriteLine("\n\nPress enter to return to the Visitor menu");
            Console.ReadLine();
            Console.Clear();

        }
        private void ShowTickets()
        {
            Console.Clear();
            // I wrote a method i Database first to get 1 event by userid , but somehow i could not get it it to work, so I just 
            // called the getallevents method and than looped through it to get the name and date of the event we want to display
            List<Event> allEvents = dataBase.GetAllEvents();
            Console.WriteLine("Here are the tickets you have booked: ");
            
            string userIDstring = loggedInUser.Id.ToString();
            List<Ticket> tickets = dataBase.GetTicketsByUserID(userIDstring);
            for (int i = 0; i < tickets.Count; i++)
            {
                Console.WriteLine($"Ticket {i+1}: ");
                for (int y = 0; y < allEvents.Count; y++)
                {
                    if(allEvents[y].ID == tickets[i].EventID)
                    {
                        Console.Write($"Event name: {allEvents[y].Name}\n");
                        Console.Write($"Event date: {allEvents[y].Date}\n");
                    }
                }
             }

            Console.WriteLine("Press enter to return to the previous menu");
            Console.ReadLine();
            Console.Clear();
        }

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

        private void HandleEvents()
        {
            Console.WriteLine("---Handle-Events");
        }
        private void HandleUsers()
        {
            Console.WriteLine("---Handle-Users---");

        }
    }
}
