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

    }
}
