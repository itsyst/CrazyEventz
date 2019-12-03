using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyEvents
{
    public class User
    {
        public readonly Role role = new Role();

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string OrgNumber { get; set; }
        public int RoleId { get; set; }
        public string SSN { get; set; }


        public void setPassword(string newPassword)
        {
            Password = newPassword;
        }
        public void setOrgNumber(string newOrgNumber)
        {
            OrgNumber = newOrgNumber;
        }
        public void setEmail(string newEmail)
        {
            Email = newEmail;

        }
        public void setSSN(string newSSN)
        {
            SSN = newSSN;
        }
        public bool checkPassword(string inputPassword)
        {
            if (inputPassword == Password)
            {
                return true;
            }

            return false;
        }


    }
}