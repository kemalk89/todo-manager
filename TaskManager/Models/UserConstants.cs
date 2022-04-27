using System;
using System.Collections.Generic;
using TaskManager.Auth;

namespace TaskManager.Models
{
    public class UserConstants
    {
        public static List<UserModel> GetUsers()
        {
            var u1 = new UserModel
            {
                Username = "max.mustermann@mail.de",
                Password = "passowrd",
                Role = UserRoles.ROLE_USER
            };

            var u2 = new UserModel
            {
                Username = "thomas.müller@mail.de",
                Password = "password",
                Role = UserRoles.ROLE_ADMIN
            };

            return new List<UserModel>() { u1, u2 };
        }
    }

}
