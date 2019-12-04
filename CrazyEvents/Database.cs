using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CrazyEvents
{
    class Database
    {
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
                        Console.WriteLine("IT EXISTS");
                        return true;
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }
            Console.WriteLine("IT DOES NOT EXIST");
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
                        Console.WriteLine("IT EXISTS");
                        return true;
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }
            Console.WriteLine("IT DOES NOT EXIST");
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
                        Console.WriteLine("IT EXISTS");
                        return true;
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }
            Console.WriteLine("IT DOES NOT EXIST");
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

            return events; // Return all users
        }


        


        //////////////////////
        //Chat related////////
        //////////////////////
        //Add a message to the database
        public void AddMessage(DateTime time, string username, string message)
        {
            //Put all the info together in one string
            string messageToSend = time + ": " + username + ": " + message;
            //Create the query that will be executed in the database
            string sqlQuery = "INSERT INTO[Message] ([Message]) VALUES(@messageToSend)"; //query to save message to db

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@messageToSend", messageToSend); //Add message to the query

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    myConnection.Close(); // Close connection to the db
                }
            }
        }

        //Retrieve messages from the database to be displayed in chat
        public List<string> GetAllMessages()
        {
            string sqlQuery = "SELECT * FROM [Message]";
            //Create a list that will hold all messages on the database
            List<string> messages = new List<string>();

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection);
                myConnection.Open();        //Open connection to db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())       //As long as database is giving us info...
                    {
                        messages.Add(dataReader["Message"].ToString());     //...add to message list
                    }

                    myConnection.Close();       //Close connection to db
                }

            }

            return messages;
        }

    }
}
