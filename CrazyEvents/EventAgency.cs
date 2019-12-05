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

            Console.Write("Company name: ");
            org.Name = Console.ReadLine();
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
                Console.WriteLine("Org number already registerd");
                RegisterOrg();
            }
            else
            {
                if (dataBase.checkEmail(org.Email) == true)
                {
                    Console.WriteLine("Email already registerd");
                    RegisterOrg();
                }
                else
                {
                    if (dataBase.checkUsername(org.Username) == true)
                    {
                        Console.WriteLine("Username already registerd");
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
            Console.Write("SSN number: ");
            string ssn = Console.ReadLine();
            user.setSSN(ssn);

            // We still need to built a control here...to check if username and so on already exists
            user.role.Id = 4;
            dataBase.addUser(user);
            loggedInUser = dataBase.GetUserByUsername(user.Username);

            ShowVisitorMenu();
        }
        private void Login()
        {
            Console.WriteLine("-----Login-----");
            Console.Write("Username: ");
            string inputUsername = Console.ReadLine();
            Console.Write("Password: ");
            string inputPassword = Console.ReadLine();

            User temp = dataBase.GetUserByUsername(inputUsername);
            if (temp != null)
            {
                if (temp.checkPassword(inputPassword) == true)
                {
                    Console.WriteLine("Password Correct");
                    loggedInUser = temp;
                    if (loggedInUser.role.Id == 1)
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
            else
            {
                Console.WriteLine("Wrong Username!");
            }

        }
        private void ShowAdminMenu()
        {
            while (loggedInUser != null)
            {
                Console.Clear();
                loggedInUser = dataBase.GetUserByUsername(loggedInUser.Username);
                if (loggedInUser == null)
                    break;

                Console.WriteLine($"Logged in as: {loggedInUser.Username}");
                Console.WriteLine("----Admin-Menu----");
                Console.WriteLine("1. Show all events");
                Console.WriteLine("2. Handle events");
                Console.WriteLine("3. Handle users");
                Console.WriteLine("4. Show Profle");
                Console.WriteLine("5. Log out");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        ShowEvents();
                        break;
                    case "2":
                        Console.Clear();
                        HandleEvents();
                        break;
                    case "3":
                        Console.Clear();
                        HandleUsers();
                        break;
                    case "4":
                        Console.Clear();
                        ShowProfile();
                        break;
                    case "5":
                        Console.Clear();
                        loggedInUser = null;
                        break;
                }
            }
        }
        private void ShowVisitorMenu()
        {
            while (loggedInUser != null)
            {
                Console.Clear();
                Console.WriteLine($"Logged in as: {loggedInUser.Username}");
                Console.WriteLine("----Visitor-Menu----");
                Console.WriteLine("1. Show all events (and buy tickets...)");
                Console.WriteLine("2. Show your purchased tickets");
                Console.WriteLine("3. Show your Profile");
                Console.WriteLine("4. Logout");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        ShowEvents();
                        break;
                    case "2":
                        Console.Clear();
                        ShowTickets();
                        break;
                    case "3":
                        Console.Clear();
                        ShowProfile();
                        break;
                    case "4":
                        Console.Clear();
                        loggedInUser = null;
                        break;
                }

            }
        }
        private void ShowProfile()
        {
            User user = dataBase.GetUserByUsername(loggedInUser.Username);
            List<Ticket> userTickets = dataBase.GetTicketsByUserID(user.Id.ToString());
            Console.Clear();
            string nameState = "Name";
            string ssnState = "SSN";
            if (user.role.Id == 1)
            {
                nameState = "Company name";
                ssnState = "Organisation number";
            }

            
            Console.WriteLine("---Profile---");
            Console.WriteLine($"{nameState}: {user.Name} ");
            Console.WriteLine($"Username: {user.Username} ");
            Console.WriteLine($"Email: {user.Email} ");
            Console.WriteLine($"{ssnState}: {user.OrgNumber} ");
            if(user.role.Id != 1)
            {
                Console.WriteLine($"Tickets: {userTickets.Count} ");
            }
            
            Console.WriteLine("Press enter to go back");
            Console.ReadLine();

        }
        private void ShowEvents()
        {

            Console.Clear();
            Console.WriteLine("---Here are all the upcoming Events---");
            List<Event> events = dataBase.GetAllEvents();
            List<Venue> venues = dataBase.GetAllVenues();
            int ticketsLeft;

            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine($"\nEvent {i + 1}");
                Console.Write($"\nName: {events[i].Name}");
                Console.Write($"\nEvent ID: {events[i].ID}");
                Console.Write($"\nDescription: {events[i].Description}");
                Console.Write($"\nDate: {events[i].Date}");
                Console.Write($"\nPrice: {events[i].EventPrice}");
                for (int y = 0; y < venues.Count; y++)
                {
                    if (venues[y].Id == events[i].VenueID)
                    {
                        Console.Write($"\nLocation: {venues[y].Location}");
                        Console.Write($"\nMax capacity: {venues[y].MaxCapacity}");
                        List<Ticket> tickets = dataBase.GetTicketsByEventID(events[i].ID);
                        ticketsLeft = venues[y].MaxCapacity - tickets.Count;
                        Console.Write($"\nTickets left: {ticketsLeft}\n");
                    }
                }
               


            }
            if (loggedInUser.role.Id == 4)
            {
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
                        ticket.Price = 0; // We don't use it, since we have Price as property of Event now.. so we still need to do a clean up later, get rid of this property everywhere...
                        ticket.UserID = loggedInUser.Id;
                        ticket.EventID = events[eventNumber - 1].ID;

                        dataBase.addTicket(ticket);
                        Console.WriteLine("Purchase confirmed");
                    }
                    else
                    {
                        break;
                    }
                    Console.WriteLine("Do you want to purchase another one? y/n");
                }
            }
            Console.WriteLine("\n\nPress enter to return to the previous menu");
            Console.ReadLine();
            

        }
        private void ShowTickets()
        {
            Console.Clear();
            // Smoother option would be to get 1 event by userid 
            List<Event> allEvents = dataBase.GetAllEvents();
            Console.WriteLine("Here are the tickets you have booked: ");

            string userIDstring = loggedInUser.Id.ToString();
            List<Ticket> tickets = dataBase.GetTicketsByUserID(userIDstring);
            for (int i = 0; i < tickets.Count; i++)
            {
                Console.WriteLine($"Ticket {i + 1}: ");
                for (int y = 0; y < allEvents.Count; y++)
                {
                    if (allEvents[y].ID == tickets[i].EventID)
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
        private void HandleEvents()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("---Handle-Events---");
                Console.WriteLine("1. Create event");
                Console.WriteLine("2. Delete event");
                Console.WriteLine("3. Return to previous menu");
                Console.WriteLine("Make a selection... ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CreateEvent();
                        break;
                    case 2:
                        DeleteEvent();
                        break;
                    case 3:
                        Console.Clear();
                        isRunning = false;
                        break;

                }
            }
        }
        private void CreateEvent()
        {

            var eventss = new Event();
            bool colorRow = true; 
            eventss.ID = 0;
            Console.Clear();
            Console.WriteLine("---Create-Event---");
            Console.Write(" Type the name of the event you want to create: ");
            string inputEvName = Console.ReadLine();
            eventss.Name = inputEvName;
            Console.Write(" Add a description: ");
            var inputDescription = Console.ReadLine();
            eventss.Description = inputDescription;
            Console.Write(" Add the event date opening: yyyy-mm-dd: ");
            var inputOpening = Console.ReadLine();
            eventss.Date = inputOpening;
            Console.WriteLine(" Where does this event take place?");

            List<Venue> venues = dataBase.GetAllVenues();
            Console.WriteLine(" Here's a list of venues to choose from: \n");
            
            for (int i = 0; i < venues.Count; i++)
            {
                int nameLength = venues[i].name.Length;
                int rest = 20 - nameLength;
                for (int y = 0; y < rest; y++)
                {
                    venues[i].name += " ";
                }
                nameLength = venues[i].Location.Length;
                rest = 20 - nameLength;
                for (int y = 0; y < rest; y++)
                {
                    venues[i].Location += " ";
                }
                if (colorRow == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    colorRow = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    colorRow = false;
                }

                Console.Write($"| ID: {venues[i].Id} | ");
                Console.Write($"| Name: {venues[i].name} |");
                Console.Write($"| Location: {venues[i].Location} |");
                Console.Write($"| Capacity: {venues[i].MaxCapacity}|\n");
                
                Console.ResetColor();
            }
            
            Console.Write("\n Enter the ID of the venue you want to hold your event: ");
            string input = Console.ReadLine();
            eventss.VenueID = int.Parse(input);
            Console.Write(" What will a ticket for this event cost? ");
            input = Console.ReadLine();
            eventss.EventPrice = int.Parse(input);
            dataBase.AddEvent(eventss);
            Console.Clear();
            Console.WriteLine("\nThank you for adding your event...");


        }
        private void DeleteEvent()
        {
            bool colorRow = true;
            List<Event> venues = dataBase.GetAllEvents();
            Console.WriteLine(" Here's a list of Events to choose from: \n");
            Console.WriteLine("---------------------------------------------");
            for (int i = 0; i < venues.Count; i++)
            {
                int nameLength = venues[i].Name.Length;
                int rest = 25 - nameLength;
                for (int y = 0; y < rest; y++)
                {
                    venues[i].Name += " ";
                }

                string id = venues[i].ID.ToString();
                rest = 3 - id.Length;
               
                for (int y = 0; y < rest; y++)
                {
                    
                    id += " ";
                }

               

                if (colorRow == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    colorRow = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    colorRow = false;
                }

                Console.Write($"| ID: {id} | ");
                Console.Write($"| Name: {venues[i].Name} |\n");
               

                Console.ResetColor();
            }
            Console.WriteLine("---------------------------------------------");
            Console.Write("What is the ID of the event you want to delete: ");
            // This code could be better....more user friendly
            //Console.WriteLine("...If you forgot enter -1 and then choose previous menu - check Show All Events first.");


            List<Event> events = dataBase.GetAllEvents();
            Event deleteThis = new Event();
            string eventToDelete = Console.ReadLine();

            for (int i = 0; i < events.Count; i++)
            {
               
                
                if (events[i].ID == int.Parse(eventToDelete))
                {
                    deleteThis = events[i];
                }
            }
            if(eventToDelete == "-1")
            {
                return;
            }
            /*
            Console.WriteLine("What is the ID of the event you want to delete");
            Console.WriteLine("...If you forgot enter -1 and then choose previous menu - check Show All Events first.");
    
            int idEventToDelete = int.Parse(Console.ReadLine());
            if (idEventToDelete == -1)
            {
                return;
            }*/
            dataBase.RemoveMessages(deleteThis.ID);
            dataBase.RemoveTickets(deleteThis.ID);
            dataBase.DeleteEvent(deleteThis.Name);
        }
        private void HandleUsers()
        {


            while (true)
            {
                Console.WriteLine("---Handle-Users---");
                Console.WriteLine("1. Show all users");
                Console.WriteLine("2. Delete a user");
                Console.WriteLine("3. Return to previous menu");
                Console.WriteLine("Make a selection...");
                int selection = int.Parse(Console.ReadLine());
                if (selection == 1)
                {
                    
                    ShowAllUsers();
                }
                else if (selection == 2)
                {
                    
                    DeleteUser();
                }
                else if (selection == 3)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Not a valid selection, try again");
                }
            }

        }
        private void ShowAllUsers()
        {
            List<User> users = dataBase.GetAllUsers();

            for (int i = 0; i < users.Count; i++)
            {
                Console.Write($"Username: {users[i].Username}\n");
                Console.Write($"User ID: {users[i].Id}\n");
                if (users[i].role.Id == 1)
                {
                    Console.Write($"Role: Admin\n");
                }
                else
                {
                    Console.Write($"Role: Visitor\n");
                }
                Console.WriteLine("----------------");
            }
        }
        private void DeleteUser()
        {
            Console.Write("Enter the username of the user you want to remove:");
            string userToDelete = Console.ReadLine();
            dataBase.DeleteUser(userToDelete);

        }

    }
}


