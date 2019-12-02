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
                Console.WriteLine("3. Logout");
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
                Console.Write($"\nDate: {events[i].Date}\n");
            }
            Console.WriteLine("\n\nPress enter to return to the previous menu");
            Console.ReadLine();
            Console.Clear();

        }
        private void ShowTickets()
        {

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
