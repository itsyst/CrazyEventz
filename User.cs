using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyEvents
{
    class User
    {

        public int Id;
        public string Name;
        public string Username;
        public string Password;
        public string Email;
        public string OrgNumber;

        public Role role = new Role(); 
        

        public void setPassword(string newPassword)
        {
            Password = newPassword;
        }
        public void setSSN(string newOrgNumber)
        {
            OrgNumber = newOrgNumber;
        }
        public void setEmail(string newEmail)
        {
            Email = newEmail;

        }
        public bool checkPassword(string inputPassword)
        {
            if(inputPassword == Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
