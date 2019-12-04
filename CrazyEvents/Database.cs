using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CrazyEvents
{
    class Database
    {
        // [IMPORTANT] Connection string: used to connect to the DB, make sure you replace "SunTrips" with your database name!!
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=CrazyEvents;Integrated Security=True";




        /*======= Check Registered Records to dataBase ==========
         * 
         *      - Check SSN if exits
         *      - Check SSN if exits
         *      - Check UserName  if exits
         *      - Check Email  if exits
         * 
         *====================================================== */
        public bool CheckSsn(string ssnNumberToTheck)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE [SSN] LIKE @SSN"; // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)
            User user = null; // Create a new user without any value

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@SSN", ssnNumberToTheck); // Add username to the query 

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    if (dataReader.Read()) // Read response from db (first row)
                    {
                        user = new User(); // create new User object
                        user.SSN = dataReader["SSN"].ToString(); // Set user Password from db
                        Console.WriteLine("\nSSN EXISTS");
                        return true;
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }
            Console.WriteLine("\nSSN DOES NOT EXIST");
            return false; // Return the user
        }

        public bool checkOrgNumber(string orgNumberToTheck)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE [OrgNumber] LIKE @OrgNumber"; // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId) VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1)
            User user; // Create a new user without any value

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                sqlCommand.Parameters.AddWithValue("@OrgNumber", orgNumberToTheck); // Add user name to the query 

                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    if (dataReader.Read()) // Read response from db (first row)
                    {
                        user = new User(); // create new User object
                        user.OrgNumber = dataReader["OrgNumber"].ToString(); // Set user Password from db
                        Console.WriteLine("\norgNumber EXISTS");
                        return true;
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }
            Console.WriteLine("\nOrgNumber DOES NOT EXIST");
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





        /*============= Add changes to dataBase ===== 
         * 
         *      -  Add New User to database
         *      -  Add Ticket to database
         *
         * 
         *=========================================== */
        public void addUser(User user)
        {
            var sqlQuery = $"INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId, SSN) " +
                              $"VALUES ('{user.Name}', '{user.Username}', '{user.Password}', '{user.Email}'," +
                              $" '{user.OrgNumber}', '{user.RoleId}', '{user.SSN}')";

            // Query to run against the DB
            //INSERT INTO[User] (Name, Username, Password, Email, OrgNumber, RoleId)
            //VALUES ('Leanne Graham', 'Leanne', 'Leanne123', 'Sincere@april.biz', '555666-0001', 1) ==> Admin

            using (var myConnection = new SqlConnection(connectionString))  // Prepare connection to the db
            {
                var sqlCommand = new SqlCommand(sqlQuery, myConnection);    // Prepare the query for the db
                myConnection.Open();                                        // Open connection to the db
                using (var dataReader = sqlCommand.ExecuteReader())         // Run query on db
                {
                    Console.WriteLine("Added new user");
                    Console.WriteLine(user.Name);
                    myConnection.Close();                                   // Close connection to the db
                }
            }
        }

        public void addTicket(Ticket ticket)
        {
            string sqlQuery = $"INSERT INTO[Ticket] (Amount, UserId, EventId) " +
                              $"VALUES ('{ticket.Amount}', '{ticket.UserId}', '{ticket.EventId}')";
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

        public void SetEvent(Event events)
        {
            string sqlQuery =   $"INSERT INTO dbo.Event (Name, Description, Date)  " +
                                $"VALUES (@Name, @Description, @Date)";


            // Query to run against the DB

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                //Pass values to Parameters
                sqlCommand.Parameters.AddWithValue("@Name", $"{events.Name}");
                sqlCommand.Parameters.AddWithValue("@Description", $"{events.Description}");
                sqlCommand.Parameters.AddWithValue("@Date", Convert.ToDateTime($"{events.Date}"));
           


                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    myConnection.Close(); // Close connection to the db
                }
            }
        }

        public void SetVenue(Venue venues)
        {
            var sqlQuery = $"INSERT INTO Venue(Name, Location, VenueSize)  " +
                           $"VALUES (@Name, @Location,  @VenueSize)";


            using (var myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
            {
                var sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db

                //Pass values to Parameters
                sqlCommand.Parameters.AddWithValue("@Name", $"{venues.Name}");
                sqlCommand.Parameters.AddWithValue("@Location", $"{venues.Location}");
                sqlCommand.Parameters.AddWithValue("@VenueSize", $"{venues.VenueSize}");


                myConnection.Open(); // Open connection to the db

                using (var dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    myConnection.Close(); // Close connection to the db
                }
            }
        }




        /*============= Get records from dataBase ===== 
        * 
        *      -  GetUserByUsername
        *      -  GetRoleByRoleUsername
        *      -  GetPerformerEvents
        *      -  GetAllVenues
        *      -  GetAllTickets
        *      -  GetTicketsByuserId
        *          
        *=========================================== */

        public List<User> GetAllUsers()
        {
            var sqlQuery = "SELECT * FROM [User] LEFT JOIN[Role] ON [User].RoleId = [Role].Id";
            var users = new List<User>();

            using (var myConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand(sqlQuery, myConnection);
                myConnection.Open();

                using (var dataReader = sqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var user = new User
                        {
                            Id = int.Parse(dataReader["Id"].ToString()),
                            Name = dataReader["Name"].ToString(),
                            Username = dataReader["Username"].ToString(),
                            Password = dataReader["Password"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            OrgNumber = dataReader["OrgNumber"].ToString(),
                            RoleId = int.Parse(dataReader["RoleId"].ToString()),
                            SSN = dataReader["SSN"].ToString()
                        };

                        users.Add(user);
                    }
                    myConnection.Close();
                }

            }

            return users;
        }

        public List<Role> GetAllRoles()
        {
            var sqlQuery = "SELECT * FROM [Role]";
            var roles = new List<Role>();

            using (var myConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand(sqlQuery, myConnection);
                myConnection.Open();

                using (var dataReader = sqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var role = new Role
                        {
                            Id = int.Parse(dataReader["Id"].ToString()),
                            Name = dataReader["Name"].ToString(),
                        };

                       roles.Add(role);
                    }
                    myConnection.Close();
                }

            }
            return roles;

        }
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
                        user.Name = dataReader["Name"].ToString(); // Set user Username from db
                        user.Username = dataReader["Username"].ToString(); // Set user Username from db
                        user.Password = dataReader["Password"].ToString(); // Set user Password from db
                        user.role.Id = int.Parse(dataReader["RoleId"].ToString()); //Set user role from db
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }

            return user; // Return the user
        }

        public Role GetRoleByRoleUsername(string username)
        {

            string sqlQuery = " SELECT* FROM[User] LEFT JOIN[Role] " +
                              "ON [User].RoleId = [Role].Id " +
                              "WHERE [User].Username LIKE @username ";

            Role role = null;
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection);
                sqlCommand.Parameters.AddWithValue("@username", username);
                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    if (dataReader.Read()) // Read response from db (first row)
                    {
                        role = new Role(); // create new User object

                        role.Id = int.Parse(dataReader["Id"].ToString()); // Set user Id from db
                        role.Name = dataReader["Name"].ToString(); // Set user Username from db

                    }

                    myConnection.Close(); // Close connection to the db
                }
            }

            return role; // Return the user
        }

        public List<Performer> GetPerformerEvents()
        {

            string sqlQuery = " SELECT * FROM [Performer]";

            List<Performer> performers = new List<Performer>();


            using (var myConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection);
                myConnection.Open();

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Performer performer = new Performer();

                        performer.Id = int.Parse(dataReader["Id"].ToString());
                        performer.Name = dataReader["Name"].ToString();
                        performer.Time = dataReader["Time"].ToString();
                        performer.VenueId = int.Parse(dataReader["VenueId"].ToString());

                        performers.Add(performer);

                    }

                    myConnection.Close();
                }

            }

            return performers;

        }

        public List<Venue> GetAllVenues()
        {

            string sqlQuery = " SELECT * FROM [Venue]";
            List<Venue> venues = new List<Venue>();
            using (var myConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection);
                myConnection.Open();

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var venue = new Venue();

                        venue.Id = int.Parse(dataReader["Id"].ToString());
                        venue.Name = dataReader["Name"].ToString();
                        venue.Location = dataReader["Location"].ToString();
                        venue.VenueSize = int.Parse(dataReader["VenueSize"].ToString());
                        venues.Add(venue);

                    }

                    myConnection.Close();
                }

            }

            return venues;

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

                        @event.Id = int.Parse(dataReader["Id"].ToString()); // Set user Id from db
                        @event.Name = dataReader["Name"].ToString(); // Set user Username from db
                        @event.Description = dataReader["Description"].ToString(); // Set user Password from db
                        @event.Date = dataReader["Date"].ToString();
                        @event.VenueId = int.Parse(dataReader["VenueId"].ToString());

                        events.Add(@event); // Add last user to list of users
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }

            return events; // Return all users
        }

        public List<Ticket> GetAllTickets()
        {
            string sqlQuery = " SELECT* FROM [Ticket] ";

            List<Ticket> tickets = new List<Ticket>();
            List<Event> events = new List<Event>();
            using (var myConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection);
                myConnection.Open();
                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Ticket ticket = new Ticket();

                        ticket.Id = int.Parse(dataReader["Id"].ToString());
                        ticket.Amount = int.Parse(dataReader["Amount"].ToString());
                        ticket.UserId = int.Parse(dataReader["UserId"].ToString());
                        ticket.EventId = int.Parse(dataReader["EventId"].ToString());

                        tickets.Add(ticket); // Add last user to list of users
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }

            return tickets; // Return all users
        }

        public List<Ticket> GetTicketsByUsername(string username)
        {

            string sqlQuery = "SELECT * FROM[Ticket] " +
                              "LEFT JOIN[Event] " +
                              "ON[Ticket].EventId = [Event].Id " +
                              "LEFT JOIN[User] " +
                              "ON[Ticket].EventId = [User].Id " +
                              "WHERE Username Like @username";

            List<Ticket> tickets = new List<Ticket>();

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection);
                sqlCommand.Parameters.AddWithValue("@username", username);
                myConnection.Open(); // Open connection to the db

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
                {
                    if (dataReader.Read()) // Read response from db (first row)
                    {
                        Ticket ticket = new Ticket(); // create new User object

                        ticket.Id = int.Parse(dataReader["Id"].ToString());
                        ticket.Amount = int.Parse(dataReader["Amount"].ToString());
                        ticket.UserId = int.Parse(dataReader["UserId"].ToString());
                        ticket.EventId = int.Parse(dataReader["EventId"].ToString());

                        tickets.Add(ticket);
                    }

                    myConnection.Close(); // Close connection to the db
                }
            }

            return tickets; // Return the user
        }


    }





    //public List<Ticket> GetTicketsByUserID(string userId)
    //{
    //    string sqlQuery = "SELECT * FROM [Ticket] WHERE [UserId]=@userid"; // Query to run against the DB
    //    List<Ticket> tickets = new List<Ticket>(); // Start with an empty list of users
    //    using (var myConnection = new SqlConnection(connectionString)) // Prepare connection to the db
    //    {
    //        SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Prepare the query for the db
    //        sqlCommand.Parameters.AddWithValue("@userid", userid);
    //        myConnection.Open(); // Open connection to the db
    //        using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Run query on db
    //        {
    //            while (dataReader.Read()) // Read response from db (all rows)
    //            {
    //                Ticket ticket = new Ticket(); // create new ticket object
    //                ticket.Id = int.Parse(dataReader["Id"].ToString()); // Set ticket Id from db
    //                //ticket.Price = int.Parse(dataReader["Price"].ToString()); // Set ticket price from db, commented it out, this is throwing expection, not solved yet
    //                ticket.UserId = int.Parse(dataReader["UserId"].ToString()); // Set user id  from db
    //                ticket.EventId = int.Parse(dataReader["EventId"].ToString());// Set event ID from db
    //                tickets.Add(ticket); // Add last ticket to list of tickets
    //            }
    //            myConnection.Close(); // Close connection to the db
    //        }
    //    }
    //    return tickets; // Return all tickets 
    //}

}

