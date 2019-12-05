using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CrazyEvents
{
    class Database
    {
        // [IMPORTANT] Connection string: used to connect to the DB, make sure you replace "SunTrips" with your database name!!
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=CrazyEvents;Integrated Security=True";

        /// <summary>
        /// Gets a list of all users from the database
        /// </summary>
        /// <returns>A list of Users</returns>
        public List<User> GetAllUsers()
        {
            string sqlQuery = "SELECT * FROM [User]"; // Query to run against the DB

            List<User> users = new List<User>(); // Start with an ampty list of users

            using (var myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    while (dataReader.Read()) // Read response from db (all rows)
                    {
                        User user = new User(); // create new User object

                        user.Id = int.Parse(dataReader["Id"].ToString()); // Set user Id from db
                        user.Username = dataReader["Username"].ToString(); // Set user Username from db
                        user.Password = dataReader["Password"].ToString(); // Set user Password from db
                        user.role.Id = int.Parse(dataReader["RoleId"].ToString());
                        users.Add(user); // Add last user to list of users
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }

            return users; // Return all users
        }

        /// <summary>
        /// Returns the first user in the database with a matching Username
        /// </summary>
        /// <param name="username">Username to match in the db</param>
        /// <returns>A User</returns>
        public User GetUserByUsername(string username)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE [Username] LIKE @username"; // Query to run against the DB

            User user = null; // Create a new user without any value

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@username", username); // Add username to the query 

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    if (dataReader.Read()) // Read response from db (first row)
                    {
                        user = new User(); // create new User object

                        user.Id = int.Parse(dataReader["Id"].ToString()); // Set user Id from db
                        user.Username = dataReader["Username"].ToString(); // Set user Username from db
                        user.Password = dataReader["Password"].ToString(); // Set user Password from db
                        user.role.Id = int.Parse(dataReader["RoleId"].ToString()); //Set user role from db
                        user.Email = dataReader["Email"].ToString();
                        user.OrgNumber = dataReader["OrgNumber"].ToString();
                        user.Name = dataReader["Name"].ToString();
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }

            return user; // Return the user
        }
        public bool checkOrgNumber(string orgNumberToTheck)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE [OrgNumber] LIKE @orgnumber"; // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)
            User user = null; // Create a new user without any value

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@orgnumber", orgNumberToTheck); // Add username to the query 

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    if (dataReader.Read()) // Read response from db (first row)
                    {
                        user = new User(); // create new User object
                        user.OrgNumber = dataReader["OrgNumber"].ToString(); // Set user Password from db
                        //Console.WriteLine("IT EXISTS");
                        return true;
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }
            //Console.WriteLine("IT DOES NOT EXIST");
            return false; // Return the user
        }
        public bool checkEmail(string emailToCheck)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE [Email] LIKE @email"; // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)
            User user = null; // Create a new user without any value

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@email", emailToCheck); // Add username to the query 

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    if (dataReader.Read()) // Read response from db (first row)
                    {
                        user = new User(); // create new User object
                        user.Email = dataReader["Email"].ToString(); // Set user Password from db
                        //Console.WriteLine("IT EXISTS");
                        return true;
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }
            //Console.WriteLine("IT DOES NOT EXIST");
            return false; // Return the user
        }
        public bool checkUsername(string usernameToCheck)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE [Username] LIKE @username"; // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)
            User user = null; // Create a new user without any value

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@username", usernameToCheck); // Add username to the query 

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    if (dataReader.Read()) // Read response from db (first row)
                    {
                        user = new User(); // create new User object
                        user.Username = dataReader["Username"].ToString(); // Set user Password from db
                        //Console.WriteLine("IT EXISTS");
                        return true;
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }
            //Console.WriteLine("IT DOES NOT EXIST");
            return false; // Return the user
        }
        public void addUser(User user)
        {
            string sqlQuery = $"INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) " +
                $"VALUES ('{user.Name}', '{user.Username}', '{user.Password}', '{user.Email}', '{user.OrgNumber}', {user.role.Id})";
            // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db


                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    Console.WriteLine("Added new user");
                    Console.WriteLine(user.Name);
                    myConnection.Close(); // Close connection to the db
                }
            }
        }

        public void addTicket(Ticket ticket)
        {
            string sqlQuery = $"INSERT INTO[Ticket] (Price, UserId, EventId) " +
                $"VALUES ('{ticket.Price}', '{ticket.UserID}', '{ticket.EventID}')";
            // Query to run against the DB

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db


                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    myConnection.Close(); // Close connection to the db
                }
            }
        }

     
        public List<Ticket> GetTicketsByUserID(string userid)
        {
            string sqlQuery = "SELECT * FROM [Ticket] WHERE [UserId]=@userid"; // Query to run against the DB

            List<Ticket> tickets = new List<Ticket>(); // Start with an ampty list of users

            using (var myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@userid", userid);

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    while (dataReader.Read()) // Read response from db (all rows)
                    {
                        Ticket ticket = new Ticket(); // create new ticket object

                        ticket.Id = int.Parse(dataReader["Id"].ToString()); // Set ticket Id from db
                        //ticket.Price = int.Parse(dataReader["Price"].ToString()); // Set ticket price from db, commented it out, this is throwing expection, not solved yet
                        ticket.UserID = int.Parse(dataReader["UserId"].ToString()); // Set user id  from db
                        ticket.EventID = int.Parse(dataReader["EventId"].ToString());// Set event ID from db

                        tickets.Add(ticket); // Add last ticket to list of tickets
                    }

                    myConnection.Close(); // Close connection to the db
                }


            }

            return tickets; // Return all tickets 
        }

        
        public List<Ticket> GetTicketsByEventID(int eventID)
        {
            string sqlQuery = "SELECT * FROM [Ticket] WHERE [EventID]=@eventid"; // Query to run against the DB

            List<Ticket> tickets = new List<Ticket>(); // Start with an ampty list of users

            using (var myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@eventid", eventID);

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    while (dataReader.Read()) // Read response from db (all rows)
                    {
                        Ticket ticket = new Ticket(); // create new ticket object

                        ticket.Id = int.Parse(dataReader["Id"].ToString()); // Set ticket Id from db
                        //ticket.Price = int.Parse(dataReader["Price"].ToString()); // Set ticket price from db, commented it out, this is throwing expection, not solved yet
                        ticket.UserID = int.Parse(dataReader["UserId"].ToString()); // Set user id  from db
                        ticket.EventID = int.Parse(dataReader["EventId"].ToString());// Set event ID from db

                        tickets.Add(ticket); // Add last ticket to list of tickets
                    }

                    myConnection.Close(); // Close connection to the db
                }


            }

            return tickets; // Return all tickets 
        }
        public List<Event> GetAllEvents()
        {
            string sqlQuery = "SELECT * FROM [Event]"; // Query to run against the DB

            List<Event> events = new List<Event>();

            using (var myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    while (dataReader.Read()) // Read response from db (all rows)
                    {
                        Event @event = new Event(); // create new User object

                        @event.ID = int.Parse(dataReader["Id"].ToString()); // Set user Id from db
                        @event.Name = dataReader["Name"].ToString(); // Set user Username from db
                        @event.Description = dataReader["Description"].ToString(); // Set user Password from db
                        @event.Date = dataReader["Date"].ToString();
                        @event.VenueID = int.Parse(dataReader["VenueId"].ToString());
                        @event.EventPrice = int.Parse(dataReader["Price"].ToString()); //added an extra property , also in DB  

                        events.Add(@event); // Add last user to list of users
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }

            return events; // Return all events
        }


        public List<Venue> GetAllVenues()
        {
            string sqlQuery = "SELECT * FROM [Venue]"; // Query to run against the DB

            List<Venue> venues = new List<Venue>(); // Start with an ampty list of users

            using (var myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    while (dataReader.Read()) // Read response from db (all rows)
                    {
                        Venue venue = new Venue(); // create new User object

                        venue.Id = int.Parse(dataReader["Id"].ToString()); // Set user Id from db
                        venue.name = dataReader["Name"].ToString(); // Set user Username from db
                        venue.Location = dataReader["Location"].ToString(); // Set user Password from db
                        venue.MaxCapacity = int.Parse(dataReader["VenueSize"].ToString());
                        venues.Add(venue); // Add last user to list of users
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }

            return venues; // Return all users
        }

        public void AddEvent(Event eventToStore)
        {
            string sqlQuery = $"INSERT INTO[Event] (Name, Description, Date, VenueId, Price) " +
                $"VALUES ('{eventToStore.Name}', '{eventToStore.Description}', '{eventToStore.Date}', '{eventToStore.VenueID}','{eventToStore.EventPrice}')";
            // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db


                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    Console.WriteLine("Added new event to the database...");
                    myConnection.Close(); // Close connection to the db
                }
            }
        }
        public void DeleteEvent(string eventToRemove)
        {
            string sqlQuery = $"DELETE FROM [Event] WHERE Name LIKE @eventname";
            // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@eventname", eventToRemove);
                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    Console.WriteLine("Event succesfully removed...");
                    myConnection.Close(); // Close connection to the db
                }
            }
        }

        public void DeleteUser(string userToRemove)
        {
            string sqlQuery = $"DELETE FROM [User] WHERE Username LIKE @username";
            // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@username", userToRemove);
                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    Console.WriteLine("User succesfully removed...");
                    myConnection.Close(); // Close connection to the db
                }
            }
        }

        public void RemoveTickets(int eventID)
        {
            string sqlQuery = $"DELETE FROM [Ticket] WHERE EventId=@eventid";
            // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@eventid", eventID);
                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    myConnection.Close(); // Close connection to the db
                }
            }
        }

        public void RemoveMessages(int eventID)
        {
            string sqlQuery = $"DELETE FROM [Message] WHERE EventId=@eventID";
            // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@eventID", eventID);
                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    myConnection.Close(); // Close connection to the db
                }
            }
        }

    }
}
