using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrazyEvents
{
    class EventAgency
    {
        public User loggedInUser = null;
        public Role logRoleUser = null;
        public Database dataBase = new Database();
        public bool running = true;


        //=========================================
        //  Start Menu
        //=========================================

        /// <summary>
        /// Principal menu
        /// </summary>
        /// <returns></returns>
        public int StartMenu()
        {
            Banner();
            Console.WriteLine("\nSelect an option");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("0. Exit");
            Console.Write("Selection > ");
            var choice = int.Parse(Console.ReadLine());

            return choice;
        }

        /// <summary>
        /// Fires on when register new user
        /// </summary>
        public void Start()
        {
            while (running == true)
            {
                int choice = StartMenu();
                switch (choice)
                {
                    case 1:
                        Banner();
                        Login();
                        break;
                    case 2:
                        Banner();
                        RegisterMenu();
                        break;
                    case 0:
                        Console.WriteLine("\nLogged out");
                        running = false;
                        break;

                }
            }

        }


        //=========================================
        //  Login menus
        //=========================================

        /// <summary>
        /// Login  menu
        /// </summary>
        private void Login()
        {

            Console.Write("\nUsername: ");
            string inputUsername = Console.ReadLine();
            Console.Write("Password: ");
            string inputPassword = Console.ReadLine();

            User temp = dataBase.GetUserByUsername(inputUsername);
            Role logRole = dataBase.GetRoleByRoleUsername(inputUsername);
            if (temp.checkPassword(inputPassword) == true)
            {
                Console.WriteLine("\nPassword Correct");
                loggedInUser = temp;
                logRoleUser = logRole;
                if (loggedInUser.role.Id == 1)
                {
                    logRoleUser.Name = "Admin";
                    ShowAdminMenu();
                }
                else if (loggedInUser.role.Id == 2)
                {
                    logRoleUser.Name = "Organizer";
                    ShowAdminMenu();
                }
                else if (loggedInUser.role.Id == 3)
                {
                    logRoleUser.Name = "Performer";
                    ShowPerformerMenu();

                }
                else if (loggedInUser.role.Id == 4)
                {
                    logRoleUser.Name = "User";
                    ShowUserMenu();
                }

                else
                {
                    logRoleUser.Name = "Visitor";
                    ShowVisitorMenu();
                }
            }
            else
            {
                ShowLoginError();
            }

        }

        /// <summary>
        /// Represents admins menu
        /// </summary>
        public void ShowAdminMenu()
        {
            while (loggedInUser != null)
            {

                Banner();
                Console.WriteLine($"Logged in as: {loggedInUser.Name}");
                Console.WriteLine($"\n----{logRoleUser.Name}-Menu----");
                Console.WriteLine("1.Create an event");
                Console.WriteLine("2.Show all events");
                Console.WriteLine("3.Delete an event");
                Console.WriteLine("4.Create ticket");
                Console.WriteLine("5.Delete a ticket");
                Console.WriteLine("6.Show tickets");
                Console.WriteLine("7.Show All users");
                Console.WriteLine("8.Delete user account");
                Console.WriteLine("0.Log out");
                Console.Write("Selection > ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("\nCreating an event...");
                        break;
                    case "2":
                        Console.WriteLine("\nShowing events..");
                        ShowEvents();
                        ShowAdminMenu();
                        break;
                    case "3":
                        Console.WriteLine("\nDeleting the event..");
                        break;
                    case "4":
                        Console.WriteLine("\nCreating a ticket..");

                        break;
                    case "5":
                        Console.WriteLine("\nDeleting the ticket..");
                        break;
                    case "6":
                        Console.WriteLine("\nShowing tickets..");
                        ShowTickets();
                        ShowAdminMenu();
                        break;
                    case "7":
                        Console.WriteLine("\nShowing All users..");
                        HandleUsers();
                        break;
                    case "8":
                        Console.WriteLine("\nDeleting the user account..");
                        HandleUsers();
                        break;
                    case "0":
                        loggedInUser = null;
                        break;
                }
            }
        }

        /// <summary>
        /// Represents performers menu
        /// </summary>
        public void ShowPerformerMenu()
        {
            Banner();
            Console.WriteLine($"Logged as : {loggedInUser.Name}");
            Console.WriteLine($"\n---- {logRoleUser.Name} Menu----");


            var performers = dataBase.GetPerformerEvents();
            var venues = dataBase.GetAllVenues();
            Console.WriteLine(" Venue name\t         " +
                              "  Location\t  " +
                              " Time\t " +
                              " VenueId\t");

            for (var i = 0; i < performers.Count; i++)
            {
                Console.WriteLine($" {venues[i].Name}\t   " +
                                  $"{venues[i].Location}\t " +
                                  $"{performers[i].Time}\t " +
                                  $"{performers[i].VenueId}\t");
            }

            Console.WriteLine("\n-----------------------------");
            Console.WriteLine("1. Show all events");
            Console.WriteLine("0. Exit");
            Console.Write("Selection > ");


            string input = Console.ReadLine();

            if (input == "1")
            {
                ShowEvents();
            }

            else if (input == "0")
            {
                loggedInUser = null;
            }
        }

        /// <summary>
        /// Represents Users menu
        /// </summary>
        private void ShowUserMenu()
        {
            while (loggedInUser != null)
            {
                Banner();
                Console.WriteLine($"Logged in as {loggedInUser.Name}");
                Console.WriteLine($"\n---- { logRoleUser.Name} Menu----");
                Console.WriteLine("1.Show current event");
                Console.WriteLine("2.Buy a ticket");
                Console.WriteLine("3.Show bought tickets");
                Console.WriteLine("0.Logout");
                Console.Write("Selection > ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowEvents();
                        ShowUserMenu();
                        break;
                    case "2":
                        Console.WriteLine("Buying ticket now..");
                        BuyTicket();
                        ShowUserMenu();
                        break;
                    case "3":
                        Console.WriteLine("Showing bought ticket..");
                        ShowTicketsByUsername();
                        ShowUserMenu();
                        break;
                    case "0":
                        loggedInUser = null;
                        break;
                }

            }
        }

        /// <summary>
        /// Represents Visors menu
        /// </summary>
        private void ShowVisitorMenu()
        {
            Banner();
            Console.WriteLine($"Logged in as : {loggedInUser.Username}");
            Console.WriteLine($"\n---- { logRoleUser.Name} Menu----");

            Console.WriteLine("1. Show all events");
            Console.WriteLine("2. Buy a Ticket");
            Console.WriteLine("0. Exit");
            Console.Write("Selection > ");


            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowEvents();
                    ShowVisitorMenu();
                    break;
                case "2":
                    BuyTicket();
                    ShowVisitorMenu();
                    break;
                case "0":
                    loggedInUser = null;
                    break;
            }

        }


        //=========================================
        // Register menus
        //=========================================

        /// <summary>
        /// Menu for registering a user
        /// </summary>
        private void RegisterMenu()
        {
            Console.WriteLine("\n1. Register as individual" +
                              "\n2. Register as Organization");
            Console.Write("\nSelection > ");

            var choice = int.Parse(Console.ReadLine());

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

        /// <summary>
        /// Represents a registering function of private person
        /// </summary>
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
            user.OrgNumber = "NULL";
            user.RoleId = 4;
            Console.Write("SSN: ");

            string inputSSN = Console.ReadLine();
            user.setSSN(inputSSN);

            if (dataBase.CheckSsn(inputSSN) == true)
            {
                Console.WriteLine("User account is already existing");
                RegisterMenu();
            }
            else
            {
                Console.WriteLine("You are welcome as a new user");
                dataBase.addUser(user);
            }

        }

        /// <summary>
        /// Represents a registering function of organization
        /// </summary>
        private void RegisterOrg()
        {
            var org = new User();

            Console.Write("Organization Name: ");
            org.Name = Console.ReadLine();
            Console.Write("Username: ");
            org.Username = Console.ReadLine();
            Console.Write("Password: ");
            org.setPassword(Console.ReadLine());
            Console.Write("Email: ");
            org.Email = Console.ReadLine();
            org.SSN = "NULL";
            org.RoleId = 2;
            Console.Write("Org number: ");

            var orgNumber = Console.ReadLine();
            org.setOrgNumber(orgNumber);

            if (dataBase.checkOrgNumber(orgNumber) == true)
            {
                Console.WriteLine("Org number already registered");
                RegisterMenu();
            }
            else
            {
                if (dataBase.checkEmail(org.Email) == true)
                {
                    Console.WriteLine("Email already registered");
                    RegisterOrg();
                }
                else
                {
                    if (dataBase.checkUsername(org.Username) == true)
                    {
                        Console.WriteLine("Username already registered");
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


        //=========================================
        // Ticket And Event Menu
        //=========================================

        /// <summary>
        /// Represents a function to show all events
        /// </summary>
        private void ShowEvents()
        {

            Console.WriteLine("\n---Here are all the upcoming Events---");

            var events = dataBase.GetAllEvents();
            var tickets = dataBase.GetAllTickets();

            for (var i = 0; i < events.Count; i++)
            {
                Console.WriteLine("Date\t             | Event name\t  | Price\t");
                Console.WriteLine($"{events[i].Date}\t" +
                                  $"{events[i].Name}       \t" +
                                  $"    {tickets[i].Amount}       \n\t" +
                                  $"Description: { events[i].Description}\t");
                var _builder = new StringBuilder();
                Console.WriteLine(_builder.Append('-', 90));
            }

            Console.Write("Press enter to return to menu: ");
            Console.ReadLine();


        }

        /// <summary>
        /// Returns a list of all tickets
        /// </summary>
        private void ShowTickets()
        {

            var tickets = dataBase.GetAllTickets();
            var events = dataBase.GetAllEvents();
            Console.WriteLine("Date\t   " + "            Price\t" + "  Event Name\t");

            for (var i = 0; i < tickets.Count; i++)
            {
                Console.WriteLine($"{events[i].Date}\t" +
                                  $"{tickets[i].Amount}\t " +
                                  $"{events[i].Name}\t");
            }

            Console.WriteLine("\n-----------------------------");
            Console.Write("Press enter to return admin menu: ");
            Console.ReadLine();
        }

        /// <summary>
        /// Represents a function to buy a ticket
        /// </summary>
        private void BuyTicket()
        {
            Console.Write("\nDo you want to buy a ticket to one of these events? y/n: ");
            while (true)
            {
                string answer = Console.ReadLine();
                if (answer.ToLower() == "y")
                {
                    Ticket ticket = new Ticket();
                    var prices = new int[] { 600, 250, 500, 600, 250, 700, 1000, 300, 150, 200 };

                    Console.Write("Enter the number of the event: >");
                    string input = Console.ReadLine();
                    int eventNumber = int.Parse(input);

                    ticket.EventId = eventNumber;
                    ticket.Amount = prices[eventNumber];

                    ticket.UserId = loggedInUser.Id;

                    // ticket.Amount = 0;
                    // // måste vi senare hitta en lösning för - for now gave it a default, get rid of this property in database and class for example


                    dataBase.addTicket(ticket);

                }
                else
                {
                    break;
                }

                Console.WriteLine("\nYour purchase is confirmed");
                Console.Write("Do you want to purchase another one? y/n: ");
            }
            Console.Write("Press enter to return to menu ");
            Console.ReadLine();

        }

        /// <summary>
        /// Returns a list of tickets by username
        /// </summary>
        private void ShowTicketsByUsername()
        {
            Console.Write("\nType your Username to see your tickets: ");
            var username = Console.ReadLine();
            var ticketByUsername = dataBase.GetTicketsByUsername(username);
            var tickets = dataBase.GetAllTickets();
            var events = dataBase.GetAllEvents();
            Console.WriteLine("Date\t Price\t Event Name\t Date\t");

            for (var i = 0; i < ticketByUsername.Count; i++)
            {
                Console.WriteLine($"{ticketByUsername[i].Id}\t" +
                                  $"{ticketByUsername[i].Amount}\t  {events[i].Name}\t {events[i].Name}\t");

            }

            Console.WriteLine("\n-----------------------------");
            Console.Write("Press enter to return to menu: ");
            Console.ReadLine();

        }



        //=========================================
        // Manage Events and users
        //=========================================

        /// <summary>
        /// Represents a managing of events
        /// </summary>
        private void HandleEvents()
        {
            Console.WriteLine("---Handle-Events");
        }

        /// <summary>
        /// Represents a managing of user
        /// </summary>
        private void HandleUsers()
        {
            Console.WriteLine("---Handle-Users---");

        }

        /// <summary>
        /// Throws an error message when login is incorrect
        /// </summary>
        private void ShowLoginError()
        {
            Console.WriteLine("Wrong Password");

        }

        /// <summary>
        /// Menu header
        /// </summary>
        private void Banner()
        {
            Console.Clear();  //Start banner.
            var builder = new StringBuilder();
            builder.Append('*', 50);

            Console.WriteLine(builder);
            Console.WriteLine("*             Welcome to CrazyEvents             *");
            Console.WriteLine(builder);
        }
    }
}
